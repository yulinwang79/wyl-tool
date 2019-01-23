#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <fcntl.h>
#include <unistd.h>
#include <signal.h>
#include <stdarg.h>
#include <termios.h>
#include <errno.h>
#include <pthread.h>
#include<dirent.h>
//#include <sys/ipc.h>
#include <sys/shm.h>
#include <sys/types.h>
#include <sys/time.h>
#include <sys/msg.h>
#include <sys/prctl.h>
#include "ipc_key.h"
#include "ipc_message.h"
#include "ring_buffer.h"
#include "hmi_communication.h"
#include "realdb_operation.h"
#include "file_operation.h"
#include "com_datastruct.h"

#define COMMNUCATION_DEBUG	0

typedef enum {
    SEND_ON_PORT_1 = 0x0001,
    SEND_ON_PORT_2 = 0x0002,
    SEND_ON_BOTH = 0x0003,
}eHMI2MCPort;

typedef enum {
    UART_PORT_0 = 0,
    UART_PORT_1,
    MAX_COMM_PORT
}eCommPort;

typedef enum {
    FRAME_IN,
    FRAME_OUT,
}eFrameDBGType;

typedef struct {
    int iModuleCfgUpdate;
    unsigned int uiHashVal;
    unsigned int uiEth0CompactActiveIP;
    unsigned int uiEth0CompactBackupIp;
    unsigned int uiEth0CompactGateWay;
    unsigned int uiEth0CompactNetMask;
    unsigned int uiEth1CompactActiveIP;
    unsigned int uiEth1CompactBackupIp;
    unsigned int uiEth1CompactGateWay;
    unsigned int uiEth1CompactNetMask;
    int iPortFd;
    int iBytesLeft;
    int iBytesReceived;
    unsigned char *pRecvBuf;
    xRingBuffer xSendBuf;
    struct timeval xTimeStamp;
}xPortControl;

typedef struct {
    struct timeval xGroupTimeStamp[VALVE_MAX_GROUP];
    struct timeval xDataBaseTimeStamp[REAL_DB_SIZE];
}xCommTimeStampControl;

char acFrameDBGBuf[512];

char acDebugHexValue[16] =
{
    '0',
    '1',
    '2',
    '3',
    '4',
    '5',
    '6',
    '7',
    '8',
    '9',
    'A',
    'B',
    'C',
    'D',
    'E',
    'F',
};

static const unsigned char aucCRCHi[] = {
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 
    0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40
};

static const unsigned char aucCRCLo[] = {
    0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06, 0x07, 0xC7,
    0x05, 0xC5, 0xC4, 0x04, 0xCC, 0x0C, 0x0D, 0xCD, 0x0F, 0xCF, 0xCE, 0x0E,
    0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09, 0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9,
    0x1B, 0xDB, 0xDA, 0x1A, 0x1E, 0xDE, 0xDF, 0x1F, 0xDD, 0x1D, 0x1C, 0xDC,
    0x14, 0xD4, 0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3,
    0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 0xF2, 0x32,
    0x36, 0xF6, 0xF7, 0x37, 0xF5, 0x35, 0x34, 0xF4, 0x3C, 0xFC, 0xFD, 0x3D,
    0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A, 0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38, 
    0x28, 0xE8, 0xE9, 0x29, 0xEB, 0x2B, 0x2A, 0xEA, 0xEE, 0x2E, 0x2F, 0xEF,
    0x2D, 0xED, 0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,
    0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 0x61, 0xA1,
    0x63, 0xA3, 0xA2, 0x62, 0x66, 0xA6, 0xA7, 0x67, 0xA5, 0x65, 0x64, 0xA4,
    0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F, 0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB, 
    0x69, 0xA9, 0xA8, 0x68, 0x78, 0xB8, 0xB9, 0x79, 0xBB, 0x7B, 0x7A, 0xBA,
    0xBE, 0x7E, 0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5,
    0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 0x70, 0xB0,
    0x50, 0x90, 0x91, 0x51, 0x93, 0x53, 0x52, 0x92, 0x96, 0x56, 0x57, 0x97,
    0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C, 0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E,
    0x5A, 0x9A, 0x9B, 0x5B, 0x99, 0x59, 0x58, 0x98, 0x88, 0x48, 0x49, 0x89,
    0x4B, 0x8B, 0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C,
    0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 0x43, 0x83,
    0x41, 0x81, 0x80, 0x40
};


static xCommTimeStampControl xCommTimeStamp;

static xPortControl gCommPortCtrl[MAX_COMM_PORT];

static unsigned char *pCommSendBuffer;

static struct timeval xGTimeStamp;
static struct timeval xMMITimeStamp;

static fd_set commPortFdsr,commPortFdsw;

extern xRealTimeDBMap *pRealDB;
xGeneralSharedDataMap *pGenSharedData;

static char csFileName[MAX_FILE_PATH];

static int iSendPort[MAX_COMM_PORT] = {
    SEND_ON_PORT_1,
    SEND_ON_PORT_2,
};

static unsigned char ucsFileReadBuf[MAX_MSG_LEN>>1];
static char bin2text[16] = {'0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F'};

static unsigned short crc16( unsigned char * pucFrame, unsigned short usLen )
{
    unsigned char   ucCRCHi = 0xFF;
    unsigned char   ucCRCLo = 0xFF;
    int             iIndex;

    while( usLen-- )
    {
        iIndex = ucCRCLo ^ *( pucFrame++ );
        ucCRCLo = ( unsigned char )( ucCRCHi ^ aucCRCHi[iIndex] );
        ucCRCHi = aucCRCLo[iIndex];
    }
    return ( unsigned short )( ucCRCHi << 8 | ucCRCLo );
}

#if 0
static void frame_debug(unsigned char *pFrame,int iFrameLen,eFrameDBGType eFrameType)
    {
#if COMMNUCATION_DEBUG
    int offset = 0;
    int debug_offset;
    if(eFrameType == FRAME_IN)
    sprintf(acFrameDBGBuf,"Rx:");
    else
    sprintf(acFrameDBGBuf,"Tx:");
    debug_offset = strlen(acFrameDBGBuf);
    while(offset < iFrameLen)
    {   
        acFrameDBGBuf[debug_offset++] = acDebugHexValue[(pFrame[offset]&0xF0)>>4];
        acFrameDBGBuf[debug_offset++] = acDebugHexValue[pFrame[offset++]&0x0F];
        acFrameDBGBuf[debug_offset++] = ' ';
        if(offset > 60)
        {
            acFrameDBGBuf[debug_offset++] = '.';
            acFrameDBGBuf[debug_offset++] = '.';
            acFrameDBGBuf[debug_offset++] = '.';
            break;
        }
    }
    acFrameDBGBuf[debug_offset] = 0x00;
    printf("%s\n",acFrameDBGBuf);
#endif
}
#endif

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

