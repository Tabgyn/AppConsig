using System.Web.Mvc;
using AppConsig.Web.Base.DataGrid;

namespace AppConsig.Web.Base
{
    public static class AppExtensions
    {
        /// <summary>
        /// Return an instance of a DataTableBuilder.
        /// </summary>
        /// <typeparam name="TModel">Type of model to render in the table.</typeparam>
        /// <returns>Instance of a DataTableBuilder.</returns>
        public static IDataTableBuilder<TModel> DataTableFor<TModel>(this HtmlHelper helper) where TModel : class
        {
            return new DataTableBuilder<TModel>(helper);
        }
    }
}