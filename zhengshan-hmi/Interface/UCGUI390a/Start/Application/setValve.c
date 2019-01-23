#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include "GUI.h"
#include "BUTTON.h"
#include "PROGBAR.h"
#include "LCD_ConfDefaults.h"      /* valid LCD configuration */
#include "common.h"
#include "hmiData.h"

#include ".\reource\valveconfig\vc_background.c"

#include ".\reource\valveconfig\vc_enter_pressed.c"
#include ".\reource\valveconfig\vc_enter_unpressed.c"


/*Set Valve*/
static void get_sv_contral_open_addr(char *lable);
static void get_sv_contral_open_cmd(char *lable);
static void get_sv_contral_close_addr(char *lable);
static void get_sv_contral_close_cmd(char *lable);
static void get_sv_contral_stop_addr(char *lable);
static void get_sv_contral_stop_cmd(char *lable);
static void get_sv_contral_posc_addr(char *lable);
static void get_sv_contral_posc_cmd(char *lable);
static void get_sv_contral_pos_addr(char *lable);
static void get_sv_contral_pos_max_val(char *lable);
static void get_sv_view_open_add(char *lable);
static void get_sv_view_close_add(char *lable);
static void get_sv_view_l_r_add(char *lable);
static void get_sv_view_pos_add(char *lable);
static void get_sv_view_pos_max_val(char *lable);
static void get_sv_name_text(char *lable);

static const t_lable_info s_sv_lable[]={
    //{ID_SV_ENTER,THOUCH_CLICK,{244,14,309,47},NULL,NULL,NULL},
    {GUI_ID_CANCEL,THOUCH_CLICK,{1,1,45,54},&GUI_Font24_ASCII,hmi_get_group_num_text,NULL},
    {ID_STATE_NONE,THOUCH_CLICK,{47,26,98,54},&GUI_Font16_ASCII,hmi_get_valve_num_text,NULL},
    {ID_SV_CONTRAL_OPEN_ADDR,THOUCH_CLICK,{49 ,94, 108,112},&GUI_Font16_ASCII,get_sv_contral_open_addr,NULL},
    {ID_SV_CONTRAL_OPEN_CMD,THOUCH_CLICK,{ 115,94, 176,112},&GUI_Font16_ASCII,get_sv_contral_open_cmd,NULL},
    {ID_SV_CONTRAL_CLOSE_ADDR,THOUCH_CLICK,{ 49,123,108 ,141},&GUI_Font16_ASCII,get_sv_contral_close_addr,NULL},
    {ID_SV_CONTRAL_CLOSE_CMD,THOUCH_CLICK,{ 115,123,176,141},&GUI_Font16_ASCII,get_sv_contral_close_cmd,NULL},
    {ID_SV_CONTRAL_STOP_ADDR,THOUCH_CLICK,{49 ,152,108 ,170},&GUI_Font16_ASCII,get_sv_contral_stop_addr,NULL},
    {ID_SV_CONTRAL_STOP_CMD,THOUCH_CLICK,{ 115,152,176 ,170},&GUI_Font16_ASCII,get_sv_contral_stop_cmd,NULL},
    {ID_SV_CONTRAL_POSC_ADDR,THOUCH_CLICK,{ 49,181,108 ,198},&GUI_Font16_ASCII,get_sv_contral_posc_addr,NULL},
    {ID_SV_CONTRAL_POSC_CMD,THOUCH_CLICK,{ 115,181,176 ,198},&GUI_Font16_ASCII,get_sv_contral_posc_cmd,NULL},
    {ID_SV_CONTRAL_POS_ADDR,THOUCH_CLICK,{ 49,210,108,228},&GUI_Font16_ASCII,get_sv_contral_pos_addr,NULL},
    {ID_SV_CONTRAL_POS_MAX_VAL,THOUCH_CLICK,{ 115,210,159,228},&GUI_Font16_ASCII,get_sv_contral_pos_max_val,NULL},
    {ID_SV_VIEW_OPEN_ADDR,THOUCH_CLICK,{226 ,94, 311,112},&GUI_Font16_ASCII,get_sv_view_open_add,NULL},
    {ID_SV_VIEW_CLOSE_ADDR,THOUCH_CLICK,{226 ,123,311 ,141},&GUI_Font16_ASCII,get_sv_view_close_add,NULL},
    {ID_SV_VIEW_LR_ADDR,THOUCH_CLICK,{226 ,152,311 ,170},&GUI_Font16_ASCII,get_sv_view_l_r_add,NULL},
    {ID_SV_VIEW_POS_ADDR,THOUCH_CLICK,{226,181,266 ,198},&GUI_Font16_ASCII,get_sv_view_pos_add,NULL},
    {ID_SV_VIEW_POS_MAX_VAL,THOUCH_CLICK,{271,181,311 ,198},&GUI_Font16_ASCII,get_sv_view_pos_max_val,NULL},
    {ID_SV_SET_NAME,THOUCH_CLICK,{ 204,210,311 ,228},&GUI_Font16_ASCII,get_sv_name_text,NULL},
      
}; 

