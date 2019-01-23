#include <stdio.h>
#include <stdlib.h>
#include "GUI.h"
#include "common.h"
#include "hmiData.h"
#include "LCD_ConfDefaults.h"      /* valid LCD configuration */
#include "GUI_Protected.h"

extern int touchCaliFlag;

typedef struct
{  
   /*x*/ 
   double x_slope;
   double x_offset; 
   /*y*/   
   double y_slope;
   double y_offset; 
}xTouchPanelCaliStruct ; 


#define   TOUCH_PANEL_CALI_CHECK_OFFSET  12

unsigned int ADC_X_START=230;
unsigned int ADC_X_END=3867;
unsigned int ADC_Y_START=287;
unsigned int ADC_Y_END=3853;

unsigned int SCREEN_X_START=0;
unsigned int SCREEN_Y_START=0;
unsigned int SCREEN_X_END=320; 
unsigned int SCREEN_Y_END=240; 

static xTouchPanelCaliStruct xTPCali;

static void touch_panel_tuning(int coord1, int adc1,
                        int coord2, int adc2,
                        double *slope,     double *offset)
{
   *slope=((double)(coord2-coord1)/((double)(adc2-adc1)));
   *offset=(double)coord1-(*slope)*(double)adc1;      
}

static int touch_panel_check_cali_stage1(xPoint *xScreenPtr,xPoint *xAdcPtr)
{
   int x_adc_range, x_coord_range, x_adc_high, x_adc_low;   
   int y_adc_range, y_coord_range, y_adc_high, y_adc_low;
   
   /*use the relative ADC difference*/
   /*X ADC Diff*/
   if(xAdcPtr[1].x>=xAdcPtr[0].x)
      x_adc_range=xAdcPtr[1].x-xAdcPtr[0].x;   
   else
      x_adc_range=xAdcPtr[0].x-xAdcPtr[1].x;   
   /*Y ADC Diff*/   
   if(xAdcPtr[1].y>=xAdcPtr[0].y)   
      y_adc_range=xAdcPtr[1].y-xAdcPtr[0].y;   
   else
      y_adc_range=xAdcPtr[0].y-xAdcPtr[1].y; 
                 
   /*X Coord Diff*/   
   if(xScreenPtr[1].x>=xScreenPtr[0].x)         
      x_coord_range=xScreenPtr[1].x-xScreenPtr[0].x;   
   else
      x_coord_range=xScreenPtr[0].x-xScreenPtr[1].x;      
   /*Y Coord Diff*/      
   if(xScreenPtr[1].y>=xScreenPtr[0].y)   
      y_coord_range=xScreenPtr[1].y-xScreenPtr[0].y;  
   else   
      y_coord_range=xScreenPtr[0].y-xScreenPtr[1].y;  
               
   x_adc_high=x_coord_range*(ADC_X_END-ADC_X_START)*150/(SCREEN_X_END-SCREEN_X_START)/100; 
   x_adc_low=x_coord_range*(ADC_X_END-ADC_X_START)*50/(SCREEN_X_END-SCREEN_X_START)/100;
   y_adc_high=y_coord_range*(ADC_Y_END-ADC_Y_START)*150/(SCREEN_Y_END-SCREEN_Y_START)/100; 
   y_adc_low=y_coord_range*(ADC_Y_END-ADC_Y_START)*50/(SCREEN_Y_END-SCREEN_Y_START)/100;       
   if((x_adc_range<x_adc_low) || (x_adc_range>x_adc_high))
      return 0;
   if((y_adc_range<y_adc_low) || (y_adc_range>y_adc_high))
      return 0;                           
   return 1;      
}

