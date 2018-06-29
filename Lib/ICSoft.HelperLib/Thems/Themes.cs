/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 * http://www.yetanotherforum.net/
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */

namespace ICSoft.HelperLib
{
    #region Using

    using System.IO;
    using System.Linq;
    using System.Web;
    using System;
    using ICSoft.CMSLib;
    using System.Configuration;
    using System.Net.Mail;
    using System.Text;

    #endregion

    /// <summary>
    /// The yaf theme.
    /// </summary>
    public class Themes 
    {
        #region Constants and Fields

        /// <summary>
        ///   The _theme file.
        /// </summary>
        private string _themeFile;

        /// <summary>
        /// The _theme resources.
        /// </summary>
        private ThemeResources _themeResources;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="Theme" /> class.
        /// </summary>
        public Themes()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Theme"/> class.
        /// </summary>
        /// <param name="newThemeFile">
        /// The new theme file. 
        /// </param>
        public Themes(string newThemeFile)
        {
           
            this.ThemeFile = newThemeFile;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets a value indicating whether LogMissingThemeItem.
        /// </summary>
        public bool LogMissingThemeItem { get; set; }

        /// <summary>
        ///   Gets ThemeDir.
        /// </summary>
        public string ThemeDir
        {
            get
            {
                this.LoadThemeFile();
                return string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, ICSoft.HelperLib.ConstantHelpers.LanguageDir);
            }
        }