static int comm_port_open(int iPortNo)
    {
    char szDeviceName[16];
    struct termios	xNewTIO;
    speed_t     	xNewSpeed = B115200;
    if(gCommPortCtrl[iPortNo].iPortFd != -1)
    {
        close(gCommPortCtrl[iPortNo].iPortFd);
        gCommPortCtrl[iPortNo].iPortFd = -1;
    }
    sprintf(szDeviceName,"/dev/ttyS%d",iPortNo+1);
    if( ( gCommPortCtrl[iPortNo].iPortFd = open( szDeviceName, O_RDWR | O_NOCTTY ) ) < 0 )
    {
        return 0;
    }
    bzero( &xNewTIO, sizeof( struct termios ) );

    xNewTIO.c_iflag |= IGNBRK | INPCK;
    xNewTIO.c_cflag |= CREAD | CLOCAL;

    xNewTIO.c_cflag |= CS8;//8 data bits
    xNewSpeed = B115200;
    cfsetispeed( &xNewTIO, xNewSpeed);
    cfsetospeed( &xNewTIO, xNewSpeed);
    tcflush(gCommPortCtrl[iPortNo].iPortFd,TCIOFLUSH);
    tcsetattr( gCommPortCtrl[iPortNo].iPortFd, TCSANOW, &xNewTIO );
#if COMMNUCATION_DEBUG
    printf("comm_port_open [%d]\n",iPortNo);
#endif
    gCommPortCtrl[iPortNo].iBytesLeft = sizeof(xMC2HMIFrameHead);
    gCommPortCtrl[iPortNo].iBytesReceived = 0;
    gCommPortCtrl[iPortNo].xSendBuf.rIndex = gCommPortCtrl[iPortNo].xSendBuf.wIndex = 0;
    return 1;
}


static int comm_build_send_buf(unsigned char *pucFrame,int iFrameLen,int iPort)
{
    int iRoomLeft;
    if(iPort & SEND_ON_PORT_1)
    {
        ringbuf_get_roomleft(gCommPortCtrl[UART_PORT_0].xSendBuf,iRoomLeft);
        if(iRoomLeft < iFrameLen)
            return -1;
        ringbuf_put(gCommPortCtrl[UART_PORT_0].xSendBuf,pucFrame,iFrameLen);
    }
    if(iPort & SEND_ON_PORT_2)
    {
        ringbuf_get_roomleft(gCommPortCtrl[UART_PORT_1].xSendBuf,iRoomLeft);
        if(iRoomLeft < iFrameLen)
            return -1;
        ringbuf_put(gCommPortCtrl[UART_PORT_1].xSendBuf,pucFrame,iFrameLen);
    }
#if COMMNUCATION_DEBUG
    {
        int offset = 0;
        int debug_offset;
        sprintf(acFrameDBGBuf,"Tx:");
        debug_offset = strlen(acFrameDBGBuf);
        while(offset < iFrameLen)
        {    
            acFrameDBGBuf[debug_offset++] = acDebugHexValue[(pucFrame[offset]&0xF0)>>4];
            acFrameDBGBuf[debug_offset++] = acDebugHexValue[pucFrame[offset++]&0x0F];
            acFrameDBGBuf[debug_offset++] = ' ';
            if(offset > 60)
            {
                acFrameDBGBuf[debug_offset++] = '.';
                acFrameDBGBuf[debug_offset++] = '.';
                acFrameDBGBuf[debug_offset++] = '.';
                break;
            }
        }
        acFrameDBGBuf[debug_offset] = 0x00;
        printf("%s\n",acFrameDBGBuf);
    }
#endif
    return 0;
    }


static void reg_write_req(int iOffset,int iValue)
{
    int iFrameLen;
    long ulDeltaMS;
    unsigned short usCRC16;
    xVCRegWordWriteReq *pFrameReq;
    pFrameReq = (xVCRegWordWriteReq *)pCommSendBuffer;
    pFrameReq->xHead.usFrameType = REG_WORD_WRITE_REQ;
    pFrameReq->usDbOffset = (unsigned short)iOffset;
    pFrameReq->usValue = (unsigned short)iValue;
    #if COMMNUCATION_DEBUG
    printf("reg_write_req,usOffset = %d,iValue = %d\n",iOffset,iValue);
    #endif

    if(iOffset == 0xFFFF)
        return;
    if(iOffset > sizeof(xRealTimeDBMap)/sizeof(unsigned short))
    {
        return;
    }

    iFrameLen = sizeof(xVCRegWordReadReq);
    pFrameReq->xHead.usFrameLen = iFrameLen + CRC16_SIZE;
    usCRC16 = crc16(pCommSendBuffer,iFrameLen);
    pCommSendBuffer[iFrameLen++] = (unsigned char)(usCRC16&0xFF);
    pCommSendBuffer[iFrameLen++] = (unsigned char)(usCRC16>>8);
    if(xGTimeStamp.tv_usec<xCommTimeStamp.xDataBaseTimeStamp[pFrameReq->usDbOffset].tv_usec)
    {      
        ulDeltaMS = ((xGTimeStamp.tv_sec-xCommTimeStamp.xDataBaseTimeStamp[pFrameReq->usDbOffset].tv_sec)<<10) - 
                            ((xCommTimeStamp.xDataBaseTimeStamp[pFrameReq->usDbOffset].tv_usec-xGTimeStamp.tv_usec)>>10);
    }    
    else
    {      
        ulDeltaMS = ((xGTimeStamp.tv_sec-xCommTimeStamp.xDataBaseTimeStamp[pFrameReq->usDbOffset].tv_sec)<<10) + 
                            ((xGTimeStamp.tv_usec-xCommTimeStamp.xDataBaseTimeStamp[pFrameReq->usDbOffset].tv_usec)>>10);
    }

    if(ulDeltaMS > COMMUNICATION_DELAY)
    {
        xCommTimeStamp.xDataBaseTimeStamp[pFrameReq->usDbOffset].tv_sec = xGTimeStamp.tv_sec;
        xCommTimeStamp.xDataBaseTimeStamp[pFrameReq->usDbOffset].tv_usec = xGTimeStamp.tv_usec;
        comm_build_send_buf(pCommSendBuffer,iFrameLen,SEND_ON_BOTH);
    }
}

static void reg_word_write_res(unsigned char *pucFrame)
{
    xVCRegWordWriteRes *pRegWordWriteRes;
    pRegWordWriteRes = (xVCRegWordWriteRes *)pucFrame;
    eMBRegHoldingCB((unsigned char *)&pRegWordWriteRes->usValue,(unsigned int)pRegWordWriteRes->usDbOffset,1,REG_WRITE|0x80);
}

