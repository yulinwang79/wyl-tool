#ifndef __COMMUNICATION_H_
#define __COMMUNICATION_H_
#include "realdb_operation.h"

typedef enum {
	MODULE_RUNNING = 	0x0001,
	MODULE_BACKUP = 	0x0002,
	MODULE_BAD = 	   	0x0004,
}eModuleStatus;


#define MB_DEV_STATUS_MASK	0x00FF
#define MB_DEV_PORT_MASK	0xFF00
enum mb_slave_dev_status {
	S_DEV_INACTIVE =	0x0000,//inactive device
	S_DEV_POLLING =	0x0001,//the slave is actively being polled,and the communication is ok
	S_DEV_SUSPEND =	0x0002,//failed to communicate with the slave,suspend for a user defined period
};
enum mb_slave_on_port {
	S_ON_NONE =		0x0000,//slave is NOT connected to any port of the loop circuit
	S_ON_PORT_A =		0x1000,//slave is on port A of a loop circuit
	S_ON_PORT_B =		0x0100,//slave is on port B of a loop circuit
	S_ON_PORT_AB =	0x1100,//the loop circuit is good
};

void InitCommunication(void);

int GetGroupValveStatus(eValveGroup group_no);
int GetValveCurPos(unsigned int dbOffset);
int GetValveOpenStatus(unsigned int dbOffset);
int GetValveCloseStatus(unsigned int dbOffset);
int GetValveLRStatus(unsigned int dbOffset);
int OpenValve(unsigned short dbOffset,unsigned short cmd);
int StopValve(unsigned short dbOffset,unsigned short cmd);
int CloseValve(unsigned short dbOffset,unsigned short cmd);
int SetValveCurPos(unsigned short dbCmdOffset,unsigned short cmd,unsigned short dbCurPosOffset,unsigned short curPos);
#endif

