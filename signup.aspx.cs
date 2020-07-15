using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace APLiberary
{
    public partial class signup : System.Web.UI.Page
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ASPC;Integrated Security=true; ");
        protected void Page_Load(object sender, EventArgs e)


        {
            

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
            string query = "select count (*) from login100 where email='" + email.Text + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            string output = cmd.ExecuteScalar().ToString();

            if (output != "1")
            {

                if (pass.Text == pass2.Text)
                {
                    string strpassword = encryption(pass.Text);

                    bool isHuman = captchaBox.Validate(txtCaptcha.Text);
                    txtCaptcha.Text = null;
                    if (!isHuman)
                    {
                        lblsc.Text = "Wrong Captha entered, Please try again.";
                        lblsc.Visible = true;

                       
                    }
                    else
                    {
                        if (sqlCon.State == ConnectionState.Closed)
                            sqlCon.Open();
                        SqlCommand sqlCmd = new SqlCommand("AdminReg6", sqlCon);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@pass", strpassword);
                        sqlCmd.Parameters.AddWithValue("@name", name.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@email", email.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@role", "user");

                        sqlCmd.ExecuteNonQuery();
                        sqlCon.Close();
                        string ID = HiddenField1.Value;
                        Session["signup"] = "1";
                        Response.Redirect("login.aspx");
                    }

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