static int touch_panel_check_cali_stage2(xPoint *xScreenPtr,xPoint *xAdcPtr)
{   
   int x02_diff, y02_diff, x12_diff, y12_diff;
      
   /*use the point 3 to check if the previous 2 two points are opposite*/      
   if(xAdcPtr[1].x>=xAdcPtr[0].x)/*1>2>0*/
   {
      x12_diff=xAdcPtr[1].x-xAdcPtr[2].x;
      x02_diff=xAdcPtr[2].x-xAdcPtr[0].x;            
   }
   else/*0>2>1*/
   {  
      x12_diff=xAdcPtr[2].x-xAdcPtr[1].x;
      x02_diff=xAdcPtr[0].x-xAdcPtr[2].x;            
   }            
   if(xAdcPtr[1].y>=xAdcPtr[0].y)/*1>2>0*/
   {
      y12_diff=xAdcPtr[1].y-xAdcPtr[2].y;
      y02_diff=xAdcPtr[2].y-xAdcPtr[0].y;            
   }
   else/*0>2>1*/
   {  
      y12_diff=xAdcPtr[2].y-xAdcPtr[1].y;
      y02_diff=xAdcPtr[0].y-xAdcPtr[2].y;            
   }   
      
   if(y12_diff>y02_diff||x12_diff>x02_diff)
      return 0;      
   return 1;
}

static int touch_panel_check_cali_stage3(xPoint *xScreenPtr,xPoint *xAdcPtr)
{
   short x, y, x_diff, y_diff;
   double x_slope, y_slope, x_offset, y_offset; 
      
   touch_panel_tuning(xScreenPtr[0].x, xAdcPtr[0].x, 
                      xScreenPtr[1].x, xAdcPtr[1].x, 
                      &x_slope, &x_offset);
   touch_panel_tuning(xScreenPtr[0].y, xAdcPtr[0].y, 
                      xScreenPtr[1].y, xAdcPtr[1].y, 
                      &y_slope, &y_offset); 
        
   x=(short)(x_slope*(double)(xAdcPtr[2].x)+x_offset);   
   y=(short)(y_slope*(double)(xAdcPtr[2].y)+y_offset); 
   
   x_diff=xScreenPtr[2].x-x;
   y_diff=xScreenPtr[2].y-y;
   if(x_diff>TOUCH_PANEL_CALI_CHECK_OFFSET||x_diff<-TOUCH_PANEL_CALI_CHECK_OFFSET||
      y_diff>TOUCH_PANEL_CALI_CHECK_OFFSET||y_diff<-TOUCH_PANEL_CALI_CHECK_OFFSET)
      return 0;
   return 1;
}   

static void cali_draw_point(int x,int y)
{
	#if 1
    GUI_DrawLine(x-7,y-2,x-2,y-2);
    GUI_DrawLine(x+2,y-2,x+7,y-2);
    GUI_DrawLine(x-7,y+2,x-2,y+2);
    GUI_DrawLine(x+2,y+2,x+7,y+2);
    GUI_DrawLine(x-2,y-7,x-2,y-2);
    GUI_DrawLine(x-2,y+2,x-2,y+7);
    GUI_DrawLine(x+2,y-7,x+2,y-2);
    GUI_DrawLine(x+2,y+2,x+2,y+7);
	GUI_DrawLine(x-7,y-2,x-2,y-7);
	GUI_DrawLine(x-7,y+2,x-2,y+7);
	GUI_DrawLine(x+2,y-7,x+7,y-2);
	GUI_DrawLine(x+2,y+7,x+7,y+2);
	#else
	GUI_DrawLine(x-5,y-1,x-1,y-1);
    GUI_DrawLine(x+1,y-1,x+5,y-1);
    GUI_DrawLine(x-5,y+1,x-1,y+1);
    GUI_DrawLine(x+1,y+1,x+5,y+1);
    GUI_DrawLine(x-1,y-5,x-1,y-1);
    GUI_DrawLine(x-1,y+1,x-1,y+5);
    GUI_DrawLine(x+1,y-5,x+1,y-1);
    GUI_DrawLine(x+1,y+1,x+1,y+5);
	GUI_DrawLine(x-5,y-1,x-1,y-5);
	GUI_DrawLine(x-5,y+1,x-1,y+5);
	GUI_DrawLine(x+1,y-5,x+5,y-1);
	GUI_DrawLine(x+1,y+5,x+5,y+1);
	#endif
}


/*interface*/

