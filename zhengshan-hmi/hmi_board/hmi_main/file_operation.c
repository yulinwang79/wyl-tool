#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <stdarg.h>
#include <sys/msg.h>
#include <time.h> 
#include <pthread.h>
#include "file_operation.h"

#define FILE_PATH_SEPERATOR '/'

typedef enum {
	FILE_DESC_FREE,
	FILE_DESC_WRITE,
	FILE_DESC_READ,
	FILE_DESC_MOVE_FILE,
}eFileOperation;

typedef struct {
	int status;
    eFileOwner owner;
	FILE *file_handler;
	char file_name[MAX_FILE_PATH];
	char tmp_file_name[MAX_FILE_PATH];
}xFileDescriptor;

static xFileDescriptor xFileDesc[FILE_POOL_SIZE];

static void *file_aysnc_close( void *pvParameter)
{
    int offset;
    char sys_cmd[128];
    offset = *((int *)pvParameter);
    if(xFileDesc[offset].status == FILE_DESC_MOVE_FILE)
    {
        sprintf(sys_cmd,"cp -f %s %s",xFileDesc[offset].tmp_file_name,xFileDesc[offset].file_name);
        system(sys_cmd);
        xFileDesc[offset].status = FILE_DESC_FREE;
        if(strstr(xFileDesc[offset].file_name,APP_EXT))
        {
            sprintf(sys_cmd,"chmod 500 %s",xFileDesc[offset].file_name);
            system(sys_cmd);
        }
    }
    return NULL;
}

void xFileOperationInit(void)
{
    bzero(&xFileDesc[0],sizeof(xFileDescriptor) * FILE_POOL_SIZE);
}

int xFileCreate(eFileOwner eOwner,char *csFileName,int iFileLen)
{
    int index;
    char *tp;
    FILE *fp = NULL;
    for(index = 0; index < FILE_POOL_SIZE; index++)
    {
        if(xFileDesc[index].status != FILE_DESC_FREE)
        {
            if(!strncmp(xFileDesc[index].file_name,csFileName,strlen(csFileName)))
            {
                if(xFileDesc[index].owner != eOwner)
                    return -1;
                if(xFileDesc[index].status != FILE_DESC_MOVE_FILE)
                {
                    fclose(xFileDesc[index].file_handler);
                    break;
                }
                else
                {
                    return -1;
                }
            }
        }
    }
    if(index == FILE_POOL_SIZE)
    {
        //find an empty descriptor
        for(index = 0; index < FILE_POOL_SIZE; index++)
        {
            if(xFileDesc[index].status == FILE_DESC_FREE)
            {
                xFileDesc[index].status = FILE_DESC_WRITE;
                xFileDesc[index].owner = eOwner;
                break;
            }
        }
    }
    if(index == FILE_POOL_SIZE)
        return -1;
    strcpy(xFileDesc[index].file_name,csFileName);
    tp = rindex(xFileDesc[index].file_name,FILE_PATH_SEPERATOR);
    if(tp == NULL)
        return -1;
    tp++;
    sprintf(xFileDesc[index].tmp_file_name,"/tmp/%s",tp);
    fp = fopen(xFileDesc[index].tmp_file_name,"wb+");
    truncate(xFileDesc[index].tmp_file_name,iFileLen);
    //fclose(fp);
    xFileDesc[index].file_handler = fp;
    return index;
}

int xFileOpen(eFileOwner eOwner,char *csFileName)
{
    int index;
    FILE *fp = NULL;
    //printf("xFileOpen[%s]\n",csFileName);
    for(index = 0; index < FILE_POOL_SIZE; index++)
    {
        if(xFileDesc[index].status != FILE_DESC_FREE)
        {
            if(!strncmp(xFileDesc[index].file_name,csFileName,strlen(csFileName)))
            {
                if(xFileDesc[index].owner != eOwner)
                    return -1;
                if(xFileDesc[index].status == FILE_DESC_READ)
                {
                    fclose(xFileDesc[index].file_handler);
                    break;
                }
                else
                {
                    return -1;
                }
            }
        }
    }
    if(index == FILE_POOL_SIZE)
    {
        //find an empty descriptor
        for(index = 0; index < FILE_POOL_SIZE; index++)
        {
            if(xFileDesc[index].status == FILE_DESC_FREE)
            {
                xFileDesc[index].status = FILE_DESC_READ;
                xFileDesc[index].owner = eOwner;
                break;
            }
        }
    }
    if(index == FILE_POOL_SIZE)
        return -1;
    fp = fopen(csFileName,"rb");
    if(fp == NULL)
        return -1;
    xFileDesc[index].file_handler = fp;
    //printf("xFileOpen,file id = %d\n",index);
    return index;
}

int xFileWrite(void *pData,int iDataLen,int iOffset,int iFd)
{
    if(iFd > FILE_POOL_SIZE - 1)
        return -1;
    //FILE *fp = NULL;
    //fp = fopen(xFileDesc[iFd].tmp_file_name,"wb+");
    if(xFileDesc[iFd].status != FILE_DESC_WRITE)
        return -1;
    fseek(xFileDesc[iFd].file_handler,iOffset,SEEK_SET);
    fwrite(pData,1,iDataLen,xFileDesc[iFd].file_handler);
    //fclose(fp);
    return iOffset;
}

int xFileRead(void *pBuf,int iBufLen,int iOffset,int iFd)
{
    if(iFd > FILE_POOL_SIZE - 1)
        return -1;
    //printf("xFileRead,Offset = %d,iFd = %d\n",iOffset,iFd);
    if(xFileDesc[iFd].status != FILE_DESC_READ)
        return -1;
    fseek(xFileDesc[iFd].file_handler,iOffset,SEEK_SET);
    return fread(pBuf,1,iBufLen,xFileDesc[iFd].file_handler);
}

int xFileGetSize(int iFd)
{
    int iFileLen;
    if(iFd > FILE_POOL_SIZE - 1)
        return 0;
    fseek(xFileDesc[iFd].file_handler,0,SEEK_END);
    iFileLen = ftell(xFileDesc[iFd].file_handler);
    return iFileLen;
}

int xFileClose(int iFd)
{
    pthread_t xThread;
    if(iFd > FILE_POOL_SIZE - 1)
        return 0;
    fclose(xFileDesc[iFd].file_handler);
    if(xFileDesc[iFd].status == FILE_DESC_WRITE)
    {
        xFileDesc[iFd].status = FILE_DESC_MOVE_FILE;
        pthread_create( &xThread, NULL, file_aysnc_close, &iFd );
    }
    else
    {	
        xFileDesc[iFd].status = FILE_DESC_FREE;
    }
    return 0;
}
