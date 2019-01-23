#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include "GUI.h"
#include "BUTTON.h"
#include "PROGBAR.h"
#include "LCD_ConfDefaults.h"      /* valid LCD configuration */
#include "common.h"
#include "hmiData.h"

#define VALVEVIEW_TITLE_HEIGHT 55
/*********************************************************************
*                SEGGER MICROCONTROLLER SYSTEME GmbH                 *
*        Solutions for real time microcontroller applications        *
*                           www.segger.com                           *
**********************************************************************
*
* C-file generated by
*
*        �C/GUI-BitmapConvert V3.90.
*        Compiled Aug 19 2004, 09:07:56
*          (c) 2002  Micrium, Inc.
  www.micrium.com

  (c) 1998-2002  Segger
  Microcontroller Systeme GmbH
  www.segger.com
*
**********************************************************************
*
* Source file: vv_title
* Dimensions:  320 * 104
* NumColors:   2
*
**********************************************************************
*/

#include "stdlib.h"

#include "GUI.h"

#ifndef GUI_CONST_STORAGE
  #define GUI_CONST_STORAGE const
#endif

/*   Palette
The following are the entries of the palette table.
Every entry is a 32-bit value (of which 24 bits are actually used)
the lower   8 bits represent the Red component,
the middle  8 bits represent the Green component,
the highest 8 bits (of the 24 bits used) represent the Blue component
as follows:   0xBBGGRR
*/

/*resource*/

#include ".\reource\valveView\vv_prev_pressed.c"
#include ".\reource\valveView\vv_prev_unpressed.c"

#include ".\reource\valveView\vv_next_pressed.c"
#include ".\reource\valveView\vv_next_unpressed.c"

#include ".\reource\valveView\vv_close_pressed.c"
#include ".\reource\valveView\vv_close_unpressed.c"
#include ".\reource\valveView\vv_config_pressed.c"
#include ".\reource\valveView\vv_config_unpressed.c"
#include ".\reource\valveView\vv_logon_pressed.c"
#include ".\reource\valveView\vv_logon_unpressed.c"
#include ".\reource\valveView\vv_open_pressed.c"
#include ".\reource\valveView\vv_open_unpressed.c"
#include ".\reource\valveView\vv_spos_pressed.c"
#include ".\reource\valveView\vv_spos_unpressed.c"
#include ".\reource\valveView\vv_stop_pressed.c"
#include ".\reource\valveView\vv_stop_unpressed.c"
#include ".\reource\valveView\vv_title.c"
#include ".\reource\valveView\vv_view_close_disable.c"
#include ".\reource\valveView\vv_view_close_enable.c"
#include ".\reource\valveView\vv_view_L.c"
#include ".\reource\valveView\vv_view_open_disable.c"
#include ".\reource\valveView\vv_view_open_enable.c"
#include ".\reource\valveView\vv_view_R.c"



static const t_lable_info s_vv_lable[]={
    {GUI_ID_CANCEL,THOUCH_CLICK,{1,1,45,54},&GUI_Font24_ASCII,hmi_get_group_num_text,NULL},
    {ID_STATE_NONE,THOUCH_CLICK,{47,26,98,54},&GUI_Font16_ASCII,hmi_get_valve_num_text,NULL},
    {ID_STATE_NONE,THOUCH_CLICK,{100,1,208,54},&GUI_Font32_ASCII,hmi_get_valve_name,NULL},
    {ID_VV_CONTRAL_SPOS_VALUE,THOUCH_CLICK,{200 ,152, 315,178},NULL,NULL,NULL},
 }; 

