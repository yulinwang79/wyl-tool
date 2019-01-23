#include "GUI.h"
#include "BUTTON.h"
#include "EDIT.h"
#include "LCD_ConfDefaults.h"      /* valid LCD configuration */
#include "common.h"
#include "hmiData.h"

#define VVEDIT_TITLE_HEIGHT 33

#define VVEDIT_EDIT_TOP       37
#define VVEDIT_EDIT_HEIGHT 25

#define VVEDIT_VK_TOP          65
#define VVEDITNUM_VK_TOP          80

#define VVEDIT_BUTTON_WIDTH 80
#define VVEDIT_BUTTON_HEIGHT 35

#define ID_BUTTON_KEY_CAPS 0XFE
#define ID_BUTTON_KEY_DEL 0XFD
static const char _acUpText[] = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-',
                                      'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P','+',
                                      'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', ';','{',
                                      'Z', 'X', 'C', 'V', 'B', 'N', 'M', ',','.','/','}',
                                      '`','\\',' '
};

static const char _acLowText[] = { '!', '@', '#', '$', '%','^','&','*','(',')','_',
                                      'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p','=',
                                      'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', ':','[',
                                      'z', 'x', 'c', 'v', 'b', 'n', 'm', '<','>','?',']',
                                      '~','|',' '
};
static const char _acNum[] = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
};

