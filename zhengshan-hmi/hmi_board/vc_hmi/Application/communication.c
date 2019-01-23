#include <unistd.h> 
#include <stdio.h>
#include <stdlib.h>
#include <fcntl.h>
#include <string.h> 
#include <errno.h>
#include <sys/types.h>
#include <sys/time.h>
#include <sys/select.h>
#include <sys/ipc.h>
#include <sys/shm.h>
#include <sys/msg.h>
#include <sys/prctl.h>
#include "GUI.h"
#include "common.h"
#include "hmiData.h"
#include "ipc_message.h"
#include "ipc_key.h"
#include "realdb_operation.h"
#include "communication.h"
#include "com_datastruct.h"

extern xRealTimeDBMap *pRealDB;
xGeneralSharedDataMap *pGenSharedData = NULL;

eValveStatus gGroupValveStatus[VALVE_MAX_GROUP][GROUP_VALVE_NUM];

static int gCommMsgQueue;


static int get_message_queue(char *path,int index)
{
    key_t key;
    int msg_q;
    if((key=ftok(path,index))== -1){
        return -1;
    }
    if((msg_q=msgget(key,IPC_CREAT|0666))== -1){
        return -1;
    }
    return msg_q;
}

/*********************************interface**********************************/
void InitCommunication(void)
{
    int index;
    key_t key;
    int shm_id;
    printf("InitCommunication\n");
    gCommMsgQueue = get_message_queue(IPC_KEY_NAME,COMM_MQ_KEY_INDEX);
    printf("hmi_app message queue created,id = %d\n",gCommMsgQueue);
    //create realtime database
    if((key=ftok(IPC_KEY_NAME,REAL_DATABASE_KEY_INDEX))== -1){
        printf("can't get realtime database\n");
        exit(1);
    }
    shm_id = shmget(key,sizeof(xRealTimeDBMap),IPC_CREAT|0666);
    if(shm_id<0)
    {
        printf("can't get realtime database\n");
        exit(1);
    }
    pRealDB = (xRealTimeDBMap *)shmat(shm_id,NULL,0);
    bzero(pRealDB,sizeof(xRealTimeDBMap));
    for(index = 0; index < GROUP_VALVE_NUM; index++)
    {
        gGroupValveStatus[VALVE_GROUP_1][index] = VALVE_NOT_EXIST;
        gGroupValveStatus[VALVE_GROUP_2][index] = VALVE_NOT_EXIST;
        gGroupValveStatus[VALVE_GROUP_3][index] = VALVE_NOT_EXIST;
        gGroupValveStatus[VALVE_GROUP_4][index] = VALVE_NOT_EXIST;
    }
    if((key=ftok(IPC_KEY_NAME,GENERAL_SHARED_MEMORY))== -1){
        printf("can't get general shared memory\n");
        exit(1);
    }
    shm_id = shmget(key,sizeof(xGeneralSharedDataMap),IPC_CREAT|0666);
    if(shm_id<0)
    {
        printf("can't get general shared memory\n");
        exit(1);
    }
    pGenSharedData = (xGeneralSharedDataMap *)shmat(shm_id,NULL,0);
    bzero(pGenSharedData,sizeof(xGeneralSharedDataMap));
    
}
/*
	获取指定环路的所有阀门状态
	参数:
		group_num:环路号
	返回值:
		-1:       通信失败
		其他:通信成功
*/

