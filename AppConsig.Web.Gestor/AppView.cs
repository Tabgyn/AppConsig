using System;
using System.Globalization;
using System.Web.Mvc;
using AppConsig.Common.Security;
using AppConsig.Data;

namespace AppConsig.Web.Gestor
{
    public abstract class AppView : WebViewPage
    {
        public virtual new AppPrincipal User => base.User as AppPrincipal;

        public virtual string CurrentDate => DateTime.Now.ToString("dd/MM/yyyy");

        public virtual string CurrentDay
        {
            get
            {
                var culture = new CultureInfo("pt-BR");
                var dtfi = culture.DateTimeFormat;
                var weekendDay = dtfi.GetDayName(DateTime.Now.DayOfWeek);

                return weekendDay;
            }
        }

        public virtual string Avatar
        {
            get
            {
                var contexto = new AppContext();

                if (!User.Identity.IsAuthenticated) return Url.Content("~/Content/Images/no_image_available.png");
                var avatar = contexto.Users.Find(User.Id).Picture;

                return !string.IsNullOrEmpty(avatar)
                    ? $"data:image/png;base64,{avatar}"
                    : Url.Content("~/Content/Images/no_image_available.png");
            }
        }
    }

    public abstract class AppView<T> : WebViewPage<T>
    {
        public virtual new AppPrincipal User => base.User as AppPrincipal;

        public virtual string CurrentDate => DateTime.Now.ToString("dd/MM/yyyy");

        public virtual string CurrentDay
        {
            get
            {
                var culture = new CultureInfo("pt-BR");
                var dtfi = culture.DateTimeFormat;
                var weekendDay = dtfi.GetDayName(DateTime.Now.DayOfWeek);

                return weekendDay;
            }
        }

        public virtual string Avatar
        {
            get
            {
                var contexto = new AppContext();

                if (!User.Identity.IsAuthenticated) return Url.Content("~/Content/Images/no_image_available.png");
                var avatar = contexto.Users.Find(User.Id).Picture;

                return !string.IsNullOrEmpty(avatar)
                    ? $"data:image/png;base64,{avatar}"
                    : Url.Content("~/Content/Images/no_image_available.png");
            }
        }
    }
}