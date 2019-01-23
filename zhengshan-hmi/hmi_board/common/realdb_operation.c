#include <stdio.h>
#include <stdlib.h>
#include <errno.h>
#include <string.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/stat.h>
#include "realdb_operation.h"

xRealTimeDBMap *pRealDB;

void eMBRegHoldingCB( unsigned char * pucRegBuffer, unsigned int usAddress, unsigned int usNRegs, eDBRegisterMode eMode )
{
    unsigned char *pucRDBPtr;
    //unsigned int usSyncNRegs = usNRegs;

    if( usAddress + usNRegs < sizeof(xRealTimeDBMap)/sizeof(unsigned short)) 
    {
        switch ( eMode&0x7F )
        {
            case REG_READ:
                pucRDBPtr = (unsigned char *)(&pRealDB->real_db[usAddress]);
                if(eMode&0x80)//byte swap
                {
                    do {
                        *pucRegBuffer++ = *pucRDBPtr++;//low byte
                        *pucRegBuffer++ = *pucRDBPtr++;//high byte
                    }while( --usNRegs );
                }
                else
                {
                    do {
                        *pucRegBuffer++ = pucRDBPtr[1];//high byte
                        *pucRegBuffer++ = pucRDBPtr[0];//low byte
                        pucRDBPtr += 2;
                    }while( --usNRegs );
                }
                break;
            case REG_WRITE:
                //printf("eMBRegHoldingCB write,usAddress = %d\n",usAddress);
                pucRDBPtr = (unsigned char *)(&pRealDB->real_db[usAddress]);
                if(eMode&0x80)//byte swap
                {
                    do {
                            *pucRDBPtr++ = *pucRegBuffer++;//low byte
                            *pucRDBPtr++ = *pucRegBuffer++;//high byte
                        }while(--usNRegs);
                }
                else
                {
                    do {
                        *pucRDBPtr++ = pucRegBuffer[1];//high byte
                        *pucRDBPtr++ = pucRegBuffer[0];//low byte
                        pucRegBuffer += 2;
                    }while(--usNRegs);
                }
                break;
            default:
                break;
        }
    }
}

static unsigned char BitMask[8]=
{
	0x01,
	0x03,
	0x07,
	0x0f,
	0x1f,
	0x3f,
	0x7f,
	0xff,
};


#define LEFT_RIGHT_SHIFT(A,L,R) \
	(((unsigned char)((A)<<(L)))>>(R))

void eMBRegCoilsCB( unsigned char * pucRegBuffer, unsigned int usAddress, unsigned int usNCoils, eDBRegisterMode eMode )
{
    unsigned char *pucRDBPtr;
    unsigned int usNRegs;
    unsigned int db_offset;
    usNRegs = (usNCoils + 7)>>3;
    db_offset = (usAddress>>3);//in UNIT of bytes
    if( db_offset + usNRegs < (((sizeof(xRealTimeDBMap)/sizeof(unsigned short)))<<1) )
        {
            switch ( eMode )
            {
                case REG_READ:
                //printf("eMBRegCoilsCB,usAddress = %d,usNcoils = %d\n",usAddress,usNCoils);
                pucRDBPtr = (unsigned char *)(&pRealDB->real_db[0]);
                pucRDBPtr += db_offset;
                if(usAddress & 0x0000007)
                {
                    unsigned int bits_remained;
                    bits_remained = 8-(usAddress & 0x0000007);	
                    while(usNCoils > 7)
                    {
                        //copy prev part
                        *pucRegBuffer = ((*pucRDBPtr)>>(8-bits_remained));
                        //copy next part
                        pucRDBPtr++;
                        *pucRegBuffer |= ((*pucRDBPtr)<<bits_remained);
                        usNCoils -= 8;
                        pucRegBuffer++;
                    }
                    if(usNCoils > 0)
                    {
                        if(bits_remained < usNCoils)
                        {
                            *pucRegBuffer = (*pucRDBPtr>>(8-bits_remained));
                            usNCoils -= bits_remained;
                            *pucRegBuffer |= LEFT_RIGHT_SHIFT(*pucRDBPtr,(8-usNCoils),(8-usNCoils-bits_remained));//(((unsigned char)(*pucRDBPtr<<(8-usNCoils)))>>(8-usNCoils-bits_remained));
                        }
                        else
                        {
                            *pucRegBuffer = LEFT_RIGHT_SHIFT(*pucRDBPtr,(bits_remained-usNCoils),(8-usNCoils));// ((*pucRDBPtr<<(bits_remained-usNCoils))>>(8-usNCoils));
                        }
                    }
                }
                else
                {
                    while(usNCoils > 7)
                    {
                        *pucRegBuffer++ = (*pucRDBPtr)++;
                        usNCoils -= 8;
                    }	
                    if(usNCoils > 0)
                    {
                        //printf("eMBRegCoilsCB,usAddress = %d,usNcoils = %d\n",usAddress,usNCoils);
                        *pucRegBuffer = LEFT_RIGHT_SHIFT(*pucRDBPtr,(8-usNCoils),(8-usNCoils));//((*pucRDBPtr<<(8-usNCoils))>>(8-usNCoils));
                    }
                }
                break;
                case REG_WRITE:
                    pucRDBPtr = (unsigned char *)(&pRealDB->real_db[0]);
                    pucRDBPtr += db_offset;
                    if(usAddress & 0x0000007)
                    {	
                        unsigned int bits_offset;
                        unsigned char mask;
                        bits_offset = usAddress & 0x0000007;
                        while(usNCoils > 7)
                        {
                            //save the prev part
                            *pucRDBPtr <<= (8-bits_offset);
                            *pucRDBPtr >>= (8-bits_offset);
                            *pucRDBPtr |= (*pucRegBuffer<<bits_offset);
                            //save the next part
                            pucRDBPtr++;
                            *pucRDBPtr >>= bits_offset;
                            *pucRDBPtr <<= bits_offset;
                            *pucRDBPtr |= (*pucRegBuffer>>(8-bits_offset));
                            usNCoils -= 8;
                            pucRegBuffer++;
                        }
                        if(usNCoils > 0)
                        {
                            if(8-bits_offset < usNCoils)
                            {
                                *pucRDBPtr <<= (8-bits_offset);
                                *pucRDBPtr >>= (8-bits_offset);
                                *pucRDBPtr |= (*pucRegBuffer<<bits_offset);
                                usNCoils -= 8-bits_offset;
                                pucRDBPtr++;
                                *pucRDBPtr >>= usNCoils;
                                *pucRDBPtr <<= usNCoils;
                                *pucRDBPtr |= (*pucRegBuffer >>(8-bits_offset))&BitMask[usNCoils-1];
                            }
                            else
                            {
                                mask = BitMask[usNCoils-1];
                                mask<<=bits_offset;
                                *pucRDBPtr &=~mask;
                                *pucRegBuffer &= BitMask[usNCoils-1];
                                *pucRegBuffer <<= bits_offset;
                                *pucRDBPtr |= *pucRegBuffer;
                            }
                        }
                    }
                    else
                    {
                        while(usNCoils > 7)
                        {
                            *pucRDBPtr++ = *pucRegBuffer++;
                            usNCoils -= 8;
                        }	
                        if(usNCoils > 0)
                        {
                            *pucRDBPtr >>= usNCoils;
                            *pucRDBPtr <<= usNCoils;
                            *pucRegBuffer <<= (8-usNCoils);
                            *pucRegBuffer >>= (8-usNCoils);
                            *pucRDBPtr |= *pucRegBuffer;
                        }
                    }
                    break;
                default:
                    break;
                }
        }
    }