static void reg_read_req(int iOffset, int iNum)
{
    unsigned short usCRC16;
    int iFrameLen;
    long ulDeltaMS;
    xVCRegWordReadReq *pFrameReq;
    pFrameReq = (xVCRegWordReadReq *)pCommSendBuffer;
    pFrameReq->xHead.usFrameType = REG_WORD_READ_REQ;
    #if COMMNUCATION_DEBUG
    printf("reg_read_req,usOffset = %d,usNum = %d\n",iOffset,iNum);
    #endif
    if((unsigned short)iOffset == 0xFFFF)
        return;
    if((unsigned short)iOffset > sizeof(xRealTimeDBMap)/sizeof(unsigned short))
    {
        return;
    }
    pFrameReq->usDbOffset = (unsigned short)iOffset; 
    pFrameReq->usRegNum = (unsigned short)iNum;
    iFrameLen = sizeof(xVCRegWordReadReq);
    pFrameReq->xHead.usFrameLen = iFrameLen + CRC16_SIZE;
    usCRC16 = crc16(pCommSendBuffer,iFrameLen);
    pCommSendBuffer[iFrameLen++] = (unsigned char)(usCRC16&0xFF);
    pCommSendBuffer[iFrameLen++] = (unsigned char)(usCRC16>>8);
    if(xGTimeStamp.tv_usec<xCommTimeStamp.xDataBaseTimeStamp[pFrameReq->usDbOffset].tv_usec)
    {       
        ulDeltaMS = ((xGTimeStamp.tv_sec-xCommTimeStamp.xDataBaseTimeStamp[pFrameReq->usDbOffset].tv_sec)<<10) - 
                           ((xCommTimeStamp.xDataBaseTimeStamp[pFrameReq->usDbOffset].tv_usec-xGTimeStamp.tv_usec)>>10);
    }     
    else
    {       
        ulDeltaMS = ((xGTimeStamp.tv_sec-xCommTimeStamp.xDataBaseTimeStamp[pFrameReq->usDbOffset].tv_sec)<<10) + 
                           ((xGTimeStamp.tv_usec-xCommTimeStamp.xDataBaseTimeStamp[pFrameReq->usDbOffset].tv_usec)>>10);
    }
    if(ulDeltaMS > COMMUNICATION_DELAY)
    {
        xCommTimeStamp.xDataBaseTimeStamp[pFrameReq->usDbOffset].tv_sec = xGTimeStamp.tv_sec;
        xCommTimeStamp.xDataBaseTimeStamp[pFrameReq->usDbOffset].tv_usec = xGTimeStamp.tv_usec;
        comm_build_send_buf(pCommSendBuffer,iFrameLen,SEND_ON_BOTH);
    }
}

static void reg_word_read_res(unsigned char *pucFrame)
{
    xVCRegWordReadRes *pRegWordReadRes;
    pRegWordReadRes = (xVCRegWordReadRes *)pucFrame;
    if(pRegWordReadRes->usRegNum > MAX_REG_NUM)
        return;
    eMBRegHoldingCB((unsigned char *)pRegWordReadRes->usValue,(unsigned int)pRegWordReadRes->usDbOffset,pRegWordReadRes->usRegNum,REG_WRITE|0x80);
}

static void build_ftp_frame(xFTPFrame *pFTPFrame,int iFrameType,int iFrameLen,int iPortNo)
{
    unsigned short usCRC16;
    iFrameLen += sizeof(xMC2HMIFrameHead);
    pFTPFrame->xHead.usFrameType = (unsigned short)iFrameType;
    pFTPFrame->xHead.usFrameLen = (unsigned short)iFrameLen + CRC16_SIZE;
    usCRC16 = crc16((unsigned char *)pFTPFrame,iFrameLen);
    ((unsigned char *)pFTPFrame)[iFrameLen++] = (unsigned char)(usCRC16&0xFF);
    ((unsigned char *)pFTPFrame)[iFrameLen++] = (unsigned char)(usCRC16>>8);
    comm_build_send_buf((unsigned char *)pFTPFrame,iFrameLen,iSendPort[iPortNo - UART_PORT_0]); 
}

/*AT+REBOOT,TARGET=*/
static void hmi_board_reboot(int iPortNo)
{
    xFTPFrame *pFTPFrame;
    pFTPFrame = (xFTPFrame *)pCommSendBuffer;
    sprintf(pFTPFrame->csFrame,"AT+OK"CONFIG_CMD_FRAME_END);
    build_ftp_frame(pFTPFrame,HMI_BOARD_REBOOT_RES,strlen(pFTPFrame->csFrame),iPortNo);
    usleep(200000);
    system("reboot");
}

/*AT+FGET,NAME=filename*/
static void file_get_req(int iPortNo,unsigned char *pucFrame)
{
    xFTPFrame *pFTPFrame;
    char *pcString;
    int iFd;
    csFileName[0] = 0;
    pFTPFrame = (xFTPFrame *)pucFrame;
    pcString = strtok(pFTPFrame->csFrame,CONFIG_CMD_FRAME_SEPERATOR);
    //bypass header and cmd part
    do{
        pcString = strtok(NULL,CONFIG_CMD_FRAME_SEPERATOR);
        if(pcString == NULL)
            break;
        if(!strncmp(pcString,"NAME=",5))
        {
            sprintf(csFileName,"%s",pcString + 5);
        }
    }while(1);
    if(strlen(csFileName) > 0)
    {

        iFd = xFileOpen(CONFIG_MODULE,csFileName+strlen(FILE_LOCATION_HMI));
        pFTPFrame = (xFTPFrame *)pCommSendBuffer;
        if(iFd == -1)
        {
            sprintf(pFTPFrame->csFrame,"AT+FAIL"CONFIG_CMD_FRAME_END);
        }
        else
        {
            sprintf(pFTPFrame->csFrame,"AT+OK,ID=%d,LEN=%d"CONFIG_CMD_FRAME_END,iFd + FILE_POOL_SIZE,xFileGetSize(iFd));
        }
        build_ftp_frame(pFTPFrame,HMI_FILE_GET_RES,strlen(pFTPFrame->csFrame),iPortNo);
    }
}

