/*
*********************************************************************************************************
*                                                µC/GUI
*                        Universal graphic software for embedded applications
*
*                       (c) Copyright 2002, Micrium Inc., Weston, FL
*                       (c) Copyright 2000, SEGGER Microcontroller Systeme GmbH          
*
*              µC/GUI is protected by international copyright laws. Knowledge of the
*              source code may not be used to write a similar product. This file may
*              only be used in accordance with a license and should not be redistributed 
*              in any way. We appreciate your understanding and fairness.
*
* File        : MainTask.c
* Purpose     : Application program in windows simulator
*********************************************************************************************************
*/

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
#include "GUI.h"
#include "LCD_ConfDefaults.h"      /* valid LCD configuration */
#include "common.h"
#include "hmiData.h"
#include "communication.h"
#include "tp_main.h"

extern const GUI_BITMAP bmMicriumLogo;
extern const GUI_BITMAP bmMicriumLogo_1bpp;

#define countof(Obj) (sizeof(Obj)/sizeof(Obj[0]))

#define ID_MAINMENU  1
#define ID_CALIBRATE 2

typedef int GET_TOUCH_ID_FUNC(GUI_PID_STATE state);



/*
  *******************************************************************
  *
  *              main()
  *
  *******************************************************************
*/
    static const t_lable_info s_mm_lable[]={
        {ID_MAINMENU,THOUCH_CLICK,{0,0,LCD_XSIZE-1,LCD_YSIZE-1},NULL,NULL,NULL},
        {ID_CALIBRATE,THOUCH_SLIDE,{0,0,LCD_XSIZE-1,LCD_YSIZE-1},NULL,NULL,NULL},
    }; 

void MainTask(void) {
  //int i;
  int r;
  pthread_t xTPThread;
  int XSize = LCD_GetXSize();
  //int YSize = LCD_GetYSize();
  //int XMid = XSize / 2;
  //int YMid = YSize / 2;
  InitCommunication();
  GUI_Init();
  HmiDataInit();
  //GUIDEMO_NotifyStartNext();
  //GUIDEMO_HideInfoWin();  
  pthread_create( &xTPThread, NULL, TouchPanelMonitor, NULL );
  InitTPCalibration();
  usleep(500);
  do {
    GUI_RECT rText;/*= {0, 80, XSize, 120};*/
    rText.x0=0;
    rText.y0=80;
    rText.x1=XSize;
    rText.y1=120;
    GUI_SetBkColor(GUI_WHITE);
    GUI_Clear();
    GUI_SetFont(&GUI_Font24B_ASCII);
    GUI_SetColor(GUI_BLACK);
    GUI_DispStringInRect("BLJ-SPEC-VC-MDM4-T", &rText, GUI_TA_HCENTER | GUI_TA_VCENTER);
    
    //GUI_DrawBitmap(&logo_bmp, 0, 0);
    r = MMM_WaitEvent(WAITEVENT_TIME,(t_lable_info*)&s_mm_lable[0],countof(s_mm_lable),NULL);

    switch (r) {
    case ID_MAINMENU:  _MainMenu(); break;
    case ID_CALIBRATE: _ExecCalibration(); break;
    }
  } while (1);
}
