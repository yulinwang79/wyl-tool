#include <stdio.h>
#include <stdlib.h>
#include <errno.h>
#include <string.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <sys/select.h>
#include <sys/time.h>
#include "GUI.h"
#include "BUTTON.h"
#include "LCD_ConfDefaults.h"      /* valid LCD configuration */
#include "common.h"
#include "hmiData.h"
#include "communication.h"

#ifndef GUI_CONST_STORAGE
  #define GUI_CONST_STORAGE const
#endif

#define GROUPSTATE_TITLE_HEIGHT 33

/*   Palette
The following are the entries of the palette table.
Every entry is a 32-bit value (of which 24 bits are actually used)
the lower   8 bits represent the Red component,
the middle  8 bits represent the Green component,
the highest 8 bits (of the 24 bits used) represent the Blue component
as follows:   0xBBGGRR
*/

#include ".\reource\groupstate\gs_left_pressed.c"
#include ".\reource\groupstate\gs_left_unpressed.c"

#include ".\reource\groupstate\gs_right_pressed.c"
#include ".\reource\groupstate\gs_right_unpressed.c"

#include ".\reource\groupstate\gs_valve_open_a.c"
#include ".\reource\groupstate\gs_valve_open_b.c"
#include ".\reource\groupstate\gs_valve_open_ab.c"
#include ".\reource\groupstate\gs_valve_close.c"


extern eValveStatus gGroupValveStatus[VALVE_MAX_GROUP][GROUP_VALVE_NUM];

static void DrawGroupStatus(void)
{
    GUI_RECT rText,r;/*= {0, 80, XSize, 120};*/
    char buff[16];
    int i;
    int group_id = hmi_get_curr_group();
    const GUI_BITMAP* pBmp;
    //LCD_DRAWMODE OldDrawMode;
    GUI_COLOR color_l;
    GUI_COLOR color_r;
    
    rText.x0=bmgs_right_pressed.XSize + 1;
    rText.y0=1;
    rText.x1=LCD_XSIZE - bmgs_left_pressed.XSize -2;
    rText.y1=bmgs_right_pressed.YSize +1;
    
    GUI_SetFont(&GUI_Font24_ASCII);
    
    GUI_SetColor(GUI_BLACK);
    sprintf(buff,"G%d Status",group_id+1);
    GUI_DispStringInRect(buff, &rText, GUI_TA_HCENTER | GUI_TA_VCENTER);

    GUI_SetColor(GUI_WHITE);
    GUI_FillRect(1,GROUPSTATE_TITLE_HEIGHT+1,LCD_XSIZE -2,LCD_YSIZE -2);

    GUI_SetColor(GUI_BLACK);
    GUI_SetFont(&GUI_Font16_ASCII);
    for(i=0; i<GROUP_VALVE_NUM;i++)
    {
        rText.x0 = (i%8)*bmgs_valve_open_a.XSize + 4;
        rText.y0 = (i/8)*bmgs_valve_open_a.YSize + GROUPSTATE_TITLE_HEIGHT +1;
        
        switch(gGroupValveStatus[group_id][i])
        {
            case VALVE_GOOD_ON_PORT_A:
                pBmp = &bmgs_valve_open_a;
                color_l = GUI_WHITE;
                color_r = GUI_BLACK;
                break;
            case VALVE_GOOD_ON_PORT_B:
                pBmp = &bmgs_valve_open_b;
                color_l = GUI_BLACK;
                color_r = GUI_WHITE;
                break;
            case VALVE_GOOD_ON_PORT_AB:
                pBmp = &bmgs_valve_open_ab;
                color_l = GUI_WHITE;
                color_r = GUI_WHITE;
                break;
            case VALVE_SUSPEND:
                pBmp = &bmgs_valve_close;
                color_l = GUI_BLACK;
                color_r = GUI_BLACK;
                break;
            default:
                #ifdef WIN32
                pBmp = &bmgs_valve_open_ab;
                color_l = GUI_WHITE;
                color_r = GUI_WHITE;
                #else
                pBmp = NULL;
                #endif
                    break;
        }
        if(pBmp)
        {
            int tm;
            GUI_DrawBitmap(pBmp,rText.x0,rText.y0);
            rText.x1=rText.x0 +pBmp->XSize -1;
            rText.y1=rText.y0 +pBmp->YSize -1;
            sprintf(buff,"V%d",i+1);


            tm = GUI_SetTextMode(GUI_TM_TRANS);
            /* Draw left bar */
            r=rText;
            r.x1 = rText.x0 + pBmp->XSize/2 - 1;
            WM_SetUserClipArea(&r);
            GUI_SetColor(color_l);
            GUI_DispStringInRect(buff, &rText, GUI_TA_HCENTER | GUI_TA_VCENTER);
            /* Draw right bar */
            r=rText;
            r.x0 = rText.x0 + pBmp->XSize/2;
            WM_SetUserClipArea(&r);
            GUI_SetColor(color_r);
            GUI_DispStringInRect(buff, &rText, GUI_TA_HCENTER | GUI_TA_VCENTER);
            WM_SetUserClipArea(NULL);
            GUI_SetTextMode(tm);

        }
    }
}