int _VVEdit(const char* title,char * text_buf,int max_len) {
  int i;
  int Key;
  BUTTON_Handle ahButton[countof(_acUpText)];
  BUTTON_Handle hButtonESC;
  BUTTON_Handle hButtonCAPS;
  BUTTON_Handle hButtonOK;
  BUTTON_Handle hButtonDEL;
  EDIT_Handle   hEdit;
  int is_Caps =1;

  GUI_RECT rText = {1,1, LCD_XSIZE-2, VVEDIT_TITLE_HEIGHT-1};
  GUI_SetBkColor(GUI_WHITE);
  GUI_Clear();
  GUI_SetColor(GUI_BLACK);
  GUI_DrawRect(0,0,LCD_XSIZE-1,LCD_YSIZE-1);
  GUI_DrawHLine(VVEDIT_TITLE_HEIGHT,0,LCD_XSIZE-1);
  
  if(title){
      GUI_SetFont(&GUI_Font24B_ASCII);

      GUI_DispStringInRect(title, &rText, GUI_TA_HCENTER | GUI_TA_VCENTER);
  }

  /* Create Keyboard Buttons */
  for (i=0; i< countof(_acUpText) - 1; i++) {

    int x0 = 5  + 28*(i%11);
    int y0 = VVEDIT_VK_TOP + 28*(i/11);
    char c = _acUpText[i];
    int Id = c ? c : 1;
    ahButton[i] = BUTTON_Create( x0, y0, 25, 25, Id,BUTTON_CF_SHOW );
    #if GUI_SUPPORT_MEMDEV
      BUTTON_EnableMemdev(ahButton[i]);
    #endif
    BUTTON_SetState(ahButton[i],BUTTON_INVERT_DRAW);
  }
  ahButton[i] = BUTTON_Create( 5  + 28*(i%11), VVEDIT_VK_TOP + 28*(i/11), 109, 25, ' ',BUTTON_CF_SHOW );
  BUTTON_SetState(ahButton[i],BUTTON_INVERT_DRAW);
  hButtonCAPS= BUTTON_Create( 5  + 28*(i%11) + 109+3, VVEDIT_VK_TOP + 28*(i/11), 53, 25, ID_BUTTON_KEY_CAPS,BUTTON_CF_SHOW );
  BUTTON_SetText(hButtonCAPS, "CAPS");
  BUTTON_SetState(hButtonCAPS,BUTTON_INVERT_DRAW);

  hButtonDEL= BUTTON_Create( 5  + 28*(i%11) + 109+3 + 56, VVEDIT_VK_TOP + 28*(i/11), 53, 25, ID_BUTTON_KEY_DEL,BUTTON_CF_SHOW );
  BUTTON_SetText(hButtonDEL, "DEL");
  BUTTON_SetState(hButtonDEL,BUTTON_INVERT_DRAW);
  
  hButtonESC = BUTTON_Create( 0, LCD_YSIZE -VVEDIT_BUTTON_HEIGHT, VVEDIT_BUTTON_WIDTH, VVEDIT_BUTTON_HEIGHT, GUI_ID_CANCEL,BUTTON_CF_SHOW );
  BUTTON_SetText(hButtonESC, "ESC");
  BUTTON_SetState(hButtonESC,BUTTON_INVERT_DRAW);

  hButtonOK = BUTTON_Create( LCD_XSIZE -VVEDIT_BUTTON_WIDTH , LCD_YSIZE -VVEDIT_BUTTON_HEIGHT, VVEDIT_BUTTON_WIDTH, VVEDIT_BUTTON_HEIGHT, GUI_ID_OK,BUTTON_CF_SHOW );
  BUTTON_SetText(hButtonOK, "OK");
  BUTTON_SetState(hButtonOK,BUTTON_INVERT_DRAW);


  hEdit = EDIT_Create( 5, VVEDIT_EDIT_TOP, 310, VVEDIT_EDIT_HEIGHT, ' ', max_len, 0 );
  GUI_DrawRect(6, VVEDIT_EDIT_TOP+1,316,VVEDIT_EDIT_TOP + VVEDIT_EDIT_HEIGHT);
  EDIT_SetFont(hEdit, &GUI_Font8x16);
  EDIT_SetText(hEdit,text_buf);
  /* Handle Keyboard until ESC or ENTER is pressed */
  do {
    
    char ac[2] = {0};
    char *s= ac;
    for (i=0; i< countof(_acUpText); i++) {
        if(is_Caps)
            ac[0]= _acUpText[i];
        else
            ac[0]= _acLowText[i];
        BUTTON_SetText   (ahButton[i], s);
    }
    Key = MMM_WaitEvent(WAITEVENT_TIME,NULL,0,NULL);
    switch (Key) {
    case 0:
    case GUI_ID_CANCEL:
        Key =0;
     break;

    case GUI_ID_OK:
        EDIT_GetText(hEdit,text_buf,TEXT_MAX_LENTH);
      break;

    case ID_BUTTON_KEY_CAPS:
        is_Caps = !is_Caps;
        break;
    case ID_BUTTON_KEY_DEL:
        EDIT_AddKey(hEdit, GUI_KEY_BACKSPACE);
        break;

    default:
        for (i=0; i< countof(_acUpText); i++) {
            if(Key == _acUpText[i])
            {
                if(is_Caps)
                    ac[0]= _acUpText[i];
                else
                    ac[0]= _acLowText[i];
                EDIT_AddKey(hEdit, ac[0]);
            }
        }
        
    }
  } while ( (Key!=GUI_ID_OK) &&(Key!=GUI_ID_CANCEL) && (Key!=0));
  /* Cleanup */
  for (i=0; i< countof(ahButton); i++) {
    BUTTON_Delete(ahButton[i]);
  }
  BUTTON_Delete(hButtonCAPS);
  BUTTON_Delete(hButtonDEL);
  BUTTON_Delete(hButtonESC);
  BUTTON_Delete(hButtonOK);
  EDIT_Delete(hEdit);
  return Key;
}

