using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace AppConsig.Web.Base.Helpers
{
    public static class CaptchaHelper
    {
        /// <summary>
        /// Create an HTML captcha
        /// </summary>
        public static Captcha Captcha(this HtmlHelper html)
        {
            return new Captcha(html);
        }
    }

    /// <summary>
    /// Create an HTML captcha
    /// </summary>
    public class Captcha : IHtmlString
    {
        private readonly HtmlHelper _html;
        private string _captchaPublicKey = "";
        private CaptchaTheme _theme;
        private CaptchaType _type;
        private string _language;
        private const string UrlApi = "https://www.google.com/recaptcha/api.js";

        public Captcha(HtmlHelper html)
        {
            if (html == null) throw new ArgumentNullException(nameof(html));
            _html = html;
        }

        /// <summary>
        /// 
        /// </summary>
        public Captcha PublicKey(string key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            _captchaPublicKey = key;
            return this;
        }

        /// <summary>
        /// Define a theme for captcha.
        /// </summary>
        public Captcha Theme(CaptchaTheme theme)
        {
            _theme = theme;

            return this;
        }

        /// <summary>
        /// Define a type for captcha.
        /// </summary>
        public Captcha Type(CaptchaType type)
        {
            _type = type;

            return this;
        }

        /// <summary>
        /// Define a language for captcha.
        /// See https://developers.google.com/recaptcha/docs/language for codes
        /// </summary>
        public Captcha Language(string lang)
        {
            _language = string.IsNullOrEmpty(lang) ? "en" : lang;

            return this;
        }

        public string ToHtmlString()
        {
            return ToString();
        }

        public void Render()
        {
            var writer = _html.ViewContext.Writer;
            using (var textWriter = new HtmlTextWriter(writer))
            {
                textWriter.Write(ToString());
            }
        }

        private void ValidateSettings()
        {
            if (string.IsNullOrEmpty(_captchaPublicKey))
            {
                throw new InvalidOperationException("You need set the public key for captcha");
            }
        }

        public override string ToString()
        {
            ValidateSettings();

            var sb = new StringBuilder();

            sb.AppendLine("<script type='text/javascript' src = '" + UrlApi + "?hl=" + _language +
                          "' async defer></ script > ");
            sb.AppendLine("<div class='g-recaptcha' data-sitekey ='" + _captchaPublicKey +
                          "' data-theme='" + _theme.ToString().ToLower() +
                          "' data-type='" + _type.ToString().ToLower() + "'></div>");

            return sb.ToString();
        }

        public enum CaptchaTheme
        {
            Light,//default
            Dark
        }

        public enum CaptchaType
        {
            Image,//default
            Audio
        }
    }
}