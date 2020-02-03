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

public partial class Population : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LabelError.Visible = false;
        LabelSuccess.Visible = false;
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        cmd.Connection = con;

        cmd.CommandText = "UPDATE Population_tab SET Logmin=" + Convert.ToInt32(TextBox2.Text) + ",Logmax=" + Convert.ToInt32(TextBox1.Text) + ",Latmin=" + Convert.ToInt32(TextBox4.Text) + ",Latmax=" + Convert.ToInt32(TextBox6.Text);
        da.UpdateCommand = cmd;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception eex)
        {
            Response.Write(eex.Message);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        TextBox1.Text = " ";
        TextBox2.Text = " ";
        TextBox4.Text = " ";
        TextBox6.Text = " ";
    }
    protected void btn2_Click(object sender, EventArgs e)
    {

    }
    protected void btn1_Click(object sender, EventArgs e)
    {

    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {
        Response.Redirect("PopulationAlgo.aspx");
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {

    }
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox6_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox4_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox1_TextChanged1(object sender, EventArgs e)
    {

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

        cmd.CommandText = "UPDATE Save_image SET varify_bit = 1 WHERE id = 6";

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