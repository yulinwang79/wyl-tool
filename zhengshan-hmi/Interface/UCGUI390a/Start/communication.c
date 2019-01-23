#include "hmidata.h"

eValveStatus gGroupValveStatus[VALVE_MAX_GROUP][GROUP_VALVE_NUM];

/*
	获取指定环路的所有阀门状态
	参数:
		group_num:环路号
	返回值:
		-1:       通信失败
		其他:通信成功
*/
int GetGroupValveStatus(eValveGroup group_num)
{
    #ifdef WIN32 
        int i;
        for(i=0; i< GROUP_VALVE_NUM;i++)
        {
            gGroupValveStatus[group_num][i] = rand()%4;
        }
    #endif
	return 1;
}

/*
	获取指定阀门的开度值
	参数:
		dbOffset:对应于xValveView中的dBOffsetForCurPos
	返回值:
		0~100:开度值
		-1:通信失败
*/
int GetValveCurPos(unsigned int dbOffset)
{
	return 20;
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
	return 1;
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
	return 0;
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
	return 0;
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
int CloseValve(unsigned short dbOffset,unsigned short cmd)
{
	return 1;
}

/*
	阀门停止命令
	参数:
		dbOffset:对应xValveCtrlCmd中的dBoffsetForStopCmd
		cmd:对应xValveCtrlCmd中的stopCmd
	返回值:
		-1:通信失败
		其他:通信成功
*/
int StopValve(unsigned short dbOffset,unsigned short cmd)
{
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
int SetValveCurPos(unsigned short dbCmdOffset,unsigned short cmd,unsigned short dbCurPosOffset,unsigned short value)
{
	return 1;
}