/*AT+FRD,ID=,OFF=*/
static void file_read_req(int iPortNo,unsigned char *pucFrame)
    {
    xFTPFrame *pFTPFrame;
    char *pcString;
    int iFd = -1;
    int iFileOffset = -1;
    int iHexOffset;
    int iAsciiOffset;
    int iBytesRead;
    pFTPFrame = (xFTPFrame *)pucFrame;
    pcString = strtok(pFTPFrame->csFrame,CONFIG_CMD_FRAME_SEPERATOR);
    //bypass header and cmd part
    do{
        pcString = strtok(NULL,CONFIG_CMD_FRAME_SEPERATOR);
        if(pcString == NULL)
            break;
        if(!strncmp(pcString,"ID=",3))
        {
            iFd = atoi(pcString+3);
        }
        else if(!strncmp(pcString,"OFF=",4))
        {
            iFileOffset = atoi(pcString+4);
        }
        if((iFd != -1) && (iFileOffset != -1))
        {
            iHexOffset = 0;
            iAsciiOffset = 0;
            pFTPFrame = (xFTPFrame *)pCommSendBuffer;
            iBytesRead = xFileRead(ucsFileReadBuf,100,iFileOffset,iFd - FILE_POOL_SIZE);
            if(iBytesRead == -1)
            {
                sprintf(pFTPFrame->csFrame,"AT+FAIL"CONFIG_CMD_FRAME_END);
                build_ftp_frame(pFTPFrame,HMI_FILE_READ_RES,strlen(pFTPFrame->csFrame),iPortNo);
                return;
            }

            if(iBytesRead == 0)
            {
                xFileClose(iFd - FILE_POOL_SIZE);
                sprintf(pFTPFrame->csFrame,"AT+OK,LEN=%d,OK"CONFIG_CMD_FRAME_END,iBytesRead<<1);
                build_ftp_frame(pFTPFrame,HMI_FILE_READ_RES,strlen(pFTPFrame->csFrame),iPortNo);
                return;
            }
            sprintf(pFTPFrame->csFrame,"AT+OK,LEN=%d,",iBytesRead<<1);
            iAsciiOffset = strlen(pFTPFrame->csFrame);
            do{
                pFTPFrame->csFrame[iAsciiOffset++] = bin2text[ucsFileReadBuf[iHexOffset]>>4];
                pFTPFrame->csFrame[iAsciiOffset++] = bin2text[ucsFileReadBuf[iHexOffset]&0x0F];
                iHexOffset++;
                iBytesRead--;
            }while(iBytesRead);
            sprintf(&pFTPFrame->csFrame[iAsciiOffset],CONFIG_CMD_FRAME_END);
            build_ftp_frame(pFTPFrame,HMI_FILE_READ_RES,strlen(pFTPFrame->csFrame),iPortNo);
            return;
        }
    }while(1);
}


/*AT+FPUT,NAME=filename,LEN=*/
static void file_put_req(int iPortNo,unsigned char *pucFrame)
    {
    xFTPFrame *pFTPFrame;
    char *pcString;
    int iFileLen = 0;
    int iFd;
    csFileName[0] = 0;
    pFTPFrame = (xFTPFrame *)pucFrame;
    pcString = strtok(pFTPFrame->csFrame,CONFIG_CMD_FRAME_SEPERATOR);
    //bypass header and cmd part
    do{
        pcString = strtok(NULL,CONFIG_CMD_FRAME_SEPERATOR);
        if(pcString == NULL)
        break;
        if(!strncmp(pcString,"NAME=",5))
        {
            sprintf(csFileName,"%s",pcString + 5);
        }
        else if(!strncmp(pcString,"LEN=",4))
        {
            iFileLen = atoi(pcString+4);
        }
    }while(1);
    if((iFileLen > 0) && (strlen(csFileName) > 0))
    {
        iFd = xFileCreate(CONFIG_MODULE,csFileName+strlen(FILE_LOCATION_HMI),iFileLen);
        pFTPFrame = (xFTPFrame *)pCommSendBuffer;
        if(iFd == -1)
        {
            sprintf(pFTPFrame->csFrame,"AT+FAIL"CONFIG_CMD_FRAME_END);
        }
        else
        {
            sprintf(pFTPFrame->csFrame,"AT+OK,ID=%d"CONFIG_CMD_FRAME_END,iFd + FILE_POOL_SIZE);
        }
        build_ftp_frame(pFTPFrame,HMI_FILE_PUT_RES,strlen(pFTPFrame->csFrame),iPortNo);
    }
}

static void file_write_fail(int iPortNo)
{
    xFTPFrame *pFTPFrame;
    pFTPFrame = (xFTPFrame *)pCommSendBuffer; 
    sprintf(pFTPFrame->csFrame,"AT+FAIL"CONFIG_CMD_FRAME_END);
    build_ftp_frame(pFTPFrame,HMI_FILE_WRITE_RES,strlen(pFTPFrame->csFrame),iPortNo);
}


/*AT+FWR,ID=,OFF=,LEN=,content*/
static void file_write_req(int iPortNo,unsigned char *pucFrame)
{
    char *pcString;
    int iFd = -1;
    int iRet;
    int iFileOffset = -1;
    int iHexOffset;
    int iAsciiOffset;
    int iContentLen = -1;
    xFTPFrame *pFTPFrame;
    pFTPFrame = (xFTPFrame *)pucFrame;
    pcString = strtok(pFTPFrame->csFrame,CONFIG_CMD_FRAME_SEPERATOR);
    //bypass header and cmd part
    do{
        pcString = strtok(NULL,CONFIG_CMD_FRAME_SEPERATOR);
        if(pcString == NULL)
            break;
        if((iFd != -1) && (iFileOffset != -1) && (iContentLen != -1))
        {
            iHexOffset = 0;
            iAsciiOffset = 0;
            while(iAsciiOffset < iContentLen)
            {
                if(pcString[iAsciiOffset] > 0x40)
                {
                    pCommSendBuffer[iHexOffset] = pcString[iAsciiOffset] - 0x41 + 10;
                }
                else
                {
                    pCommSendBuffer[iHexOffset] = pcString[iAsciiOffset] - 0x30;
                }
                pCommSendBuffer[iHexOffset] <<= 4;
                if(pcString[iAsciiOffset+1] > 0x40)
                {
                    pCommSendBuffer[iHexOffset] |= pcString[iAsciiOffset+1] - 0x41 + 10;
                }
                else
                {
                    pCommSendBuffer[iHexOffset] |= pcString[iAsciiOffset+1] - 0x30;
                }
                iHexOffset++;
                iAsciiOffset += 2;
            }
            if(iContentLen > 0)
            {
                iRet = xFileWrite(pCommSendBuffer,iContentLen>>1,iFileOffset,iFd - FILE_POOL_SIZE);
                pFTPFrame = (xFTPFrame *)pCommSendBuffer;
                if(iRet != -1)
                sprintf(pFTPFrame->csFrame,"AT+OK"CONFIG_CMD_FRAME_END);
                else
                sprintf(pFTPFrame->csFrame,"AT+FAIL"CONFIG_CMD_FRAME_END);
                build_ftp_frame(pFTPFrame,HMI_FILE_WRITE_RES,strlen(pFTPFrame->csFrame),iPortNo);
            }
            return;
        }
        if(!strncmp(pcString,"ID=",3))
        {
            iFd = atoi(pcString+3);
        }
        else if(!strncmp(pcString,"OFF=",4))
        {
            iFileOffset = atoi(pcString+4);
        }
        else if(!strncmp(pcString,"LEN=",4))
        {
            iContentLen = atoi(pcString+4);
            if(iContentLen == 0)
            {
                if(iFd != -1)
                xFileClose(iFd - FILE_POOL_SIZE);
                pFTPFrame = (xFTPFrame *)pCommSendBuffer;
                sprintf(pFTPFrame->csFrame,"AT+OK"CONFIG_CMD_FRAME_END);
                build_ftp_frame(pFTPFrame,HMI_FILE_WRITE_RES,strlen(pFTPFrame->csFrame),iPortNo);
                return;
            }
        }
    }while(1);

}

