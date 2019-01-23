#include <stdio.h>
#include "GUI.h"
#include "BUTTON.h"
#include "LCD_ConfDefaults.h"      /* valid LCD configuration */
#include "common.h"
#include "hmidata.h"
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

static GUI_CONST_STORAGE GUI_COLOR Colorsgs_right[] = {
     0x000000,0xFFFFFF
};

static GUI_CONST_STORAGE GUI_LOGPALETTE Palgs_right = {
  2,	/* number of entries */
  0, 	/* No transparency */
  &Colorsgs_right[0]
};

static GUI_CONST_STORAGE unsigned char acgs_right[] = {
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXX__, _____XXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXX___XX, XXXXX_XX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XX_XXXXX, XXXXXX__, ________, ________, ___XXXX_,
  XX______, ___XXXXX, X_XXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_XXX_,
  XX_XXXXX, ________, _XXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_XXX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X__XXXX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX____, ________, _XXXXXX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_, XXXXXXXX, XXXXXXX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_, XXXXXXXX, XXXXXXX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_, XXXXXXXX, XXXXXXX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XX_____X, XXXXXXXX, XXXXXXX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_XXXX, XXXXXXXX, XXXXXXX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XXX, XXXXXXXX, XXXXXXX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XXX, XXXXXXXX, XXXXXXX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XX___XXX, XXXXXXXX, XXXXXXX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X_XXXXXX, XXXXXXXX, XXXXXXX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, _XXXXXXX, XXXXXXXX, XXXXXXX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X_XXXXXX, XXXXXXXX, XXXXXXX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X_XXXXXX, XXXXXXXX, XXXXXXX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, __XXXXXX, XXXXXXXX, XXXXXXX_,
  XX_XXXXX, ________, ____XXXX, XXX_____, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XX______, ___XXXXX, XXXX____, ___XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_
};

GUI_CONST_STORAGE GUI_BITMAP bmgs_right = {
  55, /* XSize */
  31, /* YSize */
  7, /* BytesPerLine */
  1, /* BitsPerPixel */
  acgs_right,  /* Pointer to picture data (indices) */
  &Palgs_right  /* Pointer to palette */
};

static GUI_CONST_STORAGE unsigned char acgs_left[] = {
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XX______, _XXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, X_XXXXXX, X___XXXX, XXXXXXXX, XXXXXXX_,
  XXXX____, ________, ________, _XXXXXXX, XXXX_XXX, XXXXXXXX, XXXXXXX_,
  XXX_XXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX_XX, XXXX____, _____XX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXX__, _______X, XXXX_XX_,
  XXX_XXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXX__XX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXX__, ________, ___XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXX_, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXX_, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXX_, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXXX, _____XXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXXX, XXX_XXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXXX, XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXXX, XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXXX, XX___XXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXXX, XXXXX_XX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXXX, XXXXXX_X, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXXX, XXXXX_XX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXXX, XXXXX_XX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXXX, XXXXX__X, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXXX, XXXXXXX_, ____XXXX, XXX_____, _______X, XXXX_XX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX____, ___XXXXX, XXXX____, _____XX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_
};

GUI_CONST_STORAGE GUI_BITMAP bmgs_left = {
  55, /* XSize */
  31, /* YSize */
  7, /* BytesPerLine */
  1, /* BitsPerPixel */
  acgs_left,  /* Pointer to picture data (indices) */
  &Palgs_right/* Pointer to palette */
};

static GUI_CONST_STORAGE unsigned char acgs_valve_open_b[] = {
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, ________, ________, _______X, XXXXXXX_,
  XXXXXX__, XXXXXXXX, XXX_____, ________, _XXXXXX_,
  XXXXX_XX, XXXXXXXX, XXX_____, ________, __XXXXX_,
  XXXX_XXX, XXXXXXXX, XXX_____, ________, ___XXXX_,
  XXX_XXXX, XXXXXXXX, XXX_____, ________, ____XXX_,
  XXX_XXXX, XXXXXXXX, XXX_____, ________, ____XXX_,
  XX_XXXXX, XXXXXXXX, XXX_____, ________, _____XX_,
  XX_XXXXX, XXXXXXXX, XXX_____, ________, _____XX_,
  XX_XXXXX, XXXXXXXX, XXX_____, ________, _____XX_,
  XX_XXXXX, XXXXXXXX, XXX_____, ________, _____XX_,
  XX_XXXXX, XXXXXXXX, XXX_____, ________, _____XX_,
  XX_XXXXX, XXXXXXXX, XXX_____, ________, _____XX_,
  XX_XXXXX, XXXXXXXX, XXX_____, ________, _____XX_,
  XX_XXXXX, XXXXXXXX, XXX_____, ________, _____XX_,
  XX_XXXXX, XXXXXXXX, XXX_____, ________, _____XX_,
  XX_XXXXX, XXXXXXXX, XXX_____, ________, _____XX_,
  XX_XXXXX, XXXXXXXX, XXX_____, ________, _____XX_,
  XX_XXXXX, XXXXXXXX, XXX_____, ________, _____XX_,
  XX_XXXXX, XXXXXXXX, XXX_____, ________, _____XX_,
  XX_XXXXX, XXXXXXXX, XXX_____, ________, _____XX_,
  XXX_XXXX, XXXXXXXX, XXX_____, ________, ____XXX_,
  XXX_XXXX, XXXXXXXX, XXX_____, ________, ____XXX_,
  XXXX_XXX, XXXXXXXX, XXX_____, ________, ___XXXX_,
  XXXXX_XX, XXXXXXXX, XXX_____, ________, __XXXXX_,
  XXXXXX__, XXXXXXXX, XXX_____, ________, _XXXXXX_,
  XXXXXXXX, ________, ________, _______X, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_
};