int GetGroupValveStatus(eValveGroup group_no)
{
    int index;
    xCommMessage xMsg;
    xPortStatus *pPortStatus;
    xMsg.iMsgType = REG_READ_MSG;
    switch(group_no)
    {
        case VALVE_GROUP_1:
            xMsg.uMsgContent.xRegRd.iOffset = SLAVE_DEV_STATUS_OFFSET(xRealTimeDBMap,g1a_status);
            pPortStatus = &pRealDB->g1a_status;
            break;
        case VALVE_GROUP_2:
            xMsg.uMsgContent.xRegRd.iOffset = SLAVE_DEV_STATUS_OFFSET(xRealTimeDBMap,g2a_status);
            pPortStatus = &pRealDB->g2a_status;
            break;
        case VALVE_GROUP_3:
            xMsg.uMsgContent.xRegRd.iOffset = SLAVE_DEV_STATUS_OFFSET(xRealTimeDBMap,g3a_status);
            pPortStatus = &pRealDB->g3a_status;
            break;
        case VALVE_GROUP_4:
            xMsg.uMsgContent.xRegRd.iOffset = SLAVE_DEV_STATUS_OFFSET(xRealTimeDBMap,g4a_status);
            pPortStatus = &pRealDB->g4a_status;
            break;
        default:
            return 0;
            break;
    }
    xMsg.uMsgContent.xRegRd.iNum = GROUP_VALVE_NUM;
    msgsnd(gCommMsgQueue,&xMsg,sizeof(xRegRead),0);
    
    for(index = 0; index < GROUP_VALVE_NUM; index++)
    {
        switch(pPortStatus->slave_dev_status[index]&MB_DEV_STATUS_MASK)
        {
            case S_DEV_INACTIVE:
                gGroupValveStatus[group_no][index] = VALVE_NOT_EXIST;
                break;
            case S_DEV_POLLING:
                switch(pPortStatus->slave_dev_status[index]&MB_DEV_PORT_MASK)
                {
                    case S_ON_NONE:
                        gGroupValveStatus[group_no][index] = VALVE_SUSPEND;
                        break;
                    case S_ON_PORT_A:
                        gGroupValveStatus[group_no][index] = VALVE_GOOD_ON_PORT_A;
                        break;
                    case S_ON_PORT_B:
                        gGroupValveStatus[group_no][index] = VALVE_GOOD_ON_PORT_B;
                        break;
                    case S_ON_PORT_AB:
                        gGroupValveStatus[group_no][index] = VALVE_GOOD_ON_PORT_AB;
                        break;
                }
                break;
            case S_DEV_SUSPEND:
                gGroupValveStatus[group_no][index] = VALVE_SUSPEND;
                break;
        }
    }
    return 0;
}

/*
	获取指定阀门的开度值
	参数:
		dbOffset:对应于xValveView中的dBOffsetForCurPos
	返回值:
		其他:开度值
		-1:通信失败
*/
int GetValveCurPos(unsigned int dbOffset)
{
    unsigned short curPos;
    xCommMessage xMsg;
    if(dbOffset == 0xFFFFFFFF)
        return 0;
    xMsg.iMsgType = REG_READ_MSG;
    xMsg.uMsgContent.xRegRd.iOffset = dbOffset;
    xMsg.uMsgContent.xRegRd.iNum = 1;
    msgsnd(gCommMsgQueue,&xMsg,sizeof(xRegRead),0);
    eMBRegHoldingCB((unsigned char *)&curPos,dbOffset,1,REG_READ|0x80);
    printf("GetValveCurPos,dbOffset = %d,value = %d\n",dbOffset,curPos);
    return (int)curPos;	
}

/*
	获取指定阀门的全开状态
	参数:
		dbOffset:对应于xValveView中的dBOffsetForOpenVal
	返回值:
		0:全关
		1:全开
		-1:通信失败
*/
int GetValveOpenStatus(unsigned int dbOffset)
{
    unsigned char ucStatus[2];
    xCommMessage xMsg;
    if(dbOffset == 0xFFFFFFFF)
        return 0;
    xMsg.iMsgType = REG_READ_MSG;
    xMsg.uMsgContent.xRegRd.iOffset = (int)(dbOffset>>4); 
    xMsg.uMsgContent.xRegRd.iNum = 1;
    msgsnd(gCommMsgQueue,&xMsg,sizeof(xRegRead),0);
    eMBRegCoilsCB(ucStatus,dbOffset,1,REG_READ);
    printf("GetValveOpenStatus,dBOffset = %d,value = %d,%d\n",dbOffset,ucStatus[0],ucStatus[1]);
    return ucStatus[0] ? 1:0;
} 

/*
	获取指定阀门的全关状态
	参数:
		dbOffset:对应于xValveView中的dBOffsetForCloseVal
	返回值:
		0:全开
		1:全关
		-1:通信失败
*/
int GetValveCloseStatus(unsigned int dbOffset)
{
    unsigned char ucStatus[2];
    xCommMessage xMsg;
    if(dbOffset == 0xFFFFFFFF)
        return 0;
    xMsg.iMsgType = REG_READ_MSG;
    xMsg.uMsgContent.xRegRd.iOffset = (int)(dbOffset>>4); 
    xMsg.uMsgContent.xRegRd.iNum = 1;
    msgsnd(gCommMsgQueue,&xMsg,sizeof(xRegRead),0);
    eMBRegCoilsCB(ucStatus,dbOffset,1,REG_READ);
    printf("GetValveOpenStatus,dBOffset = %d,value = %d,%d\n",dbOffset,ucStatus[0],ucStatus[1]);
    return ucStatus[0] ? 1:0;
}

