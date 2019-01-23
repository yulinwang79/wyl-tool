typedef struct {
	unsigned short dBOffsetForOpenCmd;//offset in the database
	unsigned short openCmd;
	unsigned short dBOffsetForCloseCmd;//offset in the databse
	unsigned short closeCmd;
	unsigned short dBoffsetForStopCmd;//offset in the databse
	unsigned short stopCmd;
	unsigned short dBOffsetForCurPosCmd;//offset in the databse
	unsigned short getCurPosCmd;
	unsigned short dbOffsetForCurPos;//offset in the database
}xValveCtrlCmd;

typedef struct {
	char acValveName[16];
	xValveCtrlCmd xCmd;
}xValveData;

typedef struct {
	xValveData xValve[48];
	
}xValveGroup;