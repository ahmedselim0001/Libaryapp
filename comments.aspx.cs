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
    public partial class comments : System.Web.UI.Page
    {
     SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ASPC;Integrated Security=true; ");
    protected void Page_Load(object sender, EventArgs e)




        {
            if (!IsPostBack)
            {
                view();
                lblsc.Text = "";
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
            if (Session["bookID"] == null)
        {
            Response.Redirect("home.aspx");

        }
        if (sqlCon.State == ConnectionState.Closed)
            sqlCon.Open();
        int bookID = Convert.ToInt32(Session["bookID"]) + 1;

        if (sqlCon.State == ConnectionState.Closed)
            sqlCon.Open();
        SqlDataAdapter sqlData = new SqlDataAdapter("contactviewbyID", sqlCon);
        sqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
        DataTable dtb2 = new DataTable();
        sqlData.SelectCommand.Parameters.AddWithValue("@ID", Session["bookID"].ToString());
        sqlData.Fill(dtb2);
        sqlCon.Close();
        ID.Text = dtb2.Rows[0]["ID"].ToString();



        SqlDataAdapter sqlDa = new SqlDataAdapter("contactviewbyID", sqlCon);
        sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
        sqlDa.SelectCommand.Parameters.AddWithValue("@ID", dtb2.Rows[0]["ID"].ToString());
        DataTable dtbl = new DataTable();
        sqlDa.Fill(dtbl);
        sqlCon.Close();
        gridcontact.DataSource = dtbl;
        gridcontact.DataBind();
        view1();

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
            if(Session["user"] == null)
            {
                Response.Redirect("login.aspx");
            }
        if (sqlCon.State == ConnectionState.Closed)
            sqlCon.Open();
        // view user name
        SqlDataAdapter sqlDat = new SqlDataAdapter("name", sqlCon);
        sqlDat.SelectCommand.CommandType = CommandType.StoredProcedure;
        DataTable dtb2 = new DataTable();
        sqlDat.SelectCommand.Parameters.AddWithValue("@ID", Session["user"].ToString());
        sqlDat.Fill(dtb2);




        // assigning userID value to variable
        string userID = dtb2.Rows[0]["ID"].ToString();


        SqlCommand sqlCmd = new SqlCommand("createorupdatecomment", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.AddWithValue("@ID", (HiddenField1.Value == "" ? 0 : Convert.ToInt32(HiddenField1.Value)));
        sqlCmd.Parameters.AddWithValue("@comment", comment.Text.Trim());
        sqlCmd.Parameters.AddWithValue("@userID", dtb2.Rows[0]["ID"].ToString());
        sqlCmd.Parameters.AddWithValue("@bookID", Session["bookID"].ToString());


        sqlCmd.ExecuteNonQuery();
        sqlCon.Close();
        string ID = HiddenField1.Value;
        if (ID == "")
            lblsc.Text = "saved Successfully";
        else
            lblsc.Text = "Updated Successfully";
        btnsave.Text = "Save";


        view1();
        comment.Text = "";


    }
    void view1()
    {
        if (sqlCon.State == ConnectionState.Closed)
            sqlCon.Open();
        SqlDataAdapter sqlDa = new SqlDataAdapter("commentsviewall", sqlCon);
        sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
        sqlDa.SelectCommand.Parameters.AddWithValue("@ID", Session["bookID"].ToString());
        DataTable dtbl = new DataTable();
        sqlDa.Fill(dtbl);
        sqlCon.Close();
        GridView1.DataSource = dtbl;
        GridView1.DataBind();
    }


    protected void gridcontact_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("login.aspx");

    }

        protected void HiddenField1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}