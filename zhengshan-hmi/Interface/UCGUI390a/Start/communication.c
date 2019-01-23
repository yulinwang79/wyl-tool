#include "hmidata.h"

eValveStatus gGroupValveStatus[VALVE_MAX_GROUP][GROUP_VALVE_NUM];

/*
	��ȡָ����·�����з���״̬
	����:
		group_num:��·��
	����ֵ:
		-1:       ͨ��ʧ��
		����:ͨ�ųɹ�
*/
int GetGroupValveStatus(eValveGroup group_num)
{
    #ifdef WIN32 
        int i;
        for(i=0; i< GROUP_VALVE_NUM;i++)
        {
            gGroupValveStatus[group_num][i] = rand()%4;
        }
    #endif
	return 1;
}

/*
	��ȡָ�����ŵĿ���ֵ
	����:
		dbOffset:��Ӧ��xValveView�е�dBOffsetForCurPos
	����ֵ:
		0~100:����ֵ
		-1:ͨ��ʧ��
*/
int GetValveCurPos(unsigned int dbOffset)
{
	return 20;
}

/*
	��ȡָ�����ŵ�ȫ��״̬
	����:
		dbOffset:��Ӧ��xValveView�е�dBOffsetForOpenVal
	����ֵ:
		0:ȫ��
		1:ȫ��
		-1:ͨ��ʧ��
*/
int GetValveOpenStatus(unsigned int dbOffset)
{
	return 1;
}

/*
	��ȡָ�����ŵ�ȫ��״̬
	����:
		dbOffset:��Ӧ��xValveView�е�dBOffsetForCloseVal
	����ֵ:
		0:ȫ��
		1:ȫ��
		-1:ͨ��ʧ��
*/
int GetValveCloseStatus(unsigned int dbOffset)
{
	return 0;
}

/*
	��ȡָ�����ŵ�L/R״̬
	����:
		dbOffset:��Ӧ��xValveView�е�dBOffsetForLRVal
	����ֵ:
		0:L
		1:R
		-1:ͨ��ʧ��
*/
int GetValveLRStatus(unsigned int dbOffset)
{
	return 0;
}

/*
	���ſ�������
	����:
		dbOffset:��ӦxValveCtrlCmd�е�dBOffsetForOpenCmd
		cmd:��ӦxValveCtrlCmd�е�openCmd
	����ֵ:
		-1:ͨ��ʧ��
		����:ͨ�ųɹ�
*/
int OpenValve(unsigned short dbOffset,unsigned short cmd)
{
	return 1;
}

/*
	���Źر�����
	����:
		dbOffset:��ӦxValveCtrlCmd�е�dBOffsetForCloseCmd
		cmd:��ӦxValveCtrlCmd�е�closeCmd
	����ֵ:
		-1:ͨ��ʧ��
		����:ͨ�ųɹ�
*/
int CloseValve(unsigned short dbOffset,unsigned short cmd)
{
	return 1;
}

/*
	����ֹͣ����
	����:
		dbOffset:��ӦxValveCtrlCmd�е�dBoffsetForStopCmd
		cmd:��ӦxValveCtrlCmd�е�stopCmd
	����ֵ:
		-1:ͨ��ʧ��
		����:ͨ�ųɹ�
*/
int StopValve(unsigned short dbOffset,unsigned short cmd)
{
	return 1;
}


/*
	�趨���ſ���
	����:
		dbCmdOffset:��ӦxValveCtrlCmd�е�dBOffsetForCurPosCmd
		cmd:��ӦxValveCtrlCmd�е�getCurPosCmd
		dbCurPosOffset:��ӦxValveCtrlCmd�е�dbOffsetForCurPos
	����ֵ:
		-1:ͨ��ʧ��
		����:ͨ�ųɹ�
*/
int SetValveCurPos(unsigned short dbCmdOffset,unsigned short cmd,unsigned short dbCurPosOffset,unsigned short value)
{
	return 1;
}