int _VVEditNum(const char* title,char * text_buf,int max_len) {
  int i;
  int Key;
  BUTTON_Handle ahButton[countof(_acNum)];
  BUTTON_Handle hButtonESC;
  BUTTON_Handle hButtonOK;
  BUTTON_Handle hButtonDEL;
  EDIT_Handle   hEdit;
  int is_Caps =1;
  char ac[2] = {0};

  GUI_RECT rText = {1,1, LCD_XSIZE-2, VVEDIT_TITLE_HEIGHT-2};
  GUI_SetBkColor(GUI_WHITE);
  GUI_Clear();
  GUI_SetColor(GUI_BLACK);
  GUI_DrawRect(0,0,LCD_XSIZE-1,LCD_YSIZE-1);
  GUI_DrawHLine(VVEDIT_TITLE_HEIGHT,0,LCD_XSIZE-1);
  
  if(title){
      GUI_SetFont(&GUI_Font24B_ASCII);

      GUI_DispStringInRect(title, &rText, GUI_TA_HCENTER | GUI_TA_VCENTER);
  }

  /* Create Keyboard Buttons */
  for (i=0; i< countof(_acNum); i++) {
    int x0 = 10  + 50*(i%5);
    int y0 = VVEDITNUM_VK_TOP + 50*(i/5);
    char c = _acNum[i];
    int Id = c ? c : 1;
    ac[0] = c;
    ahButton[i] = BUTTON_Create( x0, y0, 46, 46, c,BUTTON_CF_SHOW );
    BUTTON_SetText   (ahButton[i], ac);
    #if GUI_SUPPORT_MEMDEV
      BUTTON_EnableMemdev(ahButton[i]);
    #endif
    BUTTON_SetState(ahButton[i],BUTTON_INVERT_DRAW);
  }

  hButtonDEL= BUTTON_Create( 10  + 50*(5), VVEDITNUM_VK_TOP, 46, 46, ID_BUTTON_KEY_DEL,BUTTON_CF_SHOW );
  BUTTON_SetText(hButtonDEL, "DEL");
  BUTTON_SetState(hButtonDEL,BUTTON_INVERT_DRAW);
  
  hButtonESC = BUTTON_Create( 0, LCD_YSIZE -VVEDIT_BUTTON_HEIGHT, VVEDIT_BUTTON_WIDTH, VVEDIT_BUTTON_HEIGHT, GUI_ID_CANCEL,BUTTON_CF_SHOW );
  BUTTON_SetText(hButtonESC, "ESC");
  BUTTON_SetState(hButtonESC,BUTTON_INVERT_DRAW);

  hButtonOK = BUTTON_Create( LCD_XSIZE -VVEDIT_BUTTON_WIDTH , LCD_YSIZE -VVEDIT_BUTTON_HEIGHT, VVEDIT_BUTTON_WIDTH, VVEDIT_BUTTON_HEIGHT, GUI_ID_OK,BUTTON_CF_SHOW );
  BUTTON_SetText(hButtonOK, "OK");
  BUTTON_SetState(hButtonOK,BUTTON_INVERT_DRAW);


  hEdit = EDIT_Create( 10, VVEDIT_EDIT_TOP, LCD_XSIZE - 20, 40, ' ', max_len, 0 );
  GUI_DrawRect(11, VVEDIT_EDIT_TOP+1,LCD_XSIZE -10,VVEDIT_EDIT_TOP + 40);
  EDIT_SetFont(hEdit, &GUI_FontD24x32);
  EDIT_SetText(hEdit,text_buf);
  /* Handle Keyboard until ESC or ENTER is pressed */
  do {
    
    Key = MMM_WaitEvent(WAITEVENT_TIME,NULL,0,NULL);
    switch (Key) {
    case 0:
    case GUI_ID_CANCEL:
        Key =0;
     break;

    case GUI_ID_OK:
        EDIT_GetText(hEdit,text_buf,TEXT_MAX_LENTH);
      break;

    case ID_BUTTON_KEY_DEL:
        EDIT_AddKey(hEdit, GUI_KEY_BACKSPACE);
        break;

    default:
        if(Key>='0' && Key <='9')
                EDIT_AddKey(hEdit, Key);
    }
        
  } while ( (Key!=GUI_ID_OK) &&(Key!=GUI_ID_CANCEL) && (Key!=0));
  /* Cleanup */
  for (i=0; i< countof(ahButton); i++) {
    BUTTON_Delete(ahButton[i]);
  }
  BUTTON_Delete(hButtonDEL);
  BUTTON_Delete(hButtonESC);
  BUTTON_Delete(hButtonOK);
  EDIT_Delete(hEdit);
  return Key;
}