GUI_CONST_STORAGE GUI_BITMAP bmgs_valve_open_b = {
  39, /* XSize */
  35, /* YSize */
  5, /* BytesPerLine */
  1, /* BitsPerPixel */
  acgs_valve_open_b,  /* Pointer to picture data (indices) */
  &Palgs_right  /* Pointer to palette */
};
static GUI_CONST_STORAGE unsigned char acgs_valve_open_ab[] = {
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, ________, ________, _______X, XXXXXXX_,
  XXXXXX__, ________, ________, ________, _XXXXXX_,
  XXXXX___, ________, ________, ________, __XXXXX_,
  XXXX____, ________, ________, ________, ___XXXX_,
  XXX_____, ________, ________, ________, ____XXX_,
  XXX_____, ________, ________, ________, ____XXX_,
  XX______, ________, ________, ________, _____XX_,
  XX______, ________, ________, ________, _____XX_,
  XX______, ________, ________, ________, _____XX_,
  XX______, ________, ________, ________, _____XX_,
  XX______, ________, ________, ________, _____XX_,
  XX______, ________, ________, ________, _____XX_,
  XX______, ________, ________, ________, _____XX_,
  XX______, ________, ________, ________, _____XX_,
  XX______, ________, ________, ________, _____XX_,
  XX______, ________, ________, ________, _____XX_,
  XX______, ________, ________, ________, _____XX_,
  XX______, ________, ________, ________, _____XX_,
  XX______, ________, ________, ________, _____XX_,
  XX______, ________, ________, ________, _____XX_,
  XXX_____, ________, ________, ________, ____XXX_,
  XXX_____, ________, ________, ________, ____XXX_,
  XXXX____, ________, ________, ________, ___XXXX_,
  XXXXX___, ________, ________, ________, __XXXXX_,
  XXXXXX__, ________, ________, ________, _XXXXXX_,
  XXXXXXXX, ________, ________, _______X, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_
};

GUI_CONST_STORAGE GUI_BITMAP bmgs_valve_open_ab = {
  39, /* XSize */
  35, /* YSize */
  5, /* BytesPerLine */
  1, /* BitsPerPixel */
  acgs_valve_open_ab,  /* Pointer to picture data (indices) */
  &Palgs_right  /* Pointer to palette */
};

static GUI_CONST_STORAGE unsigned char acgs_valve_open_a[] = {
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, ________, ________, _______X, XXXXXXX_,
  XXXXXX__, ________, ____XXXX, XXXXXXX_, _XXXXXX_,
  XXXXX___, ________, ____XXXX, XXXXXXXX, X_XXXXX_,
  XXXX____, ________, ____XXXX, XXXXXXXX, XX_XXXX_,
  XXX_____, ________, ____XXXX, XXXXXXXX, XXX_XXX_,
  XXX_____, ________, ____XXXX, XXXXXXXX, XXX_XXX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XXX_____, ________, ____XXXX, XXXXXXXX, XXX_XXX_,
  XXX_____, ________, ____XXXX, XXXXXXXX, XXX_XXX_,
  XXXX____, ________, ____XXXX, XXXXXXXX, XX_XXXX_,
  XXXXX___, ________, ____XXXX, XXXXXXXX, X_XXXXX_,
  XXXXXX__, ________, ____XXXX, XXXXXXX_, _XXXXXX_,
  XXXXXXXX, ________, ________, _______X, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_
};

GUI_CONST_STORAGE GUI_BITMAP bmgs_valve_open_a = {
  39, /* XSize */
  35, /* YSize */
  5, /* BytesPerLine */
  1, /* BitsPerPixel */
  acgs_valve_open_a,  /* Pointer to picture data (indices) */
  &Palgs_right  /* Pointer to palette */
};

