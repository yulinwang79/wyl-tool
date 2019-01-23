using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Data;
using System.Xml;
using System.Windows.Forms;
using System.Text.RegularExpressions;

//using System.Data.DataColumn;

namespace Config
{
    public enum EditType{
                EDIT_NONE =0x0,
                EDIT_PORT_CONFIG = 0x1001,
                EDIT_PCPORT_CONFIG = 0x1002,
                EDIT_ETHERNET_CONFIG = 0x1003,
                EDIT_ETHERNET_CLIENT_CONFIG = 0x1004,
                EDIT_ETHERNET_SERVER_CONFIG = 0x1005,
                EDIT_PORT_COMMAND = 0x2001,
                EDIT_PCPORT_COMMAND = 0x2002,
                EDIT_ETHERNET_CLIENT_COMMAND = 0x2003,
                EDIT_DATA_REMAP = 0x2004,
    };
    
    public enum ErrorInfo{
        Error_None = 0x0,
        Error_Field = 0x1,
        Error_Value = 0x2, 
        Error_All = 0x3, 
        
        Error_Unknown = 0x10,
        };
    public class ConfigNode
    {
        public string m_name = "";
        protected string m_text = "";
        public EditType m_type;
        public XmlNodeList m_nodeList;

        public EditType Type 
        {
            get
            {
                return m_type;
            }
        }

        public string GetName()
        {
            if (m_name.Length > 3)
            {
                string res = m_name.Trim('[');

                return res.Trim(']');
            }
            return m_name;
        }



        public virtual DataTable CreateDataTable()
        {
            return null; 
        }
        public virtual int Update(DataTable dt)
        {
            return 0;
        }
        
        public virtual string GetTextStringWithErrorInfo( ArrayList OffsetArray, ArrayList LengthArray )
        {
            return null; 
        }
        public virtual string GetTextString( )
        {
            return null; 
        }
        public virtual bool CheckContent( )
        {
            return false;
        }
        public bool isNumeric(string message, out int result)
        {
            result = -1;
            try
            {
   
              //result = int.Parse(message);
              //result = Convert.ToInt16(message);
                result = Convert.ToInt32(message);    
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool IsValid(string s_value, string s_min,string s_max)
        {
            int value, min, max;
            if(isNumeric(s_value,out value) && isNumeric(s_min,out min) &&isNumeric(s_max,out max)
                && value >= min && value <= max)
            {
                return true;
            }
            return false;
        }

        public bool CheckValue(string s_range, string s_value)
        {
            string[] stringSeparators = new string[] { "/"  };
            string[] item_sel = s_range.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            if(item_sel.Length>=2)
            {
                for(int i=0; i<item_sel.Length; i++)
                {
                    if (string.Compare(s_value, item_sel[i]) ==0)
                    {
                        return true;
                    }
                }
            }
            else if(s_range.IndexOf('~')!=-1)
            {
                string[] TildeSeparators = new string[] { "~"  };
                string[] range = s_range.Split(TildeSeparators, StringSplitOptions.RemoveEmptyEntries);
                if(IsValid(s_value,range[0],range[1]))
                {
                    return true;

                }
            }
            else if(s_range.IndexOf("IP:Port")!=-1)
            {
            
                string ip_pattern = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5]):\d+";//ip 地?址·的?正y则ò表括?达?式?
                Regex rgx_ip_adr = new Regex(ip_pattern, RegexOptions.IgnoreCase);
            
            // Find matches.
                MatchCollection matches = rgx_ip_adr.Matches(s_value);
                if(matches.Count ==1)
                {
                    return true;

                }
            }
            else if(s_range.IndexOf("IP")!=-1)
            {
            
                string ip_pattern = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])";//ip 地?址·的?正y则ò表括?达?式?
                Regex rgx_ip_adr = new Regex(ip_pattern, RegexOptions.IgnoreCase);
            
