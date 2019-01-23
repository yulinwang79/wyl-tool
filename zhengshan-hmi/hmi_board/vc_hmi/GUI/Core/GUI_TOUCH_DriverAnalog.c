/*
*********************************************************************************************************
*                                                uC/GUI
*                        Universal graphic software for embedded applications
*
*                       (c) Copyright 2002, Micrium Inc., Weston, FL
*                       (c) Copyright 2002, SEGGER Microcontroller Systeme GmbH
*
*              µC/GUI is protected by international copyright laws. Knowledge of the
*              source code may not be used to write a similar product. This file may
*              only be used in accordance with a license and should not be redistributed
*              in any way. We appreciate your understanding and fairness.
*
----------------------------------------------------------------------
File        : GUITOUCH.C
Purpose     : Touch screen manager
----------------------------------------------------------------------
This module handles the touch screen. It is configured in the file
GUITouch.conf.h (Should be located in the Config\ directory).
----------------------------------------------------------------------
*/

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "LCD_Private.h"      /* private modul definitions & config */
#include "GUI_Protected.h"

/* Generate code only if configuration says so ! */
#if GUI_SUPPORT_TOUCH

#include "GUITouchConf.h"    /* Located in GUIx, will include GUITouchConf.h */
int touchCaliFlag = 0;

extern int TPAdcToCoordinate(int *x, int *y);
/*********************************************************************
*
*       Public code
*
**********************************************************************
*/

/*********************************************************************
*
*       GUI_TOUCH_Exec
*/
void GUI_TOUCH_Exec(GUI_PID_STATE *pState) {
  #ifndef WIN32
  if(touchCaliFlag)
  	GUI_TOUCH_StoreStateEx(pState);
  else
  {
  	if(TPAdcToCoordinate(&pState->x,&pState->y))
  		GUI_TOUCH_StoreStateEx(pState);
  }
  #endif /* WIN32 */
}
#else

void GUI_TOUCH_DriverAnalog_C(void);  /* Avoid "no prototype" warnings */
void GUI_TOUCH_DriverAnalog_C(void) {}

#endif    /* defined(GUI_SUPPORT_TOUCH) && GUI_SUPPORT_TOUCH */

/*************************** End of file ****************************/