static const t_button_info s_vv_button[]={
    {ID_VV_BUTTON_LEFT,&bmvv_prev_pressed,&bmvv_prev_unpressed,&bmvv_prev_pressed,{211 ,10, 264,55},NULL},
    {ID_VV_BUTTON_RIGHT,&bmvv_next_pressed,&bmvv_next_unpressed,&bmvv_next_pressed,{265 ,10, 318,55,},NULL}, 
    {ID_VV_CONTRAL_OPEN,&bmvv_open_pressed,&bmvv_open_unpressed,&bmvv_open_pressed,{150 ,105, 192,136},hmi_is_login},
    {ID_VV_CONTRAL_STOP,&bmvv_stop_pressed,&bmvv_stop_unpressed,&bmvv_stop_pressed,{211 ,105, 253,136,},hmi_is_login},
    {ID_VV_CONTRAL_CLOSE,&bmvv_close_pressed,&bmvv_close_unpressed,&bmvv_close_pressed,{272 ,105, 314,136},hmi_is_login},
    {ID_VV_CONTRAL_SPOS,&bmvv_spos_pressed,&bmvv_spos_unpressed,&bmvv_spos_pressed,{150 ,150, 192,181},hmi_is_login},
    {ID_VV_LOGON,&bmvv_logon_pressed,&bmvv_logon_unpressed,&bmvv_logon_pressed,{153 ,200, 219,236},NULL},
    {ID_VV_SET_VALVE,&bmvv_config_pressed,&bmvv_config_unpressed,&bmvv_config_pressed,{243 ,200, 309,236},hmi_is_login},
 }; 

const GUI_RECT s_v_rect = { 5, 
                                              113, 
                                              5 + 131, 113 + 26};
const GUI_RECT s_c_rect = { 202, 
                                          152, 
                                          202 + 114, 152 +26 };
FuncPtr ValveViewCB = NULL;
//PROGBAR_Handle hVProg = 0;

void DrawProgbar(GUI_RECT r,int valve,int max)
{
    
    GUI_RECT r1;/*= {0, 80, XSize, 120};*/
    char buff[16];
    int tm;
    int pos;
	if (max == 0)
		max = 100;
    if (valve > max) {
        valve = max;
    }
    pos = r.x0 + (valve * (r.x1+1 - r.x0)/ max);
    GUI_SetColor(GUI_BLACK);
    GUI_DrawRect(r.x0-1,r.y0-1,r.x1+1,r.y1+1);
    GUI_SetFont(&GUI_Font16_ASCII);
    sprintf(buff,"%d",valve);
    tm = GUI_SetTextMode(GUI_TM_TRANS);
    /* Draw left bar */
    r1=r;
    r1.x1 = pos;
    WM_SetUserClipArea(&r1);
    GUI_SetColor(GUI_BLACK);
    GUI_FillRect(r1.x0,r1.y0,r1.x1,r1.y1);
    GUI_SetColor(GUI_WHITE);
    GUI_DispStringInRect(buff, &r, GUI_TA_HCENTER | GUI_TA_VCENTER);
    /* Draw right bar */
    r1=r;
    r1.x0 = pos;
    WM_SetUserClipArea(&r1);
    GUI_SetColor(GUI_WHITE);
    GUI_FillRect(r1.x0,r1.y0,r1.x1,r1.y1);
    GUI_SetColor(GUI_BLACK);
    GUI_DispStringInRect(buff, &r, GUI_TA_HCENTER | GUI_TA_VCENTER);
    WM_SetUserClipArea(NULL);
    GUI_SetTextMode(tm);

}
void ValveViewOpenValveCB(void)
{
    static U32 i=0xFFFF;
    //PROGBAR_SetValue(hVProg, hmi_get_curr_valve_pos_value());
    i--;
    if(i==0)
    {
        DrawProgbar(s_v_rect,hmi_get_curr_valve_pos_value(),hmi_get_curr_valve_pos_max_value());
        i=0xFFFF;
    }
    /*reset*/
}

