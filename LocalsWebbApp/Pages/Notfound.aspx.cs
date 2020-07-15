using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LocalsWebbApp.Pages
{
    public partial class Notfound : Page
    {
        public UsuarioDTO Usuario
        {
            get
            {
                return (UsuarioDTO)Session["Usuario"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Usuario == null)
                Response.Redirect("Login.aspx");
        }
    }
}