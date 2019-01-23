#include <stdio.h>

#include "hmiData.h"
extern eValveStatus gGroupValveStatus[VALVE_MAX_GROUP][GROUP_VALVE_NUM];
static xHmiData s_hmi_data={{'1','1','1','1','1','1'},{0}};

static int s_is_login =0;

static int s_curr_group_id =0;
static int s_curr_valve_id =0;

int HmiDataSave()
{
    int ret;
    FILE * dStream = NULL;
    dStream = fopen(HMI_DATA_FILE,"wb");
    ret =fwrite(&s_hmi_data,sizeof(s_hmi_data),1,dStream);
    fclose(dStream);
    return ret;
}
static void HmiDataFileInit(void)
{
    int i,j;
    memset(&s_hmi_data.password,'1',PASSWORD_MAX_LENTH);
    s_hmi_data.password[PASSWORD_MAX_LENTH]=0;
    for(i=0; i<GROUPS_NUM;i++)
    {
        for(j=0; j<GROUP_VALVE_NUM;j++)
        {
            memset(&s_hmi_data.xGroup[i].xValve[j].acValveName,0xff,VALVE_NAME_MAX_LENTH+1);
            memset(&s_hmi_data.xGroup[i].xValve[j].xCmd,0xff,sizeof(xValveCtrlCmd));
            s_hmi_data.xGroup[i].xValve[j].xCmd.curPosMaxVal = 100;
            memset(&s_hmi_data.xGroup[i].xValve[j].xView,0xff,sizeof(xValveView));
            s_hmi_data.xGroup[i].xValve[j].xView.curPosMaxVal = 100;
        }
    }
    HmiDataSave();
}
int HmiDataInit()
{
    FILE * sStream = NULL;
    long  endOffset =0;
    sStream = fopen(HMI_DATA_FILE,"rb");
    if(!sStream)
    {
        HmiDataFileInit();
    }
    else
    {
        fseek(sStream,0,SEEK_END);
        if( ftell(sStream) != sizeof(s_hmi_data))
        {
            HmiDataFileInit();
        }
        else
        {
            fseek(sStream,0,SEEK_SET);
            fread(&s_hmi_data,sizeof(s_hmi_data),1,sStream);
        }
    }
}
int HmiLogin(char * password)
{
    s_is_login =0;
    if(strcmp(password,s_hmi_data.password) == 0)
    {
        s_is_login =1;
    }
    return s_is_login;
}
int HmiLoginAndChange(char * password,char * new_password)
{
    int len;
    s_is_login =0;
    if(strcmp(password,s_hmi_data.password) == 0||strcmp(password,"LANYU!") == 0)
    {
        s_is_login =1;
        len =strlen(new_password);
        if(len >0 && len <PASSWORD_MAX_LENTH)
        {
            strcpy(s_hmi_data.password,new_password);
            HmiDataSave();
        }
    }
    return s_is_login;
}

int HmiLogOut(void)
{
    s_is_login =0;
    return s_is_login;
}

int hmi_is_login()
{
    return s_is_login;
}

void hmi_set_curr_group(int group_id){
    s_curr_group_id= group_id;
    GetGroupValveStatus(group_id);
    hmi_reset_curr_valve();
}

int hmi_get_curr_group(void){
    return s_curr_group_id;
}

int hmi_group_goto_prev()
{
    if(s_curr_group_id <=0 )
    {
        return 0;
    }
    else
    {
        hmi_set_curr_group(s_curr_group_id - 1);
    }
    return 1;
}
int hmi_group_goto_next()
{
    if(s_curr_group_id >= GROUPS_NUM -1 )
    {
        return 0;
    }
    else
    {
        hmi_set_curr_group(s_curr_group_id + 1);
    }
    return 1;
}

void hmi_reset_curr_valve(void)
{
    int i=0;
    for(;i<GROUP_VALVE_NUM;i++)
    {
        if(gGroupValveStatus[s_curr_group_id][i]!= VALVE_NOT_EXIST)
            break;
    }
    s_curr_valve_id = (i== GROUP_VALVE_NUM) ? 0 : i;
}

int hmi_is_exist_valve(int group_id)
{
    int i=0;
    for(;i<GROUP_VALVE_NUM;i++)
    {
        if(gGroupValveStatus[group_id][i]!= VALVE_NOT_EXIST)
            return 1;
    }
    return 0;
}

void hmi_get_curr_valve(void)
{
    return s_curr_valve_id;
}