int sync_module_ip(int iPortNo)
{
    int iFrameLen;
    unsigned short usCRC16;
    xSyncModuleIPReq *pFrameReq;
    pFrameReq = (xSyncModuleIPReq *)pCommSendBuffer;
    iFrameLen = sizeof(xSyncModuleIPReq);
    pFrameReq->xHead.usFrameType = SYNC_MODULE_IP_REQ;
    pFrameReq->xHead.usFrameLen = iFrameLen + CRC16_SIZE;
    usCRC16 = crc16(pCommSendBuffer,iFrameLen);
    pCommSendBuffer[iFrameLen++] = (unsigned char)(usCRC16&0xFF);
    pCommSendBuffer[iFrameLen++] = (unsigned char)(usCRC16>>8);
    #if COMMNUCATION_DEBUG
    printf("sync_module_ip\n");
    #endif
    if(iPortNo == UART_PORT_0)
        comm_build_send_buf(pCommSendBuffer,iFrameLen,SEND_ON_PORT_1);
    else
        comm_build_send_buf(pCommSendBuffer,iFrameLen,SEND_ON_PORT_2);
    gCommPortCtrl[iPortNo].iModuleCfgUpdate = 0;
    return 0;
}


int sync_module_ip_res(int iPortNo,unsigned char *pucFrame)
{
    int iFrameLen;
    unsigned short usCRC16;
    xSyncModuleIPRes *pFrameRes;
    xUpdateModuleIPReq *pIPUpdateReq;
    pFrameRes = (xSyncModuleIPRes *)pucFrame;
    #if COMMNUCATION_DEBUG
    printf("sync_module_ip_res\n");
    #endif
    gCommPortCtrl[iPortNo].uiHashVal = pFrameRes->uiHashVal;
    gCommPortCtrl[iPortNo].uiEth0CompactActiveIP = pFrameRes->uiEth0CompactActiveIP;
    gCommPortCtrl[iPortNo].uiEth0CompactBackupIp = pFrameRes->uiEth0CompactBackupIp;
    gCommPortCtrl[iPortNo].uiEth0CompactGateWay = pFrameRes->uiEth0CompactGateWay;
    gCommPortCtrl[iPortNo].uiEth0CompactNetMask = pFrameRes->uiEth0CompactNetMask;
    gCommPortCtrl[iPortNo].uiEth1CompactActiveIP = pFrameRes->uiEth1CompactActiveIP;
    gCommPortCtrl[iPortNo].uiEth1CompactBackupIp = pFrameRes->uiEth1CompactBackupIp;
    gCommPortCtrl[iPortNo].uiEth1CompactGateWay = pFrameRes->uiEth1CompactGateWay;
    gCommPortCtrl[iPortNo].uiEth1CompactNetMask = pFrameRes->uiEth1CompactNetMask;
    gCommPortCtrl[iPortNo].iModuleCfgUpdate = 1;
    if(iPortNo == UART_PORT_0)
    {
        pGenSharedData->data.xSharedData.uiM0Eth0ActiveIP = gCommPortCtrl[iPortNo].uiEth0CompactActiveIP;
        pGenSharedData->data.xSharedData.uiM0Eth0BackupIP = gCommPortCtrl[iPortNo].uiEth0CompactBackupIp;
        pGenSharedData->data.xSharedData.uiM0Eth0GateWay = gCommPortCtrl[iPortNo].uiEth0CompactGateWay;
        pGenSharedData->data.xSharedData.uiM0Eth0NetMask = gCommPortCtrl[iPortNo].uiEth0CompactNetMask;
        pGenSharedData->data.xSharedData.uiM0Eth1ActiveIP = gCommPortCtrl[iPortNo].uiEth1CompactActiveIP;
        pGenSharedData->data.xSharedData.uiM0Eth1BackupIP = gCommPortCtrl[iPortNo].uiEth1CompactBackupIp;
        pGenSharedData->data.xSharedData.uiM0Eth1GateWay = gCommPortCtrl[iPortNo].uiEth1CompactGateWay;
        pGenSharedData->data.xSharedData.uiM0Eth1NetMask = gCommPortCtrl[iPortNo].uiEth1CompactNetMask;
    }
    else if(iPortNo == UART_PORT_1)
    {
        pGenSharedData->data.xSharedData.uiM1Eth0ActiveIP = gCommPortCtrl[iPortNo].uiEth0CompactActiveIP;
        pGenSharedData->data.xSharedData.uiM1Eth0BackupIP = gCommPortCtrl[iPortNo].uiEth0CompactBackupIp;
        pGenSharedData->data.xSharedData.uiM1Eth0GateWay = gCommPortCtrl[iPortNo].uiEth0CompactGateWay;
        pGenSharedData->data.xSharedData.uiM1Eth0NetMask = gCommPortCtrl[iPortNo].uiEth0CompactNetMask;
        pGenSharedData->data.xSharedData.uiM1Eth1ActiveIP = gCommPortCtrl[iPortNo].uiEth1CompactActiveIP;
        pGenSharedData->data.xSharedData.uiM1Eth1BackupIP = gCommPortCtrl[iPortNo].uiEth1CompactBackupIp;
        pGenSharedData->data.xSharedData.uiM1Eth1GateWay = gCommPortCtrl[iPortNo].uiEth1CompactGateWay;
        pGenSharedData->data.xSharedData.uiM1Eth1NetMask = gCommPortCtrl[iPortNo].uiEth1CompactNetMask;
    }
    if(((iPortNo == UART_PORT_0) && gCommPortCtrl[UART_PORT_1].iModuleCfgUpdate) ||
        ((iPortNo == UART_PORT_1) && gCommPortCtrl[UART_PORT_0].iModuleCfgUpdate))        
    {
        if((gCommPortCtrl[UART_PORT_0].uiHashVal != gCommPortCtrl[UART_PORT_1].uiHashVal) ||
        (gCommPortCtrl[UART_PORT_0].uiEth0CompactActiveIP != gCommPortCtrl[UART_PORT_1].uiEth0CompactActiveIP) || 
        (gCommPortCtrl[UART_PORT_0].uiEth0CompactBackupIp != gCommPortCtrl[UART_PORT_1].uiEth0CompactBackupIp) || 
        (gCommPortCtrl[UART_PORT_0].uiEth0CompactGateWay != gCommPortCtrl[UART_PORT_1].uiEth0CompactGateWay) || 
        (gCommPortCtrl[UART_PORT_0].uiEth0CompactNetMask != gCommPortCtrl[UART_PORT_1].uiEth0CompactNetMask) ||
        (gCommPortCtrl[UART_PORT_0].uiEth1CompactActiveIP != gCommPortCtrl[UART_PORT_1].uiEth1CompactActiveIP) || 
        (gCommPortCtrl[UART_PORT_0].uiEth1CompactBackupIp != gCommPortCtrl[UART_PORT_1].uiEth1CompactBackupIp) || 
        (gCommPortCtrl[UART_PORT_0].uiEth1CompactGateWay != gCommPortCtrl[UART_PORT_1].uiEth1CompactGateWay) || 
        (gCommPortCtrl[UART_PORT_0].uiEth1CompactNetMask != gCommPortCtrl[UART_PORT_1].uiEth1CompactNetMask))
        {
            if(gCommPortCtrl[UART_PORT_0].uiHashVal > gCommPortCtrl[UART_PORT_1].uiHashVal)
            {
                pIPUpdateReq = (xUpdateModuleIPReq *)pCommSendBuffer;
                pIPUpdateReq->uiEth0CompactActiveIP = gCommPortCtrl[UART_PORT_0].uiEth0CompactActiveIP;
                pIPUpdateReq->uiEth0CompactBackupIp = gCommPortCtrl[UART_PORT_0].uiEth0CompactBackupIp;
                pIPUpdateReq->uiEth0CompactGateWay = gCommPortCtrl[UART_PORT_0].uiEth0CompactGateWay;
                pIPUpdateReq->uiEth0CompactNetMask = gCommPortCtrl[UART_PORT_0].uiEth0CompactNetMask;
                pIPUpdateReq->uiEth1CompactActiveIP = gCommPortCtrl[UART_PORT_0].uiEth1CompactActiveIP;
                pIPUpdateReq->uiEth1CompactBackupIp = gCommPortCtrl[UART_PORT_0].uiEth1CompactBackupIp;
                pIPUpdateReq->uiEth1CompactGateWay = gCommPortCtrl[UART_PORT_0].uiEth1CompactGateWay;
                pIPUpdateReq->uiEth1CompactNetMask = gCommPortCtrl[UART_PORT_0].uiEth1CompactNetMask;
                pIPUpdateReq->xHead.usFrameType = UPDATE_MODULE_IP_REQ;
                iFrameLen = sizeof(xUpdateModuleIPReq);
                pIPUpdateReq->xHead.usFrameLen = iFrameLen + CRC16_SIZE;
                usCRC16 = crc16(pCommSendBuffer,iFrameLen);
                pCommSendBuffer[iFrameLen++] = (unsigned char)(usCRC16&0xFF);
                pCommSendBuffer[iFrameLen++] = (unsigned char)(usCRC16>>8);
                comm_build_send_buf(pCommSendBuffer,iFrameLen,SEND_ON_PORT_2);
                #if COMMNUCATION_DEBUG
                printf("sync module 0 config to module 1\n");
                #endif
            }
            else if(gCommPortCtrl[UART_PORT_1].uiHashVal > gCommPortCtrl[UART_PORT_0].uiHashVal)
            {
                pIPUpdateReq = (xUpdateModuleIPReq *)pCommSendBuffer;
                pIPUpdateReq->uiEth0CompactActiveIP = gCommPortCtrl[UART_PORT_1].uiEth0CompactActiveIP;
                pIPUpdateReq->uiEth0CompactBackupIp = gCommPortCtrl[UART_PORT_1].uiEth0CompactBackupIp;
                pIPUpdateReq->uiEth0CompactGateWay = gCommPortCtrl[UART_PORT_1].uiEth0CompactGateWay;
                pIPUpdateReq->uiEth0CompactNetMask = gCommPortCtrl[UART_PORT_1].uiEth0CompactNetMask;
                pIPUpdateReq->uiEth1CompactActiveIP = gCommPortCtrl[UART_PORT_1].uiEth1CompactActiveIP;
                pIPUpdateReq->uiEth1CompactBackupIp = gCommPortCtrl[UART_PORT_1].uiEth1CompactBackupIp;
                pIPUpdateReq->uiEth1CompactGateWay = gCommPortCtrl[UART_PORT_1].uiEth1CompactGateWay;
                pIPUpdateReq->uiEth1CompactNetMask = gCommPortCtrl[UART_PORT_1].uiEth1CompactNetMask;
                pIPUpdateReq->xHead.usFrameType = UPDATE_MODULE_IP_REQ;
                iFrameLen = sizeof(xUpdateModuleIPReq);
                pIPUpdateReq->xHead.usFrameLen = iFrameLen + CRC16_SIZE;
                usCRC16 = crc16(pCommSendBuffer,iFrameLen);
                pCommSendBuffer[iFrameLen++] = (unsigned char)(usCRC16&0xFF);
                pCommSendBuffer[iFrameLen++] = (unsigned char)(usCRC16>>8);
                comm_build_send_buf(pCommSendBuffer,iFrameLen,SEND_ON_PORT_1);
                #if COMMNUCATION_DEBUG
                printf("sync module 1 config to module 0\n");
                #endif
            }
        }
    }
    return 0;
}

