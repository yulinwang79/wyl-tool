#ifndef __COM_DATASTRUCT_H__
#define __COM_DATASTRUCT_H__
typedef struct {
       unsigned int uiM0Eth0ActiveIP;
       unsigned int uiM0Eth0BackupIP;
       unsigned int uiM0Eth0GateWay;
       unsigned int uiM0Eth0NetMask;
       unsigned int uiM0Eth1ActiveIP;
       unsigned int uiM0Eth1BackupIP;
       unsigned int uiM0Eth1GateWay;
       unsigned int uiM0Eth1tNetMask;
       unsigned int uiM1Eth0ActiveIP;
       unsigned int uiM1Eth0BackupIP;
       unsigned int uiM1Eth0GateWay;
       unsigned int uiM1Eth0NetMask;
       unsigned int uiM1Eth1ActiveIP;
       unsigned int uiM1Eth1BackupIP;
       unsigned int uiM1Eth1GateWay;
       unsigned int uiM1Eth1tNetMask;
}xSharedDataMap;

typedef struct {
    union{
        unsigned int uiMemorySize[1024];
        xSharedDataMap xSharedData;
    }data;
}xGeneralSharedDataMap;
#endif