/*
	获取指定阀门的L/R状态
	参数:
		dbOffset:对应于xValveView中的dBOffsetForLRVal
	返回值:
		0:L
		1:R
		-1:通信失败
*/
int GetValveLRStatus(unsigned int dbOffset)
{
    unsigned char ucStatus[2];
    xCommMessage xMsg;
    if(dbOffset == 0xFFFFFFFF)
        return 0;
    xMsg.iMsgType = REG_READ_MSG;
    xMsg.uMsgContent.xRegRd.iOffset = (int)(dbOffset>>4); 
    xMsg.uMsgContent.xRegRd.iNum = 1;
    msgsnd(gCommMsgQueue,&xMsg,sizeof(xRegRead),0);
    eMBRegCoilsCB(ucStatus,dbOffset,1,REG_READ);
    printf("GetValveOpenStatus,dBOffset = %d,value = %d,%d\n",dbOffset,ucStatus[0],ucStatus[1]);
    return ucStatus[0] ? 1:0;
}

/*
	阀门开启命令
	参数:
		dbOffset:对应xValveCtrlCmd中的dBOffsetForOpenCmd
		cmd:对应xValveCtrlCmd中的openCmd
	返回值:
		-1:通信失败
		其他:通信成功
*/
int OpenValve(unsigned short dbOffset,unsigned short cmd)
{
    xCommMessage xMsg;
    if(dbOffset == 0xFFFF)
        return 0;
    xMsg.iMsgType = REG_WRITE_MSG;
    xMsg.uMsgContent.xRegWr.iOffset = dbOffset;
    xMsg.uMsgContent.xRegWr.iValue = cmd;
    msgsnd(gCommMsgQueue,&xMsg,sizeof(xRegWrite),0);
    return 1;
}

/*
	阀门关闭命令
	参数:
		dbOffset:对应xValveCtrlCmd中的dBOffsetForCloseCmd
		cmd:对应xValveCtrlCmd中的closeCmd
	返回值:
		-1:通信失败
		其他:通信成功
*/
int StopValve(unsigned short dbOffset,unsigned short cmd)
{
    xCommMessage xMsg;
    if(dbOffset == 0xFFFF)
        return 0;
    xMsg.iMsgType = REG_WRITE_MSG;
    xMsg.uMsgContent.xRegWr.iOffset = (int)dbOffset;
    xMsg.uMsgContent.xRegWr.iValue = (int)cmd;
    msgsnd(gCommMsgQueue,&xMsg,sizeof(xRegWrite),0);
    return 1;
}

/*
	阀门关闭命令
	参数:
		dbOffset:对应xValveCtrlCmd中的dBoffsetForStopCmd
		cmd:对应xValveCtrlCmd中的stopCmd
	返回值:
		-1:通信失败
		其他:通信成功
*/
int CloseValve(unsigned short dbOffset,unsigned short cmd)
{
    xCommMessage xMsg;
    if(dbOffset == 0xFFFF)
        return 0;
    xMsg.iMsgType = REG_WRITE_MSG;
    xMsg.uMsgContent.xRegWr.iOffset = (int)dbOffset;
    xMsg.uMsgContent.xRegWr.iValue = (int)cmd;
    msgsnd(gCommMsgQueue,&xMsg,sizeof(xRegWrite),0);
    return 1;
}


/*
	设定阀门开度
	参数:
		dbCmdOffset:对应xValveCtrlCmd中的dBOffsetForCurPosCmd
		cmd:对应xValveCtrlCmd中的getCurPosCmd
		dbCurPosOffset:对应xValveCtrlCmd中的dbOffsetForCurPos
	返回值:
		-1:通信失败
		其他:通信成功
*/
int SetValveCurPos(unsigned short dbCmdOffset,unsigned short cmd,unsigned short dbCurPosOffset,unsigned short curPos)
{
    xCommMessage xMsg;
    if(dbCmdOffset == 0xFFFF)
    return 0;
    if(dbCmdOffset == dbCurPosOffset)
    {
        xMsg.iMsgType = REG_WRITE_MSG;
        xMsg.uMsgContent.xRegWr.iOffset = (int)dbCmdOffset;
        xMsg.uMsgContent.xRegWr.iValue = (int)cmd + curPos;
        msgsnd(gCommMsgQueue,&xMsg,sizeof(xRegWrite),0);
    }
    else
    {
        //first cmd
        xMsg.iMsgType = REG_WRITE_MSG;
        xMsg.uMsgContent.xRegWr.iOffset = (int)dbCmdOffset;
        xMsg.uMsgContent.xRegWr.iValue = (int)cmd;
        msgsnd(gCommMsgQueue,&xMsg,sizeof(xRegWrite),0);
        //second cmd
        xMsg.iMsgType = REG_WRITE_MSG;
        xMsg.uMsgContent.xRegWr.iOffset = (int)dbCurPosOffset;
        xMsg.uMsgContent.xRegWr.iValue = (int)curPos;
        msgsnd(gCommMsgQueue,&xMsg,sizeof(xRegWrite),0);
    }
    return 1;
}