void GroupStatusCB(void)
{
    #ifndef WIN32
    static U32 i = 0x80;
    if(i > 1)
    {
        i--;
        usleep(2000);//2ms
        return;
    }
    i = 0x80;
    GetGroupValveStatus(hmi_get_curr_group());
    DrawGroupStatus();
    #else
    static U32 i=0xFFFF;
    i--;
    if(i==0)
    {
       GetGroupValveStatus(hmi_get_curr_group());
       DrawGroupStatus();
       i= 0xFFFF;
    }
    #endif
}

int _GroupState(int group_id) {
    int is_return =0,r = 0;
    
    BUTTON_Handle ahButton[3];
    //g_cur_group = id;
    
    GUI_SetBkColor(GUI_WHITE);
    GUI_Clear();
    GUI_SetColor(GUI_BLACK);

    GUI_DrawRect(0,0,LCD_XSIZE-1,LCD_YSIZE-1);
    GUI_DrawHLine(GROUPSTATE_TITLE_HEIGHT,0,LCD_XSIZE-1);

    ahButton[0] =  BUTTON_Create( 1, 
                                                           1, 
                                                            bmgs_left_pressed.XSize, 
                                                            bmgs_left_pressed.YSize, 
                                                            ID_GS_BUTTON_LEFT, BUTTON_CF_SHOW );

    ahButton[1] =  BUTTON_Create( LCD_XSIZE -1 - bmgs_right_pressed.XSize, 
                                                           1, 
                                                            bmgs_right_pressed.XSize, 
                                                            bmgs_right_pressed.YSize, 
                                                            ID_GS_BUTTON_RIGHT, BUTTON_CF_SHOW );
    ahButton[2] = BUTTON_Create( 0, LCD_YSIZE -35, 80, 35, GUI_ID_CANCEL,BUTTON_CF_SHOW );
    BUTTON_SetText(ahButton[2], "Back");
    BUTTON_SetState(ahButton[2],BUTTON_INVERT_DRAW);
    

    BUTTON_SetBitmap(ahButton[0],BUTTON_BI_UNPRESSED,&bmgs_left_unpressed);
    BUTTON_SetBitmap(ahButton[0],BUTTON_BI_PRESSED,&bmgs_left_pressed);
    BUTTON_SetState(ahButton[0],BUTTON_UNDRAW_BORDER);
 
    BUTTON_SetBitmap(ahButton[1],BUTTON_BI_UNPRESSED,&bmgs_right_unpressed);
    BUTTON_SetBitmap(ahButton[1],BUTTON_BI_PRESSED,&bmgs_right_pressed);
    BUTTON_SetState(ahButton[1],BUTTON_UNDRAW_BORDER);
    DrawGroupStatus();
    LCD_Update();
    hmi_set_curr_group(group_id);

    do {
        DrawGroupStatus();
        r= MMM_WaitEvent(WAITEVENT_TIME,NULL,0,GroupStatusCB);

        switch (r) {
            case    ID_GS_BUTTON_LEFT:
                hmi_group_goto_prev();
                break;
            case    ID_GS_BUTTON_RIGHT:
                hmi_group_goto_next();
                break;
            case ID_STATE_NONE:
            case GUI_ID_CANCEL:
            case WAIT_EVENT_TIMEOUT:	
                is_return = 1;
                break;
        }
        
    }while(!is_return);
    BUTTON_Delete(ahButton[0]);
    BUTTON_Delete(ahButton[1]);
    BUTTON_Delete(ahButton[2]);
    return r;
}

