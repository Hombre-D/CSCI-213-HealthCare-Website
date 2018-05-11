using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

public partial class ProtectedContent_AppointmentsDoctor : System.Web.UI.Page
{
    AchcDatabaseEntities dbcon = new AchcDatabaseEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadReqAppt();
            LoadConfirmedAppt();
        }
    }

    private void LoadReqAppt()
    {

        dbcon.AppointmentTables.Load();
        var reqAppt = from appointment in dbcon.AppointmentTables.Local
                      where appointment.DoctorUserName.Equals(HttpContext.Current.User.Identity.Name) &&
                      appointment.Confirmed.Equals(false)
                      orderby appointment.DateAndTime
                      select new { appointment.DateAndTime, Patient = appointment.PatientUserName,
                          Description = appointment.Description, ID = appointment.AppointmentId, };
        GridView1.AutoGenerateColumns = true;
        GridView1.AutoGenerateSelectButton = true;
        GridView1.DataKeyNames = new string[] { "ID" };
        GridView1.DataSource = reqAppt.ToList();
        GridView1.DataBind();

    }

    private void LoadConfirmedAppt()
    {

        dbcon.AppointmentTables.Load();
        var confAppt = from appointment in dbcon.AppointmentTables.Local
                       where appointment.DoctorUserName.Equals(HttpContext.Current.User.Identity.Name) &&
                       appointment.Confirmed.Equals(true)
                      orderby appointment.DateAndTime
                      select new { appointment.DateAndTime, Patient = appointment.PatientUserName,
                          Description = appointment.Description, ID = appointment.AppointmentId };
        GridView2.AutoGenerateColumns = true;
        GridView2.AutoGenerateSelectButton = true;
        GridView2.DataKeyNames = new string[] { "ID" };
        GridView2.DataSource = confAppt.ToList();
        GridView2.DataBind();

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {

            dbcon.AppointmentTables.Load();
            dbcon.MessageTables.Load();
            int id = Convert.ToInt32(GridView1.SelectedDataKey.Value);
            var appt = dbcon.AppointmentTables.Single(a => a.AppointmentId == id);
            var x = from y in dbcon.AppointmentTables.Local
                    where y.DoctorUserName.Equals(appt.DoctorUserName) &&
                    y.DateAndTime.ToString().Equals(appt.DateAndTime.ToString())
                    && y.Confirmed == true
                    select y;
            if(x.ToList().Count == 0)
            {
                appt.Confirmed = true;

                //Send a message to patient
                MessageTable msg = new MessageTable();
                msg.MessageSubject = "Appointment Confirmed";
                msg.MessageBody = "Your appointment with " + appt.DoctorUserName + " at " + appt.DateAndTime
                    + "has been confirmed.";
                msg.Date = DateTime.Now;
                msg.UserNameTo = appt.PatientUserName;
                msg.UserNameFrom = appt.DoctorUserName;
                dbcon.MessageTables.Add(msg);

                dbcon.SaveChanges();
                LoadReqAppt();
                LoadConfirmedAppt();
                Response.Write("<script type=\"text/javascript\">alert" +
                    "('Appointment Confirmed!');</script>");

            }
            else
            {
                Response.Write("<script type=\"text/javascript\">alert" +
                    "('You have a previously scheduled appointment at this time');</script>");
            }                
            
        }

        catch(NullReferenceException)
        {

        }


    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            int id = Convert.ToInt32(GridView1.SelectedDataKey.Value);

            var appt = dbcon.AppointmentTables.Single(a => a.AppointmentId == id);
            dbcon.AppointmentTables.Remove(appt);

            //Message the patient
            MessageTable msg = new MessageTable();
            msg.MessageSubject = "Appointment Cancelled";
            msg.MessageBody = "Your appointment with " + appt.DoctorUserName + " at " + appt.DateAndTime
                + "has been cancelled.";
            msg.Date = DateTime.Now;
            msg.UserNameTo = appt.PatientUserName;
            msg.UserNameFrom = appt.DoctorUserName;
            dbcon.MessageTables.Add(msg);

            dbcon.SaveChanges();
            LoadReqAppt();
            LoadConfirmedAppt();   
        }

        catch(NullReferenceException)
        {

        }
        
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            
            int id = Convert.ToInt32(GridView2.SelectedDataKey.Value);

            var appt = dbcon.AppointmentTables.Single(a => a.AppointmentId == id);
            dbcon.AppointmentTables.Remove(appt);

            //Message the patient
            MessageTable msg = new MessageTable();
            msg.MessageSubject = "Appointment Cancelled";
            msg.MessageBody = "Your appointment with " + appt.DoctorUserName + " at " + appt.DateAndTime
                + "has been cancelled.";
            msg.Date = DateTime.Now;
            msg.UserNameTo = appt.PatientUserName;
            msg.UserNameFrom = appt.DoctorUserName;
            dbcon.MessageTables.Add(msg);

            dbcon.SaveChanges();
            LoadReqAppt();
            LoadConfirmedAppt();
            
        }

        catch (NullReferenceException)
        {

        }

    }
}