static int frame_received(int iPortNo)
{
    unsigned char *pucFrame;
    int iFrameLen;
    xMC2HMIFrameHead *pFrameHead;
    pucFrame = gCommPortCtrl[iPortNo].pRecvBuf;
    iFrameLen = gCommPortCtrl[iPortNo].iBytesReceived;
    pFrameHead = (xMC2HMIFrameHead *)pucFrame;    
#if COMMNUCATION_DEBUG
    {
        int offset = 0;
        int debug_offset;
        sprintf(acFrameDBGBuf,"Rx:");
        debug_offset = strlen(acFrameDBGBuf);
        while(offset < iFrameLen)
        {   
            acFrameDBGBuf[debug_offset++] = acDebugHexValue[(pucFrame[offset]&0xF0)>>4];
            acFrameDBGBuf[debug_offset++] = acDebugHexValue[pucFrame[offset++]&0x0F];
            acFrameDBGBuf[debug_offset++] = ' ';
#if 0
            if(offset > 16)
            {
                acFrameDBGBuf[debug_offset++] = '.';
                acFrameDBGBuf[debug_offset++] = '.';
                acFrameDBGBuf[debug_offset++] = '.';
                break;
            }
#endif
        }
        acFrameDBGBuf[debug_offset] = 0x00;
        printf("%s\n",acFrameDBGBuf);
    }
#endif
    if(!crc16(pucFrame,pFrameHead->usFrameLen))//frame is ok
    {        
        pucFrame[pFrameHead->usFrameLen-1] = 0;
        pucFrame[pFrameHead->usFrameLen-2] = 0;
        switch(pFrameHead->usFrameType)
        {
            case SYNC_MODULE_IP_RES:
                sync_module_ip_res(iPortNo,pucFrame);
                break;

            case UPDATE_MODULE_IP_RES:
                break;

            case REG_WORD_READ_RES:
                reg_word_read_res(pucFrame);
                break;

            case REG_WORD_WRITE_RES:
                reg_word_write_res(pucFrame);
                break;

            case HMI_FILE_PUT_REQ:
                file_put_req(iPortNo,pucFrame);
                break;

            case HMI_FILE_WRITE_REQ:
                file_write_req(iPortNo,pucFrame);
                break;

            case HMI_FILE_GET_REQ:
                file_get_req(iPortNo,pucFrame);
                break;

            case HMI_FILE_READ_REQ:
                file_read_req(iPortNo,pucFrame);
                break;

            case HMI_BOARD_REBOOT_REQ:
                hmi_board_reboot(iPortNo);
                break;

            default:
                usleep(50000);//50ms
                #if COMMNUCATION_DEBUG
                printf("unknown frame type.\n");
                #endif
                comm_port_open(iPortNo);
                return -1;
                break;
        }
    }
    else//frame corrupt
    {
#if 0
        usleep(50000);//50ms
        printf("frame corrupted.\n");
        comm_port_open(iPortNo);
        return -1;
#else
        switch(pFrameHead->usFrameType)
        {
            case HMI_FILE_WRITE_REQ:
                file_write_fail(iPortNo);
                break;
            default:
                break;
    }
#endif
    }
    gCommPortCtrl[iPortNo].iBytesLeft = sizeof(xMC2HMIFrameHead);
    gCommPortCtrl[iPortNo].iBytesReceived = 0;
    return 0;
    }