        /// <summary>
        ///   Gets or sets the current Theme File
        /// </summary>
        public string ThemeFile
        {
            get
            {
                return this._themeFile;
            }

            set
            {
                if (this._themeFile == value)
                {
                    return;
                }

                if (!IsValidTheme(value))
                {
                    return;
                }

                this._themeFile = value;
                this._themeResources = null;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Basic testing of the theme's validity...
        /// </summary>
        /// <param name="themeFile">The theme file.</param>
        /// <returns>
        /// The is valid theme.
        /// </returns>
        public static bool IsValidTheme(string themeFile)
        {
           
            themeFile = themeFile.Trim().ToLower();

            return themeFile.Length != 0 && (themeFile.EndsWith(".xml") && (File.Exists(GetMappedThemeFile(themeFile)) || File.Exists(GetMappedThemeFileAdmin(themeFile))));
        }

        /// <summary>
        /// Gets full path to the given theme file.
        /// </summary>
        /// <param name="filename">
        /// Short name of theme file. 
        /// </param>
        /// <returns>
        /// The build theme path. 
        /// </returns>
        public string BuildThemePath(string filename)
        {
           
            return this.ThemeDir + filename;
        }

        
        #endregion

        #region Methods
        /// <summary>
        /// Get the Item.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="tag">The tag.</param>
        /// <returns>
        /// The item.
        /// </returns>
        public static string GetItem(string tag)
        {
            string page = "DEFAULT";
            string defaultValue = tag;
            string Language = LanguageHelpers.GetCurentName();
            return GetItem(page, tag, Language, defaultValue);
        }
        #region Methods
        /// <summary>
        /// Get the Item.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="tag">The tag.</param>
        /// <returns>
        /// The item.
        /// </returns>
        public static string GetItemByLanguage(string tag, string Language)
        {
            string page = "DEFAULT";
            string defaultValue = tag;
            
            return GetItem(page, tag, Language, defaultValue);
        }
        #endregion
        /// <summary>
        /// Get the Item.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// The item.
        /// </returns>
        public static string GetItem(string page, string tag)
        {
            string defaultValue = tag;
            string Language = LanguageHelpers.GetCurentName();            
            return GetItem(page, tag, Language, defaultValue);
        }

        /// <summary>
        /// Gets the Item string by Language.
        /// </summary>
        /// <param name="page">Page Tag.</param>
        /// <param name="tag">Tag name.</param>
        /// <param name="Language">Language keyword that is xml resource file name</param>        /// 
        /// <returns>
        /// The string in language
        /// </returns>
        /// 
        public static string GetItem(string page, string tag, string Language, string defaultValue)
        {

            string item = string.Empty;
            Themes m_Theme = new Themes();
            
            m_Theme.ThemeFile = Language.ToLower() + ".xml";
            m_Theme.LoadThemeFile();

            if (m_Theme._themeResources != null)
            {

                var selectedPage = m_Theme._themeResources.page.FirstOrDefault(x => x.name.ToUpper() == page.ToUpper());

                if (selectedPage != null)
                {
                    var selectedItem =
                        selectedPage.Resource.FirstOrDefault(
                            x =>
                            x.tag.ToUpper() == tag.ToUpper());

                    if (selectedItem == null)
                    {


                        return defaultValue;
                    }

                    return string.IsNullOrEmpty(selectedItem.Value) == false
                               ? selectedItem.Value.Replace(
                                   "~",CmsConstants.ROOT_PATH).Replace("[br]", "<br />")
                               : defaultValue;
                }

                return defaultValue;
            }

            return item.Replace("[br]", "<br />");
        }
        /// <summary>
        /// Gets the Item string by Language fro admin page.
        /// </summary>
        /// <param name="page">Page Tag.</param>
        /// <param name="tag">Tag name.</param>
        /// <param name="Language">Language keyword that is xml resource file name</param>        /// 
        /// <returns>
        /// The string in language
        /// </returns>
        /// 
        public static string GetItemAdmin(string tag)
        {
            string page = "DEFAULT";
            string Language = LanguageHelpers.GetCurentName();
            string defaultValue = tag;

            return GetItemAdmin(page, tag, Language, defaultValue);
        }
        /// <summary>
        /// Gets the Item string by Language fro admin page.
        /// </summary>
        /// <param name="page">Page Tag.</param>
        /// <param name="tag">Tag name.</param>
        /// <param name="Language">Language keyword that is xml resource file name</param>        /// 
        /// <returns>
        /// The string in language
        /// </returns>
        /// 
        public static string GetItemAdmin(string page, string tag)
        {
            string Language = LanguageHelpers.GetCurentName();
            string defaultValue = tag;
            
            return GetItemAdmin(page, tag, Language, defaultValue );
        }
        /// <summary>
        /// Gets the Item string by Language fro admin page.
        /// </summary>
        /// <param name="page">Page Tag.</param>
        /// <param name="tag">Tag name.</param>
        /// <param name="Language">Language keyword that is xml resource file name</param>        /// 
        /// <returns>
        /// The string in language
        /// </returns>
        /// 
        public static string GetItemAdmin(string page, string tag, string Language, string defaultValue = "")
        {

            string item = string.Empty;
            Themes m_Theme = new Themes();

            m_Theme.ThemeFile = Language + ".xml";
            m_Theme.LoadThemeFileAdmin();

            if (m_Theme._themeResources != null)
            {

                var selectedPage = m_Theme._themeResources.page.FirstOrDefault(x => x.name.ToUpper() == page.ToUpper());

                if (selectedPage != null)
                {
                    var selectedItem =
                        selectedPage.Resource.FirstOrDefault(
                            x =>
                            x.tag.ToUpper() == tag.ToUpper());

                    if (selectedItem == null)
                    {


                        return defaultValue;
                    }

                    return string.IsNullOrEmpty(selectedItem.Value) == false
                               ? selectedItem.Value.Replace(
                                   "~", CmsConstants.ROOT_PATH).Replace("[br]", "<br />")
                               : defaultValue;
                }

                return defaultValue;
            }

            return item.Replace("[br]", "<br />");
        }
        /// <summary>
        /// Gets the mapped theme file.
        /// </summary>
        /// <param name="themeFile">The theme file.</param>
        /// <returns>
        /// The get mapped theme file.
        /// </returns>
        /// 
        private static string GetMappedThemeFile(string themeFile)
        {
            
            return string.Format("{0}{1}{2}", AppDomain.CurrentDomain.BaseDirectory, ConstantHelpers.LanguageDir, themeFile.Trim()); 
        }
        /// <summary>
        /// Gets the mapped theme file.
        /// </summary>
        /// <param name="themeFile">The theme file.</param>
        /// <returns>
        /// The get mapped theme file.
        /// </returns>
        /// 
        private static string GetMappedThemeFileAdmin(string themeFile)
        {
            
            return string.Format("{0}{1}{2}{3}", AppDomain.CurrentDomain.BaseDirectory, CmsConstants.PRJ_ROOT.Substring(1), ConstantHelpers.LanguageDir, themeFile.Trim());
        }
        /// <summary>
        /// Loads the theme file.
        /// </summary>
        private void LoadThemeFile()
        {
            if (this.ThemeFile != null && this._themeResources == null)
            {
                this._themeResources =
                    new LoadSerializedXmlFile<ThemeResources>().FromFile(
                        GetMappedThemeFile(this.ThemeFile),
                        string.Format("THEMEFILE{0}",this.ThemeFile));
            }
        }
        /// <summary>
        /// Loads the theme file.
        /// </summary>
        private void LoadThemeFileAdmin()
        {
            if (this.ThemeFile != null && this._themeResources == null)
            {
                this._themeResources =
                    new LoadSerializedXmlFile<ThemeResources>().FromFile(
                        GetMappedThemeFileAdmin(this.ThemeFile),
                        string.Format("THEMEFILEADMIN{0}", this.ThemeFile));
            }
        }
        //----------------------------------------------------------------------------------------------
        public static string readFile(string FileName)
        {
            try
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "languages\\" + FileName))
                {
                    string text = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "languages\\" + FileName, Encoding.UTF8);
                    return text;
                }
                else
                {
                    sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + FileName);
                    return "";
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
                return "";

            }
        }
        //----------------------------------------------------------------------------------------------
        public static bool sendMessage2(string mailTo, string subject, string bodyMail)
        {
            string mail_host = ConfigurationManager.AppSettings["MAIL_SERVER_HOST"];
            string mail_user = ConfigurationManager.AppSettings["MAIL_USER"];
            string mail_pass = ConfigurationManager.AppSettings["MAIL_PASS"];
            string mail_domain = ConfigurationManager.AppSettings["MAIL_DOMAIN"];
            string mail_urlbase = ConfigurationManager.AppSettings["MAIL_URLBASE"];
            int mail_port = int.Parse(ConfigurationManager.AppSettings["MAIL_PORT"]);
            SmtpClient mail = new SmtpClient(mail_host);

            MailAddress from = new MailAddress(mail_user);

            MailAddress to = new MailAddress(mailTo);

            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(mail_user, mailTo);
            message.BodyEncoding = UTF8Encoding.UTF8;
            message.IsBodyHtml = true;

            message.Body = bodyMail;

            message.Subject = subject;
            mail.EnableSsl = true;
            mail.UseDefaultCredentials = false;
            mail.Credentials = new System.Net.NetworkCredential(mail_user, mail_pass);
            mail.Host = mail_host;
            mail.Port = mail_port;
            mail.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                mail.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
                return false;
            }
        }
        #endregion
    }
}