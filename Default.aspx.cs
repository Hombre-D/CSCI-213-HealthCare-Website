using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

public partial class _Default : Page
{
    AchcDatabaseEntities dbcontext = new AchcDatabaseEntities();
    UserTable user;
    protected void Page_Load(object sender, EventArgs e)
    {
        LoginView2.Visible = false;

        if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
        {
            dbcontext.UserTables.Load();
            UserTable user = (from x in dbcontext.UserTables.Local
                              where x.UserName.Equals(HttpContext.Current.User.Identity.Name)
                              select x).First();
            if (user.UserType.Equals("Doctor"))
            {
                LoginView2.Visible = true;
            }
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/ProtectedContent/PatientSearch.aspx", true);
    }
}