static int comm_port_receiving(int iPortNo)
{
    int iBytesRead;
    xMC2HMIFrameHead *pFrameHead;
    iBytesRead = read(gCommPortCtrl[iPortNo].iPortFd,gCommPortCtrl[iPortNo].pRecvBuf + gCommPortCtrl[iPortNo].iBytesReceived,gCommPortCtrl[iPortNo].iBytesLeft);
    gCommPortCtrl[iPortNo].iBytesReceived += iBytesRead;
    if(gCommPortCtrl[iPortNo].iBytesReceived < sizeof(xMC2HMIFrameHead))
    {
        gCommPortCtrl[iPortNo].iBytesLeft = sizeof(xMC2HMIFrameHead) - gCommPortCtrl[iPortNo].iBytesReceived;
        return 0;
    }
    else
    {
        pFrameHead = (xMC2HMIFrameHead *)gCommPortCtrl[iPortNo].pRecvBuf;
        if(pFrameHead->usFrameLen > MAX_FRAME_SIZE)//invalid frame
        {
            usleep(50000);//50ms
            comm_port_open(iPortNo);
            return -1;
        }
        if(pFrameHead->usFrameLen > gCommPortCtrl[iPortNo].iBytesReceived)
        {
            gCommPortCtrl[iPortNo].iBytesLeft = pFrameHead->usFrameLen - gCommPortCtrl[iPortNo].iBytesReceived;
            return 0;
        }
#if 1
        if(pFrameHead->usFrameLen < gCommPortCtrl[iPortNo].iBytesReceived)
        {
            usleep(50000);//50ms
            comm_port_open(iPortNo);
            return -1;
        }
#endif
        //frame received
        return frame_received(iPortNo);
    }
    return 0;
    }

static int comm_send_frame(int iPortNo)
{
    int res;
    if(gCommPortCtrl[iPortNo].xSendBuf.wIndex == gCommPortCtrl[iPortNo].xSendBuf.rIndex)
        return 0;
    #if COMMNUCATION_DEBUG
    printf("comm send frame on port %d\n",iPortNo);
    #endif
    if(gCommPortCtrl[iPortNo].xSendBuf.wIndex > gCommPortCtrl[iPortNo].xSendBuf.rIndex)
    {
        res = write( gCommPortCtrl[iPortNo].iPortFd, 
                            gCommPortCtrl[iPortNo].xSendBuf.aucBuf + gCommPortCtrl[iPortNo].xSendBuf.rIndex,
                            gCommPortCtrl[iPortNo].xSendBuf.wIndex - gCommPortCtrl[iPortNo].xSendBuf.rIndex);
    }
    else
    {
        res = write( gCommPortCtrl[iPortNo].iPortFd, 
                            gCommPortCtrl[iPortNo].xSendBuf.aucBuf + gCommPortCtrl[iPortNo].xSendBuf.rIndex,
                            RING_BUF_SIZE - gCommPortCtrl[iPortNo].xSendBuf.rIndex);
        #if 0
        if((res == RING_BUF_SIZE - gCommPortCtrl[iPortNo].xSendBuf.rIndex) && gCommPortCtrl[iPortNo].xSendBuf.wIndex)
        {
            gCommPortCtrl[iPortNo].xSendBuf.rIndex = 0;
            res = write( gCommPortCtrl[iPortNo].iPortFd, 
                                gCommPortCtrl[iPortNo].xSendBuf.aucBuf,
                                gCommPortCtrl[iPortNo].xSendBuf.wIndex);
        }
        #endif
    }
    if(res == -1)
    {
        if( errno != EINTR )
        {
            comm_port_open(iPortNo);
            return -1;
        }
        return -1;
    }
    gCommPortCtrl[iPortNo].xSendBuf.rIndex += res;
    gCommPortCtrl[iPortNo].xSendBuf.rIndex &= ~RING_BUF_SIZE;
    return 0;
}


static void *communication_monitor( void *pvParameter)
{
    int iPortNo;
    long ulDeltaMS;
    int iMaxFd = -1;
    struct timeval tval;
    //open communication port
    comm_port_open(0);
    comm_port_open(1);
    do{
        iMaxFd = -1;
        if(gettimeofday(&xGTimeStamp,NULL) < 0)
        {
            usleep(2000);
            continue;
        }
        FD_ZERO( &commPortFdsr );
        FD_ZERO( &commPortFdsw );
        tval.tv_sec = 1;
        tval.tv_usec = 50000;//50ms
        for(iPortNo = UART_PORT_0; iPortNo < MAX_COMM_PORT; iPortNo++)
        {
            if(xGTimeStamp.tv_usec<gCommPortCtrl[iPortNo].xTimeStamp.tv_usec)
            {      
                ulDeltaMS = ((xGTimeStamp.tv_sec-gCommPortCtrl[iPortNo].xTimeStamp.tv_sec)<<10) - 
                ((gCommPortCtrl[iPortNo].xTimeStamp.tv_usec-xGTimeStamp.tv_usec)>>10);
            }    
            else
            {      
                ulDeltaMS = ((xGTimeStamp.tv_sec-gCommPortCtrl[iPortNo].xTimeStamp.tv_sec)<<10) + 
                                   ((xGTimeStamp.tv_usec-gCommPortCtrl[iPortNo].xTimeStamp.tv_usec)>>10);
            }
            if(ulDeltaMS > SYNC_MODULE_IP_PERIOD)
            {
                sync_module_ip(iPortNo);
                gCommPortCtrl[iPortNo].xTimeStamp.tv_sec = xGTimeStamp.tv_sec;
                gCommPortCtrl[iPortNo].xTimeStamp.tv_usec = xGTimeStamp.tv_usec;
            }
            if(gCommPortCtrl[iPortNo].iPortFd != -1)
            {
                if(iMaxFd < gCommPortCtrl[iPortNo].iPortFd)
                    iMaxFd = gCommPortCtrl[iPortNo].iPortFd;
                FD_SET( gCommPortCtrl[iPortNo].iPortFd, &commPortFdsr);
                if(gCommPortCtrl[iPortNo].xSendBuf.rIndex != gCommPortCtrl[iPortNo].xSendBuf.wIndex)
                {
                    FD_SET( gCommPortCtrl[iPortNo].iPortFd, &commPortFdsw);    
                }
            }
        }
        if(iMaxFd < 0)
        {
            usleep(100000);//100ms 
            continue;
        }
        select(iMaxFd + 1, &commPortFdsr, &commPortFdsw, NULL,&tval);
        for(iPortNo = UART_PORT_0; iPortNo < MAX_COMM_PORT; iPortNo++)
        {
            if(gCommPortCtrl[iPortNo].iPortFd != -1)
            {
                if(FD_ISSET(gCommPortCtrl[iPortNo].iPortFd,&commPortFdsr))
                {
                    if(comm_port_receiving(iPortNo) < 0)
                    continue;
                }
                if(gCommPortCtrl[iPortNo].xSendBuf.rIndex != gCommPortCtrl[iPortNo].xSendBuf.wIndex)
                {
                    if(FD_ISSET(gCommPortCtrl[iPortNo].iPortFd,&commPortFdsw))
                    {
                        if(comm_send_frame(iPortNo) < 0)
                        continue;
                    }
                }
            }
        }
    }while(1);
    return 0;
}

