using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

public partial class ProtectedContent_PatientSearch : System.Web.UI.Page
{
    AchcDatabaseEntities dbcontext = new AchcDatabaseEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dbcontext.PatientTables.Load();
            var patients = from patient in dbcontext.PatientTables.Local
                           select new { patient.Name, patient.Email, patient.Phone, patient.PatientId };
            var patientList = patients.ToList();
            foreach (var p in patientList)
            {
                DropDownList1.Items.Add(p.Name);
            }
        }
        

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        ListBox1.Items.Clear();
        ListBox2.Items.Clear();
        dbcontext.PatientTables.Load();
        var patients = from patient1 in dbcontext.PatientTables.Local
                       select new { patient1.Name, patient1.Email, patient1.Phone, patient1.PatientId };
        var patientList = patients.ToList();
        var patient = patientList[DropDownList1.SelectedIndex];
        ListBox1.Items.Add(patient.Name);
        ListBox1.Items.Add(patient.Email);
        ListBox1.Items.Add(patient.Phone);

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ListBox1.Items.Clear();
        ListBox2.Items.Clear();
        dbcontext.PatientTables.Load();

        if (TextBox1.Text == string.Empty && TextBox2.Text == string.Empty)
        {
            ListBox2.Items.Add("Enter a first or last name");
        }
        else if (TextBox1.Text == string.Empty && TextBox2.Text != string.Empty)
        {
            string c = TextBox2.Text.Trim();
            var patients2 = from patient2 in dbcontext.PatientTables.Local
                            where patient2.Name.Split(' ')[1].ToLower().StartsWith(c.ToLower())
                            select new { patient2.Name, patient2.Email, patient2.Phone };
            var patientsList2 = patients2.ToList();

            if (patientsList2.Count > 0)
            {
                foreach (var p2 in patientsList2)
                {
                    ListBox2.Items.Add(p2.Name);
                    ListBox2.Items.Add(p2.Email);
                    ListBox2.Items.Add(p2.Phone);
                    ListBox2.Items.Add("");
                }
            }
            else
            {
                ListBox2.Items.Add("No items found.");
            }
                
            
            
        }
        else if (TextBox1.Text != string.Empty && TextBox2.Text == string.Empty)
        {
            string c = TextBox1.Text.Trim();
            var patients2 = from patient2 in dbcontext.PatientTables.Local
                            where patient2.Name.Split(' ')[0].ToLower().StartsWith(c.ToLower())
                            select new { patient2.Name, patient2.Email, patient2.Phone };
            var patientsList2 = patients2.ToList();
            if (patientsList2.Count > 0)
            {
                foreach (var p2 in patientsList2)
                {
                    ListBox2.Items.Add(p2.Name);
                    ListBox2.Items.Add(p2.Email);
                    ListBox2.Items.Add(p2.Phone);
                    ListBox2.Items.Add("");
                }
            }
            else
            {
                ListBox2.Items.Add("No items found.");
            }
        }
        else
        {
            string c = TextBox1.Text.Trim() + " " + TextBox2.Text.Trim();
            var patients2 = from patient2 in dbcontext.PatientTables.Local
                            where patient2.Name.Split(' ')[0].ToLower().StartsWith(c.Split(' ')[0].ToLower()) ||
                                  patient2.Name.Split(' ')[1].ToLower().StartsWith(c.Split(' ')[1].ToLower())
                            select new { patient2.Name, patient2.Email, patient2.Phone };
            var patientsList2 = patients2.ToList();
            if (patientsList2.Count > 0)
            {
                foreach (var p2 in patientsList2)
                {
                    ListBox2.Items.Add(p2.Name);
                    ListBox2.Items.Add(p2.Email);
                    ListBox2.Items.Add(p2.Phone);
                    ListBox2.Items.Add("");
                }
            }
            else
            {
                ListBox2.Items.Add("No items found.");
            }
        }


    }
}