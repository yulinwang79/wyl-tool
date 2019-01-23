#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <direct.h>
#include <io.h>

#pragma pack(push,1)
typedef unsigned short WORD;
typedef unsigned int DWORD;
typedef int LONG; 
typedef unsigned char BYTE;

typedef struct tagBITMAPFILEHEADER {

WORD           bfType; 

DWORD bfSize; 

WORD           bfReserved1; 

WORD           bfReserved2; 

DWORD bfOffBits; 

} BITMAPFILEHEADER; 

typedef struct tagBITMAPINFOHEADER{

DWORD  biSize; 

LONG            biWidth; 

LONG            biHeight; 

WORD           biPlanes; 

WORD           biBitCount;

DWORD  biCompression; 

DWORD  biSizeImage; 

LONG            biXPelsPerMeter; 

LONG            biYPelsPerMeter; 

DWORD  biClrUsed; 

DWORD  biClrImportant; 

} BITMAPINFOHEADER; 

typedef struct tagRGBQUAD { 

BYTE    rgbBlue; //该颜色的蓝色分量

BYTE    rgbGreen; //该颜色的绿色分量

BYTE    rgbRed; //该颜色的红色分量

BYTE    rgbReserved; //保留值

} RGBQUAD; 

#pragma pack(pop)


void main(void)
{
	FILE * sStream = NULL;
	FILE * dStream = NULL;
	char    sDir[64]={0};
	char    dDir[64]={0};
	
	long  endOffset =0;
	long  fileOffset =0;
	long  index =0;
	long  lineWidth=0;
	BYTE  sfileBuffer[512]={0};
	BYTE  dfileBuffer[512]={0};

	long  leftOffset =0,rightOffset =0,topOffset=0,bottomOffset=0;
	int i,j;
	BITMAPFILEHEADER* spBmpHeader,*dpBmpHeader;
	BITMAPINFOHEADER* spBmpInfo,*dpBmpInfo;
	RGBQUAD** spRgbQuad,*dpRgbQuad;
	int rgb_count =0;
	struct _finddata_t filestruct;

	long handle; 

	printf("Please source directory:\n");
	scanf("%s",sDir);

	printf("Please target directory:\n");
	scanf("%s",dDir);



	_chdir(sDir);
	handle   =   _findfirst("*.bmp",   &filestruct);   

	if(filestruct.size ==0)
	{

	_findclose(handle); 
	return;
	}
	_chdir("..");
	_mkdir(dDir);
   
	do
	{
		int index =0;
		leftOffset =0;
		rightOffset =0;
		topOffset=0;
		bottomOffset=0;
		_chdir(sDir);
		sStream = fopen(filestruct.name,"rb");
		fseek(sStream,0,SEEK_END);
		endOffset = ftell(sStream);
		fseek(sStream,0,SEEK_SET);

		memset(sfileBuffer,0,512);
		memset(dfileBuffer,0,512);
		fread(sfileBuffer,endOffset,1,sStream);
		spBmpHeader = (BITMAPFILEHEADER*)sfileBuffer;
		spBmpInfo = (BITMAPINFOHEADER*)&sfileBuffer[sizeof(BITMAPFILEHEADER)];
		spRgbQuad = (RGBQUAD**)&sfileBuffer[sizeof(BITMAPFILEHEADER)+sizeof(BITMAPINFOHEADER)]; 


		fileOffset = sizeof(BITMAPFILEHEADER)+sizeof(BITMAPINFOHEADER) + sizeof(RGBQUAD) * (1<< spBmpInfo->biBitCount);
		lineWidth = (((spBmpInfo->biWidth * (1<<(spBmpInfo->biBitCount-1))/8+3) >>2) <<2);

		leftOffset = spBmpInfo->biWidth;
		rightOffset = spBmpInfo->biWidth;
		fileOffset = endOffset;
		
		memcpy(dfileBuffer,sfileBuffer,spBmpHeader->bfSize);
		dpBmpHeader = (BITMAPFILEHEADER*)dfileBuffer;
		dpBmpInfo = (BITMAPINFOHEADER*)&dfileBuffer[sizeof(BITMAPFILEHEADER)];
		dpRgbQuad = (RGBQUAD*)&dfileBuffer[sizeof(BITMAPFILEHEADER)+sizeof(BITMAPINFOHEADER)]; 

		rgb_count = (spBmpHeader ->bfOffBits - sizeof(BITMAPFILEHEADER) - sizeof(BITMAPINFOHEADER))/4;
		for (i=0; i <rgb_count ;i ++ )
		{
			if(dpRgbQuad[i].rgbRed== 0 && dpRgbQuad[i].rgbGreen== 0 &&dpRgbQuad[i].rgbBlue == 0)
			{
				dpRgbQuad[i].rgbRed= 0xFF;
				dpRgbQuad[i].rgbGreen = 0xFF;
				dpRgbQuad[i].rgbBlue = 0xFF;
			}
			else if(dpRgbQuad[i].rgbRed== 0xFF && dpRgbQuad[i].rgbGreen== 0xFF &&dpRgbQuad[i].rgbBlue == 0xFF)
			{
				dpRgbQuad[i].rgbRed= 0;
				dpRgbQuad[i].rgbGreen = 0;
				dpRgbQuad[i].rgbBlue = 0;
			}
				
		}

//		for (index =dpBmpHeader->bfOffBits ;index < dpBmpHeader->bfSize ;index ++)
//			dfileBuffer[index] = ~dfileBuffer[index];

		_chdir("..");
		_chdir(dDir);
		dStream = fopen(filestruct.name,"wb");
		fwrite(dfileBuffer,dpBmpHeader->bfSize,1,dStream);
		_chdir("..");

		fclose(sStream);
		fclose(dStream);

	}while(!(_findnext(handle,&filestruct)));   
	_findclose(handle); 
}