static void init_communication(void)
{
    int index;
    pthread_t xThread;
    for(index = UART_PORT_0; index < MAX_COMM_PORT; index++)
    {
        gCommPortCtrl[index].iPortFd = -1;
        gCommPortCtrl[index].iBytesLeft = sizeof(xMC2HMIFrameHead);
        gCommPortCtrl[index].iBytesReceived = 0;
        gCommPortCtrl[index].uiHashVal = 0;
        gCommPortCtrl[index].uiEth0CompactActiveIP = 0;
        gCommPortCtrl[index].uiEth0CompactBackupIp = 0;
        gCommPortCtrl[index].uiEth0CompactGateWay = 0;
        gCommPortCtrl[index].uiEth0CompactNetMask = 0;
        gCommPortCtrl[index].uiEth1CompactActiveIP = 0;
        gCommPortCtrl[index].uiEth1CompactBackupIp = 0;
        gCommPortCtrl[index].uiEth1CompactGateWay = 0;
        gCommPortCtrl[index].uiEth1CompactNetMask = 0;
        gCommPortCtrl[index].iModuleCfgUpdate = 0;
        gCommPortCtrl[index].pRecvBuf = (unsigned char *)malloc(2048);
        if(gCommPortCtrl[index].pRecvBuf == NULL)
        {
            printf("Can not alloc memory for communication port,system exit.\n");
            exit(1);
        }    
        gCommPortCtrl[index].xSendBuf.rIndex = gCommPortCtrl[index].xSendBuf.wIndex = 0;
        gCommPortCtrl[index].xSendBuf.aucBuf = (unsigned char *)malloc(RING_BUF_SIZE);
        if(gCommPortCtrl[index].xSendBuf.aucBuf == NULL)
        {
            printf("ring buffer create failed,system exit.\n");
            exit(1);
        }
        gettimeofday(&gCommPortCtrl[index].xTimeStamp,NULL);
    }
    gettimeofday(&xMMITimeStamp,NULL);
    xGTimeStamp.tv_sec = xMMITimeStamp.tv_sec;
    xGTimeStamp.tv_usec = xMMITimeStamp.tv_usec;
    for(index = 0; index < VALVE_MAX_GROUP; index++)
    {
        xCommTimeStamp.xGroupTimeStamp[index].tv_sec = 0;
        xCommTimeStamp.xGroupTimeStamp[index].tv_usec = 0;
    }
    for(index = 0; index < REAL_DB_SIZE; index++)
    {
        xCommTimeStamp.xDataBaseTimeStamp[index].tv_sec = 0;
        xCommTimeStamp.xDataBaseTimeStamp[index].tv_usec = 0;
    }
    pCommSendBuffer = (unsigned char *)malloc(MAX_FRAME_SIZE);
    if(pCommSendBuffer == NULL)
        return;
    pthread_create( &xThread, NULL, communication_monitor, NULL );
}

#if 1
static void run_app(void)
{
    char usr_file_name[64];
    struct dirent *ptr;   
    FILE *fd;
    DIR *dir;
    int file_len = 0;
    int pid_daemon;
    dir=opendir("/disk");
    while((ptr=readdir(dir))!=NULL)
    {
        bzero(usr_file_name,sizeof(usr_file_name));
        if(ptr->d_name[0] == '.')
            continue;
        sprintf(usr_file_name,"/disk/%s",ptr->d_name);
        if(strstr(usr_file_name,APP_EXT) == NULL)
            continue;
        fd = fopen(usr_file_name,"rb");
        file_len = 0;
        if(fd != NULL)
        {
            fseek(fd,0,SEEK_END);
            file_len = ftell(fd);
            fclose(fd);
        }
        if(file_len < 100)
        continue;        
        pid_daemon=fork();
        if(pid_daemon==0)//child process
        {
            execl(usr_file_name,ptr->d_name,NULL);
            exit(0);
        }
        else if(pid_daemon<0)//fork fail
        {
            exit(1);
        }
    }
}
#else

static void run_app(void)
{
    int pid_daemon;
    pid_daemon=fork();
    if(pid_daemon==0)//child process
    {
        execl("/usr/app/vc_hmi.app","VC_HMI",NULL);
        exit(0);
    }
    else if(pid_daemon<0)//fork fail
    {
        exit(1);
    }
}
#endif

int main( int argc , char *argv[] )
{
    int pid;
    key_t key;
    int shm_id;
    int local_msg_q;
    xCommMessage xMsg;


    pid=fork();
    if(pid>0) //parent process
    {
        exit(0);
    }
    else if(pid< 0)//fork fail
    {
        exit(1);
    }
    setsid();
    chdir("/");
    umask(0);
    //for(i=0;i<65535;i++)
    //    close(i);
    local_msg_q = get_message_queue(IPC_KEY_NAME,COMM_MQ_KEY_INDEX);
    if(local_msg_q<0)
    {
        printf("can't get message queue.\n");
        exit(1);
    }
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
    
    xFileOperationInit();
    init_communication();
    run_app();
    do{
        if(msgrcv(local_msg_q,&xMsg,sizeof(xCommMessage)-sizeof(int),0,0)<0){
            printf("message error.\n");
            continue;
            //exit(1);
        }
        #if COMMNUCATION_DEBUG
        printf("message received,type = %d.\n",xMsg.iMsgType);
        #endif
        switch(xMsg.iMsgType)
        {
            case REG_WRITE_MSG:
                reg_write_req(xMsg.uMsgContent.xRegWr.iOffset,xMsg.uMsgContent.xRegWr.iValue);
                break;
            case REG_READ_MSG:
                reg_read_req(xMsg.uMsgContent.xRegRd.iOffset,xMsg.uMsgContent.xRegRd.iNum);
                break;
        }
    }while(1);
}
