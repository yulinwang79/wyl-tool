#include <stdio.h>
#include <stdlib.h>
#include <string.h>
int Enteroffset =0;
int acsiitonumber(char ch)
{
	if((ch >= '0' && ch <= '9')
		||(ch >= 'a' && ch <= 'f')
		||(ch >= 'A' && ch <= 'F'))
	{
				if(ch >= 'a')
					return ch- 'a' + 10;
				else if(ch >= 'A')
					return ch- 'A' + 10;
				else
					return ch- '0';
	}
	__asm int 3;
	return 0;
}
int isNumber(char ch)
{
	if((ch >= '0' && ch <= '9')
		||(ch >= 'a' && ch <= 'f')
		||(ch >= 'A' && ch <= 'F')
		||ch == 'x'
		||ch == 'X')
	{
				return 1;
	}
	return 0;
}
long  in1dex1 =0;
void main(void)
{
	FILE * sStream = NULL;
	FILE * dStream = NULL;
	char    sFilename[64]={0};
	char    dFilename[64]={0};
	long  endOffset =0;
	long  buffOffset =0;
	long  j=0,k=0,q=0;
	char char_buf[20]={0};
	char ch;
	char * fileptr=0,*filehead,*srcBuff;
	int shiwei=0,gewei =0,value;
    

	char*  bin = NULL;
	do{
		printf("Please input file name:\n");
		scanf("%s",sFilename);
		if(sFilename[0]== 'q')
		{
			return;
		}
		if(strlen(sFilename)>= 0)
		{
			sStream = fopen(sFilename,"rb");
		}
	}while(!sStream);

	sprintf(dFilename,"%s.a",sFilename);

	fseek(sStream,0,SEEK_END);
	endOffset = ftell(sStream);
	fseek(sStream,0,SEEK_SET);

	filehead=fileptr = (char*)malloc(endOffset + 100);
	srcBuff = (char*)malloc(endOffset + 100);

	dStream = fopen(dFilename,"wb");
	fread(srcBuff,1,endOffset,sStream);
	fclose(sStream);
	do{
		ch=srcBuff[in1dex1 ++];
	}while(ch != '{');
	in1dex1++;
	while(in1dex1<endOffset)
	{
		j =q= 0;
		
		do{
			char_buf[j] = srcBuff[in1dex1 ++];
			j ++;
		}while(isNumber(char_buf[j-1]));
        
		if(j-1==q) continue;
		j--;
		char_buf[j] = 0;
		while(q < j)
		{
			if(char_buf[q] == '0' && (char_buf[q+1] == 'x'|| char_buf[q+1] == 'X'))
			{
				if((char_buf[q+3] >= '0' && char_buf[q+3] <= '9')
					||(char_buf[q+3] >= 'a' && char_buf[q+3] <= 'f')
					||(char_buf[q+3] >= 'A' && char_buf[q+3] <= 'F'))
					{
						shiwei = acsiitonumber(char_buf[q+2]);
						gewei = acsiitonumber(char_buf[q+3]);

					}
					else
					{
						shiwei = 0;
						gewei = acsiitonumber(char_buf[q+2]);
					}
					*fileptr = shiwei *16 +gewei;
					fileptr ++;
			}
			else
			{
					int n = atoi(char_buf);
					*fileptr = (char)n;
					fileptr ++;
					break;
			
			}
			q++;
		}
	}
//	fwrite(filehead,fileptr-filehead,1,dStream);
if(fwrite(filehead,fileptr-filehead,1,dStream)!=1)
    printf("file write error\n");

	free(filehead);
	free(srcBuff);
	fclose(dStream);

	printf("Done!\n");

}
