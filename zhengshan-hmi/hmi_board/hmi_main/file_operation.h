#ifndef __FILE_OPERATION_H__
#define __FILE_OPERATION_H__

#define FILE_POOL_SIZE	16
#define MAX_FILE_PATH   256
#define APP_EXT ".app"

typedef enum {
    CONFIG_MODULE,
    INVALID_MODULE
}eFileOwner;

void xFileOperationInit(void);
int xFileCreate(eFileOwner eOwner,char *csFileName,int iFileLen);
int xFileOpen(eFileOwner eOwner,char *csFileName);
int xFileWrite(void *pData,int iDataLen,int iOffset,int iFd);
int xFileRead(void *pBuf,int iBufLen,int iOffset,int iFd);
int xFileGetSize(int iFd);
int xFileClose(int iFd);


#endif
