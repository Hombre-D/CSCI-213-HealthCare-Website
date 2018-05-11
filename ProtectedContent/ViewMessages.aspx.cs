using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

public partial class ProtectedContent_ViewMessages : System.Web.UI.Page
{
    AchcDatabaseEntities dbcontext = new AchcDatabaseEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RadioButtonList1.SelectedIndex = 0;
            LoadMessageList();
        }
    }

    private void LoadMessageList()
    {
        dbcontext.MessageTables.Load();

        if (RadioButtonList1.SelectedIndex == 0)
        {
            var inboxQuery = from message in dbcontext.MessageTables.Local
                             where message.UserNameTo.Equals(HttpContext.Current.User.Identity.Name)
                             orderby message.Date descending
                             select new { message.Date, From = message.UserNameFrom, Subject = message.MessageSubject, Body = message.MessageBody, message.MessageId };
            GridView1.DataSource = inboxQuery.ToList();
        } else
        {
            var outboxQuery = from message in dbcontext.MessageTables.Local
                              where message.UserNameFrom.Equals(HttpContext.Current.User.Identity.Name)
                              orderby message.Date descending
                              select new { message.Date, To = message.UserNameTo, Subject = message.MessageSubject, Body = message.MessageBody, message.MessageId };
            GridView1.DataSource = outboxQuery.ToList();
        }

        GridView1.DataBind();
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.SelectedIndex = -1;
        TextBox1.Text = "";
        LoadMessageList();
    }


    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox1.Text = HttpUtility.HtmlDecode(GridView1.SelectedRow.Cells[5].Text);
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[5].Visible = false;
        e.Row.Cells[6].Visible = false;
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        dbcontext.MessageTables.Load();
        int msgId = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells[6].Text);
        MessageTable msg = (from message in dbcontext.MessageTables.Local
                            where message.MessageId == msgId
                            select message).First();
        dbcontext.MessageTables.Remove(msg);
        dbcontext.SaveChanges();
        TextBox1.Text = "";
        LoadMessageList();
    }
}