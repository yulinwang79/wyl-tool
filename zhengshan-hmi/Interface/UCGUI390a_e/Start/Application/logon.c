#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include "GUI.h"
#include "BUTTON.h"
#include "EDIT.h"
#include "LCD_ConfDefaults.h"      /* valid LCD configuration */
#include "common.h"
#include "hmiData.h"
#define LOGON_TITLE_HEIGHT 55
#define LOGON_BUTTON_WIDTH 80
#define LOGON_BUTTON_HEIGHT 35
char s_password[PASSWORD_MAX_LENTH+1]={0};
char s_new_password[PASSWORD_MAX_LENTH+1]={0};

static void get_logon_password(char *lable);
static void get_logon_new_password(char *lable);

static const t_lable_info s_logon_lable[]={
        {ID_STATE_NONE,THOUCH_CLICK,{1,1,LCD_XSIZE -1,LOGON_TITLE_HEIGHT},&GUI_Font24_ASCII,NULL,"Logon"},
        {ID_STATE_NONE,THOUCH_CLICK,{5,105,110,130},&GUI_Font16_ASCII,NULL,"Password:"},
        {ID_STATE_NONE,THOUCH_CLICK,{5 ,134, 110,159},&GUI_Font16_ASCII,NULL,"New Password:"},
        {ID_LOGON_PASSWORD,THOUCH_CLICK,{112,105,310 ,130},&GUI_Font16_ASCII,get_logon_password,NULL},
        {ID_LOGON_NEWPASSWORD,THOUCH_CLICK,{112,134,310 ,159},&GUI_Font16_ASCII,get_logon_new_password,NULL},
    }; 

void get_logon_password(char *lable){
    int len =strlen(s_password); 
    if(len)
    {
        memset(lable,'*',len);
    }
}
void get_logon_new_password(char *lable){
    int len =strlen(s_new_password); 
    if(len)
    {
        memset(lable,'*',len);
    }

}

int _login(void)
{
    int r;
    memset(s_password,0,sizeof(s_password));
    memset(s_new_password,0,sizeof(s_new_password));
    
  do {
      BUTTON_Handle hButtonESC;
      BUTTON_Handle hButtonOK;
      
      GUI_SetBkColor(GUI_WHITE);
      GUI_Clear();
      GUI_SetColor(GUI_BLACK);
      
      GUI_DrawRect(0,0,LCD_XSIZE-1,LCD_YSIZE-1);
      GUI_DrawHLine(LOGON_TITLE_HEIGHT,0,LCD_XSIZE-1);
      hButtonESC = BUTTON_Create( 0, LCD_YSIZE -LOGON_BUTTON_HEIGHT, LOGON_BUTTON_WIDTH, LOGON_BUTTON_HEIGHT, GUI_ID_CANCEL,BUTTON_CF_SHOW );
      BUTTON_SetText(hButtonESC, "Cancel");
      BUTTON_SetState(hButtonESC,BUTTON_INVERT_DRAW);
      
      hButtonOK = BUTTON_Create( LCD_XSIZE -LOGON_BUTTON_WIDTH , LCD_YSIZE -LOGON_BUTTON_HEIGHT, LOGON_BUTTON_WIDTH, LOGON_BUTTON_HEIGHT, GUI_ID_OK,BUTTON_CF_SHOW );
      BUTTON_SetText(hButtonOK, "OK");
      BUTTON_SetState(hButtonOK,BUTTON_INVERT_DRAW);
      
      MMM_DrawLable((t_lable_info*)s_logon_lable,countof(s_logon_lable));
      
      GUI_DrawRect(112,105,310 ,130);
      GUI_DrawRect(112,134,310 ,159);

    r= MMM_WaitEvent(WAITEVENT_TIME,(t_lable_info*)s_logon_lable,countof(s_logon_lable),NULL);
    BUTTON_Delete(hButtonESC);
    BUTTON_Delete(hButtonOK);

    switch (r) {
    case 0:
    case GUI_ID_CANCEL:
        r =0;
     break;

    case GUI_ID_OK:
        if(HmiLoginAndChange(s_password,s_new_password))
        {
            r=0;
        }
      break;

    case ID_LOGON_PASSWORD:
        _VVEditNum("Password",s_password,PASSWORD_MAX_LENTH);
        break;
    case ID_LOGON_NEWPASSWORD:
        _VVEditNum("New Password",s_new_password,PASSWORD_MAX_LENTH);
        break;
    }
  }while(r);
  return r;
}
