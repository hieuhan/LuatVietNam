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
	public class MessageNotifyWorkItem : WorkItem
	{
        private SendMTGatewayForm m_Form;
        private MessageSends m_MessageSends;
        private static Random random = new Random();

        public MessageNotifyWorkItem(MessageSends myMessageSends, SendMTGatewayForm myForm)
        {
            m_Form = myForm;
            m_MessageSends = myMessageSends;
        }

        public override void Perform()
        {
            try
            {
                if (m_MessageSends == null)
                {
                    ProcessSingleThread();
                }
                else
                {
                    ProcessMultiThread();
                }
            }
            catch (Exception ex)
            {
                m_Form.WriteLog_Error(ex.ToString());
            }
        }

        private void ProcessMultiThread()
        {
            short SysMessageId = 0;
            try
            {
                if (m_MessageSends.SendMethodId == 1) //Email
                {
                    m_Form.WriteLog("Sending: " + m_MessageSends.SendFrom + " >> " + m_MessageSends.SendTo + " >> " + m_MessageSends.Title);
                    string result = MailUtils.sendMessage(m_MessageSends.SendFrom, m_MessageSends.SendTo, m_MessageSends.Title, m_MessageSends.MsgContent);
                    m_Form.WriteLog("Result: " + m_MessageSends.SendFrom + " >> " + m_MessageSends.SendTo + " >> " + m_MessageSends.Title + " >> " + result);
                    if (result == "success")
                    {
                        m_MessageSends.SendStatusId = 2;//Sent
                        m_MessageSends.UpdateSendStatusId(ref SysMessageId);
                    }
                    else
                    {
                        m_MessageSends.SendStatusId = 3;//Error
                        m_MessageSends.UpdateSendStatusId(ref SysMessageId);
                    }
                }
                else //SMS
                {
                    m_MessageSends.SendStatusId = 3;//Error
                    m_MessageSends.UpdateSendStatusId(ref SysMessageId);
                }

            }
            catch (Exception ex)
            {
                m_MessageSends.SendStatusId = 3;//Error
                m_MessageSends.UpdateSendStatusId(ref SysMessageId);
                m_Form.WriteLog_Error("Result: " + m_MessageSends.SendFrom + " >> " + m_MessageSends.SendTo + " >> " + m_MessageSends.Title + " >> " + ex.ToString());
            }
        }
        private void ProcessSingleThread()
        {
            m_Form.m_MessageNotifyRun = true;
            try
            {
                short SysMessageId = 0;
                MessageSends m_MessageSends = new MessageSends();
                List<MessageSends> l_MessageSends = m_MessageSends.MessageSends_GetListToGateway(0, 10);
                while (l_MessageSends.Count > 0)
                {
                    for (int i = 0; i < l_MessageSends.Count; i++)
                    {
                        try
                        {
                            if (l_MessageSends[i].SendMethodId == 1) //Email
                            {
                                m_Form.WriteLog("Sending: " + l_MessageSends[i].SendFrom + " >> " + l_MessageSends[i].SendTo + " >> " + l_MessageSends[i].Title);
                                string result = MailUtils.sendMessage(l_MessageSends[i].SendFrom, l_MessageSends[i].SendTo, l_MessageSends[i].Title, l_MessageSends[i].MsgContent);
                                m_Form.WriteLog("Result: " + l_MessageSends[i].SendFrom + " >> " + l_MessageSends[i].SendTo + " >> " + l_MessageSends[i].Title + " >> " + result);
                                if (result == "success")
                                {
                                    l_MessageSends[i].SendStatusId = 2;//Sent
                                    l_MessageSends[i].UpdateSendStatusId(ref SysMessageId);
                                }
                                else
                                {
                                    l_MessageSends[i].SendStatusId = 3;//Error
                                    l_MessageSends[i].UpdateSendStatusId(ref SysMessageId);
                                }
                            }
                            else //SMS
                            {
                                l_MessageSends[i].SendStatusId = 3;//Error
                                l_MessageSends[i].UpdateSendStatusId(ref SysMessageId);
                            }

                        }
                        catch (Exception ex)
                        {
                            l_MessageSends[i].SendStatusId = 3;//Error
                            l_MessageSends[i].UpdateSendStatusId(ref SysMessageId);
                            m_Form.WriteLog_Error("Result: " + l_MessageSends[i].SendFrom + " >> " + l_MessageSends[i].SendTo + " >> " + l_MessageSends[i].Title + " >> " + ex.ToString());
                        }
                        Thread.Sleep(100);
                    }
                    l_MessageSends = new List<MessageSends>();
                    //l_MessageSends = m_MessageSends.MessageSends_GetListToGateway(0, 100);
                }
            }
            catch (Exception ex)
            {
                m_Form.WriteLog_Error(ex.ToString());
            }
            m_Form.m_MessageNotifyRun = false;
        }
	}
}
