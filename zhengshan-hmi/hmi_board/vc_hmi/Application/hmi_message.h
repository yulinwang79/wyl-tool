#ifndef __HMI_MESSAGE_H__
#define __HMI_MESSAGE_H__
#include "common.h"

typedef enum{
    TOUCH_NONE,
	TOUCH_PRESS,
    TOUCH_RELEASE,
    TOUCH_SLIDE,    
}eTouchEvent;

typedef enum {
	TOUCH_EVENT,
	MMI_TICK,
	GET_GROUP_VALVE_STATUS,
	GET_VALVE_CUR_POS,
	GET_VALVE_OPEN_STATUS,
	GET_VALVE_CLOSE_STATUS,
	GET_VALVE_LR_STATUS,
	SET_VALVE_OPEN,
	SET_VALVE_STOP,
	SET_VALVE_CLOSE,
	SET_VALVE_CUR_POS,
}eHMIMessageType;

typedef struct {
	int msgType;
	xPoint touchPoint;
	eTouchEvent touchEvent;
}xTP_Msg;

#endif
