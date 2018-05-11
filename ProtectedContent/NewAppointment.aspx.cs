using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

public partial class ProtectedContent_NewAppointment : System.Web.UI.Page
{
    AchcDatabaseEntities dbcontext = new AchcDatabaseEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dbcontext.HospitalTables.Load();
            var hosp = from h in dbcontext.HospitalTables.Local
                       select new { h.Name, h.HospitalId };
            var hospitalList = hosp.ToList();

            foreach (var x in hospitalList)
            {
                DropDownList2.Items.Add(new ListItem(x.Name, x.HospitalId.ToString()));
            }


            DropDownList3.Enabled = false;
            DropDownList4.Enabled = false;

        }
    }

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        TextBox1.Text =
            Calendar1.SelectedDate.ToShortDateString();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        dbcontext.PatientTables.Load();
        PatientTable user = (from x in dbcontext.PatientTables.Local
                          where x.UserName.Equals(HttpContext.Current.User.Identity.Name)
                          select x).First();
        AppointmentTable app = new AppointmentTable();
        app.PatientUserName = user.UserName;
        app.DoctorUserName = DropDownList4.SelectedValue.ToString();
        DateTime a = Convert.ToDateTime(TextBox1.Text + " " + DropDownList1.SelectedValue);
        app.DateAndTime = a;
        
        app.Description = TextBox2.Text;

        //System.Diagnostics.Debug.WriteLine(app.DateAndTime.ToString());
        dbcontext.AppointmentTables.Load();
        var y = from time in dbcontext.AppointmentTables.Local
                where time.DateAndTime.ToString().Equals(app.DateAndTime.ToString()) && time.DoctorUserName.Equals(app.DoctorUserName)
                select time;
        if (y.ToList().Count == 0)
        {
            dbcontext.AppointmentTables.Add(app);
            dbcontext.SaveChanges();
            Server.Transfer("AppointmentsPatient.aspx", true);
           
        }
        else
        {
            Server.Transfer("AppointmentsPatient.aspx", true);
        }

    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList3.Items.Clear();
        dbcontext.DepartmentTables.Load();
        var dept = from d in dbcontext.DepartmentTables.Local
                   where d.HospitalId.Equals(Convert.ToInt32(DropDownList2.SelectedValue))
                   select new { d.Name, d.DepartmentId };
        var deptList = dept.ToList();

        foreach (var x in deptList)
        {
            DropDownList3.Items.Add(new ListItem(x.Name, x.DepartmentId.ToString()));
        }

        DropDownList3.Enabled = true;
    }

    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList4.Items.Clear();
        dbcontext.DoctorTables.Load();
        var doc = from d in dbcontext.DoctorTables.Local
                   where d.DepartmentId.Equals(Convert.ToInt32(DropDownList3.SelectedValue))
                   select new { d.UserName };
        var docList = doc.ToList();

        foreach (var x in docList)
        {
            DropDownList4.Items.Add(x.UserName);
        }

        DropDownList4.Enabled = true;
    }
}