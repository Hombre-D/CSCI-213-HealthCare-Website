using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

public partial class ProtectedContent_AppointmentsPatient : System.Web.UI.Page
{
    AchcDatabaseEntities dbcontext = new AchcDatabaseEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadAppointments();
        }
    }
    private void LoadAppointments()
    {
        dbcontext.AppointmentTables.Load();
        var app = from appointment in dbcontext.AppointmentTables.Local
                      where appointment.PatientUserName.Equals(HttpContext.Current.User.Identity.Name)
                      orderby appointment.DateAndTime ascending
                      select new { appointment.DateAndTime, Doctor = appointment.DoctorUserName, Description = appointment.Description, appointment.AppointmentId, Confirmed = appointment.Confirmed ? "Confirmed" : "Pending"};
        GridView1.DataSource = app.ToList();
        GridView1.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Server.Transfer("NewAppointment.aspx", true);
    }
}