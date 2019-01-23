#ifndef __HMI_COMMUNICATION_H__
#define __HMI_COMMUNICATION_H__
#define COMMUNICATION_DELAY	1000 /*ms*/
#define SYNC_MODULE_IP_PERIOD	8000 /*ms*/

#define CONFIG_CMD_FRAME_END ";\n"
#define CONFIG_CMD_FRAME_SEPERATOR ","
#define FILE_LOCATION_HMI   "/hmi"
#define FILE_LOCATION_CORE  "/core"

#define CRC16_SIZE	2
#define MAX_FRAME_SIZE 4096
#define MAX_REG_NUM 100
#define MAX_MSG_LEN 512

typedef enum {
    SYNC_MODULE_IP_REQ =    0x0001,
    SYNC_MODULE_IP_RES =    0x0002,
    UPDATE_MODULE_IP_REQ =  0x0003,
    UPDATE_MODULE_IP_RES =  0x0004,
    REG_WORD_READ_REQ =     0x0005,
    REG_WORD_READ_RES =     0x0006,
    REG_WORD_WRITE_REQ =    0x0007,
    REG_WORD_WRITE_RES =    0x0008,
    HMI_FILE_PUT_REQ =      0x0009,
    HMI_FILE_PUT_RES =      0x000A,
    HMI_FILE_WRITE_REQ =    0x000B,
    HMI_FILE_WRITE_RES =    0x000C,
    HMI_FILE_GET_REQ =      0x000D,
    HMI_FILE_GET_RES =      0x000E,
    HMI_FILE_READ_REQ =     0x000F,
    HMI_FILE_READ_RES =     0x0010,
    HMI_BOARD_REBOOT_REQ =  0x0011,
    HMI_BOARD_REBOOT_RES =  0x0012,
}eFrameType;

#pragma pack(2)

typedef struct {
    unsigned short usFrameType;
    unsigned short usFrameLen;
}xMC2HMIFrameHead;

/*Reg Word Read */
typedef struct {
    xMC2HMIFrameHead xHead;
    unsigned short usDbOffset;
    unsigned short usRegNum;
}xVCRegWordReadReq;

typedef struct {
    xMC2HMIFrameHead xHead;
    unsigned short usDbOffset;
    unsigned short usRegNum;
    unsigned short usValue[MAX_REG_NUM];
}xVCRegWordReadRes;

/*Reg Word Write */
typedef struct {
    xMC2HMIFrameHead xHead;
    unsigned short usDbOffset;
    unsigned short usValue;
}xVCRegWordWriteReq;

typedef struct {
    xMC2HMIFrameHead xHead;
    unsigned short usDbOffset;
    unsigned short usValue;
}xVCRegWordWriteRes;

/*sync module ip*/
typedef struct {
    xMC2HMIFrameHead xHead;
}xSyncModuleIPReq;

typedef struct {
    xMC2HMIFrameHead xHead;
    unsigned int uiHashVal;
    unsigned int uiEth0CompactActiveIP;
    unsigned int uiEth0CompactBackupIp;
    unsigned int uiEth0CompactGateWay;
    unsigned int uiEth0CompactNetMask;
    unsigned int uiEth1CompactActiveIP;
    unsigned int uiEth1CompactBackupIp;
    unsigned int uiEth1CompactGateWay;
    unsigned int uiEth1CompactNetMask;
}xSyncModuleIPRes;

/*update module ip*/
typedef struct {
    xMC2HMIFrameHead xHead;
    unsigned int uiEth0CompactActiveIP;
    unsigned int uiEth0CompactBackupIp;
    unsigned int uiEth0CompactGateWay;
    unsigned int uiEth0CompactNetMask;
    unsigned int uiEth1CompactActiveIP;
    unsigned int uiEth1CompactBackupIp;
    unsigned int uiEth1CompactGateWay;
    unsigned int uiEth1CompactNetMask;
}xUpdateModuleIPReq;

typedef struct {
    xMC2HMIFrameHead xHead;
    unsigned short usStatus;
}xUpdateModuleIPRes;

/*file transfer*/
typedef struct {
    xMC2HMIFrameHead xHead;
    char csFrame[MAX_MSG_LEN];
}xFTPFrame;

#pragma pack()

#endif
