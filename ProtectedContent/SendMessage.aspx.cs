using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

public partial class ProtectedContent_SendMessage : System.Web.UI.Page
{
    AchcDatabaseEntities dbcontext = new AchcDatabaseEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dbcontext.UserTables.Load();
            UserTable user = (from x in dbcontext.UserTables.Local
                              where x.UserName.Equals(HttpContext.Current.User.Identity.Name)
                              select x).First();

            if (user.UserType.Equals("Patient"))
            {
                dbcontext.PatientTables.Load();
                PatientTable patient = (from x in dbcontext.PatientTables.Local
                                        where x.UserName.Equals(HttpContext.Current.User.Identity.Name)
                                        select x).First();
                dbcontext.DoctorTables.Load();
                DoctorTable doctor = (from x in dbcontext.DoctorTables.Local
                                      where x.DoctorId == patient.DoctorId
                                      select x).First();
                DropDownList1.Items.Add(doctor.UserName);
            } else
            {
                dbcontext.DoctorTables.Load();
                DoctorTable doctor = (from x in dbcontext.DoctorTables.Local
                                      where x.UserName.Equals(HttpContext.Current.User.Identity.Name)
                                      select x).First();
                foreach (PatientTable patient in doctor.PatientTables)
                {
                    DropDownList1.Items.Add(patient.UserName);
                }
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        MessageTable msg = new MessageTable();
        msg.MessageSubject = TextBox2.Text;
        msg.MessageBody = TextBox1.Text;
        msg.Date = DateTime.Now;
        msg.UserNameTo = DropDownList1.SelectedItem.Text;
        msg.UserNameFrom = HttpContext.Current.User.Identity.Name;

        dbcontext.MessageTables.Add(msg);
        dbcontext.SaveChanges();

        TextBox1.Text = "";
        TextBox2.Text = "";
    }
}