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
    public partial class fav : System.Web.UI.Page
    {
         
        SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ASPC;Integrated Security=true; ");
        protected void Page_Load(object sender, EventArgs e)


        {
            if (!IsPostBack)
            {
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


                }
                else
                {
                }


            }



        }

        void view()
        {
            if (Session["user"] == null)
            {

                Response.Redirect("login.aspx");

            }
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();


            // view user ID
            SqlDataAdapter sqlDat = new SqlDataAdapter("name", sqlCon);
            sqlDat.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtb2 = new DataTable();
            sqlDat.SelectCommand.Parameters.AddWithValue("@ID", Session["user"].ToString());
            sqlDat.Fill(dtb2);

            sqlCon.Close();


            SqlDataAdapter sqlDa = new SqlDataAdapter("favoritesviewbyID", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@ID", dtb2.Rows[0]["ID"].ToString());
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            sqlCon.Close();
            gridcontact.DataSource = dtbl;
            gridcontact.DataBind();
            HiddenField1.Value = dtb2.Rows[0]["ID"].ToString();
        }


        protected void lnk_onclick(object sender, EventArgs e)
        {
            int contactID = Convert.ToInt32((sender as LinkButton).CommandArgument);
            if (Session["user"] == null)
            {

                Response.Redirect("login.aspx");

            }
            else
            {
                Session["bookID"] = null;
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                // view book ID
                SqlDataAdapter sqlDa = new SqlDataAdapter("contactviewbyID", sqlCon);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("@ID", contactID);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                HiddenField1.Value = contactID.ToString();
                Session["bookID"] = contactID.ToString();


                sqlCon.Close();
                Response.Redirect("comments.aspx");
            }
        }

        protected void lnk_delete(object sender, EventArgs e)
        {
            int contactID = Convert.ToInt32((sender as LinkButton).CommandArgument);

            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();

            SqlDataAdapter sqlDa = new SqlDataAdapter("contactviewbyID", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@ID", contactID);
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            HiddenField1.Value = contactID.ToString();


            SqlCommand sqlCmd = new SqlCommand("deletefromfavorites", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ID", Convert.ToInt32(HiddenField1.Value));
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            view();

        }

        protected void gridcontact_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");

        }
    }
}