#region Copyright (c) 2004 Richard Schneider (Black Hen Limited) 
/*
   Copyright (c) 2004 Richard Schneider (Black Hen Limited) 
   All rights are reserved.

   Permission to use, copy, modify, and distribute this software 
   for any purpose and without any fee is hereby granted, 
   provided this notice is included in its entirety in the 
   documentation and in the source files.
  
   This software and any related documentation is provided "as is" 
   without any warranty of any kind, either express or implied, 
   including, without limitation, the implied warranties of 
   merchantibility or fitness for a particular purpose. The entire 
   risk arising out of use or performance of the software remains 
   with you. 
   
   In no event shall Richard Schneider, Black Hen Limited, or their agents 
   be liable for any cost, loss, or damage incurred due to the 
   use, malfunction, or misuse of the software or the inaccuracy 
   of the documentation associated with the software. 
*/
#endregion

using BlackHen.Threading;
using System;
using System.Configuration;
using System.Threading;
using System.Reflection;
using ICSoft.LawDocsLib;
using System.Collections.Generic;

namespace SendMTGateway
{
	/// <summary>
	/// Summary description for SampleWorkItem.
	/// </summary>
	public class NewsletterWorkItem : WorkItem
	{
        private SendMTGatewayForm m_Form;
        private static Random random = new Random();

        public NewsletterWorkItem(SendMTGatewayForm myForm)
        {
            m_Form = myForm;
        }

        public override void Perform()
        {
            m_Form.m_NewsletterRun = true;
            try
            {
                short SysMessageId = 0;
                int m_NewsletterId = 0;
                Newsletters m_Newsletters = new Newsletters();
                NewsletterSends m_NewsletterSends = new NewsletterSends();
                List<NewsletterSends> l_NewsletterSends = m_NewsletterSends.GetListToGateway(100);
                while (l_NewsletterSends.Count > 0)
                {
                    for (int i = 0; i < l_NewsletterSends.Count; i++)
                    {
                        try
                        {
                            if (m_NewsletterId != l_NewsletterSends[i].NewsletterId)
                            {
                                m_Newsletters.NewsletterId = l_NewsletterSends[i].NewsletterId;
                                m_Newsletters = m_Newsletters.Get();
                                m_NewsletterId = l_NewsletterSends[i].NewsletterId;
                            }

                            string result = MailUtils.sendMessage(m_Newsletters.SendFrom, l_NewsletterSends[i].Email, m_Newsletters.Title, m_Newsletters.MsgContent);
                            m_Form.WriteLog("Send mail: " + m_Newsletters.SendFrom + " >> " + l_NewsletterSends[i].Email + " >> " + m_Newsletters.Title + " >> " + result);

                            l_NewsletterSends[i].SendStatusId = 2;//Sent
                            l_NewsletterSends[i].UpdateSendStatusId(1, 1, ref SysMessageId);
                            Thread.Sleep(200);
                        }
                        catch (Exception ex)
                        {
                            l_NewsletterSends[i].SendStatusId = 3;//Error
                            l_NewsletterSends[i].UpdateSendStatusId(1, 1, ref SysMessageId);
                            m_Form.WriteLog_Error(ex.ToString());
                        }
                    }
                    l_NewsletterSends = m_NewsletterSends.GetListToGateway(100);
                }
            }
            catch (Exception ex)
            {
                m_Form.WriteLog_Error(ex.ToString());
            }
            m_Form.m_NewsletterRun = false;
        }
	}
}
