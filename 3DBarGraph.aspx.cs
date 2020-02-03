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

public partial class _3DBarGraph : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LabelError.Visible = false;
        LabelSuccess.Visible = false;
    }

    protected void btn1_Click(object sender, EventArgs e)
    {
        //Right Button
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        SqlDataReader reader;

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        cmd.Connection = con;

        //Change Part
        cmd.CommandText = "SELECT * from TEMP2";
        con.Open();

        reader = cmd.ExecuteReader();
        dt.Load(reader);

        double[] rtp = new double[3];

        rtp[0] = Convert.ToDouble(dt.Rows[0][1].ToString());
        rtp[1] = Convert.ToDouble(dt.Rows[0][2].ToString());
        rtp[2] = Convert.ToDouble(dt.Rows[0][3].ToString());

        if (rtp[1] + Program.dTheta < 360)
            rtp[1] += Program.dTheta;
        else
            rtp[1] = 0;

        cmd.CommandText = "UPDATE TEMP2 SET t ='" + rtp[1] + "' WHERE Id = 1";

        //con.Open();
        try
        {
            cmd.ExecuteNonQuery();
            con.Close();
        }catch(Exception e1)
        {
            Response.Write(e1.Message);
        }
        //Right Button
    }
    protected void btn2_Click(object sender, EventArgs e)
    {
        //Left Button
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        SqlDataReader reader;

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        cmd.Connection = con;

        //Change Part
        cmd.CommandText = "SELECT * from TEMP2";
        con.Open();

        reader = cmd.ExecuteReader();
        dt.Load(reader);

        double[] rtp = new double[3];

        rtp[0] = Convert.ToDouble(dt.Rows[0][1].ToString());
        rtp[1] = Convert.ToDouble(dt.Rows[0][2].ToString());
        rtp[2] = Convert.ToDouble(dt.Rows[0][3].ToString());

        if (rtp[1] - Program.dTheta > 0)
            rtp[1] -= Program.dTheta;
        else
            rtp[1] = 360;

        cmd.CommandText = "UPDATE TEMP2 SET t ='" + rtp[1] + "' WHERE Id = 1";

        //con.Open();
        try
        {
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception e1)
        {
            Response.Write(e1.Message);
        }
    }

    protected void btn3_Click(object sender, EventArgs e)
    {
        //Up Button
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        SqlDataReader reader;

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        cmd.Connection = con;

        //Change Part
        cmd.CommandText = "SELECT * from TEMP2";
        con.Open();

        reader = cmd.ExecuteReader();
        dt.Load(reader);

        double[] rtp = new double[3];

        rtp[0] = Convert.ToDouble(dt.Rows[0][1].ToString());
        rtp[1] = Convert.ToDouble(dt.Rows[0][2].ToString());
        rtp[2] = Convert.ToDouble(dt.Rows[0][3].ToString());

        if (rtp[2] < 180)
            rtp[2] += Program.dPhi;

        cmd.CommandText = "UPDATE TEMP2 SET p ='" + rtp[2] + "' WHERE Id = 1";

        //con.Open();
        try
        {
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception e1)
        {
            Response.Write(e1.Message);
        }
    }
    protected void btn4_Click(object sender, EventArgs e)
    {
        //Down Button
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        SqlDataReader reader;

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        cmd.Connection = con;

        //Change Part
        cmd.CommandText = "SELECT * from TEMP2";
        con.Open();

        reader = cmd.ExecuteReader();
        dt.Load(reader);

        double[] rtp = new double[3];

        rtp[0] = Convert.ToDouble(dt.Rows[0][1].ToString());
        rtp[1] = Convert.ToDouble(dt.Rows[0][2].ToString());
        rtp[2] = Convert.ToDouble(dt.Rows[0][3].ToString());

        if (rtp[2] > 0)
            rtp[2] -= Program.dPhi;

        cmd.CommandText = "UPDATE TEMP2 SET p ='" + rtp[2] + "' WHERE Id = 1";

        //con.Open();
        try
        {
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception e1)
        {
            Response.Write(e1.Message);
        }
    }
    protected void btn5_Click(object sender, EventArgs e)
    {
        //IN
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        SqlDataReader reader;

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        cmd.Connection = con;

        //Change Part
        cmd.CommandText = "SELECT * from TEMP2";
        con.Open();

        reader = cmd.ExecuteReader();
        dt.Load(reader);

        double[] rtp = new double[3];

        rtp[0] = Convert.ToDouble(dt.Rows[0][1].ToString());
        rtp[1] = Convert.ToDouble(dt.Rows[0][2].ToString());
        rtp[2] = Convert.ToDouble(dt.Rows[0][3].ToString());

        rtp[0] -= Program.dRadii;

        cmd.CommandText = "UPDATE TEMP2 SET r ='" + rtp[0] + "' WHERE Id = 1";

        //con.Open();
        try
        {
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception e1)
        {
            Response.Write(e1.Message);
        }
    }
    protected void btn6_Click(object sender, EventArgs e)
    {
        //Out
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        SqlDataReader reader;

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        cmd.Connection = con;

        //Change Part
        cmd.CommandText = "SELECT * from TEMP2";
        con.Open();

        reader = cmd.ExecuteReader();
        dt.Load(reader);

        double[] rtp = new double[3];

        rtp[0] = Convert.ToDouble(dt.Rows[0][1].ToString());
        rtp[1] = Convert.ToDouble(dt.Rows[0][2].ToString());
        rtp[2] = Convert.ToDouble(dt.Rows[0][3].ToString());

        rtp[0] += Program.dRadii;

        cmd.CommandText = "UPDATE TEMP2 SET r ='" + rtp[0] + "' WHERE Id = 1";

        //con.Open();
        try
        {
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception e1)
        {
            Response.Write(e1.Message);
        }
    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {
        Response.Redirect("3DBarGraphAlgo.aspx");
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        //Entry Button
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        //SqlDataReader reader;

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        cmd.Connection = con;

        //Change Part
        //cmd.CommandText = "SELECT * from Bar_Graph";
        //con.Open();

        //reader = cmd.ExecuteReader();
        //dt.Load(reader);
        cmd.CommandText = "Insert into Bar_Graph(Team_Id,Batsman_position,Runs) values (@Team_Id,@Batsman_position,@Runs) ";

        /*
        double[] rtp = new double[3];

        rtp[0] = Convert.ToDouble(dt.Rows[0][1].ToString());
        rtp[1] = Convert.ToDouble(dt.Rows[0][2].ToString());
        rtp[2] = Convert.ToDouble(dt.Rows[0][3].ToString());

        if (rtp[1] - Program.dTheta > 0)
            rtp[1] -= Program.dTheta;
        else
            rtp[1] = 360;
        */
        //cmd.CommandText = "UPDATE TEMP2 SET t ='" + rtp[1] + "' WHERE Id = 1";

        SqlParameter p1 = new SqlParameter();
        p1.ParameterName = "@Team_Id";
        p1.Value = TextBox1.Text;
        p1.DbType = DbType.Int32;

        SqlParameter p2 = new SqlParameter();
        p2.ParameterName = "@Batsman_position";
        p2.Value = TextBox2.Text;
        p2.DbType = DbType.Int32;

        SqlParameter p3 = new SqlParameter();
        p3.ParameterName = "@Runs";
        p3.Value = TextBox3.Text;
        p3.DbType = DbType.Int32;

        cmd.Parameters.Add(p1);
        cmd.Parameters.Add(p2);
        cmd.Parameters.Add(p3);

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception e1)
        {
           LabelError.Visible=true;
        }
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        cmd.Connection = con;
        cmd.CommandText = "Delete from Bar_Graph";

        da.DeleteCommand = cmd;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch(Exception eex)
        {
            Response.Write(eex.Message);
        }

        cmd.CommandText = "UPDATE Save_image SET varify_bit = 0 WHERE id = 1";
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
        TextBox3.Text = " ";
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        LabelSuccess.Visible = true;

        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        SqlDataReader reader;

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        cmd.Connection = con;

        con.Open();

        cmd.CommandText = "UPDATE Save_image SET varify_bit = 1 WHERE id = 1";

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