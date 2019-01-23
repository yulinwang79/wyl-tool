#include <unistd.h> 
#include <stdio.h>
#include <stdlib.h>
#include <fcntl.h>
#include <string.h> 
#include <unistd.h>
#include <sys/types.h>
#include <sys/time.h>
#include <sys/select.h>
#include <sys/ipc.h>
#include <sys/msg.h>
#include <sys/prctl.h>
#include <pthread.h>
#include <linux/input.h>
#include <GUIType.h>
#include <GUI.h>
#include "common.h"
#include "hmiData.h"
#include "communication.h"
#include "hmi_message.h"

static int gLcdBackLightStatus = 0;

#define INVALID_HEAER_PACKET  	4
#define INVALID_TAIL_PACKET		6
#define TOUCH_DEBOUNCE  3
#define TP_ADC_THRESHOLD   100
#define TP_POINT_BUF_SIZE	32

typedef enum {
	TP_IDLE = 0,
	TP_X_OK,
}eTouchPanelState;

typedef enum
{
	LCD_BACKLIGHT = 0,
	LED_INVALID,
}LED_INDEX;

typedef struct {
	xPoint touch_point[TP_POINT_BUF_SIZE];
	xPoint start;
	int point_num;
	int cur_pos;
	int invalid_point;
}xTouchControl;


typedef struct {
	struct timeval time;
	unsigned short type;
	unsigned short code;
	unsigned int value;
}xInputEvent;

static GUI_PID_STATE pid_state;
static eTouchPanelState tp_state;
static fd_set tp_fdsr;

static xTouchControl xTPCtrl;

extern int touchCaliFlag;
extern int gLcdBackLightStatus;

static void touch_ctrl_reset(void)
{
    xTPCtrl.point_num = 0;
    xTPCtrl.cur_pos = 0;
    xTPCtrl.start.x = -1;
    xTPCtrl.start.y = -1;
    xTPCtrl.invalid_point = INVALID_HEAER_PACKET;
    tp_state = TP_IDLE;
}


void LCDBackLight(int on_off)
{
    int fd;
    fd = open("/dev/atmel_gpio",O_RDONLY); 
    if(on_off)
    {
        ioctl(fd,1,LCD_BACKLIGHT);
        gLcdBackLightStatus = 1;
    }
    else
    {
        ioctl(fd,0,LCD_BACKLIGHT);
        gLcdBackLightStatus = 0;
    }
    close(fd);
}