void _ValveView(int group_id) {
    int value = hmi_get_curr_valve_pos_value();
    hmi_set_curr_group(group_id);
    
    if(!hmi_is_exist_valve(group_id))
    {
        return;
    }
    do {
    int r;
    const GUI_BITMAP* pBmp;
    char text[TEXT_MAX_LENTH]={0};
    BUTTON_Handle buttonHandle[countof(s_vv_button)];
    //PROGBAR_Handle hCProg = PROGBAR_Create(202, 
    //                                      152, 
    //                                      114, 26, WM_CF_SHOW);
    
    //hVProg = PROGBAR_Create(5, 
    //                                          113, 
    //                                          131, 26, WM_CF_SHOW);
    GUI_SetColor(GUI_BLACK);
    GUI_Clear();
    /*titel*/
    GUI_DrawBitmap(&bmvv_title,0,0);

    //PROGBAR_SetValue(hCProg, value);
    DrawProgbar(s_c_rect,value,hmi_get_curr_pos_curr_max_valve());

    /*view*/
    GUI_DrawRect(0,0,LCD_XSIZE-1,LCD_YSIZE-1);
    GUI_DrawRect(5,112,5+131,113+26);
    GUI_DrawVLine(144, bmvv_title.YSize,LCD_YSIZE-1);
    GUI_DrawHLine(LCD_YSIZE-1-45,144,LCD_XSIZE-1);
    if(hmi_get_curr_valve_open_status())
    {
        pBmp = &bmvv_view_open_enable;
    }
    else
    {
        pBmp = &bmvv_view_open_disable;
    }
    GUI_DrawBitmap(pBmp,4,176);

    if(hmi_get_curr_valve_close_status())
    {
        pBmp = &bmvv_view_close_enable;
    }
    else
    {
        pBmp = &bmvv_view_close_disable;
    }
    GUI_DrawBitmap(pBmp,96,176);

    if(hmi_get_curr_valve_LR_status())
    {
        pBmp = &bmvv_view_L;
    }
    else
    {
        pBmp = &bmvv_view_R;
    }
    GUI_DrawBitmap(pBmp,54,180);

    //PROGBAR_SetValue(hVProg, hmi_get_curr_valve_pos_value());
    DrawProgbar(s_v_rect,hmi_get_curr_valve_pos_value(),hmi_get_curr_valve_pos_max_value());

    MMM_CreateButton((t_button_info*)s_vv_button,countof(s_vv_button),buttonHandle);

    MMM_DrawLable((t_lable_info*)s_vv_lable,countof(s_vv_lable));

    r= MMM_WaitEvent(WAITEVENT_TIME,(t_lable_info*)s_vv_lable,countof(s_vv_lable),ValveViewCB);

    MMM_DeleteButton(buttonHandle,countof(s_vv_button));
    //PROGBAR_Delete(hVProg);
    //PROGBAR_Delete(hCProg);
    switch(r){
        case  ID_VV_BUTTON_LEFT:
            hmi_valve_goto_prev();
            break;
        case  ID_VV_BUTTON_RIGHT:
            hmi_valve_goto_next();
            break;
#if 0
        case  ID_VV_VIEW_OPEN:
            break;
        case  ID_VV_VIEW_R:
            break;
        case  ID_VV_VIEW_CLOSE:
            break;
#endif
        case  ID_VV_CONTRAL_OPEN:
            hmi_open_curr_valve();
            ValveViewCB=ValveViewOpenValveCB;
            break;
        case  ID_VV_CONTRAL_STOP:
            hmi_stop_curr_valve();
            break;
        case  ID_VV_CONTRAL_CLOSE:
            hmi_close_curr_valve();
            break;
        case  ID_VV_CONTRAL_SPOS:
            hmi_set_cur_pos_curr_valve(value);
            break;
        case ID_VV_CONTRAL_SPOS_VALUE:    
            if(hmi_is_login() && _VVEditNum("SPOS Value",text,3))
            {
                value = atoi(text);
                
                if(value > 100)
                {
                    value =100;
                }
            }
            break;
        case  ID_VV_LOGON:
            if(!hmi_is_login())
                _login();
            else
                HmiLogOut();
            break;
        case  ID_VV_SET_VALVE:
            if(hmi_is_login())
                _SetValve(0,0);
            break;
        case GUI_ID_CANCEL:
        case  ID_STATE_NONE:
            HmiLogOut();
            return;
            break;
    }
        
    }while(1);

}
