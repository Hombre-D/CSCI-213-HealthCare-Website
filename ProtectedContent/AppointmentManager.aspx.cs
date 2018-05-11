using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

public partial class ProtectedContent_AppointmentManager : System.Web.UI.Page
{
    AchcDatabaseEntities dbcontext = new AchcDatabaseEntities();
    UserTable user;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        
        dbcontext.UserTables.Load();

        UserTable user = (from x in dbcontext.UserTables.Local
                          where x.UserName.Equals(HttpContext.Current.User.Identity.Name)
                          select x).First();

        if (user.UserType.Equals("Doctor"))
            Server.Transfer("AppointmentsDoctor.aspx", true);
        else
            Server.Transfer("AppointmentsPatient.aspx", true);
    }
}