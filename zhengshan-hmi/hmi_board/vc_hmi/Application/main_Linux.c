/*
                uC/GUI Linux Port
----------------------------------------------------------------
File        : GUI_X_Linux.c
Purrpose    : Port uC/GUI on GNU/Linux
-----------------------END-OF-HEADER--------------------------*/

#include <unistd.h> 
#include <stdio.h>
#include <stdlib.h>
#include <fcntl.h>
#include <string.h> 
#include <sys/types.h>
#include <sys/time.h>
#include <sys/select.h>
#include <sys/ipc.h>
#include <sys/msg.h>
#include <sys/prctl.h>
#include <pthread.h>
#include <linux/input.h>
#include <GUIType.h>
#include "GUI.h"
#include "common.h"
#include "hmiData.h"
#include "communication.h"
#include "tp_main.h"



#define ABS(a) ((a)>0?(a):-(a)) 


extern void MainTask(void);


void GuiWaitEvent(void)
{
}


int main(int argc, char *argv[])
{
	
    int pid;
    printf("HMI starting...\n");
    pid=fork();
    if(pid>0) //parent process
    {
        exit(0);
    }
    else if(pid< 0)//fork fail
    {
        exit(1);
    }
    setsid();
    chdir("/");
    umask(0);
    //for(i=0;i<65535;i++)
    //    close(i);
    printf("create touch screen monitor\n");
    //touchCaliFlag = 1;

    MainTask();
    return 0;
}

