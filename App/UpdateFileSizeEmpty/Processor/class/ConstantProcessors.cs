using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web.Configuration;

namespace Processor
{
    public class ConstantProcessors        
    {

        public static string WatterMarkImage = (ConfigurationManager.AppSettings["WatterMarkImage"] == null) ? "" : ConfigurationManager.AppSettings["WatterMarkImage"];
        public static string WatterMarkText = (ConfigurationManager.AppSettings["WatterMarkText"] == null) ? "" : ConfigurationManager.AppSettings["WatterMarkText"];
        public static string DefaultOutputFolder = (ConfigurationManager.AppSettings["DefaultOutputFolder"] == null) ? "All" : ConfigurationManager.AppSettings["DefaultOutputFolder"];
        public static string ErrorOutputFolder = (ConfigurationManager.AppSettings["ErrorOutputFolder"] == null) ? @"E:\temp\PhapDien\Error\" : ConfigurationManager.AppSettings["ErrorOutputFolder"];
        public static string CompleteOutputFolder = (ConfigurationManager.AppSettings["CompleteOutputFolder"] == null) ? @"E:\temp\PhapDien\Complete\" : ConfigurationManager.AppSettings["CompleteOutputFolder"];

        public static int SleepTimeAfterCloseWordApp = (ConfigurationManager.AppSettings["SleepTimeAfterCloseWordApp"] == null) ? 1000 : int.Parse(ConfigurationManager.AppSettings["SleepTimeAfterCloseWordApp"]);
        public static int CrUserId = (ConfigurationManager.AppSettings["CrUserId"] == null) ? 1 : int.Parse(ConfigurationManager.AppSettings["CrUserId"]);
        public static short DataSourceId = (ConfigurationManager.AppSettings["DataSourceId"] == null) ? (short)7 : short.Parse(ConfigurationManager.AppSettings["DataSourceId"]);
        public static byte EffectStatusId = (ConfigurationManager.AppSettings["EffectStatusId"] == null) ? (byte)5 : byte.Parse(ConfigurationManager.AppSettings["EffectStatusId"]);
        public static byte ReviewStatusId = (ConfigurationManager.AppSettings["ReviewStatusId"] == null) ? (byte)5 : byte.Parse(ConfigurationManager.AppSettings["ReviewStatusId"]);
        public static byte UseStatusId = (ConfigurationManager.AppSettings["UseStatusId"] == null) ? (byte)1 : byte.Parse(ConfigurationManager.AppSettings["UseStatusId"]);
        public static byte Replicated = (ConfigurationManager.AppSettings["Replicated"] == null) ? (byte)0 : byte.Parse(ConfigurationManager.AppSettings["Replicated"]);
        public static byte IsUpdateDocItem = (ConfigurationManager.AppSettings["IsUpdateDocItem"] == null) ? (byte)1 : byte.Parse(ConfigurationManager.AppSettings["IsUpdateDocItem"]);
        public static byte IsInsertNote = (ConfigurationManager.AppSettings["IsInsertNote"] == null) ? (byte)1 : byte.Parse(ConfigurationManager.AppSettings["IsInsertNote"]);
        public static byte IsInsertFooter = (ConfigurationManager.AppSettings["IsInsertFooter"] == null) ? (byte)1 : byte.Parse(ConfigurationManager.AppSettings["IsInsertFooter"]);
        public static byte IsInsertAppendix = (ConfigurationManager.AppSettings["IsInsertAppendix"] == null) ? (byte)1 : byte.Parse(ConfigurationManager.AppSettings["IsInsertAppendix"]);
        public static byte IsResertOrderByLevel = (ConfigurationManager.AppSettings["IsResertOrderByLevel"] == null) ? (byte)1 : byte.Parse(ConfigurationManager.AppSettings["IsResertOrderByLevel"]);
        public static byte RelateTypeId = (ConfigurationManager.AppSettings["RelateTypeId"] == null) ? (byte)6 : byte.Parse(ConfigurationManager.AppSettings["RelateTypeId"]);

        public static string DocSearchLink = (ConfigurationManager.AppSettings["DocSearchLink"] == null) ? "" : ConfigurationManager.AppSettings["DocSearchLink"];
    }
}
