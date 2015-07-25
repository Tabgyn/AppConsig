using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Xml;

namespace AppConsig.Web.Base.DataGrid
{
    /// <summary>
    /// Build a table based on an enumerable list of model objects.
    /// </summary>
    /// <typeparam name="TModel">Type of model to render in the table.</typeparam>
    public class DataTableBuilder<TModel> : IDataTableBuilder<TModel> where TModel : class
    {
        private HtmlHelper HtmlHelper { get; set; }
        private IEnumerable<TModel> Data { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        internal DataTableBuilder(HtmlHelper helper)
        {
            HtmlHelper = helper;

            TableColumns = new List<IDataTableColumnInternal<TModel>>();
        }

        /// <summary>
        /// Set the enumerable list of model objects.
        /// </summary>
        /// <param name="dataSource">Enumerable list of model objects.</param>
        /// <returns>Reference to the DataTableBuilder object.</returns>
        public DataTableBuilder<TModel> DataSource(IEnumerable<TModel> dataSource)
        {
            Data = dataSource;
            return this;
        }

        /// <summary>
        /// List of table columns to be rendered in the table.
        /// </summary>
        internal IList<IDataTableColumnInternal<TModel>> TableColumns { get; set; }

        /// <summary>
        /// Add an lambda expression as a DataTableColumn.
        /// </summary>
        /// <typeparam name="TProperty">Model class property to be added as a column.</typeparam>
        /// <param name="expression">Lambda expression identifying a property to be rendered.</param>
        /// <returns>An instance of DataTableColumn.</returns>
        internal IDataTableColumn AddColumn<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            var column = new DataTableColumn<TModel, TProperty>(expression);
            TableColumns.Add(column);
            return column;
        }

        /// <summary>
        /// Create an instance of the ColumnBuilder to add columns to the table.
        /// </summary>
        /// <param name="columnBuilder">Delegate to create an instance of ColumnBuilder.</param>
        /// <returns>An instance of DataTableBuilder.</returns>
        public DataTableBuilder<TModel> Columns(Action<ColumnBuilder<TModel>> columnBuilder)
        {
            var builder = new ColumnBuilder<TModel>(this);
            columnBuilder(builder);
            return this;
        }

        /// <summary>
        /// Convert the DataTableBuilder to HTML.
        /// </summary>
        public MvcHtmlString ToHtml()
        {
            var html = new XmlDocument();
            var table = html.CreateElement("table");
            html.AppendChild(table);
            table.SetAttribute("border", "1px");
            table.SetAttribute("cellpadding", "5px");
            table.SetAttribute("cellspacing", "0px");
            var thead = html.CreateElement("thead");
            table.AppendChild(thead);
            var tr = html.CreateElement("tr");
            thead.AppendChild(tr);

            foreach (var tc in TableColumns)
            {
                var td = html.CreateElement("td");
                td.SetAttribute("style", "background-color:Black; color:White;font-weight:bold;");
                td.InnerText = tc.ColumnTitle;
                tr.AppendChild(td);
            }

            var tbody = html.CreateElement("tbody");
            table.AppendChild(tbody);

            //var row = 0;
            foreach (var model in Data)
            {
                tr = html.CreateElement("tr");
                tbody.AppendChild(tr);

                foreach (var tc in TableColumns)
                {
                    var td = html.CreateElement("td");
                    td.InnerText = tc.Evaluate(model);
                    tr.AppendChild(td);
                }
                //row++;
            }

            return new MvcHtmlString(html.OuterXml);
        }
    }
}