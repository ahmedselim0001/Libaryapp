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
    public partial class home_admin : System.Web.UI.Page

    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ASPC;Integrated Security=true; ");
        string imglocation = "";
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)


        {
            if (!IsPostBack)
            {
                image1.Visible = false;
                lblsc.Text = lblerror.Text = "";
                view();


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

        protected void btnclear_Click(object sender, EventArgs e)
        {
            clear();
        }
        public void clear()
        {
            HiddenField1.Value = "";
            bookname.Text = Textmobile.Text = Textaddress.Text = Textlocation.Text = descr.Text = image1.ImageUrl = "";
            image1.Visible = false;
            lblsc.Text = lblerror.Text = "";
            btnsave.Text = "save";


        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            SN.Text = lblerror.Text = lblsc.Text = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
            con.Open();
            string query = "select count (*) from books where copy='" + Textmobile.Text + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            string output = cmd.ExecuteScalar().ToString();

            if (output != "1")
            {

                image1.Visible = true;
                string name = "file";
                string fname = bookname.Text;

               
                String filePath = Server.MapPath("PDF/");
                PDF.SaveAs(filePath + file.FileName);
                fname = "~/PDF/" + file.FileName;




                if (file.HasFile)
                {
                    string ext = Path.GetExtension(file.FileName);
                    if (ext == ".jpg" || ext == ".png" || ext == ".JPG" || ext == ".PNG")
                    {
                        string path = Server.MapPath("books/images/");
                        file.SaveAs(path + file.FileName);
                        name = "~/books/images/" + file.FileName;
                        image1.ImageUrl = "~/books/images/" + file.FileName;
                        sqlCon.Open();
                        SqlCommand sqlCmd = new SqlCommand("contactcreateorupdate", sqlCon);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@ID", (HiddenField1.Value == "" ? 0 : Convert.ToInt32(HiddenField1.Value)));
                        sqlCmd.Parameters.AddWithValue("@title", bookname.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@auther", Textaddress.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@copy", Textmobile.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@location", Textlocation.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@desc", descr.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@image", name);
                        sqlCmd.Parameters.AddWithValue("@PDFlocation", fname);
                        sqlCmd.Parameters.AddWithValue("@quan", quan.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@price", price.Text.Trim());
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
                            view();

                            btnclear.Text = "Clear";
                        }


                    }
                    else
                    {
                        lblerror.Text = "only JPG and PNG files are allowed!";
                    }
                }
                else
                {
                    lblerror.Text = "please select file";

                }
            }
            else
            {
                SN.ForeColor = Color.Red;
                SN.Visible = true;
                SN.Text = "this book's SN already exist";
            }
            view();
           




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
            SN.Text = lblerror.Text = lblsc.Text = "";

            int contactID = Convert.ToInt32((sender as LinkButton).CommandArgument);

            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();

            SqlDataAdapter sqlDa = new SqlDataAdapter("contactviewbyID", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@ID", contactID);
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            sqlCon.Close();
            HiddenField1.Value = contactID.ToString();
            bookname.Text = dtbl.Rows[0]["title"].ToString();
            Textaddress.Text = dtbl.Rows[0]["auther"].ToString();
            Textmobile.Text = dtbl.Rows[0]["copy"].ToString();
            Textlocation.Text = dtbl.Rows[0]["location"].ToString();
            descr.Text = dtbl.Rows[0]["descr"].ToString();
            image1.ImageUrl = dtbl.Rows[0]["image"].ToString();
            image1.Visible = true;




            btnsave.Text = "update";
            btnclear.Text = "Clear / Cancel";
        }
        protected void lnk_delete(object sender, EventArgs e)
        {
            SN.Text = lblerror.Text = lblsc.Text = "";

            int contactID = Convert.ToInt32((sender as LinkButton).CommandArgument);

            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();

            SqlDataAdapter sqlDa = new SqlDataAdapter("contactviewbyID", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@ID", contactID);
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            HiddenField1.Value = contactID.ToString();


            SqlCommand sqlCmd = new SqlCommand("contactdeletebyID", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ID", Convert.ToInt32(HiddenField1.Value));
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            clear();
            view();
            lblsc.Text = "The record has been deleted successfully";

        }

        protected void gridcontact_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gridcontact_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



    }
}