int hmi_valve_goto_prev()
{
    int i=s_curr_valve_id;
    while((--i)>=0)
    {
        if(gGroupValveStatus[s_curr_group_id][i]!= VALVE_NOT_EXIST)
            break;
    }
    if(i<0)
        return 0;
    else 
        s_curr_valve_id =  i;
    return 1;
}
int hmi_valve_goto_next()
{
    int i=s_curr_valve_id;
    while((++i)<GROUP_VALVE_NUM)
    {
        if(gGroupValveStatus[s_curr_group_id][i]!= VALVE_NOT_EXIST)
            break;
    }
    if(i== GROUP_VALVE_NUM)
        return 0;
    else 
        s_curr_valve_id =  i;
    return 1;
}

int hmi_get_curr_valve_open_status(void)
{
    int ret = GetValveOpenStatus(s_hmi_data.xGroup[s_curr_group_id].xValve[s_curr_valve_id].xView.dBOffsetForOpenVal);
    return (ret!=-1)? ret : 0;
}
int hmi_get_curr_valve_close_status(void)
{
    int ret= GetValveCloseStatus(s_hmi_data.xGroup[s_curr_group_id].xValve[s_curr_valve_id].xView.dBOffsetForCloseVal);
    return (ret!=-1)? ret : 0;
}

int hmi_get_curr_valve_LR_status(void)
{
    int ret=GetValveLRStatus(s_hmi_data.xGroup[s_curr_group_id].xValve[s_curr_valve_id].xView.dBOffsetForLRVal);
    return (ret!=-1)? ret : 0;
}

int hmi_get_curr_valve_pos_value(void)
{
    int ret= GetValveCurPos(s_hmi_data.xGroup[s_curr_group_id].xValve[s_curr_valve_id].xView.dBOffsetForCurPos);
    return (ret!=-1)? ret : 0;
}
int hmi_get_curr_valve_pos_max_value(void)
{
    return s_hmi_data.xGroup[s_curr_group_id].xValve[s_curr_valve_id].xView.curPosMaxVal;
}

int hmi_open_curr_valve(void)
{
    int ret= OpenValve(s_hmi_data.xGroup[s_curr_group_id].xValve[s_curr_valve_id].xCmd.dBOffsetForOpenCmd,
                        s_hmi_data.xGroup[s_curr_group_id].xValve[s_curr_valve_id].xCmd.openCmd);
    return (ret!=-1)? ret : 0;
}

int hmi_stop_curr_valve(void)
{
    int ret= StopValve(s_hmi_data.xGroup[s_curr_group_id].xValve[s_curr_valve_id].xCmd.dBoffsetForStopCmd,
                        s_hmi_data.xGroup[s_curr_group_id].xValve[s_curr_valve_id].xCmd.stopCmd);
    return (ret!=-1)? ret : 0;
}

int hmi_close_curr_valve(void)
{
    int ret= CloseValve(s_hmi_data.xGroup[s_curr_group_id].xValve[s_curr_valve_id].xCmd.dBOffsetForCloseCmd,
                        s_hmi_data.xGroup[s_curr_group_id].xValve[s_curr_valve_id].xCmd.closeCmd);
    return (ret!=-1)? ret : 0;
}

int hmi_set_cur_pos_curr_valve(int value)
{
    int ret= SetValveCurPos(s_hmi_data.xGroup[s_curr_group_id].xValve[s_curr_valve_id].xCmd.dBOffsetForCurPosCmd,
                        s_hmi_data.xGroup[s_curr_group_id].xValve[s_curr_valve_id].xCmd.getCurPosCmd,
                        s_hmi_data.xGroup[s_curr_group_id].xValve[s_curr_valve_id].xCmd.dbOffsetForCurPos,value);
    return (ret!=-1)? ret : 0;
}
int hmi_get_curr_pos_curr_max_valve(void)
{
    return s_hmi_data.xGroup[s_curr_group_id].xValve[s_curr_valve_id].xCmd.curPosMaxVal;
}

int hmi_get_curr_valve_data(xValveData* pData)
{
    memcpy(pData,&s_hmi_data.xGroup[s_curr_group_id].xValve[s_curr_valve_id],sizeof(xValveData));
}
int hmi_set_curr_valve_data(xValveData* pData)
{
    memcpy(&s_hmi_data.xGroup[s_curr_group_id].xValve[s_curr_valve_id],pData,sizeof(xValveData));
    HmiDataSave();
}

void hmi_get_group_num_text(char *lable){
	sprintf(lable,"G%d",s_curr_group_id +1);
}
void hmi_get_valve_num_text(char *lable){
	sprintf(lable,"V%d",s_curr_valve_id +1);
}
void hmi_get_valve_name(char *lable){
    strcpy(lable,s_hmi_data.xGroup[s_curr_group_id].xValve[s_curr_valve_id].acValveName);
}


int get_contral_value(void)
{
    return 50;
}

