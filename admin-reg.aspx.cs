using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using System.Configuration;


namespace APLiberary
{
    public partial class admin_reg : System.Web.UI.Page
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ASPC;Integrated Security=true; ");
        protected void Page_Load(object sender, EventArgs e)


        {
            if (!IsPostBack)
            {


            }
            if (Session["user"] != null)
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("name", sqlCon);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dtbl = new DataTable();
                sqlDa.SelectCommand.Parameters.AddWithValue("@ID", Session["user"].ToString());
                sqlDa.Fill(dtbl);
                sqlCon.Close();

                ID.Text = "Hello, " + dtbl.Rows[0]["name"].ToString();

                if ((dtbl.Rows[0]["role"].ToString()) != "admin")
                {
                    Response.Redirect("login.aspx");
                }
            }
            else
            {
                Response.Redirect("login.aspx");
            }

        }


        public string encryption(string encryptionpwd)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(encryptionpwd);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }


        protected void btnsave_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
            con.Open();
            string query = "select count (*) from login100 where email='" + email.Text +"'";
            SqlCommand cmd = new SqlCommand(query, con);
            string output = cmd.ExecuteScalar().ToString();

            if (output != "1")
            {

                if (pass.Text == pass2.Text)
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();

                    //encrypt
                    string strpassword = encryption(pass.Text);

                    SqlCommand sqlCmd = new SqlCommand("AdminReg6", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@name", name.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@email", email.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@pass", strpassword);
                    sqlCmd.Parameters.AddWithValue("@role", "admin");

                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                    string ID = HiddenField1.Value;
                    Session["signup"] = "1";
                    Response.Redirect("login.aspx");

                }
                else
                {
                    lblsc.Visible = true;
                    lblsc.Text = "Password doesn't match";
                    pass.Text = "";
                    pass2.Text = "";
                    lblsc2.Visible = false;


                }
            }
            else
            {
                lblsc2.Visible = true;

                lblsc2.Text = "Email already Exist";
                lblsc.Visible = false;


            }


        }
    }
}