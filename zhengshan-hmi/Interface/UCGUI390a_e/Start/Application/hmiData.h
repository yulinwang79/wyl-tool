#ifndef _HMI_DATA_H_
#define  _HMI_DATA_H_

#ifdef  WIN32
#define HMI_DATA_FILE  "hmidata.dat"
#else
#define HMI_DATA_FILE  "/disk/hmidata.dat"
#endif

#define GROUPS_NUM 4
#define GROUP_VALVE_NUM           40

#define PASSWORD_MAX_LENTH 6
#define TEXT_MAX_LENTH  30
#define VALVE_NAME_MAX_LENTH 15
#define VALVE_ADDR_MAX_LENTH 6
#define VALVE_CMD_MAX_LENTH 6

typedef enum {
	VALVE_NOT_EXIST = 0,
	VALVE_GOOD_ON_PORT_A,
	VALVE_GOOD_ON_PORT_B,
	VALVE_GOOD_ON_PORT_AB,
	VALVE_SUSPEND
}eValveStatus;

typedef enum {
	VALVE_GROUP_1 = 0,
	VALVE_GROUP_2,
	VALVE_GROUP_3,
	VALVE_GROUP_4,
	VALVE_MAX_GROUP
}eValveGroup;


typedef struct {
	unsigned short dBOffsetForOpenCmd;//offset in the database
	unsigned short openCmd;
	unsigned short dBOffsetForCloseCmd;//offset in the databse
	unsigned short closeCmd;
	unsigned short dBoffsetForStopCmd;//offset in the databse
	unsigned short stopCmd;
	unsigned short dBOffsetForCurPosCmd;//offset in the databse
	unsigned short getCurPosCmd;
	unsigned short dbOffsetForCurPos;//offset in the database
	unsigned short curPosMaxVal;
}xValveCtrlCmd;

typedef struct {
	unsigned int dBOffsetForOpenVal;
	unsigned int dBOffsetForCloseVal;
	unsigned int dBOffsetForLRVal;
	unsigned int dBOffsetForCurPos;
	unsigned int curPosMaxVal;
}xValveView;


typedef struct {
	char acValveName[VALVE_NAME_MAX_LENTH+1];
	xValveCtrlCmd xCmd;
	xValveView xView;
}xValveData;

typedef struct {
	xValveData xValve[GROUP_VALVE_NUM];
}xValveGroup;

typedef struct {
        char password[PASSWORD_MAX_LENTH+1];
	xValveGroup xGroup[GROUPS_NUM];
}xHmiData;


int HmiDataSave(void);
int HmiDataInit(void);
int HmiLogin(char * password);
int HmiLoginAndChange(char * password,char * new_password);
int HmiLogOut(void);

int hmi_is_login(void);
void hmi_set_curr_group(int group_id);
int hmi_get_curr_group(void);
int hmi_group_goto_prev();
int hmi_group_goto_next();
void hmi_reset_curr_valve(void);
int hmi_is_exist_valve(int group_id);
void hmi_get_curr_valve(void);
int hmi_valve_goto_prev();
int hmi_valve_goto_next();
int hmi_get_curr_valve_open_status(void);
int hmi_get_curr_valve_close_status(void);
int hmi_get_curr_valve_LR_status(void);
int hmi_get_curr_valve_pos_value(void);
int hmi_open_curr_valve(void);
int hmi_stop_curr_valve(void);
int hmi_close_curr_valve(void);
int hmi_set_cur_pos_curr_valve(int value);
int hmi_get_curr_valve_data(xValveData* pData);
int hmi_set_curr_valve_data(xValveData* pData);
void hmi_get_group_num_text(char *lable);
void hmi_get_valve_num_text(char *lable);
void hmi_get_valve_name(char *lable);

#endif /*_HMI_DATA_H_*/
