#ifndef _MMM_COMMON_H_
#define _MMM_COMMON_H_

#ifndef GUI_CONST_STORAGE
  #define GUI_CONST_STORAGE const
#endif

#ifndef NULL
    #define NULL 0
#endif
#define countof(Obj) (sizeof(Obj)/sizeof(Obj[0]))

#define WAITEVENT_TIME  (15*60*1000)

typedef enum{
    THOUCH_NONE,
    THOUCH_CLICK,
    THOUCH_SLIDE,    
}eThouchEvent;

enum{
    ID_STATE_NONE,

    /*main menu*/
    ID_MM_GROUP_STATE_1,
    ID_MM_GROUP_STATE_2,
    ID_MM_GROUP_STATE_3,
    ID_MM_GROUP_STATE_4,
    ID_MM_VALVE_VIEW_1,
    ID_MM_VALVE_VIEW_2,
    ID_MM_VALVE_VIEW_3,
    ID_MM_VALVE_VIEW_4,
        
    /*Group State*/
    ID_GS_BUTTON_LEFT,
    ID_GS_BUTTON_RIGHT,

    /*Valve View*/
    ID_VV_BUTTON_LEFT,
    ID_VV_BUTTON_RIGHT,
    ID_VV_VIEW_OPEN,
    ID_VV_VIEW_R,
    ID_VV_VIEW_CLOSE,
    ID_VV_CONTRAL_OPEN,
    ID_VV_CONTRAL_STOP,
    ID_VV_CONTRAL_CLOSE,
    ID_VV_CONTRAL_SPOS,
    ID_VV_CONTRAL_SPOS_VALUE,
    ID_VV_LOGON,
    ID_VV_SET_VALVE,
    
    /*Set Valve*/
    ID_SV_ENTER,
    ID_SV_CONTRAL_OPEN_ADDR,
    ID_SV_CONTRAL_OPEN_CMD,
    ID_SV_CONTRAL_CLOSE_ADDR,
    ID_SV_CONTRAL_CLOSE_CMD,
    ID_SV_CONTRAL_STOP_ADDR,
    ID_SV_CONTRAL_STOP_CMD,
    ID_SV_CONTRAL_POSC_ADDR,
    ID_SV_CONTRAL_POSC_CMD,
    ID_SV_CONTRAL_POS_ADDR,
    ID_SV_CONTRAL_POS_MAX_VAL,

    ID_SV_VIEW_OPEN_ADDR,
    ID_SV_VIEW_CLOSE_ADDR,
    ID_SV_VIEW_LR_ADDR,
    ID_SV_VIEW_POS_ADDR,
    ID_SV_VIEW_POS_MAX_VAL,

    ID_SV_SET_NAME,

    /*Logon*/
    ID_LOGON_PASSWORD,
    ID_LOGON_NEWPASSWORD,
};

typedef void (*GetLablePtr) (char* lable);
typedef void (*FuncPtr) (void);
typedef int (*FuncPtrResult) (void);
 
typedef struct
{
    int contral_id;
    eThouchEvent thouchEvent;
    GUI_RECT rect;
    const GUI_FONT* font;
    GetLablePtr getLable;
    const char * text;
}t_lable_info;

typedef struct
{
    int button_id;
    GUI_BITMAP* pressed_bitmap;
    GUI_BITMAP* unpressed_bitmap;
    GUI_BITMAP* disable_bitmap;
    GUI_RECT rect;
    FuncPtrResult IsEnableFunc;
}t_button_info;

void MMM_DrawLable(t_lable_info* lableArray,int lableNum);
int MMM_WaitKey(U32 time) ;
int MMM_WaitEvent(U32 time,t_lable_info* contralArray,int contralNum,FuncPtr cb);

int _login(void);
void _SetValve(int group_id, int valve_id);
int _VVEdit(const char* title,char * text_buf,int max_len) ;
int _VVEditNum(const char* title,char * text_buf,int max_len);

void _GroupState(int id);
void _ValveView(int id);
void _MainMenu(void);
void _ExecCalibration(void);

extern GUI_CONST_STORAGE GUI_COLOR ColorsPalette[];

#endif //_MMM_COMMON_H_
