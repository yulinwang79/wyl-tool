#ifndef __REALDB_OPERATION_H__
#define __REALDB_OPERATION_H__


#define MAX_CMD_PER_PORT	200	//define the max command number per master port
#define MAX_DEV_PER_PORT	200	//define the max slave device per master port


typedef enum {
	MB_MODULE_A = 0,
	MB_MODULE_B,
	MB_MODULE_MAX,
	MB_INVALID_MODULE,
}eMBModlueName;

typedef enum {
	VALVE_GROUP_1 = 0,
	VALVE_GROUP_2,
	VALVE_GROUP_3,
	VALVE_GROUP_4,
	VALVE_MAX_GROUP
}eValveGroup;

typedef enum {
    MB_RING_NONE = 0,
    MB_RING_1,
    MB_RING_2,
    MB_RING_3,
    MB_RING_4,
    MB_RING_5,
    MB_RING_MAX,// = MB_RING_5,
}eModuleRing;

typedef enum {
	UART_PORT_G1A = 0,
	UART_PORT_G1B,
	UART_PORT_G2A,
	UART_PORT_G2B,
	UART_PORT_G3A,
	UART_PORT_G3B,
	UART_PORT_G4A,
	UART_PORT_G4B,
	UART_PORT_PC0,
	UART_PORT_PC1,
	UART_PORT_MAX,	
}eModuleUartPort;

typedef enum {
	ETHER_PORT_0 = 0,
	ETHER_PORT_1,
	ETHER_PORT_MAX,
}eModuleTCPPort;

typedef enum
{
    REG_READ,                
    REG_WRITE                
} eDBRegisterMode;
#pragma pack(2)
typedef struct {
	/*error in commands list*/
	unsigned short cmd_list_err[MAX_CMD_PER_PORT];//refer to: mb_cmd_list_err
	/*communication error for each command in command list*/
	unsigned short cmd_comm_err[MAX_CMD_PER_PORT]; //refer to:mb_cmd_comm_err
	unsigned short slave_dev_status[MAX_DEV_PER_PORT];//composed by mb_slave_dev_status and mb_slave_on_port,mb_slave_on_port on high byte,mb_slave_dev_status on low byte.
}xPortStatus;

#define REAL_DB_SIZE 20000

/*the byte order of the database is little-endian,MODBUS send frame in big-endian format*/
typedef struct {
	/**************database section********************/
	unsigned short real_db[REAL_DB_SIZE];
	/*************status and control data section**********/
	unsigned short sync_status;//refer to eModSyncStatus
	unsigned short module_status[MB_MODULE_MAX];//refer to eModuleStatus
	unsigned short ring_status[MB_RING_MAX];
	unsigned short uart_port_status[UART_PORT_MAX];
	unsigned short ether_port_status[ETHER_PORT_MAX];
	unsigned short dummy_value[100-UART_PORT_MAX-ETHER_PORT_MAX-MB_RING_MAX-MB_MODULE_MAX-1];
	xPortStatus eth_0_status;
	xPortStatus eth_1_status;
	xPortStatus pc_0_status;
	xPortStatus pc_1_status;
	xPortStatus g1a_status;
	xPortStatus g1b_status;
	xPortStatus g2a_status;
	xPortStatus g2b_status;
	xPortStatus g3a_status;
	xPortStatus g3b_status;
	xPortStatus g4a_status;
	xPortStatus g4b_status;
}xRealTimeDBMap;
#pragma pack()

#define OFFSET(s,m)   ((size_t)&(((s *)0)->m))
#define SLAVE_DEV_STATUS_OFFSET(s,m)	(((size_t)&(((s *)0)->m.slave_dev_status))/(sizeof(unsigned short)))

void eMBRegHoldingCB( unsigned char * pucRegBuffer, unsigned int usAddress, unsigned int usNRegs, eDBRegisterMode eMode );
void eMBRegCoilsCB( unsigned char * pucRegBuffer, unsigned int usAddress, unsigned int usNCoils, eDBRegisterMode eMode );

#endif
