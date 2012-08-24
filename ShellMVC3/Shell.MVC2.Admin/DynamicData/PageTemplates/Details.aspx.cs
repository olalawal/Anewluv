using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.DynamicData;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;
using Microsoft.Web.DynamicData;

namespace Shell.MVC2.Admin
{
    public partial class Details : System.Web.UI.Page
    {
        protected MetaTable table;

        protected void Page_Init(object sender, EventArgs e)
        {
            table = DynamicDataRouteHandler.GetRequestMetaTable(Context);
            FormView1.SetMetaTable(table);

            DetailsDataSource.DomainServiceTypeName = table.DataContextType.AssemblyQualifiedName;
            DetailsDataSource.QueryName = table.GetSelectMethod();
            FormView1.DataKeyNames = table.PrimaryKeyColumns.Select(c => c.Name).ToArray();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = table.DisplayName;

        }

        protected void FormView1_ItemDeleted(object sender, FormViewDeletedEventArgs e)
        {
            if (e.Exception == null || e.ExceptionHandled)
            {
                Response.Redirect(table.ListActionPath);
            }
        }

    }
}
