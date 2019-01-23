#include "GUI.h"
#include "BUTTON.h"
#include "common.h"
#include "hmidata.h"
void MMM_DrawLable(t_lable_info* lableArray,int lableNum)
{
    int i;
    char lable_text[TEXT_MAX_LENTH];
    const char* lablePtr;
    for(i=0; i<lableNum;i++)
    {
        if(lableArray[i].font)
            GUI_SetFont(lableArray[i].font);
        if(lableArray[i].getLable)
        {
            lable_text[0] =0;
            lableArray[i].getLable(lable_text); 
            lablePtr = (const char*)lable_text;
        }
        else 
        {
            lablePtr = (const char*)lableArray[i].text;
        }
        if(lablePtr)
        {
            GUI_DispStringInRect(lablePtr, &lableArray[i].rect, GUI_TA_HCENTER | GUI_TA_VCENTER);
        }
    }

}
int MMM_WaitKey(U32 time) {
  int r =0;
  I32 tMax = GUI_GetTime() + time;
  do {
    r =  GUI_GetKey();
    if (r) {
      break;
    }
    if (!GUI_Exec()) {
      GUI_X_WAIT_EVENT();      /* Wait for event (keyboard, mouse or whatever) */
    }
  } while (tMax>GUI_GetTime());
  return r;
}

int MMM_WaitEvent(U32 time,t_lable_info* contralArray,int contralNum,FuncPtr cb)
{
    #define ABS(a) ((a)>0?(a):-(a)) 
    int r =0;
    int i;
    const t_lable_info* contral = contralArray;
    I32 tMax = GUI_GetTime() + time;
    GUI_PID_STATE State;
    GUI_PID_STATE Pressed_State = {0};

    do {
      GUI_TOUCH_GetState(&State);
      if (State.Pressed == 0) {
        GUI_GetKey();
        break;
      }
      GUI_Delay (100);
    } while (tMax>GUI_GetTime());

    do {

      r =  GUI_GetKey();
      if (r) {
        break;
      }


      if(contral)
      {
          GUI_TOUCH_GetState(&State);
          if (State.Pressed) {
              if(!Pressed_State.Pressed)
              {
                    Pressed_State = State;
              }
          }
          else if(Pressed_State.Pressed && ABS(Pressed_State.x -State.x) <50 && ABS(Pressed_State.y -State.y) <50)
          {
              for(i=0; i<contralNum;i++){
                      if(contral->contral_id && contral->thouchEvent == THOUCH_CLICK&& (Pressed_State.x) >= contral->rect.x0 && (Pressed_State.x) <= contral->rect.x1 && (Pressed_State.y) >= contral->rect.y0 && (Pressed_State.y) <= contral->rect.y1)
                      {
                          return contral->contral_id;
                      }
                      contral ++;
               }
			   contral = contralArray;
			   Pressed_State.Pressed =0;
          }
          else if(Pressed_State.Pressed && (ABS(Pressed_State.x -State.x) > 100 || ABS(Pressed_State.y -State.y) >100))
          {
              for(i=0; i<contralNum;i++){
                      if(contral->contral_id && contral->thouchEvent == THOUCH_SLIDE&& (Pressed_State.x) >= contral->rect.x0 && (Pressed_State.x) <= contral->rect.x1 && (Pressed_State.y) >= contral->rect.y0 && (Pressed_State.y) <= contral->rect.y1)
                      {
                          return contral->contral_id;
                      }
                      contral ++;
               }
			   contral = contralArray;
			   Pressed_State.Pressed =0;
          }
		  else
		  {
			   contral = contralArray;
			   Pressed_State.Pressed =0;
		  }

      }
      if (!GUI_Exec()) {
        GUI_X_WAIT_EVENT();      /* Wait for event (keyboard, mouse or whatever) */
      }
      if(cb)
        cb();
    } while (tMax>GUI_GetTime());
    return r;
}

