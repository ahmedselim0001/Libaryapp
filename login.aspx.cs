using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace APLiberary
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        //SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ASPC;Integrated Security=true; ");
        SqlConnection sqlCon = new SqlConnection("Server=tcp:librarywebapp.database.windows.net,1433;Initial Catalog=ASPC;Persist Security Info=False;User ID=selim;Password=Tp046500@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30; ");
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["user"] = null;
            if (Session["signup"] != null)
            {
                
                    signup1.Visible = true;
                    signup1.Text = "You signed up successfully! ";
                    Session["signup"] = null;

                
            }
        }

        public string encryption(string encryptionpwd)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(encryptionpwd);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            signup1.Visible = false;
            Session["user"] = null;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
            con.Open();
            string strpassword = encryption(pass.Text);

            string query = "select count (*) from login100 where email='" + email.Text + "'and password='" + strpassword + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            string output = cmd.ExecuteScalar().ToString();

            if (output == "1")
            {

                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();


                SqlDataAdapter sqlDa = new SqlDataAdapter("login6", sqlCon);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("@email", email.Text);
                sqlDa.SelectCommand.Parameters.AddWithValue("@password", strpassword);


                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                sqlCon.Close();

                Session["user"] = dtbl.Rows[0]["ID"].ToString();

                string role = dtbl.Rows[0]["role"].ToString();

                if (role == "admin")
                {
                    Response.Redirect("home-admin.aspx");
                }
                if (role == "user")
                {
                    Response.Redirect("home.aspx");
                }
            }
            else
                error.Text = "Wrong Email or Password";
        }

        protected void email_TextChanged(object sender, EventArgs e)
        {

        }

        protected void signup_Click(object sender, EventArgs e)
        {
            Response.Redirect("signup.aspx");
        }
    }
}