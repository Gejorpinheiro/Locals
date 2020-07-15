using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LocalsWebbApp.Pages
{
    public partial class Root : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblTitle.Text = Page.Title;
        }
    }
}