int TPAdcToCoordinate(int *x, int *y)
{
      
   *x=(int)(xTPCali.x_slope*(double)(*x)+xTPCali.x_offset);   
   *y=(int)(xTPCali.y_slope*(double)(*y)+xTPCali.y_offset);  
   
   if( (SCREEN_X_START < *x)&&(*x < SCREEN_X_END)&&
       (SCREEN_Y_START < *y)&&(*y < SCREEN_Y_END))
   {
      return 1;  
   }
   return 0;          
}

void _ExecCalibration(void) {
	FILE *fd;
	xPoint xScreenCoord[3];
	xPoint xTouchAdc[3];	
	touchCaliFlag = 1;
	xScreenCoord[0].x = LCD_XSIZE/10;
	xScreenCoord[0].y = LCD_YSIZE/10;
	xScreenCoord[1].x = LCD_XSIZE*9/10;
	xScreenCoord[1].y = LCD_YSIZE*9/10;
	xScreenCoord[2].x = LCD_XSIZE*11/20;
	xScreenCoord[2].y = LCD_YSIZE*11/20;
	do{
		/* _Calibrate upper left */
		GUI_SetBkColor(GUI_WHITE);  
		GUI_Clear();
		GUI_SetColor(GUI_BLACK);  
        cali_draw_point(xScreenCoord[0].x,xScreenCoord[0].y);
        #if 0
		GUI_FillCircle(xScreenCoord[0].x, xScreenCoord[0].y, 10);
		GUI_SetColor(GUI_WHITE);    
		GUI_FillCircle(xScreenCoord[0].x, xScreenCoord[0].y, 5);
		GUI_SetColor(GUI_BLACK);
        #endif
		GUI_SetFont(&GUI_Font16_ASCII);
		GUI_DispStringAt("Press here", xScreenCoord[0].x+20, xScreenCoord[0].y);
		do {
			GUI_PID_STATE State;
			GUI_TOUCH_GetState(&State);
			if (State.Pressed == 1) {
			  break;
			}
			GUI_X_WAIT_EVENT(); //GUI_Delay (100);
		} while (1);
		
		do {
			GUI_PID_STATE State;
			GUI_TOUCH_GetState(&State);
			if (!State.Pressed) {
				xTouchAdc[0].x = State.x;
				xTouchAdc[0].y = State.y;
			  	break;
			}
			GUI_X_WAIT_EVENT(); //GUI_Delay (100);
		} while (1);
		/* Tell user to release */
		GUI_Clear();
		GUI_DispStringAt("OK", xScreenCoord[0].x+20, xScreenCoord[0].y);
		#if 0
		do {
			GUI_PID_STATE State;
			GUI_TOUCH_GetState(&State);
			if (State.Pressed == 0) {
			  break;
			}
			GUI_X_WAIT_EVENT(); //GUI_Delay (100);
		} while (1);
		#endif
		/* _Calibrate lower right */
		GUI_SetBkColor(GUI_WHITE);  
		GUI_Clear();
		GUI_SetColor(GUI_BLACK);  
        #if 1
        cali_draw_point(xScreenCoord[1].x,xScreenCoord[1].y);
        #else
		GUI_FillCircle(xScreenCoord[1].x, xScreenCoord[1].y, 10);
		GUI_SetColor(GUI_WHITE);    
		GUI_FillCircle(xScreenCoord[1].x, xScreenCoord[1].y, 5);
		GUI_SetColor(GUI_BLACK);
        #endif
		GUI_SetTextAlign(GUI_TA_RIGHT);
		GUI_DispStringAt("Press here", xScreenCoord[1].x-20, xScreenCoord[1].y);
		//press
		do {
			GUI_PID_STATE State;
			GUI_TOUCH_GetState(&State);
			if (State.Pressed == 1) {
			  break;
			}
			GUI_X_WAIT_EVENT(); //GUI_Delay (100);
		} while (1);
		//wait release
		do {
			GUI_PID_STATE State;
			GUI_TOUCH_GetState(&State);
			if (!State.Pressed) {
			  xTouchAdc[1].x = State.x;
			  xTouchAdc[1].y = State.y;
			  break;
			}
			GUI_X_WAIT_EVENT(); //GUI_Delay (100);
		} while (1);
		/* Tell user to release */
		GUI_Clear();
		GUI_DispStringAt("OK", xScreenCoord[1].x+20, xScreenCoord[1].y);
		#if 0
		do {
			GUI_PID_STATE State;
			GUI_TOUCH_GetState(&State);
			if (State.Pressed == 0) {
			  break;
			}
			GUI_X_WAIT_EVENT(); //GUI_Delay (100);
		} while (1);
		#endif
		/* _Calibrate center */
		GUI_SetBkColor(GUI_WHITE);  
		GUI_Clear();
		GUI_SetColor(GUI_BLACK);  
        #if 1
        cali_draw_point(xScreenCoord[2].x,xScreenCoord[2].y);
        #else
		GUI_FillCircle(xScreenCoord[2].x, xScreenCoord[2].y, 10);
		GUI_SetColor(GUI_WHITE);    
		GUI_FillCircle(xScreenCoord[2].x, xScreenCoord[2].y, 5);
		GUI_SetColor(GUI_BLACK);
        #endif
		GUI_SetFont(&GUI_Font16_ASCII);
		GUI_DispStringAt("Press here", xScreenCoord[2].x+20, xScreenCoord[2].y);
		//press
		do {
			GUI_PID_STATE State;
			GUI_TOUCH_GetState(&State);
			if (State.Pressed == 1) {
			  break;
			}
			GUI_X_WAIT_EVENT(); //GUI_Delay (100);
		} while (1);
		//wait release
		do {
			GUI_PID_STATE State;
			GUI_TOUCH_GetState(&State);
			if (!State.Pressed) {
				xTouchAdc[2].x = State.x;
				xTouchAdc[2].y = State.y;
			  	break;
			}
			GUI_X_WAIT_EVENT(); //GUI_Delay (100);
		} while (1);
		/* Tell user to release */
		GUI_Clear();
		GUI_DispStringAt("OK", xScreenCoord[2].x+20, xScreenCoord[2].y);
		#if 0
		do {
			GUI_PID_STATE State;
			GUI_TOUCH_GetState(&State);
			if (State.Pressed == 0) {
			  break;
			}
			GUI_X_WAIT_EVENT(); //GUI_Delay (100);
		} while (1);
		#endif
		//calibration
		if(touch_panel_check_cali_stage1(xScreenCoord,xTouchAdc) && 
		   touch_panel_check_cali_stage2(xScreenCoord,xTouchAdc) && 
		   touch_panel_check_cali_stage3(xScreenCoord,xTouchAdc))
		{
			touch_panel_tuning(xScreenCoord[0].x, xTouchAdc[0].x, 
	                         xScreenCoord[1].x, xTouchAdc[1].x, 
	                         &xTPCali.x_slope, &xTPCali.x_offset);
	      	touch_panel_tuning(xScreenCoord[0].y, xTouchAdc[0].y, 
	                         xScreenCoord[1].y, xTouchAdc[1].y, 
	                         &xTPCali.y_slope, &xTPCali.y_offset);  
			fd = fopen("/disk/tp_cali.dat","wb");
			fwrite(&xTPCali,1,sizeof(xTouchPanelCaliStruct),fd);
			fclose(fd);
			//printf("calibration data save,size = %f,%f,%f,%f\n",xTPCali.x_offset,xTPCali.x_slope,xTPCali.y_offset,xTPCali.y_slope);
			touchCaliFlag = 0;
			break;
		}
	}while(1);

}

void InitTPCalibration(void)
{
	FILE *fd;
	int count;
	fd = fopen("/disk/tp_cali.dat","rb");
	if(fd == NULL)
	{
		_ExecCalibration();
	}
	else
	{
		count = fread(&xTPCali,1,sizeof(xTouchPanelCaliStruct),fd);
		touchCaliFlag = 0;
		//printf("calibration data read,size = %d,%f,%f,%f,%f\n",count,xTPCali.x_offset,xTPCali.x_slope,xTPCali.y_offset,xTPCali.y_slope);
		fclose(fd);
	}
}

