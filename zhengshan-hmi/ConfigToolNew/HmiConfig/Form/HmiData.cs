using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace HmiConfig
{
    class xValveCtrlCmd
    { 
	    public UInt16 dBOffsetForOpenCmd;//offset in the database
        public UInt16 openCmd;
        public UInt16 dBOffsetForCloseCmd;//offset in the databse
        public UInt16 closeCmd;
        public UInt16 dBoffsetForStopCmd;//offset in the databse
        public UInt16 stopCmd;
        public UInt16 dBOffsetForCurPosCmd;//offset in the databse
        public UInt16 getCurPosCmd;
        public UInt16 dbOffsetForCurPos;//offset in the database
        public UInt16 curPosMaxVal;

        public xValveCtrlCmd()
        {
            dBOffsetForOpenCmd = 0xFFFF;
            openCmd = 0xFFFF;
            dBOffsetForCloseCmd = 0xFFFF;//offset in the databse
            closeCmd = 0xFFFF;
            dBoffsetForStopCmd = 0xFFFF;//offset in the databse
            stopCmd = 0xFFFF;
            dBOffsetForCurPosCmd = 0xFFFF;//offset in the databse
            getCurPosCmd = 0xFFFF;
            dbOffsetForCurPos = 0xFFFF;//offset in the database
            curPosMaxVal = 100;
        }

        public xValveCtrlCmd(byte[] b)
        {
            int offset = 0;
            dBOffsetForOpenCmd = (UInt16)(b[offset++] | (b[offset++] << 8));
            openCmd = (UInt16)(b[offset++] | (b[offset++] << 8));
            dBOffsetForCloseCmd = (UInt16)(b[offset++] | (b[offset++] << 8));//offset in the databse
            closeCmd = (UInt16)(b[offset++] | (b[offset++] << 8));
            dBoffsetForStopCmd = (UInt16)(b[offset++] | (b[offset++] << 8));//offset in the databse
            stopCmd = (UInt16)(b[offset++] | (b[offset++] << 8));
            dBOffsetForCurPosCmd = (UInt16)(b[offset++] | (b[offset++] << 8));//offset in the databse
            getCurPosCmd = (UInt16)(b[offset++] | (b[offset++] << 8));
            dbOffsetForCurPos = (UInt16)(b[offset++] | (b[offset++] << 8));//offset in the database
            curPosMaxVal = (UInt16)(b[offset++] | (b[offset++] << 8));
        }
        
        public byte[] GetData()
        {
            byte[] b= new byte[DataSizeof];
            int offset = 0;
            b[offset++] = (byte)dBOffsetForOpenCmd;
            b[offset++] = (byte)(dBOffsetForOpenCmd>>8);

            b[offset++] = (byte)openCmd;
            b[offset++] = (byte)(openCmd>>8);

            b[offset++] = (byte)dBOffsetForCloseCmd;
            b[offset++] = (byte)(dBOffsetForCloseCmd>>8);

            b[offset++] = (byte)closeCmd;
            b[offset++] = (byte)(closeCmd>>8);

            b[offset++] = (byte)dBoffsetForStopCmd;
            b[offset++] = (byte)(dBoffsetForStopCmd>>8);

            b[offset++] = (byte)stopCmd;
            b[offset++] = (byte)(stopCmd>>8);

            b[offset++] = (byte)dBOffsetForCurPosCmd;
            b[offset++] = (byte)(dBOffsetForCurPosCmd>>8);

            b[offset++] = (byte)getCurPosCmd;
            b[offset++] = (byte)(getCurPosCmd>>8);

            b[offset++] = (byte)dbOffsetForCurPos;
            b[offset++] = (byte)(dbOffsetForCurPos>>8);

            b[offset++] = (byte)curPosMaxVal;
            b[offset++] = (byte)(curPosMaxVal >> 8);

            return b;
        }

        public static int DataSizeof
        {
            get
            {
                return sizeof(UInt16)*10;
            }
        }
    }
    class xValveView
    {
        public UInt32 dBOffsetForOpenVal;
        public UInt32 dBOffsetForCloseVal;
        public UInt32 dBOffsetForLRVal;
        public UInt32 dBOffsetForCurPos;
        public UInt32 curPosMaxVal;
        public xValveView()
        {
            dBOffsetForOpenVal = 0xFFFFFFFF;
            dBOffsetForCloseVal = 0xFFFFFFFF;
            dBOffsetForLRVal = 0xFFFFFFFF;
            dBOffsetForCurPos = 0xFFFFFFFF;
            curPosMaxVal = 100;     
        }
        public xValveView(byte[] b)
        {
            int offset = 0;
            dBOffsetForOpenVal = (UInt32)(b[offset++] | (b[offset++] << 8) | (b[offset++] << 16) | (b[offset++] << 24));
            dBOffsetForCloseVal = (UInt32)(b[offset++] | (b[offset++] << 8) | (b[offset++] << 16) | (b[offset++] << 24)); 
            dBOffsetForLRVal = (UInt32)(b[offset++] | (b[offset++] << 8) | (b[offset++] << 16) | (b[offset++] << 24));
            dBOffsetForCurPos = (UInt32)(b[offset++] | (b[offset++] << 8) | (b[offset++] << 16) | (b[offset++] << 24));
            curPosMaxVal = (UInt32)(b[offset++] | (b[offset++] << 8) | (b[offset++] << 16) | (b[offset++] << 24)); 
        }
        public byte[] GetData()
        {
            byte[] b= new byte[DataSizeof];
            int offset = 0;
            b[offset++] = (byte)dBOffsetForOpenVal;
            b[offset++] = (byte)(dBOffsetForOpenVal >> 8);
            b[offset++] = (byte)(dBOffsetForOpenVal>>16);
            b[offset++] = (byte)(dBOffsetForOpenVal>>24);

            b[offset++] = (byte)dBOffsetForCloseVal;
            b[offset++] = (byte)(dBOffsetForCloseVal>>8);
            b[offset++] = (byte)(dBOffsetForCloseVal>>16);
            b[offset++] = (byte)(dBOffsetForCloseVal>>24);

            b[offset++] = (byte)dBOffsetForLRVal;
            b[offset++] = (byte)(dBOffsetForLRVal>>8);
            b[offset++] = (byte)(dBOffsetForLRVal>>16);
            b[offset++] = (byte)(dBOffsetForLRVal>>24);

            b[offset++] = (byte)dBOffsetForCurPos;
            b[offset++] = (byte)(dBOffsetForCurPos>>8);
            b[offset++] = (byte)(dBOffsetForCurPos>>16);
            b[offset++] = (byte)(dBOffsetForCurPos>>24);

            b[offset++] = (byte)curPosMaxVal;
            b[offset++] = (byte)(curPosMaxVal>>8);
            b[offset++] = (byte)(curPosMaxVal>>16);
            b[offset++] = (byte)(curPosMaxVal>>24);

            return b;
        }
        public static int DataSizeof
        {
            get
            {
                return sizeof(UInt32) * 5;
            }
        }
    }

    class xValveData
    { 
        public const int VALVE_NAME_MAX_LENTH = 15;
    	public byte[] acValveName = new byte[VALVE_NAME_MAX_LENTH+1];
	    public xValveCtrlCmd xCmd;// = new xValveCtrlCmd();
	    public xValveView xView;// = new xValveView();

        public xValveData()
        {
            for (int i = 0; i < acValveName.Length; i++)
            {
                acValveName[i] = 0;
            }
            xCmd = new xValveCtrlCmd();
	        xView = new xValveView();
        }

        public xValveData(byte[] b)
        {

            int offset = 0;
            Array.Copy(b, offset, acValveName, 0, acValveName.Length);
            offset += (((acValveName.Length + 1) >> 1) << 1);
            byte[] xValveCtrlCmdData = new byte[xValveCtrlCmd.DataSizeof];
            Array.Copy(b, offset, xValveCtrlCmdData, 0, xValveCtrlCmd.DataSizeof);
            offset = (((xValveCtrlCmd.DataSizeof + acValveName.Length  + 3) >> 2) << 2);
            xCmd = new xValveCtrlCmd(xValveCtrlCmdData);
            byte[] xValveViewData = new byte[xValveView.DataSizeof];
            Array.Copy(b, offset, xValveViewData, 0, xValveView.DataSizeof);
            //offset += xValveView.DataSizeof;
            xView = new xValveView(xValveViewData);

        }
        public byte[] GetData()
        {
            int offset=0;
            byte[] b= new byte[DataSizeof];
            
            Array.Copy(acValveName, 0, b, 0, acValveName.Length);
            
            offset += (((acValveName.Length + 1) >> 1) << 1);
            Array.Copy(xCmd.GetData(), 0, b, offset, xValveCtrlCmd.DataSizeof);
            offset = (((xValveCtrlCmd.DataSizeof + acValveName.Length  + 3) >> 2) << 2);
            Array.Copy(xView.GetData(), 0, b, offset, xValveView.DataSizeof);
            return b;
        }
        
        public static int DataSizeof
        {
            get
            {
                return xValveCtrlCmd.DataSizeof + xValveView.DataSizeof + (((VALVE_NAME_MAX_LENTH + 1 + 3) >> 2) <<2);
            }
        }
    }
    class xValveGroup 
    {
        public const int GROUP_VALVE_NUM = 40; 
        public xValveData[] xValve = new xValveData[GROUP_VALVE_NUM];
        public xValveGroup()
        {
            for (int i = 0; i < GROUP_VALVE_NUM; i++)
            {
                xValve[i] = new xValveData();
            }
        }
        public xValveGroup(byte[] b)
        {
            int offset = 0;
            byte[] xValveDataData = new byte[xValveData.DataSizeof];
            for (int i = 0; i < GROUP_VALVE_NUM; i++)
            {
                Array.Copy(b, offset, xValveDataData, 0, xValveData.DataSizeof);
                xValve[i] = new xValveData(xValveDataData);
                offset += xValveData.DataSizeof;
            }
        }
        
        public byte[] GetData()
        {
            ArrayList arrTemp = new ArrayList(); 
            for (int i = 0; i < GROUP_VALVE_NUM; i++)
            {
                arrTemp.AddRange(xValve[i].GetData()); 
            }

            return (byte[])arrTemp.ToArray(typeof(byte));
        }

        public static int DataSizeof
        {
            get
            {
                return xValveData.DataSizeof * GROUP_VALVE_NUM;
            }
        }
    }
    class HmiData
    {
        public const int GROUPS_NUM = 4;
        public const int PASSWORD_MAX_LENTH = 6;
        public const int TEXT_MAX_LENTH = 30;

        public const int VALVE_ADDR_MAX_LENTH = 6;
        public const int VALVE_CMD_MAX_LENTH = 6;
        public string m_filename;
        public bool m_loaded;
        public byte[] password = new byte[(((PASSWORD_MAX_LENTH + 1 + 1) >> 1) << 1)];
        public xValveGroup[] xGroup = new xValveGroup[GROUPS_NUM];

        public HmiData()
        {
            for (int i = 0; i < PASSWORD_MAX_LENTH; i++)
            {
                password[i] = (byte)'1';
            }
            password[PASSWORD_MAX_LENTH] = 0;
            for (int i = 0; i < GROUPS_NUM; i++)
            {
                xGroup[i] = new xValveGroup();
            }
        }

        public HmiData(string filename)
        {
            FileStream fs;
            m_filename = filename;
            using (fs = File.OpenRead(m_filename)) 
            {
                byte[] b = new byte[fs.Length];
                if (fs.Read(b, 0, (int)fs.Length) == fs.Length)
                {
                    int offset = 0;
                    byte[] xValveGroupDate = new byte[xValveGroup.DataSizeof];
                    if (fs.Length == xValveGroup.DataSizeof * GROUPS_NUM + password.Length)
                    {
                        Array.Copy(b, offset, password, 0, PASSWORD_MAX_LENTH+ 1);
                        offset += password.Length;
                        for (int i = 0; i < GROUPS_NUM; i++)
                        {
                            Array.Copy(b, offset, xValveGroupDate, 0, xValveGroup.DataSizeof);
                            xGroup[i] = new xValveGroup(xValveGroupDate);
                            offset += xValveGroup.DataSizeof;
                        }
                        m_loaded = true;
                    }
                }
            }

        }

        public byte[] GetData()
        {
            ArrayList arrTemp = new ArrayList();
            arrTemp.AddRange(password);
            for (int i = 0; i < GROUPS_NUM; i++)
            {
                arrTemp.AddRange(xGroup[i].GetData());
            }

            return (byte[])arrTemp.ToArray(typeof(byte));        
        }

        public string FileName
        {
            get
            {
                return m_filename;
            }
        }

        public void SaveFile(string filename)
        {
            File.WriteAllBytes(filename, GetData());
            m_filename = filename;

        }
        public void SaveFile()
        {
            File.WriteAllBytes(m_filename, GetData());
        }
    }
}