static GUI_CONST_STORAGE unsigned char acgs_valve_close[] = {
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, ________, ________, _______X, XXXXXXX_,
  XXXXXX__, XXXXXXXX, XXXXXXXX, XXXXXXX_, _XXXXXX_,
  XXXXX_XX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X_XXXXX_,
  XXXX_XXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XX_XXXX_,
  XXX_XXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_XXX_,
  XXX_XXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_XXX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXX_XXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_XXX_,
  XXX_XXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_XXX_,
  XXXX_XXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XX_XXXX_,
  XXXXX_XX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X_XXXXX_,
  XXXXXX__, XXXXXXXX, XXXXXXXX, XXXXXXX_, _XXXXXX_,
  XXXXXXXX, ________, ________, _______X, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_
};

GUI_CONST_STORAGE GUI_BITMAP bmgs_valve_close = {
  39, /* XSize */
  35, /* YSize */
  5, /* BytesPerLine */
  1, /* BitsPerPixel */
  acgs_valve_close,  /* Pointer to picture data (indices) */
  &Palgs_right  /* Pointer to palette */
};
extern eValveStatus gGroupValveStatus[VALVE_MAX_GROUP][GROUP_VALVE_NUM];

static void DrawGroupStatus(void)
{
    GUI_RECT rText,r;/*= {0, 80, XSize, 120};*/
    char buff[16];
    int i;
    int group_id = hmi_get_curr_group();
    const GUI_BITMAP* pBmp;
    LCD_DRAWMODE OldDrawMode;
    GUI_COLOR color_l;
    GUI_COLOR color_r;
    
    rText.x0=bmgs_right.XSize + 1;
    rText.y0=1;
    rText.x1=LCD_XSIZE - bmgs_left.XSize -2;
    rText.y1=bmgs_right.YSize +1;
    
    GUI_SetFont(&GUI_Font24_ASCII);
    
    GUI_SetColor(GUI_BLACK);
    sprintf(buff,"G%d Status",group_id+1);
    GUI_DispStringInRect(buff, &rText, GUI_TA_HCENTER | GUI_TA_VCENTER);
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
    static U32 i=0xFFFF;
    i--;
    if(i==0)
    {
       GetGroupValveStatus(hmi_get_curr_group());
       DrawGroupStatus();
       i= 0xFFFF;
    }
}

void _GroupState(int group_id) {
    int is_return =0,r;
    
    BUTTON_Handle ahButton[3];
    //g_cur_group = id;
    
    GUI_SetBkColor(GUI_WHITE);
    GUI_Clear();
    GUI_SetColor(GUI_BLACK);

    GUI_DrawRect(0,0,LCD_XSIZE-1,LCD_YSIZE-1);
    GUI_DrawHLine(GROUPSTATE_TITLE_HEIGHT,0,LCD_XSIZE-1);

    ahButton[0] =  BUTTON_Create( 1, 
                                                           1, 
                                                            bmgs_left.XSize, 
                                                            bmgs_left.YSize, 
                                                            ID_GS_BUTTON_LEFT, BUTTON_CF_SHOW );

    ahButton[1] =  BUTTON_Create( LCD_XSIZE -1 - bmgs_right.XSize, 
                                                           1, 
                                                            bmgs_right.XSize, 
                                                            bmgs_right.YSize, 
                                                            ID_GS_BUTTON_RIGHT, BUTTON_CF_SHOW );
    ahButton[2] = BUTTON_Create( 0, LCD_YSIZE -35, 80, 35, GUI_ID_CANCEL,BUTTON_CF_SHOW );
    BUTTON_SetText(ahButton[2], "Back");

    BUTTON_SetBitmap(ahButton[0],BUTTON_BI_UNPRESSED,&bmgs_left);
    BUTTON_SetBitmap(ahButton[0],BUTTON_BI_PRESSED,&bmgs_left);
    BUTTON_SetState(ahButton[0],BUTTON_UNDRAW_BORDER);
 
    BUTTON_SetBitmap(ahButton[1],BUTTON_BI_UNPRESSED,&bmgs_right);
    BUTTON_SetBitmap(ahButton[1],BUTTON_BI_PRESSED,&bmgs_right);
    BUTTON_SetState(ahButton[1],BUTTON_UNDRAW_BORDER);
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
                is_return = 1;
                break;
        }
        
    }while(!is_return);
    BUTTON_Delete(ahButton[0]);
    BUTTON_Delete(ahButton[1]);
    BUTTON_Delete(ahButton[2]);

}

