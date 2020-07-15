using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.UI.WebControls.WebParts;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Net;






namespace APLiberary
{
    public partial class desc : System.Web.UI.Page
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ASPC;Integrated Security=true; ");
        protected void Page_Load(object sender, EventArgs e)

        {
            if (!IsPostBack)
            {
                if (Session["bookID"] == null)
                {
                    Response.Redirect("home.aspx");
                }
                if (Session["user"] != null && Session["bookID"] != null)
                {
                    view1();

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
                    view1();
                }


            }



        }


        void view1()
        {
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("contactviewbyID", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@ID", Session["bookID"].ToString());
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            sqlCon.Close();

            title.Text = dtbl.Rows[0]["title"].ToString();
            auther.Text = "Auther: " + dtbl.Rows[0]["auther"].ToString();
            descr.Text = "Description: " + dtbl.Rows[0]["descr"].ToString();
            sn.Text = "SN: " + dtbl.Rows[0]["copy"].ToString();
            location.Text = "Book Location: " + dtbl.Rows[0]["location"].ToString();
            quan.Text = "Number copies: " + dtbl.Rows[0]["quan"].ToString();
            price.Text = "Price: " + "RM"+ dtbl.Rows[0]["price"].ToString();

            disimage.ImageUrl = dtbl.Rows[0]["image"].ToString();


        }

        protected void add_Click(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("login.aspx");
            }



            else
            {
                int contactID = Int32.Parse(Session["bookID"].ToString());
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
                    succ.Text = "The book is succeffuly been added to your favorites";

                }
                else
                {
                    succ.Text = "This book has been already added to your favorites";

                }
            }
        }

        protected void comments_Click(object sender, EventArgs e)
        {
            int contactID = Int32.Parse(Session["bookID"].ToString());

            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
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

        protected void bttnpdf_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("contactviewbyID", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@ID", Session["bookID"].ToString());
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            sqlCon.Close();

            string pdf = dtbl.Rows[0]["PDFlocation"].ToString();


            string FilePath = Server.MapPath(pdf);
            WebClient User = new WebClient();
            Byte[] FileBuffer = User.DownloadData(FilePath);
            if (FileBuffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);
            }
        }

        protected void order_Click(object sender, EventArgs e)
        {
            Response.Redirect("order.aspx");

        }
    }
}