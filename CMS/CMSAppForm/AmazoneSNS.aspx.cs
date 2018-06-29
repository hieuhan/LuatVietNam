using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AmazoneSNS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string jsonBody;
        using (StreamReader reader = new StreamReader(Request.InputStream))
        {
            jsonBody = reader.ReadToEnd();
        }
        try
        {
            AmazoneNotify m_Notify = new AmazoneNotify();
            JavaScriptSerializer js = new JavaScriptSerializer();
            m_Notify = js.Deserialize<AmazoneNotify>(jsonBody);
            

        }
        catch(Exception ex)
        {
            sms.utils.LogFiles.WriteLog(Environment.NewLine + "jsonBody:" + jsonBody + Environment.NewLine + ex.ToString());
        }
        sms.utils.LogFiles.WriteLog(Environment.NewLine + "jsonBody:" + jsonBody);

    }
    public class AmazoneNotify
    {
        public string Type { get; set; }
        public string MessageId { get; set; }
        public string TopicArn { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public string SignatureVersion { get; set; }
        public string Signature { get; set; }
        public string SigningCertURL { get; set; }
        public string UnsubscribeURL { get; set; }
    }
    public class Mail
    {
        public DateTime timestamp { get; set; }
        public string source { get; set; }
        public string sourceArn { get; set; }
        public string sourceIp { get; set; }
        public string sendingAccountId { get; set; }
        public string messageId { get; set; }
        public List<string> destination { get; set; }
    }

    public class Delivery
    {
        public DateTime timestamp { get; set; }
        public int processingTimeMillis { get; set; }
        public List<string> recipients { get; set; }
        public string smtpResponse { get; set; }
        public string remoteMtaIp { get; set; }
        public string reportingMTA { get; set; }
    }

    public class AmazoneMessage
    {
        public string notificationType { get; set; }
        public Mail mail { get; set; }
        public Delivery delivery { get; set; }
    }
    public class BouncedRecipient
    {
        public string emailAddress { get; set; }
        public string action { get; set; }
        public string status { get; set; }
        public string diagnosticCode { get; set; }
    }

    public class Bounce
    {
        public string bounceType { get; set; }
        public string bounceSubType { get; set; }
        public List<BouncedRecipient> bouncedRecipients { get; set; }
        public DateTime timestamp { get; set; }
        public string feedbackId { get; set; }
        public string remoteMtaIp { get; set; }
        public string reportingMTA { get; set; }
    }
    
    public class AmazoneBounce
    {
        public string notificationType { get; set; }
        public Bounce bounce { get; set; }
        public Mail mail { get; set; }
    }
}