static const t_button_info s_sv_button[]={
    {ID_SV_ENTER,&bmvc_enter_pressed,&bmvc_enter_unpressed,&bmvc_enter_pressed,{243,11,311,47},NULL},
};

static xValveData s_valve_data={0};
static void get_sv_contral_open_addr(char *lable){
    if(s_valve_data.xCmd.dBOffsetForOpenCmd!=0xffff)
    sprintf(lable,"%d",s_valve_data.xCmd.dBOffsetForOpenCmd);
}
static void get_sv_contral_open_cmd(char *lable){
    if(s_valve_data.xCmd.openCmd!=0xffff)
    sprintf(lable,"%d",s_valve_data.xCmd.openCmd);
}
static void get_sv_contral_close_addr(char *lable){
    if(s_valve_data.xCmd.dBOffsetForCloseCmd!=0xffff)
    sprintf(lable,"%d",s_valve_data.xCmd.dBOffsetForCloseCmd);
}
static void get_sv_contral_close_cmd(char *lable){
    if(s_valve_data.xCmd.closeCmd!=0xffff)
    sprintf(lable,"%d",s_valve_data.xCmd.closeCmd);
}
static void get_sv_contral_stop_addr(char *lable){
    if(s_valve_data.xCmd.dBoffsetForStopCmd!=0xffff)
    sprintf(lable,"%d",s_valve_data.xCmd.dBoffsetForStopCmd);
}
static void get_sv_contral_stop_cmd(char *lable){
    if(s_valve_data.xCmd.stopCmd!=0xffff)
    sprintf(lable,"%d",s_valve_data.xCmd.stopCmd);
}
static void get_sv_contral_posc_addr(char *lable){
    if(s_valve_data.xCmd.dBOffsetForCurPosCmd!=0xffff)
    sprintf(lable,"%d",s_valve_data.xCmd.dBOffsetForCurPosCmd);
}
static void get_sv_contral_posc_cmd(char *lable){
    if(s_valve_data.xCmd.getCurPosCmd!=0xffff)
    sprintf(lable,"%d",s_valve_data.xCmd.getCurPosCmd);
}
static void get_sv_contral_pos_addr(char *lable){
    if(s_valve_data.xCmd.dbOffsetForCurPos!=0xffff)
    sprintf(lable,"%d",s_valve_data.xCmd.dbOffsetForCurPos);
}
static void get_sv_contral_pos_max_val(char *lable){
    if(s_valve_data.xCmd.curPosMaxVal!=0xffff)
    sprintf(lable,"%d",s_valve_data.xCmd.curPosMaxVal);
}
static void get_sv_view_open_add(char *lable){
    if(s_valve_data.xView.dBOffsetForOpenVal!=0xffffffff)
    sprintf(lable,"%d",s_valve_data.xView.dBOffsetForOpenVal);
}
static void get_sv_view_close_add(char *lable){
    if(s_valve_data.xView.dBOffsetForCloseVal!=0xffffffff)
    sprintf(lable,"%d",s_valve_data.xView.dBOffsetForCloseVal);
}
static void get_sv_view_l_r_add(char *lable){
    if(s_valve_data.xView.dBOffsetForLRVal!=0xffffffff)
    sprintf(lable,"%d",s_valve_data.xView.dBOffsetForLRVal);
}
static void get_sv_view_pos_add(char *lable){
    if(s_valve_data.xView.dBOffsetForCurPos!=0xffffffff)
    sprintf(lable,"%d",s_valve_data.xView.dBOffsetForCurPos);
}
static void get_sv_view_pos_max_val(char *lable){
    if(s_valve_data.xView.curPosMaxVal!=0xffffffff)
    sprintf(lable,"%d",s_valve_data.xView.curPosMaxVal);
}
static void get_sv_name_text(char *lable){
    memcpy(lable,s_valve_data.acValveName,VALVE_NAME_MAX_LENTH);
}
#if 0
int atoi (char s[])
{
int i,n,sign;

for(i=0;isspace(s[i]);i++)//跳过空白符
      ;
sign=(s[i]=='-')?-1:1;
if(s[i]=='+'||s[i]==' -')//跳过符号
      i++;
for(n=0;isdigit(s[i]);i++)
      n=10*n+(s[i]-'0');//将数字字符转换成整形数字
return sign *n;

}
#endif
void _SetValve(int group_id, int valve_id)
{
    hmi_get_curr_valve_data( &s_valve_data);
    do {
    int r;
    char text[TEXT_MAX_LENTH]={0};
    BUTTON_Handle buttonHandle[countof(s_sv_button)];
    GUI_SetColor(GUI_BLACK);
    GUI_DrawBitmap(&bmvc_background,0,0);
    
    MMM_CreateButton((t_button_info*)s_sv_button,countof(s_sv_button),buttonHandle);
    MMM_DrawLable((t_lable_info*)s_sv_lable,countof(s_sv_lable));

    r= MMM_WaitEvent(WAITEVENT_TIME,(t_lable_info*)s_sv_lable,countof(s_sv_lable),NULL);
    MMM_DeleteButton(buttonHandle,countof(s_sv_button));
    
        switch(r){
            case GUI_ID_CANCEL:
            case  ID_STATE_NONE:
                return;
           case ID_SV_ENTER:
               hmi_set_curr_valve_data( &s_valve_data);
               return;
           case ID_SV_CONTRAL_OPEN_ADDR:
               if(_VVEditNum("Open Addr",text,VALVE_ADDR_MAX_LENTH))
                   s_valve_data.xCmd.dBOffsetForOpenCmd = atoi(text);
                break;
           case ID_SV_CONTRAL_OPEN_CMD:
               if(_VVEditNum("Open Cmd",text,VALVE_CMD_MAX_LENTH))
               s_valve_data.xCmd.openCmd = atoi(text);
                break;
           case ID_SV_CONTRAL_CLOSE_ADDR:
               if(_VVEditNum("Close Addr",text,VALVE_ADDR_MAX_LENTH))
               s_valve_data.xCmd.dBOffsetForCloseCmd = atoi(text);
                break;
           case ID_SV_CONTRAL_CLOSE_CMD:
               if(_VVEditNum("Close Cmd",text,VALVE_CMD_MAX_LENTH))
               s_valve_data.xCmd.closeCmd = atoi(text);
                break;
           case ID_SV_CONTRAL_STOP_ADDR:
               if(_VVEditNum("Stop Addr",text,VALVE_ADDR_MAX_LENTH))
               s_valve_data.xCmd.dBoffsetForStopCmd = atoi(text);
                break;
           case ID_SV_CONTRAL_STOP_CMD:
               if(_VVEditNum("Stop Cmd",text,VALVE_CMD_MAX_LENTH))
               s_valve_data.xCmd.stopCmd = atoi(text);
                break;
           case ID_SV_CONTRAL_POSC_ADDR:
               if(_VVEditNum("Posc Addr",text,VALVE_ADDR_MAX_LENTH))
               s_valve_data.xCmd.dBOffsetForCurPosCmd = atoi(text);
                break;
           case ID_SV_CONTRAL_POSC_CMD:
               if(_VVEditNum("POSC Cmd",text,VALVE_CMD_MAX_LENTH))
               s_valve_data.xCmd.getCurPosCmd = atoi(text);
                break;
           case ID_SV_CONTRAL_POS_ADDR:
               if(_VVEditNum("Pos Addr",text,VALVE_ADDR_MAX_LENTH))
               s_valve_data.xCmd.dbOffsetForCurPos = atoi(text);
               break;
          case ID_SV_CONTRAL_POS_MAX_VAL:
               if(_VVEditNum("Pos Max Val",text,VALVE_CMD_MAX_LENTH))
               s_valve_data.xCmd.curPosMaxVal = atoi(text);
               break;
          case ID_SV_VIEW_OPEN_ADDR:
              if(_VVEditNum("Open Addr",text,VALVE_ADDR_MAX_LENTH))
              s_valve_data.xView.dBOffsetForOpenVal = atoi(text);
               break;
           case ID_SV_VIEW_CLOSE_ADDR:
               if(_VVEditNum("Close Addr",text,VALVE_ADDR_MAX_LENTH))
               s_valve_data.xView.dBOffsetForCloseVal = atoi(text);
                break;
           case ID_SV_VIEW_LR_ADDR:
               if(_VVEditNum("LR Addr",text,VALVE_ADDR_MAX_LENTH))
               s_valve_data.xView.dBOffsetForLRVal = atoi(text);
                break;
           case ID_SV_VIEW_POS_ADDR:
               if(_VVEditNum("Pos Addr",text,VALVE_ADDR_MAX_LENTH))
                s_valve_data.xView.dBOffsetForCurPos = atoi(text);
                break;
           case ID_SV_VIEW_POS_MAX_VAL:
               if(_VVEditNum("Pos Max Val",text,VALVE_CMD_MAX_LENTH))
                s_valve_data.xView.curPosMaxVal = atoi(text);
                break;
           case ID_SV_SET_NAME:
                if(_VVEdit("Set Name",text,VALVE_NAME_MAX_LENTH))
                    memcpy(s_valve_data.acValveName,text,VALVE_NAME_MAX_LENTH);
                break;
        }
    }while(1);

}