            // Find matches.
                MatchCollection matches = rgx_ip_adr.Matches(s_value);
                if(matches.Count ==1)
                {
                    return true;
                }
            }    
            return false;
        }
    

    }

    public class PortConfig:ConfigNode
    {
        public string m_comment = "";
        public string[] m_item_comment;
        public string[] m_value;
        public ErrorInfo[] m_error;
        public PortConfig(string name, string content, EditType type = Config.EditType.EDIT_NONE, XmlNodeList nodeList = null)
        {
            string tmp;
            m_text = content;
            m_name = name;
            m_type = type;
            m_nodeList = nodeList;
            
            m_value = new string[m_nodeList.Count];
            m_error = new ErrorInfo[m_nodeList.Count];
            m_item_comment = new string[m_nodeList.Count];
            
            int pound_offset;
            string[] stringEnter = new string[] { "\n"  };

           if(m_text == null)
           {
               for (int i=0; i< m_nodeList.Count; i++)
               {
                   XmlElement xe = (XmlElement)m_nodeList[i];//将子节点类型转换为XmlElement类型
				   m_value[i]=xe.GetAttribute("defaultValue");
			   }
            
		   }
		   else
		   	{
           if((((uint)m_type) & (0x1000)) == 0x1000 )
            {
           string[] text_line = m_text.Split(stringEnter, StringSplitOptions.RemoveEmptyEntries);
           if(text_line.Length >= 2)
           {
               //eg:
               //Enable                          : Disable

               for (int j = 1; j < text_line.Length; j++)
               {
                   tmp = text_line[j].Trim();
                   if (tmp.Length < 3 || tmp[0] == '#')
                   {
                      if (tmp.Length >0 && tmp[0] == '#')
                      {
                          m_comment = tmp.TrimStart('#'); 
                      }
                      continue;
                   }
                   else if(text_line[j].IndexOf(':')!=-1)
                   {
                       string[] item_value = new string[2];
                       item_value[0] = text_line[j].Substring(0, text_line[j].IndexOf(":") - 1).Trim();
                       item_value[1] = text_line[j].Substring(text_line[j].IndexOf(":") + 1).Trim();

                       for (int i=0; i< m_nodeList.Count; i++)
                       {
                            XmlElement xe = (XmlElement)m_nodeList[i];//将子节点类型转换为XmlElement类型
                            if (string.Compare(xe.GetAttribute("name"), item_value[0]) ==0)//如果name属性值为tbCfgName.Text
                            {
                                pound_offset = item_value[1].IndexOf('#');
                                if(pound_offset>0)
                                {
                                    m_value[i]= item_value[1].Substring(0, pound_offset - 1).Trim();
                                    m_item_comment[i] = item_value[1].Substring(pound_offset + 1).Trim();
                                }
                                else
                                {
                                    m_value[i]= item_value[1];
                                }
                            }
                       }
                   }
               }                                 
                   
           }
            }
           else
            {
               pound_offset = m_text.IndexOf('#');
               if(pound_offset!=-1)
               {
                  m_comment = m_text.Substring(pound_offset + 1);
               }
               string[] stringSeparators = new string[] { "  "  };
               string[] item_value = content.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
               for(int i=0; i< m_nodeList.Count && i < item_value.Length; i++)
               {
                  m_value[i]=item_value[i].Trim();
               }
               if(item_value.Length < m_nodeList.Count)
               {
                   for(int i= item_value.Length; i< m_nodeList.Count; i++)
                   {
                      m_value[i]="";
                   }                
               }

           }
		   	}
           //CheckContent();
        }
        
        public override DataTable CreateDataTable()
        {

         //string[] text_line = m_text.Split('\n');

         //if(text_line.Length >=2)
         {
             DataTable dtPortCommards = new DataTable("PortConfig");
             dtPortCommards.Columns.Add(new DataColumn("Item"));
             dtPortCommards.Columns.Add(new DataColumn("Value"));
             dtPortCommards.Columns.Add(new DataColumn("Comment"));
             for (int i = 0; i < m_nodeList.Count; i++)
             {
                    XmlElement xe = (XmlElement)m_nodeList[i];
                    string[] item_value = new string[3];
                    item_value[0] = xe.GetAttribute("name");

                    if (!string.IsNullOrEmpty(m_value[i]))
                        item_value[1] = m_value[i];
                    else
                        item_value[1] = "";
                    
                    //if (!string.IsNullOrEmpty(m_item_comment[i]))
                    //    item_value[2] = m_item_comment[i];
                    //else
                    //    item_value[2] = "";
                    dtPortCommards.Rows.Add(item_value);
             }                                 
                 
             return dtPortCommards;
         }
        //return null;
        }
         public override int Update(DataTable dt)
        {
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                m_value[j]= dt.Rows[j].ItemArray[1].ToString();
            }
            CheckContent();
            return 1;
        }
         public override string GetTextStringWithErrorInfo( ArrayList OffsetArray, ArrayList LengthArray )
         {
             string temp = "";
             if ((((uint)m_type) & (0x1000)) == (uint)0x1000)
             {
         
                 temp += m_name;
                 temp += "\n";
                 for (int j = 0; j < m_nodeList.Count; j++)
                 {
                     XmlElement xe = (XmlElement)m_nodeList[j];
                     if(OffsetArray!=null && m_error[j] == ErrorInfo.Error_Field)
                     {
                         OffsetArray.Add(temp.Length);
                         LengthArray.Add(32);
                     }
                     temp += xe.GetAttribute("name").PadRight(32, ' ');
                     temp += ": ";
                     if(OffsetArray!=null && m_error[j] == ErrorInfo.Error_Value)
                     {
                         OffsetArray.Add(temp.Length);
                         LengthArray.Add(m_value[j].Length);

                     }
                     if (!string.IsNullOrEmpty(m_value[j]))
                         temp += m_value[j].PadRight(32, ' ');
                     
                     if (!string.IsNullOrEmpty(m_item_comment[j]))
                     {
                         temp += '#' + m_item_comment[j].PadRight(32, ' ');
                     }
                     temp += "\n";
                   
                 }
                 if (m_comment.Length > 0)
                 {
                     temp += '#' + m_comment + '\n';
                 }
                 temp += "\n";
             }
             else
             {
             
                 int[] ColOffset = new int[m_nodeList.Count];
                 temp = "     ";
                 for (int i = 0; i < m_nodeList.Count; i++ )
                 {
                       XmlElement xe = (XmlElement)m_nodeList[i];
                       if (xe.GetAttribute("name").IndexOf("IP") < 0)
                       {
                           ColOffset[i] = xe.GetAttribute("name").Length + 2;
                       }
                       else
                       {
                           ColOffset[i] = 21;
                       }
                 }

                 for (int i = 0; i < m_nodeList.Count; i++)
                 {
                     if(OffsetArray!=null && m_error[i] != ErrorInfo.Error_None)
                     {
                         OffsetArray.Add(temp.Length);
                         LengthArray.Add(ColOffset[i]);
                     }

                     temp += m_value[i].PadRight(ColOffset[i], ' ');
                 }
                 if (m_comment.Length > 0)
                 {
                     temp += '#' + m_comment;
                 }                  
                 if (temp.Trim().Length > 0)
                 {
                     temp = temp.Replace("\r","") + '\n';
                 }

             }
             m_text = temp;
             //CheckContent();
             
             return m_text;
         }

         public override string GetTextString( )
         {
             return GetTextStringWithErrorInfo(null,null);
         }

    
       public override bool CheckContent( )
       {
           bool result = true;
           for (int i=0; i< m_nodeList.Count; i++)
           {
               if (!string.IsNullOrEmpty(m_value[i]))
                {
                    XmlElement xe = (XmlElement)m_nodeList[i];
                    if(CheckValue(xe.InnerText.Trim(),m_value[i]))
                    {
                        m_error[i] = ErrorInfo.Error_None;
                    }
                    else
                    {
                        m_error[i] = ErrorInfo.Error_Value;
                        result = false;
                    }
                }
                else
                {
                    m_error[i] = ErrorInfo.Error_Field;
                    result = false;
                }
            }
            return result;
        }
    }

    public class PortCommands : ConfigNode
    {
        public ArrayList m_ItemArray = new ArrayList();
        public bool[] m_error;

        public PortCommands(string name, string content, EditType type = Config.EditType.EDIT_NONE, XmlNodeList nodeList = null)
         {
             m_text = content;
             m_name = name;
             m_type = type;
             m_nodeList = nodeList;
    		if(m_text != null)
    		{
                int colCount=0;
                string[] stringEnter = new string[] { "\n"  };
                string[] text_line = m_text.Split(stringEnter, StringSplitOptions.RemoveEmptyEntries);
                string tmp;
    		     if(text_line.Length >=2)
    		     {
                     colCount = m_nodeList.Count + 1;

                     for (int j = 2; j < text_line.Length; j++)
                     {
                         tmp = text_line[j].Trim();
                         if (tmp.Length < colCount -1 || tmp[0] == '#')
                             continue;
                         else
                         {
                             m_ItemArray.Add(new PortConfig(m_name, text_line[j], m_type,m_nodeList));
                         }
                     }
                 }
    		}   
			else
			{
				m_ItemArray.Add(new PortConfig(m_name, null, m_type,m_nodeList));
			}
         }


        public override DataTable CreateDataTable()
         {
                 DataTable dtPortCommards = new DataTable("PortCommands");

                 foreach (XmlNode xn in m_nodeList)
                 {
                     XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型
                     DataColumn dtCol = new DataColumn( xe.GetAttribute("name") );
                     dtPortCommards.Columns.Add(dtCol);
                 }
                 dtPortCommards.Columns.Add(new DataColumn("Comment"));
                 
                 for (int i = 0; i < m_ItemArray.Count; i++)
                 {
                     dtPortCommards.Rows.Add(((PortConfig)m_ItemArray[i]).m_value);
                     if (((PortConfig)m_ItemArray[i]).m_comment.Length > 0)
                     {
                         dtPortCommards.Rows[i][m_nodeList.Count] = '#' + ((PortConfig)m_ItemArray[i]).m_comment;
                     }
                     else
                     {
                         dtPortCommards.Rows[i][m_nodeList.Count] = "";
                     }
                 }

                 return dtPortCommards;
 
         }

         public override int Update(DataTable dt)
        {
            ArrayList ItemArray = new ArrayList();
            string temp = "    ";
            string[] item_value = new string[dt.Columns.Count];
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    temp += dt.Rows[j].ItemArray[i].ToString()+"  ";
                }
                ItemArray.Add(new PortConfig(m_name, temp, m_type,m_nodeList));
                temp = "    ";
            }
            m_ItemArray = ItemArray;
            
            CheckContent();
            return 1;
        }
          public override string GetTextStringWithErrorInfo( ArrayList OffsetArray, ArrayList LengthArray )
         {

            int[] ColOffset = new int[m_nodeList.Count];
             string temp = "";
            temp += m_name;
            temp += "\n#    ";
            for (int i = 0; i < m_nodeList.Count; i++ )
            {
                XmlElement xe = (XmlElement)m_nodeList[i];//将子节点类型转换为XmlElement类型
                if (xe.GetAttribute("name").IndexOf("IP") < 0)
                {
                    ColOffset[i] = xe.GetAttribute("name").Length + 2;
                }
                else
                {
                    ColOffset[i] = 21;
                }
                temp += xe.GetAttribute("name").PadRight(ColOffset[i], ' ');
            }
         
             temp += "\n";
             for (int j = 0; j < m_ItemArray.Count; j++)
             {
                
                if(OffsetArray!=null)
                {
                 ArrayList ItemOffsetArray = new ArrayList();
                 ArrayList ItemLengthArray = new ArrayList();
                 int offset = temp.Length;
                 temp += ((PortConfig)m_ItemArray[j]).GetTextStringWithErrorInfo(ItemOffsetArray,ItemLengthArray);
                 for(int i=0;i <ItemOffsetArray.Count; i++ )
                 {
                     OffsetArray.Add((int)ItemOffsetArray[i]+ offset);
                     LengthArray.Add((int)ItemLengthArray[i]);
                 }
                }
                else
                {
                    temp += ((PortConfig)m_ItemArray[j]).GetTextString();
                }


             }
           
             m_text = temp;
             return m_text;
         
         }

         public override string GetTextString( )
        {
            return GetTextStringWithErrorInfo(null,null);
        }
        public override bool CheckContent( )
        {
            m_error = new bool[m_ItemArray.Count];
            for (int j = 0; j < m_ItemArray.Count; j++)
            {
                m_error[j] = !((PortConfig)m_ItemArray[j]).CheckContent();
            }
            for (int j = 0; j < m_ItemArray.Count; j++)
            {
                if (m_error[j] == true)
                {
                    return false;
                }
            }
            return true;
        }		
     }

    public class PortConfigFile
    {
        private string m_filename;
        const string MODULE_HASH = "[Hash]";

        public const int PORT_CFG_NUM = 8;
        public const int PORT_PC_NUM = 2;
        public const int PORT_ETHERNET_NUM = 2;
        public const int PORT_ETHERNET_CLIENT_NUM = 2;
        public const int PORT_ETHERNET_SERVER_NUM = 2;

        //public string m_module_text;
        public string m_moduleConfig;
        public ArrayList m_portConfigArray = new ArrayList();
        public ArrayList m_portCommandsArray = new ArrayList();
        public ArrayList m_pcPortConfigArray = new ArrayList();
        public ArrayList m_pcPortCommandsArray = new ArrayList();
        public ArrayList m_etherNetPortArray = new ArrayList();
        public ArrayList m_etherNetClientConfigArray = new ArrayList();
        public ArrayList m_etherNetClientCommandsArray = new ArrayList();
        public ArrayList m_etherNetServerConfigArray = new ArrayList();
        public PortCommands m_dataRemap;
        public XmlDocument m_xmlDoc;
        public EditType m_error = EditType.EDIT_NONE;
        public PortConfigFile(string filename)
        {
            m_filename = filename;
			PortConfigInit(filename);

        }

        public PortConfigFile()
        {
        	m_filename = null;
            PortConfigInit(null);
        }

        public int PortConfigInit(string filename)
         {

			 FileStream fs;
			 byte[] b=null;
			 
             int buffer_start;
             int buffer_len;
             string field_name=null;
             UTF8Encoding temp = new UTF8Encoding(true);
			 string content=null;

			 if(filename!=null)
		 	 {
				 using (fs = File.OpenRead(filename)) 
				 {
					 b= new byte[fs.Length];
					 if (fs.Read(b, 0, (int)fs.Length) != fs.Length)
					 {
						 return -1;
					 }
				 }
		 	 }
			 
             m_xmlDoc =new XmlDocument();
             string desc_file = System.Windows.Forms.Application.StartupPath + "\\description.xml";
             if (!File.Exists(desc_file))
             {
                 File.WriteAllText(desc_file, Config.Resource.Resource.Description);
             }
             //m_xmlDoc.LoadXml(Config.Resource.Resource.Description);
             m_xmlDoc.Load(desc_file);
			 if(filename!=null)
			 {
             	content = temp.GetString(b).Replace("	","    ");
	             //buffer_start = 0;
	             buffer_start = content.IndexOf(MODULE_HASH);
	             buffer_len = content.IndexOf('[', buffer_start + 1) - buffer_start;
	             m_moduleConfig = content.Substring(buffer_start, buffer_len);
	             //m_moduleConfig = new PortConfig(MODULE_HASH, content.Substring(buffer_start, buffer_len));
			 }
             for (int i = 0; i < PORT_CFG_NUM; i++)
             {
                 if(i>= PORT_CFG_NUM/2)
                 {
                     field_name = "[Modbus G"+ (i-PORT_CFG_NUM/2 + 1)+ "B]";
                 }
                 else
                 {
                    field_name = "[Modbus G"+ (i + 1)+ "A]";
                 }
				 
				 if(filename!=null)
				 {
	                 buffer_start = content.IndexOf(field_name);
	                 if (buffer_start > 0)
	                 {
	                     buffer_len = content.IndexOf('[', buffer_start + 1) - buffer_start;
	                     if (buffer_len < 0)
	                     {
	                         buffer_len = content.Length - buffer_start;
	                     }

	                     m_portConfigArray.Add(new PortConfig(field_name, content.Substring(buffer_start, buffer_len), Config.EditType.EDIT_PORT_CONFIG,GetXmlNodeList(Config.EditType.EDIT_PORT_CONFIG)));
	                 }
				 }
                 else
                 {
                    m_portConfigArray.Add(new PortConfig(field_name, null, Config.EditType.EDIT_PORT_CONFIG,GetXmlNodeList(Config.EditType.EDIT_PORT_CONFIG)));
                 }
                    
             }
             for (int i = 0; i < PORT_CFG_NUM; i++)
             {
                 if(i>= PORT_CFG_NUM/2)
                 {
                     field_name = "[Modbus G"+ (i-PORT_CFG_NUM/2 + 1)+ "B Commands]";
                 }
                 else
                 {
                    field_name = "[Modbus G"+ (i + 1)+ "A Commands]";
                 }

				 if(filename!=null)
				 {

                 buffer_start = content.IndexOf(field_name);
                 if (buffer_start > 0)
                 {
                     buffer_len = content.IndexOf('[', buffer_start + 1) - buffer_start;
                     if (buffer_len < 0)
                     {
                         buffer_len = content.Length - buffer_start;
                     }
                     m_portCommandsArray.Add(new PortCommands(field_name, content.Substring(buffer_start, buffer_len), Config.EditType.EDIT_PORT_COMMAND,GetXmlNodeList(Config.EditType.EDIT_PORT_COMMAND)));
                 }
                    }
                 else
                 {
                     m_portCommandsArray.Add(new PortCommands(field_name, null, Config.EditType.EDIT_PORT_COMMAND,GetXmlNodeList(Config.EditType.EDIT_PORT_COMMAND)));
                 }
             }
             for (int i = 0; i < PORT_PC_NUM; i++)
             {
                 field_name = "[Modbus PC Port "+i+"]";
                 
				 if(filename!=null)
				 {
                 buffer_start = content.IndexOf(field_name);
                 if (buffer_start > 0)
                 {

                     buffer_len = content.IndexOf('[', buffer_start + 1) - buffer_start;
                     if (buffer_len < 0)
                     {
                         buffer_len = content.Length - buffer_start;
                     }

                     m_pcPortConfigArray.Add(new PortConfig(field_name, content.Substring(buffer_start, buffer_len), Config.EditType.EDIT_PCPORT_CONFIG,GetXmlNodeList(Config.EditType.EDIT_PCPORT_CONFIG)));
                 }
                    }
                 else
                 {
                     m_pcPortConfigArray.Add(new PortConfig(field_name, null, Config.EditType.EDIT_PCPORT_CONFIG,GetXmlNodeList(Config.EditType.EDIT_PCPORT_CONFIG)));
                 }
             }
             for (int i = 0; i < PORT_PC_NUM; i++)
             {
                 field_name = "[Modbus PC Port " + i + " Commands]";
                 
				 if(filename!=null)
				 {
                 buffer_start = content.IndexOf(field_name);
                 if (buffer_start > 0)
                 {

                     buffer_len = content.IndexOf('[', buffer_start + 1) - buffer_start;
                     if (buffer_len < 0)
                     {
                         buffer_len = content.Length - buffer_start;
                     }

                     m_pcPortCommandsArray.Add(new PortCommands(field_name, content.Substring(buffer_start, buffer_len), Config.EditType.EDIT_PCPORT_COMMAND,GetXmlNodeList(Config.EditType.EDIT_PCPORT_COMMAND)));
                 }
                    }
                 else
                 {
                     m_pcPortCommandsArray.Add(new PortCommands(field_name, null, Config.EditType.EDIT_PCPORT_COMMAND,GetXmlNodeList(Config.EditType.EDIT_PCPORT_COMMAND)));
                 }
             }

             for (int i = 0; i < PORT_ETHERNET_NUM; i++)
             {
                 field_name = "[Ethernet " + i + "]";
                 
				 if(filename!=null)
				 {
                 buffer_start = content.IndexOf(field_name);
                 if (buffer_start > 0)
                 {

                     buffer_len = content.IndexOf('[', buffer_start + 1) - buffer_start;
                     if (buffer_len < 0)
                     {
                         buffer_len = content.Length - buffer_start;
                     }
                     m_etherNetPortArray.Add(new PortConfig(field_name, content.Substring(buffer_start, buffer_len), Config.EditType.EDIT_ETHERNET_CONFIG,GetXmlNodeList(Config.EditType.EDIT_ETHERNET_CONFIG)));
                 }
                    }
                 else
                 {
                     m_etherNetPortArray.Add(new PortConfig(field_name, null, Config.EditType.EDIT_ETHERNET_CONFIG,GetXmlNodeList(Config.EditType.EDIT_ETHERNET_CONFIG)));
                 }
             }
              for (int i = 0; i < PORT_ETHERNET_NUM; i++)
             {
                 field_name = "[Ethernet Client " + i + "]";
                 
				 if(filename!=null)
				 {
                 buffer_start = content.IndexOf(field_name);
                 if (buffer_start > 0)
                 {

                     buffer_len = content.IndexOf('[', buffer_start + 1) - buffer_start;
                     if (buffer_len < 0)
                     {
                         buffer_len = content.Length - buffer_start;
                     }
                     m_etherNetClientConfigArray.Add(new PortConfig(field_name, content.Substring(buffer_start, buffer_len), Config.EditType.EDIT_ETHERNET_CLIENT_CONFIG,GetXmlNodeList(Config.EditType.EDIT_ETHERNET_CLIENT_CONFIG)));
                 }
                    }
                 else
                 {
                     m_etherNetClientConfigArray.Add(new PortConfig(field_name, null, Config.EditType.EDIT_ETHERNET_CLIENT_CONFIG,GetXmlNodeList(Config.EditType.EDIT_ETHERNET_CLIENT_CONFIG)));
                 }
             }            
               for (int i = 0; i < PORT_ETHERNET_NUM; i++)
              {
                  field_name = "[Ethernet Client " + i + " Commands]";
                  
                  if(filename!=null)
                  {
                  buffer_start = content.IndexOf(field_name);
                  if (buffer_start > 0)
                  {
              
                      buffer_len = content.IndexOf('[', buffer_start + 1) - buffer_start;
                      if (buffer_len < 0)
                      {
                          buffer_len = content.Length - buffer_start;
                      }
                      m_etherNetClientCommandsArray.Add(new PortCommands(field_name, content.Substring(buffer_start, buffer_len), Config.EditType.EDIT_ETHERNET_CLIENT_COMMAND,GetXmlNodeList(Config.EditType.EDIT_ETHERNET_CLIENT_COMMAND)));
                  }
                    }
                  else
                  {
                      m_etherNetClientCommandsArray.Add(new PortCommands(field_name, null, Config.EditType.EDIT_ETHERNET_CLIENT_COMMAND,GetXmlNodeList(Config.EditType.EDIT_ETHERNET_CLIENT_COMMAND)));
                  }
              }            
                for (int i = 0; i < PORT_ETHERNET_NUM; i++)
               {
                   field_name = "[Ethernet Server " + i + "]";
                   
                   if(filename!=null)
                   {
                   buffer_start = content.IndexOf(field_name);
                   if (buffer_start > 0)
                   {
               
                       buffer_len = content.IndexOf('[', buffer_start + 1) - buffer_start;
                       if (buffer_len < 0)
                       {
                           buffer_len = content.Length - buffer_start;
                       }
                       m_etherNetServerConfigArray.Add(new PortConfig(field_name, content.Substring(buffer_start, buffer_len), Config.EditType.EDIT_ETHERNET_SERVER_CONFIG,GetXmlNodeList(Config.EditType.EDIT_ETHERNET_SERVER_CONFIG)));
                   }
                    }
                   else
                   {
                       m_etherNetServerConfigArray.Add(new PortConfig(field_name, null, Config.EditType.EDIT_ETHERNET_SERVER_CONFIG,GetXmlNodeList(Config.EditType.EDIT_ETHERNET_SERVER_CONFIG)));
                   }
               }            

             field_name = "[Data Remap]";
             if(filename!=null)
             {

             buffer_start = content.IndexOf(field_name);
             if (buffer_start > 0)
             {
             
                 buffer_len = content.IndexOf('[', buffer_start + 1) - buffer_start;
                 if (buffer_len < 0)
                 {
                     buffer_len = content.Length - buffer_start;
                 }
                 m_dataRemap = new PortCommands(field_name, content.Substring(buffer_start, buffer_len), Config.EditType.EDIT_DATA_REMAP,GetXmlNodeList(Config.EditType.EDIT_DATA_REMAP));
             }
                }
             else
             {
                 m_dataRemap = new PortCommands(field_name, null, Config.EditType.EDIT_DATA_REMAP,GetXmlNodeList(Config.EditType.EDIT_DATA_REMAP));
             }
             PortConfigCheckContent();

             return 1;

     }    
     
        public XmlNodeList GetXmlNodeList(EditType type)
     {
         XmlNodeList nodeList;
         string nodeString = "PortConfig";
         switch(type)
         {
             case Config.EditType.EDIT_PORT_CONFIG:
                 nodeString = "PortConfig";
                 break;
             case Config.EditType.EDIT_PORT_COMMAND:
                 nodeString = "PortCommands";
                 break;
             case Config.EditType.EDIT_PCPORT_CONFIG:
                 nodeString = "PCPortConfig";
                 break;
             case Config.EditType.EDIT_PCPORT_COMMAND:
                 nodeString = "PCPortCommands";
                 break;
             case Config.EditType.EDIT_ETHERNET_CONFIG:
                 nodeString = "EthernetConfig";
                 break;
             case Config.EditType.EDIT_ETHERNET_CLIENT_CONFIG:
                 nodeString = "EthernetClientConfig";
                 break;
             case Config.EditType.EDIT_ETHERNET_CLIENT_COMMAND:
                 nodeString = "EthernetClientCommands";
                 break;
             case Config.EditType.EDIT_ETHERNET_SERVER_CONFIG:
                 nodeString = "EthernetServerConfig";
                 break;
             case Config.EditType.EDIT_DATA_REMAP:
                 nodeString = "DataRemap";
                 break;
             default:
                 break;
         }
         nodeList = m_xmlDoc.SelectSingleNode("modbus").ChildNodes;
         foreach (XmlNode xn in nodeList)
         {
             XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型
             if (xe.Name == nodeString)//如果name属性值为tbCfgName.Text
             {
                 return xe.ChildNodes;
             }
         }
        return null;
     }
        public EditType PortConfigCheckContent()
        {
            bool result = true;
            m_error =0;

            for (int i = 0; i < PORT_CFG_NUM; i++)
            {
                if ((result = ((PortConfig)m_portConfigArray[i]).CheckContent()) != true)
                {
                    m_error = EditType.EDIT_PORT_CONFIG;
                }
                if ((result = ((PortCommands)m_portCommandsArray[i]).CheckContent()) != true)
                {
                    m_error = EditType.EDIT_PORT_COMMAND;
                }

            }
            for (int i = 0; i < PORT_PC_NUM; i++)
            {
                if((result = ((PortConfig)m_pcPortConfigArray[i]).CheckContent())!=true)
                {
                    m_error =  EditType.EDIT_PORT_COMMAND;
                }
                if ((result = ((PortCommands)m_pcPortCommandsArray[i]).CheckContent()) != true)
                {
                    m_error = EditType.EDIT_PCPORT_COMMAND;
                }            
            }
            for (int i = 0; i < PORT_ETHERNET_NUM; i++)
            {
                if((result = ((PortConfig)m_etherNetPortArray[i]).CheckContent())!=true)
                {
                    m_error =  EditType.EDIT_ETHERNET_CONFIG;
                }
                if((result = ((PortConfig)m_etherNetClientConfigArray[i]).CheckContent())!=true)
                {
                    m_error =  EditType.EDIT_ETHERNET_CLIENT_CONFIG;
                }
                if((result = ((PortCommands)m_etherNetClientCommandsArray[i]).CheckContent())!=true)
                {
                    m_error =  EditType.EDIT_ETHERNET_CLIENT_COMMAND;
                }
                if((result = ((PortConfig)m_etherNetServerConfigArray[i]).CheckContent())!=true)
                {
                    m_error =  EditType.EDIT_ETHERNET_SERVER_CONFIG;
                }
            }
            return m_error;
        } 
        public string GetTextStringWithErrorInfo( ArrayList OffsetArray, ArrayList LengthArray )
        {
            string temp = "";
            temp += m_moduleConfig;
            ArrayList ItemOffsetArray = new ArrayList();
            ArrayList ItemLengthArray = new ArrayList();
            int offset;
            for (int i = 0; i < PORT_CFG_NUM; i++)
            {
                offset = temp.Length;
                temp += ((PortConfig)m_portConfigArray[i]).GetTextStringWithErrorInfo(ItemOffsetArray, ItemLengthArray).TrimEnd('\n');
                for (int j = 0; j < ItemOffsetArray.Count; j++)
                {
                    OffsetArray.Add((int)ItemOffsetArray[j] + offset);
                    LengthArray.Add((int)ItemLengthArray[j]);
                }
                ItemOffsetArray.Clear();
                ItemLengthArray.Clear();

                temp += "\n\n";

                offset = temp.Length;
                temp += ((PortCommands)m_portCommandsArray[i]).GetTextStringWithErrorInfo(ItemOffsetArray, ItemLengthArray).TrimEnd('\n');
                for (int j = 0; j < ItemOffsetArray.Count; j++)
                {
                    OffsetArray.Add((int)ItemOffsetArray[j] + offset);
                    LengthArray.Add((int)ItemLengthArray[j]);
                }
                ItemOffsetArray.Clear();
                ItemLengthArray.Clear();

                temp += "\n\n";
            }
            for (int i = 0; i < PORT_PC_NUM; i++)
            {
                offset = temp.Length;
                temp += ((PortConfig)m_pcPortConfigArray[i]).GetTextStringWithErrorInfo(ItemOffsetArray, ItemLengthArray).TrimEnd('\n');
                for (int j = 0; j < ItemOffsetArray.Count; j++)
                {
                    OffsetArray.Add((int)ItemOffsetArray[j] + offset);
                    LengthArray.Add((int)ItemLengthArray[j]);
                }
                ItemOffsetArray.Clear();
                ItemLengthArray.Clear();

                temp += "\n\n";

                offset = temp.Length;
                temp += ((PortCommands)m_pcPortCommandsArray[i]).GetTextStringWithErrorInfo(ItemOffsetArray, ItemLengthArray).TrimEnd('\n');
                for (int j = 0; j < ItemOffsetArray.Count; j++)
                {
                    OffsetArray.Add((int)ItemOffsetArray[j] + offset);
                    LengthArray.Add((int)ItemLengthArray[j]);
                }
                ItemOffsetArray.Clear();
                ItemLengthArray.Clear();

                temp += "\n\n";
            }
            for (int i = 0; i < PORT_ETHERNET_NUM; i++)
            {
                offset = temp.Length;
                temp += ((PortConfig)m_etherNetPortArray[i]).GetTextStringWithErrorInfo(ItemOffsetArray, ItemLengthArray).TrimEnd('\n');
                for (int j = 0; j < ItemOffsetArray.Count; j++)
                {
                    OffsetArray.Add((int)ItemOffsetArray[j] + offset);
                    LengthArray.Add((int)ItemLengthArray[j]);
                }
                ItemOffsetArray.Clear();
                ItemLengthArray.Clear();

                temp += "\n\n";
                offset = temp.Length;
                temp += ((PortConfig)m_etherNetClientConfigArray[i]).GetTextStringWithErrorInfo(ItemOffsetArray, ItemLengthArray).TrimEnd('\n');
                for (int j = 0; j < ItemOffsetArray.Count; j++)
                {
                    OffsetArray.Add((int)ItemOffsetArray[j] + offset);
                    LengthArray.Add((int)ItemLengthArray[j]);
                }
                ItemOffsetArray.Clear();
                ItemLengthArray.Clear();

                temp += "\n\n";

                offset = temp.Length;
                temp += ((PortCommands)m_etherNetClientCommandsArray[i]).GetTextStringWithErrorInfo(ItemOffsetArray, ItemLengthArray).TrimEnd('\n');
                for (int j = 0; j < ItemOffsetArray.Count; j++)
                {
                    OffsetArray.Add((int)ItemOffsetArray[j] + offset);
                    LengthArray.Add((int)ItemLengthArray[j]);
                }
                ItemOffsetArray.Clear();
                ItemLengthArray.Clear();

                temp += "\n\n";

                offset = temp.Length;
                temp += ((PortConfig)m_etherNetServerConfigArray[i]).GetTextStringWithErrorInfo(ItemOffsetArray, ItemLengthArray).TrimEnd('\n');
                for (int j = 0; j < ItemOffsetArray.Count; j++)
                {
                    OffsetArray.Add((int)ItemOffsetArray[j] + offset);
                    LengthArray.Add((int)ItemLengthArray[j]);
                }
                ItemOffsetArray.Clear();
                ItemLengthArray.Clear();

                temp += "\n\n";
            }
            temp += m_dataRemap.GetTextString().TrimEnd('\n');
            temp += "\n\n";
            return temp;

        }
        public string GetTextString()
        {
            string temp = "";
            temp += m_moduleConfig;

            for (int i = 0; i < PORT_CFG_NUM; i++)
            {
                temp += ((PortConfig)m_portConfigArray[i]).GetTextString().TrimEnd('\n');
                temp += "\n\n";
                temp += ((PortCommands)m_portCommandsArray[i]).GetTextString().TrimEnd('\n');
                temp += "\n\n";
            }
            for (int i = 0; i < PORT_PC_NUM; i++)
            {
                temp += ((PortConfig)m_pcPortConfigArray[i]).GetTextString().TrimEnd('\n');
                temp += "\n\n";
                temp += ((PortCommands)m_pcPortCommandsArray[i]).GetTextString().TrimEnd('\n');
                temp += "\n\n";
            }
            for (int i = 0; i < PORT_ETHERNET_NUM; i++)
            {
                temp += ((PortConfig)m_etherNetPortArray[i]).GetTextString().TrimEnd('\n');
                temp += "\n\n";
                temp += ((PortConfig)m_etherNetClientConfigArray[i]).GetTextString().TrimEnd('\n');
                temp += "\n\n";
                temp += ((PortCommands)m_etherNetClientCommandsArray[i]).GetTextString().TrimEnd('\n');
                temp += "\n\n";
                temp += ((PortConfig)m_etherNetServerConfigArray[i]).GetTextString().TrimEnd('\n');
                temp += "\n\n";
            }
            temp += m_dataRemap.GetTextString().TrimEnd('\n');
            temp += "\n\n";
            return temp;
        }
        
        public byte[] GetTextBytes()
        {
            return System.Text.Encoding.ASCII.GetBytes(GetTextString());
        }        
        
        public string FileName
        {
            get
            {
                return m_filename;
            }
        }
        public EditType Errors
        {
            get
            {
                return m_error;
            }
        }

        public void SaveFile( )
        {
            //FileStream fs;

            //using (fs = File.OpenWrite(m_filename))
            try
            {

                Int64 Ticks = DateTime.Now.Ticks; ;
                m_moduleConfig = MODULE_HASH.PadRight(32, ' ');
                m_moduleConfig += ": ";
                m_moduleConfig += (UInt32)(Ticks / 10000000);
                m_moduleConfig += "\n";

                // byte[] bytes = GetTextBytes();
                //fs.Write(bytes, 0, bytes.Length);
                File.WriteAllBytes(m_filename, GetTextBytes());
            }
            catch 
            {
                MessageBox.Show("Save Error!");
            }
            PortConfigCheckContent();
        }
        
        public void SaveFile(string filename)
        {
            m_filename = filename;
            SaveFile();
        }
    }
}
