using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace APLiberary
{
    public partial class order : System.Web.UI.Page

    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ASPC;Integrated Security=true; ");
        string imglocation = "";
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)


        {
            if (!IsPostBack)
                

                    if (Session["bookID"] == null)
                    {
                        Response.Redirect("home.aspx");
                    }
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    SqlDataAdapter sqlDa = new SqlDataAdapter("contactviewbyID", sqlCon);
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("@ID", Session["bookID"].ToString());
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    sqlCon.Close();

                    Title.Text = dtbl.Rows[0]["title"].ToString();                  

                    


                

        }

        protected void btnclear_Click(object sender, EventArgs e)
        {
            clear();
        }
        public void clear()
        {
            HiddenField1.Value = "";
            adress.Text = "";
            lblsc.Text = lblerror.Text = "";
            btnsave.Text = "save";


        }

        protected void btnsave_Click(object sender, EventArgs e)
        {

            if (sqlCon.State == ConnectionState.Closed)
                

            
            sqlCon.Open();
                        SqlCommand sqlCmd = new SqlCommand("createOrder", sqlCon);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@ID", (HiddenField1.Value == "" ? 0 : Convert.ToInt32(HiddenField1.Value)));
                        sqlCmd.Parameters.AddWithValue("@adress", adress.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@bookID", Session["bookID"].ToString());
                        sqlCmd.Parameters.AddWithValue("@userID", Session["user"].ToString());
                        sqlCmd.Parameters.AddWithValue("@statue", "Pending");

            sqlCmd.ExecuteNonQuery();
                        sqlCon.Close();

                        string ID = HiddenField1.Value;
                        clear();
                        if (ID == "")
                        {
                            lblsc.Text = "saved Successfully";
                        }
                        else
                        {
                            lblsc.Text = "Updated Successfully";

                            btnclear.Text = "Clear";
                        }


                  


        }
      



    }
}