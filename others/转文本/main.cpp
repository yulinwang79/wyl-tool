#include <stdio.h>
#include <stdlib.h>
#include <string.h>
int Enteroffset =0;
static int fwriteChar (char *s,FILE *stream)
{
	int i=0;
	if(!s){ fclose(stream); return 0;}
	
	while (s[i]) 
	{
		putc(s[i],stream);
		i++;
	}
	if('\n' == s[i])
	{
		Enteroffset = 0;
		return i;
	}	
	putc(',',stream);
	Enteroffset ++;
	if(Enteroffset == 16)
	{
		Enteroffset =0;
		putc('\n',stream);
		putc('\t',stream);
	}
		
	return i;
}

void main(void)
{
	FILE * sStream = NULL;
	FILE * dStream = NULL;
	char    sFilename[64]={0};
	char    dFilename[64]={0};
	long  endOffset =0;
	long  index;
	char char_buf[8]={0};
	char* pchar;
	char*  bin = NULL;
	int isLongFile =0;
	do{
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
		Enteroffset = 0;
	}while(!sStream);

	sprintf(dFilename,"%s.c",sFilename);

	fseek(sStream,0,SEEK_END);
	endOffset = ftell(sStream);
	fseek(sStream,0,SEEK_SET);
	if(endOffset > 1024*1024)
		isLongFile = 1;
	else
		isLongFile = 0;
	dStream = fopen(dFilename,"w");
	pchar = sFilename;
	while(*pchar)
	{
		if(*pchar == '.')
			*pchar = '_';
		pchar++;
	}
	fprintf(dStream,"__align(4) static const unsigned char %s[]= \n{\n\t",sFilename);

	for(index=0; index<endOffset ; index++)
	{
		if(isLongFile)
			sprintf(char_buf,"%d",getc(sStream));
		else			
			sprintf(char_buf," 0x%02X",getc(sStream));
		fwriteChar(char_buf,dStream);
	}
	fprintf(dStream,"\n};\n");

	fclose(sStream);
	fclose(dStream);

	printf("Done!\n");

	}while(1);
}
