using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for WebConstans
/// </summary>
public class WebConstans
{
    public WebConstans()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static string CONNECTION_STRING_COPY = (ConfigurationManager.AppSettings["CONNECTION_STRING_COPY"] == null) ? "" : ConfigurationManager.AppSettings["CONNECTION_STRING_COPY"];
    public static short CATEGOY_COPY = (ConfigurationManager.AppSettings["CATEGOY_COPY"] == null) ? (short)0 : Int16.Parse(ConfigurationManager.AppSettings["CATEGOY_COPY"].ToString());
    public static short DATASOURCE_COPY = (ConfigurationManager.AppSettings["DATASOURCE_COPY"] == null) ? (short)0 : Int16.Parse(ConfigurationManager.AppSettings["DATASOURCE_COPY"].ToString());
    public static short SITE_COPY = (ConfigurationManager.AppSettings["SITE_COPY"] == null) ? (short)1 : Int16.Parse(ConfigurationManager.AppSettings["SITE_COPY"].ToString());

    public static string GCM_SERVER_API_KEY = (ConfigurationManager.AppSettings["GCM_SERVER_API_KEY"] == null) ? "AIzaSyBLGk82_xlkL_z4VT58OXaBDbRNhWAT22M" : ConfigurationManager.AppSettings["GCM_SERVER_API_KEY"];
    public static string PUBLIC_KEY = (ConfigurationManager.AppSettings["PUBLIC_KEY"] == null) ? "BD5efVCktZ9HydMeF9BSqDM-ugpxQGQ2p1CrfsnRWIQnZ7vd-bphtXjMgDfAO_N2WBzcHe53NR4cDmRAcAETa-8" : ConfigurationManager.AppSettings["PUBLIC_KEY"];
    public static string PRIVATE_KEY = (ConfigurationManager.AppSettings["PRIVATE_KEY"] == null) ? "MPAsJv1ryULsxR8D5nYkX07yZ1W1HmR_Q4_ohl3oTak" : ConfigurationManager.AppSettings["PRIVATE_KEY"];

    public static string INSERT_USERNAME = (ConfigurationManager.AppSettings["INSERT_USERNAME"] == null) ? "docbao" : ConfigurationManager.AppSettings["INSERT_USERNAME"];
    public static string INSERT_USERPASS = (ConfigurationManager.AppSettings["INSERT_USERPASS"] == null) ? "MPAsJv1ryULsxR8D5nYkX07yZ1W1HmR" : ConfigurationManager.AppSettings["INSERT_USERPASS"];
    public static int INSERT_USERID = (ConfigurationManager.AppSettings["INSERT_USERID"] == null) ? 121 : int.Parse(ConfigurationManager.AppSettings["INSERT_USERID"]);
    public static short INSERT_CATEGORYID = (ConfigurationManager.AppSettings["INSERT_CATEGORYID"] == null) ? (short)2388 : short.Parse(ConfigurationManager.AppSettings["INSERT_CATEGORYID"]);
    //DisplayTypeId
    public static short TCVN_DISPLAYTYPE_ID = (ConfigurationManager.AppSettings["TCVN_DISPLAYTYPE_ID"] == null) ? (short)52 : short.Parse( ConfigurationManager.AppSettings["TCVN_DISPLAYTYPE_ID"]);
    public static short TTHC_DISPLAYTYPE_ID = (ConfigurationManager.AppSettings["TTHC_DISPLAYTYPE_ID"] == null) ? (short)53 : short.Parse(ConfigurationManager.AppSettings["TTHC_DISPLAYTYPE_ID"]);
    public static short VBUBND_DISPLAYTYPE_ID = (ConfigurationManager.AppSettings["VBUBND_DISPLAYTYPE_ID"] == null) ? (short)5 : short.Parse(ConfigurationManager.AppSettings["VBUBND_DISPLAYTYPE_ID"]);
    public static short VBHN_DISPLAYTYPE_ID = (ConfigurationManager.AppSettings["VBHN_DISPLAYTYPE_ID"] == null) ? (short)54 : short.Parse(ConfigurationManager.AppSettings["VBHN_DISPLAYTYPE_ID"]);
    //DocGroupId
    public static byte VBPQ_DocGroupId = (ConfigurationManager.AppSettings["VBPQ_DocGroupId"] == null) ? (byte)1 : byte.Parse(ConfigurationManager.AppSettings["VBPQ_DocGroupId"]);
    public static byte VBUBND_DocGroupId = (ConfigurationManager.AppSettings["VBUBND_DocGroupId"] == null) ? (byte)2 : byte.Parse(ConfigurationManager.AppSettings["VBUBND_DocGroupId"]);
    public static byte TCVN_DocGroupId = (ConfigurationManager.AppSettings["TCVN_DocGroupId"] == null) ? (byte)3 : byte.Parse(ConfigurationManager.AppSettings["TCVN_DocGroupId"]);
    public static byte TTHC_DocGroupId = (ConfigurationManager.AppSettings["TTHC_DocGroupId"] == null) ? (byte)4 : byte.Parse(ConfigurationManager.AppSettings["TTHC_DocGroupId"]);
    public static byte VBHN_DocGroupId = (ConfigurationManager.AppSettings["VBHN_DocGroupId"] == null) ? (byte)5 : byte.Parse(ConfigurationManager.AppSettings["VBHN_DocGroupId"]);

    public static string MEDIA_VIEWPATH = (ConfigurationManager.AppSettings["MEDIA_VIEWPATH"] == null) ? "" : ConfigurationManager.AppSettings["MEDIA_VIEWPATH"];
    public static string MEDIA_DOWNLOADPATH = (ConfigurationManager.AppSettings["MEDIA_DOWNLOADPATH"] == null) ? "" : ConfigurationManager.AppSettings["MEDIA_DOWNLOADPATH"];
    //Domain Web
    public static string DOMAIN_WEB = (ConfigurationManager.AppSettings["DOMAIN_WEB"] == null) ? "http://luatvietnam.vn" : ConfigurationManager.AppSettings["DOMAIN_WEB"];

    public static string DATA_PATH = (ConfigurationManager.AppSettings["DATA_PATH"] == null) ? @"E:\WebHosting\Luatvietnam\vietlaw_data\LawData\" : ConfigurationManager.AppSettings["DATA_PATH"];

}