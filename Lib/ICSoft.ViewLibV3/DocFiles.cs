using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class DocFiles
    {
        public int DocFileId { get; set; }
        public string DocFileName { get; set; }
        public byte FileTypeId { get; set; }
        public string FilePath { get; set; }
        public int FileSize { get; set; }
        public byte LanguageId { get; set; }

        //-----------------------------------------------------------------
        public static List<DocFiles> Static_Init(SmartDataReader smartReader)
        {
            List<DocFiles> l_DocFiles = new List<DocFiles>();
            DocFiles m_DocFiles;
            while (smartReader.Read())
            {
                m_DocFiles = new DocFiles();
                m_DocFiles.DocFileId = smartReader.GetInt32("DocFileId");
                m_DocFiles.DocFileName = smartReader.GetString("DocFileName");
                m_DocFiles.FileTypeId = smartReader.GetByte("FileTypeId");
                m_DocFiles.FilePath = smartReader.GetString("FilePath");
                m_DocFiles.FileSize = smartReader.GetInt32("FileSize");
                m_DocFiles.LanguageId = smartReader.GetByte("LanguageId");

                l_DocFiles.Add(m_DocFiles);
            }
            return l_DocFiles;
        }
        //-----------------------------------------------------------------
        public static DocFiles Static_InitOne(SmartDataReader smartReader)
        {
            List<DocFiles> l_EffectStatus = Static_Init(smartReader);
            if (l_EffectStatus.Count > 0) return l_EffectStatus[0];
            return null;
        }
        //-----------------------------------------------------------------
        public static List<DocFiles> Static_GetByLanguageId(byte LanguageId, List<DocFiles> list)
        {
            List<DocFiles> RetVal = list.FindAll(i => i.LanguageId == LanguageId);
            return RetVal == null ? new List<DocFiles>() : RetVal;
        }
        //-----------------------------------------------------------
        public static List<DocFiles> GetList(int DocId, byte LanguageId=1)
        {
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            SqlConnection con = db.getConnection();

            List<DocFiles> RetVal = new List<DocFiles>();
            try
            {
                string sql = "SELECT DocFileId,DocFileName,FileTypeId,FilePath,FileSize,LanguageId FROM DocFiles WHERE (DocId=" + DocId.ToString() + ") AND (ReviewStatusId=2)";
                if (LanguageId > 0) sql = sql + " AND (LanguageId=" + LanguageId.ToString() + ")";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal = DocFiles.Static_Init(smartReader);

                reader.Close();
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return RetVal;
        }
    }
}
