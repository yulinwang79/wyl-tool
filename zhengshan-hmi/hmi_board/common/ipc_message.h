#ifndef __IPC_MESSAGE_H__
#define __IPC_MESSAGE_H__

typedef enum{
    INVALID_MSG_TYPE = 0,
    REG_WRITE_MSG = 1,
    REG_READ_MSG = 2,
}eMsgType;


typedef struct {
    int iOffset;
    int iValue;
}xRegWrite;

typedef struct {
    int iOffset;
    int iNum;
}xRegRead;

typedef struct {
    int iMsgType;
    union {
        xRegWrite xRegWr;
        xRegRead xRegRd;
    }uMsgContent;
}xCommMessage;



#endif
