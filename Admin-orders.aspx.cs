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
    public partial class Admin_orders : System.Web.UI.Page
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ASPC;Integrated Security=true; ");
        protected void Page_Load(object sender, EventArgs e)

        {
            if (!IsPostBack)
            {
                ID.Text = "";
                lblerror.Text = "";
                lblsc.Text = "";
                view();
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

                    ID.Text = dtbl.Rows[0]["name"].ToString();

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



        }

        void view()
        {
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("AdminViewOdrders", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            sqlCon.Close();
            gridcontact.DataSource = dtbl;
            gridcontact.DataBind();
        }
        protected void lnk_onclick(object sender, EventArgs e)
        {
            int contactID = Convert.ToInt32((sender as LinkButton).CommandArgument);

            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();


            SqlCommand sqlCmd = new SqlCommand("order-shipped", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ID", contactID.ToString());
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            string ID = HiddenField1.Value;

                lblsc.Text = "Updated Successfully";
            view();

        }
        protected void lnk_delete(object sender, EventArgs e)
        {
            int contactID = Convert.ToInt32((sender as LinkButton).CommandArgument);

            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();


            SqlCommand sqlCmd = new SqlCommand("order-recieved", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ID", contactID.ToString());
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            string ID = HiddenField1.Value;

            lblsc.Text = "Updated Successfully";
            view();

        }
    


        protected void gridcontact_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");

        }

      
        protected void desc(object sender, EventArgs e)
        {
            int contactID = Convert.ToInt32((sender as LinkButton).CommandArgument);

            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();


            SqlCommand sqlCmd = new SqlCommand("order-delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ID", contactID.ToString());
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            string ID = HiddenField1.Value;

            lblsc.Text = "Deleted Successfully";
            view();
        }
    }
}