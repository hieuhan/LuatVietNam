using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text.RegularExpressions;
public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString FilterDocContent(SqlString DocContent)
    {
        try
        {
            string RetVal = DocContent.ToString();

            //DocLink
            string RegexLinkRule = "<a([^>]+)href=([^>]+)/([^>]+)-([0-9]+)-(d[1-8]).html([^>]+)>";
            RegexOptions m_RegexOptions = RegexOptions.None;// RegexOptions.Multiline | RegexOptions.IgnoreCase;
            Match m_Match = Regex.Match(RetVal, RegexLinkRule, m_RegexOptions);
            while (m_Match.Success)
            {
                int DocId = int.Parse(m_Match.Groups[4].Value);
                string DocHexId = DocId.ToString("x");
                string NewUrl = DocHexId + ".html";
                RetVal = RetVal.Replace(m_Match.Groups[4].Value + "-" + m_Match.Groups[5].Value + ".html", NewUrl);
                m_Match = m_Match.NextMatch();
            }
          
            //End DocLink
            //Old Doc Link
            RegexLinkRule = "<a([^>]+)href=([^>]+)/VL/([^>]+)>";
            m_Match = Regex.Match(RetVal, RegexLinkRule, m_RegexOptions);
            while (m_Match.Success)
            {

                string NewUrl = "<a href=\"#\">";
                RetVal = RetVal.Replace(m_Match.Groups[0].Value, NewUrl);
                m_Match = m_Match.NextMatch();
            }
            //end Old Doc Link
            //finanly replace all
            RetVal = RetVal.Replace("luatvietnam.vn", "vanbanluat.vn");
            // Put your code here
            return new SqlString(RetVal);
        }
        catch
        {
            throw;
        }
        
    }
}
