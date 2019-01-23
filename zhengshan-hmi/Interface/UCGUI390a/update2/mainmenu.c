#include "GUI.h"
#include "BUTTON.h"
#include "LCD_ConfDefaults.h"      /* valid LCD configuration */
#include "common.h"
#include "hmiData.h"

#define MAINMENU_TITLE_HEIGHT 55
#define MAINMENU_ROW_HEIGHT ((LCD_YSIZE - MAINMENU_TITLE_HEIGHT)/GROUPS_NUM - MAINMENU_ROW_GAP)
#define MAINMENU_ROW_GAP       4
#define MAINMENU_LABLE_WIDTH 99

void _MainMenu(void) {
    do {
    int i,r;
    GUI_RECT rText;/*= {0, 80, XSize, 120};*/
    BUTTON_Handle ahGsButton[GROUPS_NUM];
    BUTTON_Handle ahVvButton[GROUPS_NUM];
    
    GUI_SetBkColor(GUI_WHITE);
    GUI_Clear();
    GUI_SetColor(GUI_BLACK);
    
    GUI_DrawRect(0,0,LCD_XSIZE-1,LCD_YSIZE-1);
    GUI_DrawHLine(MAINMENU_TITLE_HEIGHT,0,LCD_XSIZE-1);
    
    rText.x0=MAINMENU_ROW_GAP;
    rText.y0=MAINMENU_ROW_GAP;
    rText.x1=LCD_XSIZE - MAINMENU_ROW_GAP;
    rText.y1=MAINMENU_TITLE_HEIGHT -MAINMENU_ROW_GAP;
    
    GUI_SetFont(&GUI_Font32B_ASCII);
    GUI_DispStringInRect("Main Menu", &rText, GUI_TA_HCENTER | GUI_TA_VCENTER);

    GUI_SetFont(&GUI_Font24_ASCII);

    for(i=0; i<GROUPS_NUM; i++){
        GUI_DrawHLine(MAINMENU_TITLE_HEIGHT + MAINMENU_ROW_GAP * (i+1) + MAINMENU_ROW_HEIGHT*i,0,319);
        GUI_DrawHLine(MAINMENU_TITLE_HEIGHT + MAINMENU_ROW_GAP * (i+1) + MAINMENU_ROW_HEIGHT *(i+1),0,319);
        GUI_DrawVLine(MAINMENU_LABLE_WIDTH, MAINMENU_TITLE_HEIGHT + MAINMENU_ROW_GAP * (i+1) + MAINMENU_ROW_HEIGHT*i, MAINMENU_TITLE_HEIGHT + MAINMENU_ROW_GAP * (i+1) + MAINMENU_ROW_HEIGHT *(i+1));
        ahGsButton[i] =  BUTTON_Create( MAINMENU_LABLE_WIDTH + MAINMENU_ROW_GAP, 
                                                                MAINMENU_TITLE_HEIGHT + MAINMENU_ROW_GAP * (i+1) + MAINMENU_ROW_HEIGHT*i + MAINMENU_ROW_GAP/2, 
                                                                (LCD_XSIZE -MAINMENU_LABLE_WIDTH - MAINMENU_ROW_GAP - MAINMENU_ROW_GAP)/2 , 
                                                                MAINMENU_ROW_HEIGHT -MAINMENU_ROW_GAP , 
                                                                ID_MM_GROUP_STATE_1 +i, BUTTON_CF_SHOW );
        ahVvButton[i] =  BUTTON_Create( MAINMENU_LABLE_WIDTH + (LCD_XSIZE -MAINMENU_LABLE_WIDTH)/2 + 1, 
                                                                MAINMENU_TITLE_HEIGHT + MAINMENU_ROW_GAP * (i+1) + MAINMENU_ROW_HEIGHT*i + MAINMENU_ROW_GAP/2, 
                                                                (LCD_XSIZE -MAINMENU_LABLE_WIDTH - MAINMENU_ROW_GAP - MAINMENU_ROW_GAP)/2, 
                                                                MAINMENU_ROW_HEIGHT -MAINMENU_ROW_GAP ,
                                                                ID_MM_VALVE_VIEW_1 +i, BUTTON_CF_SHOW );
        BUTTON_SetText (ahGsButton[i], "Group State");
        BUTTON_SetText (ahVvButton[i], "Valve View");

        GUI_GotoXY(MAINMENU_ROW_GAP,MAINMENU_TITLE_HEIGHT + MAINMENU_ROW_GAP * (i+2) + MAINMENU_ROW_HEIGHT*i);
        GUI_DispString("Group ");
        GUI_DispDec(i+1,1);

    }

    r = MMM_WaitKey(4000);
    for (i=0; i< GROUPS_NUM; i++) {
      BUTTON_Delete(ahGsButton[i]);
      BUTTON_Delete(ahVvButton[i]);
    }

    switch (r) {
        case    ID_MM_GROUP_STATE_1:
        case    ID_MM_GROUP_STATE_2:
        case    ID_MM_GROUP_STATE_3:
        case    ID_MM_GROUP_STATE_4:
             _GroupState(r- ID_MM_GROUP_STATE_1);
             break;
        case    ID_MM_VALVE_VIEW_1:
        case    ID_MM_VALVE_VIEW_2:
        case    ID_MM_VALVE_VIEW_3:
        case    ID_MM_VALVE_VIEW_4:
            _ValveView(r- ID_MM_VALVE_VIEW_1);
            break;
        case ID_STATE_NONE:
            return;
    //case ID_MAINMENU:  r= _MainMenu(); break;
    //case ID_CALIBRATE: _ExecCalibration(); break;
    }
    }while(1);

}
