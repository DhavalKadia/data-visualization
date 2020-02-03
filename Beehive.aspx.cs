using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public partial class Beehive : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LabelSuccess.Visible = false;
    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {
        Response.Redirect("BeehiveAlgo.aspx");
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        LabelSuccess.Visible = true;

        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        //SqlDataReader reader;

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        cmd.Connection = con;

        con.Open();

        cmd.CommandText = "UPDATE Save_image SET varify_bit = 1 WHERE id = 5";

        cmd.CommandType = CommandType.Text;

        try
        {
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception e2)
        {

        }
    }
}