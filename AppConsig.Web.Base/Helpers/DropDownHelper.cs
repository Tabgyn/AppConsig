using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace AppConsig.Web.Base.Helpers
{
    public static class DropDownHelper
    {
        public static MvcHtmlString EnumDropDownListFor<TModel, TProperty, TEnum>(
                   this HtmlHelper<TModel> htmlHelper,
                   Expression<Func<TModel, TProperty>> expression,
                   TEnum selectedValue)
        {
            IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum))
                                        .Cast<TEnum>();
            IEnumerable<SelectListItem> items = from value in values
                                                select new SelectListItem
                                                {
                                                    Text = value.ToString(),
                                                    Value = value.ToString(),
                                                    Selected = (value.Equals(selectedValue))
                                                };

            return htmlHelper.DropDownListFor(expression, items, "Escolha uma opção...",
                new { @class = "form-control w-2" });
        }
    }
}