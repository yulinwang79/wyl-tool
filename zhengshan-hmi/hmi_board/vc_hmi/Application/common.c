#include "GUI.h"
#include "BUTTON.h"
#include "common.h"
#include "hmiData.h"
GUI_CONST_STORAGE GUI_COLOR ColorsPalette[] = {
     0x000000,0xFFFFFF
};

void MMM_CreateButton(t_button_info* buttonArray,int buttonsNum,BUTTON_Handle* handleArray)
{
    int i;
    char lable_text[TEXT_MAX_LENTH];
    const char* lablePtr;
    GUI_FONT* oldFont = NULL;
    for(i=0; i<buttonsNum; i++){

        handleArray[i] =  BUTTON_Create( buttonArray[i].rect.x0, 
                                         buttonArray[i].rect.y0, 
                                         buttonArray[i].rect.x1 -buttonArray[i].rect.x0 +1, 
                                         buttonArray[i].rect.y1 -buttonArray[i].rect.y0 +1,
                                         buttonArray[i].button_id, BUTTON_CF_SHOW );
        
        BUTTON_SetBitmap(handleArray[i],BUTTON_BI_UNPRESSED,buttonArray[i].unpressed_bitmap);
        BUTTON_SetBitmap(handleArray[i],BUTTON_BI_PRESSED,buttonArray[i].pressed_bitmap);
        BUTTON_SetBitmap(handleArray[i],BUTTON_BI_DISABLED,buttonArray[i].pressed_bitmap);
        BUTTON_SetState(handleArray[i],BUTTON_UNDRAW_BORDER);
        
        if(buttonArray[i].IsEnableFunc)
        {
            WM_SetEnableState(handleArray[i],buttonArray[i].IsEnableFunc());
        }

    }

}
void MMM_DeleteButton(BUTTON_Handle* handleArray,int buttonsNum)
{
    int i;
    for (i=0; i< buttonsNum; i++) {
          BUTTON_Delete(handleArray[i]);
    }
}
void MMM_DrawLable(t_lable_info* lableArray,int lableNum)
{
    int i;
    char lable_text[TEXT_MAX_LENTH];
    const char* lablePtr;
    GUI_FONT* oldFont = NULL;
    for(i=0; i<lableNum;i++)
    {
        if(lableArray[i].font)
            oldFont = GUI_SetFont(lableArray[i].font);
        else
            oldFont = NULL;
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
        if(oldFont)
           GUI_SetFont(oldFont); 
    }

}
int MMM_WaitKey(U32 time) {
  int r =0;
  I32 tMax = GUI_GetTime() + time/1000;
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
    int r = 0;
    int i;
    const t_lable_info* contral = contralArray;
    const t_lable_info* contral_pressed = NULL;
    I32 tMax = GUI_GetTime() + time/1000;
    GUI_PID_STATE State;
    GUI_PID_STATE Pressed_State = {0};


    do {
      r =  GUI_GetKey();
      if (r) {
        break;
      }

      if(contral)
      {
          GUI_TOUCH_GetState(&State);
		  //printf("touch state = %d,x:%d,y:%d\n",State.Pressed,State.x,State.y);
            if (State.Pressed) 
            {
              if(!Pressed_State.Pressed)
              {
                    Pressed_State = State;
                    for(i=0; i<contralNum;i++){
                            if(contral->contral_id && contral->thouchEvent == THOUCH_CLICK&& (Pressed_State.x) >= contral->rect.x0 && (Pressed_State.x) <= contral->rect.x1 && (Pressed_State.y) >= contral->rect.y0 && (Pressed_State.y) <= contral->rect.y1)
                            {
                                contral_pressed = contral;
                                GUI_InvertRect(contral_pressed->rect.x0,contral_pressed->rect.y0,contral_pressed->rect.x1,contral_pressed->rect.y1);
                                contral = contralArray;
                                break;
                            }
                            contral ++;
                     }
              }
            }
            else 
            {
                if(contral_pressed!=NULL && ABS(Pressed_State.x -State.x) <50 && ABS(Pressed_State.y -State.y) <50)/*优先选择按下坐标，允许误差*/
                {
                
                    for(i=0; i<contralNum;i++)
                    {
                        if(contral->contral_id && contral->thouchEvent == THOUCH_CLICK&& (Pressed_State.x) >= contral->rect.x0 && (Pressed_State.x) <= contral->rect.x1 && (Pressed_State.y) >= contral->rect.y0 && (Pressed_State.y) <= contral->rect.y1)
                        {
                            GUI_InvertRect(contral_pressed->rect.x0,contral_pressed->rect.y0,contral_pressed->rect.x1,contral_pressed->rect.y1);
                            return contral->contral_id;
                        }
                        contral ++;
                    }
                }
                else if(Pressed_State.Pressed && (ABS(Pressed_State.x -State.x) > 100 || ABS(Pressed_State.y -State.y) >100))
                {
                    for(i=0; i<contralNum;i++)
                    {
                        if(contral->contral_id && contral->thouchEvent == THOUCH_SLIDE&& (Pressed_State.x) >= contral->rect.x0 && (Pressed_State.x) <= contral->rect.x1 && (Pressed_State.y) >= contral->rect.y0 && (Pressed_State.y) <= contral->rect.y1)
                        {
                            return contral->contral_id;
                        }
                        contral ++;
                    }
                }
                if(contral_pressed)
                {
                    GUI_InvertRect(contral_pressed->rect.x0,contral_pressed->rect.y0,contral_pressed->rect.x1,contral_pressed->rect.y1);
                    contral_pressed=NULL;
                }
                Pressed_State.Pressed =0;
            }
            contral = contralArray;
      }
	  if(cb)
        cb();
      if (!GUI_Exec()) {
        GUI_X_WAIT_EVENT();      /* Wait for event (keyboard, mouse or whatever) */
      }
	  r = WAIT_EVENT_TIMEOUT;
    } while (tMax>GUI_GetTime());
    return r;
}

