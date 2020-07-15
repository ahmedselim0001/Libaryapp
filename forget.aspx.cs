using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Net;
using System.Net.Mail;


namespace APLiberary
{
    public partial class forget : System.Web.UI.Page
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ASPC;Integrated Security=true; ");
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["user"] = null;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
            con.Open();
            string query = "select count (*) from login100 where email='" + email.Text + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            string output = cmd.ExecuteScalar().ToString();

            if (output == "1")
            { 

                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("login7", sqlCon);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("@email", email.Text);


                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                string email1 = dtbl.Rows[0]["email"].ToString();
                string name = dtbl.Rows[0]["name"].ToString();
                string password = dtbl.Rows[0]["password"].ToString();


                sqlCon.Close();

                

               

                if (!string.IsNullOrEmpty(password))
                {
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress("apliberary@gmail.com");
                    msg.To.Add(email.Text);
                    msg.Subject = "password recovery";
                    msg.Body = ("your username:" + name + "<br/><br/> your password:" + password);
                    msg.IsBodyHtml = true;

                    SmtpClient smt = new SmtpClient();
                    smt.Host = "smtp.gmail.com";
                    System.Net.NetworkCredential ntwd = new NetworkCredential();
                    ntwd.UserName = "apliberary@gmail.com";
                    ntwd.Password = "A12345AP";
                    smt.UseDefaultCredentials = true;
                    smt.Credentials = ntwd;
                    smt.Port = 587;
                    smt.EnableSsl = true;
                    smt.Send(msg);
                    Error.ForeColor = System.Drawing.Color.ForestGreen;
                    Error.Text = "check ur email to see your password and username recovery";

                }
            }
            else
            {
                Error.ForeColor = System.Drawing.Color.MediumVioletRed;
                Error.Text = "Email couldn't be found.";
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }
    }
}