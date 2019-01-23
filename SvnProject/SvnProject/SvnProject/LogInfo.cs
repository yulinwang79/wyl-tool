using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SvnProject
{
    class LogInfo
    {
        int m_r;
        int m_publisher_id;
        string m_released_dt;
        int m_project_id;
        int m_customer_id;
        int m_depository_id;
        string m_logInfo_text;
        bool m_isValid;
        public LogInfo(string text,int depository_id)
        {
            m_logInfo_text = text;
        }
    }
}