void *TouchPanelMonitor( void *pvParameter )
{
    int tp_fd;
    int count;
    struct timeval tval;
    //xTP_Msg msg;
    //int count;
    int offset;
    int backlight_timeout = 0;
    xInputEvent tp_data;
    LCDBackLight(1);
    touch_ctrl_reset();
    tp_fd = open("/dev/event0", O_RDONLY);
    printf("Touch Panel Monitor Start!\n");
    do{
        FD_ZERO( &tp_fdsr );
        FD_SET( tp_fd, &tp_fdsr);
        tval.tv_sec = 1;
        tval.tv_usec = 0;
        select(tp_fd + 1, &tp_fdsr, NULL, NULL,&tval);
        if(FD_ISSET(tp_fd, &tp_fdsr))
        {
            count = read(tp_fd,&tp_data,sizeof(tp_data));
            switch(tp_data.type)
            {
                case EV_ABS:
                    if(tp_data.code == ABS_X)
                    {
                        tp_state = TP_X_OK;
                        xTPCtrl.touch_point[xTPCtrl.cur_pos].x = tp_data.value;
                        break;
                    }
                    else if(tp_data.code == ABS_Y)
                    {
                        if(tp_state == TP_X_OK)
                        {
                            xTPCtrl.touch_point[xTPCtrl.cur_pos].y = tp_data.value;
                            if(xTPCtrl.invalid_point > 0)
                            {
                                xTPCtrl.invalid_point--;
                                tp_state = TP_IDLE;
                                break;
                            }
                            if((xTPCtrl.start.x == -1) && (xTPCtrl.start.y == -1))
                            {
                                xTPCtrl.start.x = xTPCtrl.touch_point[xTPCtrl.cur_pos].x;
                                xTPCtrl.start.y = xTPCtrl.touch_point[xTPCtrl.cur_pos].y;
                            }
                            xTPCtrl.point_num++;
                            if(xTPCtrl.point_num > TP_POINT_BUF_SIZE)
                                xTPCtrl.point_num = TP_POINT_BUF_SIZE;
                            if(xTPCtrl.point_num > INVALID_TAIL_PACKET)
                            {
                                offset = xTPCtrl.cur_pos - INVALID_TAIL_PACKET;
                                if(offset < 0)
                                offset += TP_POINT_BUF_SIZE;
                                pid_state.Pressed = 1;
                                pid_state.x = xTPCtrl.touch_point[offset].x;
                                pid_state.y = xTPCtrl.touch_point[offset].y;
                                if(gLcdBackLightStatus == 1)
                                {
                                    GUI_TOUCH_Exec(&pid_state);
                                }
#if 0
                                if(!touchCaliFlag)
                                TPAdcToCoordinate(&xTPCtrl.touch_point[xTPCtrl.cur_pos].x,&xTPCtrl.touch_point[xTPCtrl.cur_pos].y);
                                msg.msgType = TOUCH_EVENT;
                                msg.touchEvent = TOUCH_PRESS;
                                msg.touchPoint.x = xTPCtrl.touch_point[xTPCtrl.cur_pos].x;
                                msg.touchPoint.y = xTPCtrl.touch_point[xTPCtrl.cur_pos].y;
                                msgsnd(gMainMsgQueue,&msg,sizeof(xTP_Msg),0);
#endif
                            }
                            xTPCtrl.cur_pos++;
                            xTPCtrl.cur_pos %= TP_POINT_BUF_SIZE;
                        }
                        tp_state = TP_IDLE;
                        break;
                    }
                    tp_state = TP_IDLE;
                    break;
                case EV_KEY:
                    if(tp_data.code == BTN_TOUCH)
                    {
                        LCDBackLight(1);
                        backlight_timeout = 0;
                        if(gLcdBackLightStatus == 0)
                        {
                            touch_ctrl_reset();
                            break;
                        }
                        if(xTPCtrl.point_num > INVALID_TAIL_PACKET)
                        {
                            xTPCtrl.cur_pos -= INVALID_TAIL_PACKET;
                            if(xTPCtrl.cur_pos < 0)
                                xTPCtrl.cur_pos += TP_POINT_BUF_SIZE;
                            pid_state.Pressed = 0;
                            pid_state.x = xTPCtrl.touch_point[xTPCtrl.cur_pos].x;
                            pid_state.y = xTPCtrl.touch_point[xTPCtrl.cur_pos].y;
                            GUI_TOUCH_Exec(&pid_state);
#if 0
                            if(!touchCaliFlag)
                                TPAdcToCoordinate(&xTPCtrl.touch_point[xTPCtrl.cur_pos].x,&xTPCtrl.touch_point[xTPCtrl.cur_pos].y);
                            if((ABS(xTPCtrl.start.x - xTPCtrl.touch_point[xTPCtrl.cur_pos].x) > 300) || 
                            (ABS(xTPCtrl.start.y - xTPCtrl.touch_point[xTPCtrl.cur_pos].y) > 300))
                            {
                                msg.msgType = TOUCH_EVENT;
                                msg.touchEvent = TOUCH_SLIDE;
                                msg.touchPoint.x = xTPCtrl.touch_point[xTPCtrl.cur_pos].x;
                                msg.touchPoint.y = xTPCtrl.touch_point[xTPCtrl.cur_pos].y;
                            }
                            else
                            {
                                msg.msgType = TOUCH_EVENT;
                                msg.touchEvent = TOUCH_RELEASE;
                                msg.touchPoint.x = xTPCtrl.touch_point[xTPCtrl.cur_pos].x;
                                msg.touchPoint.y = xTPCtrl.touch_point[xTPCtrl.cur_pos].y;
                            }
                            msgsnd(gMainMsgQueue,&msg,sizeof(xTP_Msg),0);
#endif
                            }
                        }
                    touch_ctrl_reset();

                    break;
                default:
                    tp_state = TP_IDLE;
                    break;
            }
            //printf("touch panel data read[%d]!!!!!!!!!!!!!,type = %d,code = %d, value = %d\n",count,tp_data.type,tp_data.code,tp_data.value);
        }
        else
        {
            if(++backlight_timeout > BACKLIGHT_TIMEOUT)
            {
                LCDBackLight(0);
            }
        }
    }while(1);
    close(tp_fd);
    return 0;
}

