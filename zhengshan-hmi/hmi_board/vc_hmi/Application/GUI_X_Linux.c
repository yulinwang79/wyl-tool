/*
                uC/GUI Linux Port
                by DuHuanpeng
                W95588@gmail.com
                2009/12/21
----------------------------------------------------------------
File        : GUI_X_Linux.c
Purrpose    : Port uC/GUI on GNU/Linux
-----------------------END-OF-HEADER--------------------------*/

/*---------------------GNU/Linux Port-------------------------*/
#include <stdio.h>
#include <assert.h>
#include <unistd.h>
#include <inttypes.h>
#include <sys/time.h>
#include "GUI.h"
#include "GUI_X.h"
#include "LCD_Protected.h"
#include "common.h"

/*----------------------Linux Mutex---------------------------*/


#if defined(__cplusplus)
extern "C" {     /* Make sure we have C-declarations in C++ programs */
#endif

/**** Init ****/
void GUI_X_Init(void)
{
/*--------------------uC/GUI GNU/Linux Port-------------------*/
   printf("GUI_X_Init\n");
/*-------------------------End of Port------------------------*/
}

/**** ExecIdle - called if nothing else is left to do ****/
void GUI_X_ExecIdle(void)
{
   //printf("GUI_X_ExecIdle\n");
   LCD_Update();
   //wait event
   //GuiWaitEvent();
   return;
}

/**** Timing routines - required for blinking ****/
int  GUI_X_GetTime(void)
{
   struct timeval tv;
   //int tm;
   gettimeofday(&tv, NULL);
   return tv.tv_sec;
   //tm = tv.tv_sec*1024 + tv.tv_usec/1024;
   //printf("GUI_X_GetTime:%u.\n", tm);
   //return tm;
}

void GUI_X_Delay(int Period)
{
   //printf("GUI_X_Delay\n");
   while(Period--){
     usleep(1000);
   }
   return;
}

/**** Multitask routines - required only if multitasking is used (#define GUI_OS 1) ****/
void GUI_X_Unlock(void)
{
   printf("GUI_X_Unlock\n");
   return;
}
void GUI_X_Lock(void)
{
   printf("GUI_X_Lock\n");
   return;
}
U32  GUI_X_GetTaskId(void)
{
   printf("GUI_X_GetTaskId\n");
   return -1;
}
void GUI_X_InitOS(void)
{
   printf("GUI_X_InitOS\n");
   return;
}

/****      Event driving (optional with multitasking)  ****/
void GUI_X_WaitEvent(void)
{
   printf("GUI_X_WaitEvent\n");
   return;
}
void GUI_X_SignalEvent(void)
{
   printf("GUI_X_SignalEvent\n");
   return;
}
/**** Recording (logs/warnings and errors) - required only for higher levels ****/
void GUI_X_Log(const char *s)
{
   printf("GUI_X_Log\n");

   printf("%s", s);
   return;
}

void GUI_X_Warn(const char *s)
{
   printf("GUI_X_Warn\n");
   fprintf(stderr, "%s", s);
   return;
}
void GUI_X_ErrorOut(const char *s)
{
   printf("GUI_X_ErrorOut\n");
   fprintf(stderr, "%s", s);
   return;
} 

#if defined(__cplusplus)
  }
#endif

/*************************** End of file ****************************/


