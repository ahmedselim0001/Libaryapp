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
    public partial class home : System.Web.UI.Page
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ASPC;Integrated Security=true; ");
        protected void Page_Load(object sender, EventArgs e)


        {
            Response.AppendHeader("Refresh", "5");

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

                    if ((dtbl.Rows[0]["role"].ToString()) == "admin")
                    {
                        Response.Redirect("home-admin.aspx");
                    }


                }
                else
                {
                    hyperlink1.Text = "Login";
                }


            }



        }

        void view()
        {
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("contactviewall", sqlCon);
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

            if (Session["user"] == null)
            {

                Response.Redirect("login.aspx");

            }
            else
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                // view book ID
                SqlDataAdapter sqlDa = new SqlDataAdapter("contactviewbyID", sqlCon);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("@ID", contactID);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                HiddenField1.Value = contactID.ToString();






                //view user name

                SqlDataAdapter sqlDat = new SqlDataAdapter("name", sqlCon);
                sqlDat.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dtb2 = new DataTable();
                sqlDat.SelectCommand.Parameters.AddWithValue("@ID", Session["user"].ToString());
                sqlDat.Fill(dtb2);

                // making sure the favorite dont duplicate

                string userID = dtb2.Rows[0]["ID"].ToString();
                string bookID = contactID.ToString();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
                con.Open();
                string query = "select count (*) from favorites where bookID='" + bookID + "'and userID='" + userID + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                string output = cmd.ExecuteScalar().ToString();

                if (output == "0")
                {

                    // add bookID and userID to favorites
                    SqlCommand sqlCmd = new SqlCommand("addtofavorite0", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@bookID", contactID.ToString());
                    sqlCmd.Parameters.AddWithValue("@userID", dtb2.Rows[0]["ID"].ToString());


                    sqlCmd.ExecuteNonQuery();
                    string ID = HiddenField1.Value;

                    sqlCon.Close();
                    lblsc.Text = "The book is succeffuly been added to your favorites";
                    lblerror.Text = "";

                }
                else
                {
                    lblsc.Text = "";
                    lblerror.Text = "This book has been already added to your favorites";

                }

            }

        }
        protected void lnk_delete(object sender, EventArgs e)
        {
            int contactID = Convert.ToInt32((sender as LinkButton).CommandArgument);
           
            
            
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
        

        protected void gridcontact_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");

        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            lblerror.Text = "";
            lblsc.Text = "";
            gridcontact.DataSource = null;

            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();

            SqlDataAdapter sqlDa = new SqlDataAdapter("search", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@keyword", search.Text.Trim());
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            sqlCon.Close();
            HiddenField1.Value = search.Text.Trim();
            gridcontact.DataSource = dtbl;
            gridcontact.DataBind();

        }

        protected void showall_Click(object sender, EventArgs e)
        {
            lblerror.Text = "";
            lblsc.Text = "";
            search.Text = "";
            gridcontact.DataSource = null;
            view();
        }
        protected void desc(object sender, EventArgs e)
        {
            int contactID = Convert.ToInt32((sender as LinkButton).CommandArgument);

            Session["bookID"] = contactID.ToString();

            
            Response.Redirect("desc.aspx");
        }
    }
}