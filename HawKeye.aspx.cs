<<<<<<< HEAD
﻿using System;
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

public partial class HawKeye : System.Web.UI.Page
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
        cmd.CommandText = "SELECT * from TEMP1";
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

        cmd.CommandText = "UPDATE TEMP1 SET t ='" + rtp[1] + "' WHERE Id = 1";

        //con.Open();
        try
        {
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception e2)
        {
            Response.Write(e2.Message);
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
        cmd.CommandText = "SELECT * from TEMP1";
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
    	
        cmd.CommandText = "UPDATE TEMP1 SET t ='" + rtp[1] + "' WHERE Id = 1";

        //con.Open();
        try
        {
            cmd.ExecuteNonQuery();
            con.Close();
        }

        catch (Exception e2)
        {
            Response.Write(e2.Message);
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
        cmd.CommandText = "SELECT * from TEMP1";
        con.Open();

        reader = cmd.ExecuteReader();
        dt.Load(reader);

        double[] rtp = new double[3];

        rtp[0] = Convert.ToDouble(dt.Rows[0][1].ToString());
        rtp[1] = Convert.ToDouble(dt.Rows[0][2].ToString());
        rtp[2] = Convert.ToDouble(dt.Rows[0][3].ToString());

        if (rtp[2] < 180)
            rtp[2] += Program.dPhi;

        cmd.CommandText = "UPDATE TEMP1 SET p ='" + rtp[2] + "' WHERE Id = 1";

        //con.Open();
        try
        {
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception e2)
        {
            Response.Write(e2.Message);
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
        cmd.CommandText = "SELECT * from TEMP1";
        con.Open();

        reader = cmd.ExecuteReader();
        dt.Load(reader);

        double[] rtp = new double[3];

        rtp[0] = Convert.ToDouble(dt.Rows[0][1].ToString());
        rtp[1] = Convert.ToDouble(dt.Rows[0][2].ToString());
        rtp[2] = Convert.ToDouble(dt.Rows[0][3].ToString());

        if (rtp[2] > 0)
            rtp[2] -= Program.dPhi;
    	
        cmd.CommandText = "UPDATE TEMP1 SET p ='" + rtp[2] + "' WHERE Id = 1";

        //con.Open();
        try
        {
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception e2)
        {
            Response.Write(e2.Message);
        }
    }
    protected void btn5_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        SqlDataReader reader;

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        cmd.Connection = con;

        //Change Part
        cmd.CommandText = "SELECT * from TEMP1";
        con.Open();

        try
        {
            reader = cmd.ExecuteReader();
            dt.Load(reader);
        }
        catch (Exception e2)
        {
            Response.Write(e2.Message);
        }

        double[] rtp = new double[3];

        rtp[0] = Convert.ToDouble(dt.Rows[0][1].ToString());
        rtp[1] = Convert.ToDouble(dt.Rows[0][2].ToString());
        rtp[2] = Convert.ToDouble(dt.Rows[0][3].ToString());

        rtp[0] -= Program.dRadii;
        
        cmd.CommandText = "UPDATE TEMP1 SET r ='" + rtp[0] + "' WHERE Id = 1";

        //con.Open();
        try
        {
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception e2)
        {
            Response.Write(e2.Message);
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
        cmd.CommandText = "SELECT * from TEMP1";
        con.Open();

        try
        {
            reader = cmd.ExecuteReader();
            dt.Load(reader);
        }
        catch (Exception e2)
        {
            Response.Write(e2.Message);
        }

        double[] rtp = new double[3];

        rtp[0] = Convert.ToDouble(dt.Rows[0][1].ToString());
        rtp[1] = Convert.ToDouble(dt.Rows[0][2].ToString());
        rtp[2] = Convert.ToDouble(dt.Rows[0][3].ToString());

        rtp[0] += Program.dRadii;

        cmd.CommandText = "UPDATE TEMP1 SET r ='" + rtp[0] + "' WHERE Id = 1";

        //con.Open();
        try
        {
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception e2)
        {
            Response.Write(e2.Message);
        }
    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {
        Response.Redirect("HawKeyeAlgo.aspx");
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
        cmd.CommandText = "Insert into Ball_bowler(Ball_Id,Bowler_id,pt_of_release_x,pt_of_release_y,pt_of_release_z,angle_of_release_1,angle_of_release_2,speed,spin) values (@Ball_Id,@Bowler_id,@pt_of_release_x,0,0,@angle_of_release_1,@angle_of_release_2,@speed,@spin) ";

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
        p1.ParameterName = "@pt_of_release_x";
        p1.Value = TextBox1.Text;
        p1.DbType = DbType.Int32;

        SqlParameter p2 = new SqlParameter();
        p2.ParameterName = "@angle_of_release_1";
        p2.Value = TextBox4.Text;
        p2.DbType = DbType.Int32;

        SqlParameter p3 = new SqlParameter();
        p3.ParameterName = "@angle_of_release_2";
        p3.Value = TextBox5.Text;
        p3.DbType = DbType.Int32;

        SqlParameter p4 = new SqlParameter();
        p4.ParameterName = "@speed";
        p4.Value = TextBox6.Text;
        p4.DbType = DbType.Int32;

        SqlParameter p5 = new SqlParameter();
        p5.ParameterName = "@spin";
        p5.Value = TextBox7.Text;
        p5.DbType = DbType.Int32;

        SqlParameter p6 = new SqlParameter();
        p6.ParameterName = "@Ball_Id";
        p6.Value = TextBox2.Text;
        p6.DbType = DbType.Int32;

        SqlParameter p7 = new SqlParameter();
        p7.ParameterName = "@Bowler_id";
        p7.Value = DropDownList1.SelectedItem.Text;
        p7.DbType = DbType.Int32;

        cmd.Parameters.Add(p1);
        cmd.Parameters.Add(p2);
        cmd.Parameters.Add(p3);
        cmd.Parameters.Add(p4);
        cmd.Parameters.Add(p5);
        cmd.Parameters.Add(p6);
        cmd.Parameters.Add(p7);

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception e2)
        {
            LabelError.Visible = true;    
        }
    }
    protected void TextBox6_TextChanged(object sender, EventArgs e)
    {

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        
    }
    protected void Button2_Click1(object sender, EventArgs e)
    {
    
    }
    protected void Button2_Click2(object sender, EventArgs e)
    {
        TextBox1.Text = " ";
        TextBox2.Text = " ";
        TextBox4.Text = " ";
        TextBox5.Text = " ";
        TextBox6.Text = " ";
        TextBox7.Text = " ";
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        cmd.Connection = con;
        cmd.CommandText = "Delete from Ball_bowler";

        da.DeleteCommand = cmd;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception e2)
        {
            Response.Write(e2.Message);
        }

        cmd.CommandText = "UPDATE Save_image SET varify_bit = 0 WHERE id = 2";
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

        cmd.CommandText = "UPDATE Save_image SET varify_bit = 1 WHERE id = 2";

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
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Beehive.aspx");
    }
=======
﻿using System;
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

public partial class HawKeye : System.Web.UI.Page
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
        cmd.CommandText = "SELECT * from TEMP1";
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

        cmd.CommandText = "UPDATE TEMP1 SET t ='" + rtp[1] + "' WHERE Id = 1";

        //con.Open();
        try
        {
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception e2)
        {
            Response.Write(e2.Message);
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
        cmd.CommandText = "SELECT * from TEMP1";
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
    	
        cmd.CommandText = "UPDATE TEMP1 SET t ='" + rtp[1] + "' WHERE Id = 1";

        //con.Open();
        try
        {
            cmd.ExecuteNonQuery();
            con.Close();
        }

        catch (Exception e2)
        {
            Response.Write(e2.Message);
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
        cmd.CommandText = "SELECT * from TEMP1";
        con.Open();

        reader = cmd.ExecuteReader();
        dt.Load(reader);

        double[] rtp = new double[3];

        rtp[0] = Convert.ToDouble(dt.Rows[0][1].ToString());
        rtp[1] = Convert.ToDouble(dt.Rows[0][2].ToString());
        rtp[2] = Convert.ToDouble(dt.Rows[0][3].ToString());

        if (rtp[2] < 180)
            rtp[2] += Program.dPhi;

        cmd.CommandText = "UPDATE TEMP1 SET p ='" + rtp[2] + "' WHERE Id = 1";

        //con.Open();
        try
        {
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception e2)
        {
            Response.Write(e2.Message);
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
        cmd.CommandText = "SELECT * from TEMP1";
        con.Open();

        reader = cmd.ExecuteReader();
        dt.Load(reader);

        double[] rtp = new double[3];

        rtp[0] = Convert.ToDouble(dt.Rows[0][1].ToString());
        rtp[1] = Convert.ToDouble(dt.Rows[0][2].ToString());
        rtp[2] = Convert.ToDouble(dt.Rows[0][3].ToString());

        if (rtp[2] > 0)
            rtp[2] -= Program.dPhi;
    	
        cmd.CommandText = "UPDATE TEMP1 SET p ='" + rtp[2] + "' WHERE Id = 1";

        //con.Open();
        try
        {
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception e2)
        {
            Response.Write(e2.Message);
        }
    }
    protected void btn5_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        SqlDataReader reader;

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        cmd.Connection = con;

        //Change Part
        cmd.CommandText = "SELECT * from TEMP1";
        con.Open();

        try
        {
            reader = cmd.ExecuteReader();
            dt.Load(reader);
        }
        catch (Exception e2)
        {
            Response.Write(e2.Message);
        }

        double[] rtp = new double[3];

        rtp[0] = Convert.ToDouble(dt.Rows[0][1].ToString());
        rtp[1] = Convert.ToDouble(dt.Rows[0][2].ToString());
        rtp[2] = Convert.ToDouble(dt.Rows[0][3].ToString());

        rtp[0] -= Program.dRadii;
        
        cmd.CommandText = "UPDATE TEMP1 SET r ='" + rtp[0] + "' WHERE Id = 1";

        //con.Open();
        try
        {
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception e2)
        {
            Response.Write(e2.Message);
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
        cmd.CommandText = "SELECT * from TEMP1";
        con.Open();

        try
        {
            reader = cmd.ExecuteReader();
            dt.Load(reader);
        }
        catch (Exception e2)
        {
            Response.Write(e2.Message);
        }

        double[] rtp = new double[3];

        rtp[0] = Convert.ToDouble(dt.Rows[0][1].ToString());
        rtp[1] = Convert.ToDouble(dt.Rows[0][2].ToString());
        rtp[2] = Convert.ToDouble(dt.Rows[0][3].ToString());

        rtp[0] += Program.dRadii;

        cmd.CommandText = "UPDATE TEMP1 SET r ='" + rtp[0] + "' WHERE Id = 1";

        //con.Open();
        try
        {
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception e2)
        {
            Response.Write(e2.Message);
        }
    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {
        Response.Redirect("HawKeyeAlgo.aspx");
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
        cmd.CommandText = "Insert into Ball_bowler(Ball_Id,Bowler_id,pt_of_release_x,pt_of_release_y,pt_of_release_z,angle_of_release_1,angle_of_release_2,speed,spin) values (@Ball_Id,@Bowler_id,@pt_of_release_x,0,0,@angle_of_release_1,@angle_of_release_2,@speed,@spin) ";

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
        p1.ParameterName = "@pt_of_release_x";
        p1.Value = TextBox1.Text;
        p1.DbType = DbType.Int32;

        SqlParameter p2 = new SqlParameter();
        p2.ParameterName = "@angle_of_release_1";
        p2.Value = TextBox4.Text;
        p2.DbType = DbType.Int32;

        SqlParameter p3 = new SqlParameter();
        p3.ParameterName = "@angle_of_release_2";
        p3.Value = TextBox5.Text;
        p3.DbType = DbType.Int32;

        SqlParameter p4 = new SqlParameter();
        p4.ParameterName = "@speed";
        p4.Value = TextBox6.Text;
        p4.DbType = DbType.Int32;

        SqlParameter p5 = new SqlParameter();
        p5.ParameterName = "@spin";
        p5.Value = TextBox7.Text;
        p5.DbType = DbType.Int32;

        SqlParameter p6 = new SqlParameter();
        p6.ParameterName = "@Ball_Id";
        p6.Value = TextBox2.Text;
        p6.DbType = DbType.Int32;

        SqlParameter p7 = new SqlParameter();
        p7.ParameterName = "@Bowler_id";
        p7.Value = DropDownList1.SelectedItem.Text;
        p7.DbType = DbType.Int32;

        cmd.Parameters.Add(p1);
        cmd.Parameters.Add(p2);
        cmd.Parameters.Add(p3);
        cmd.Parameters.Add(p4);
        cmd.Parameters.Add(p5);
        cmd.Parameters.Add(p6);
        cmd.Parameters.Add(p7);

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception e2)
        {
            LabelError.Visible = true;    
        }
    }
    protected void TextBox6_TextChanged(object sender, EventArgs e)
    {

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        
    }
    protected void Button2_Click1(object sender, EventArgs e)
    {
    
    }
    protected void Button2_Click2(object sender, EventArgs e)
    {
        TextBox1.Text = " ";
        TextBox2.Text = " ";
        TextBox4.Text = " ";
        TextBox5.Text = " ";
        TextBox6.Text = " ";
        TextBox7.Text = " ";
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        cmd.Connection = con;
        cmd.CommandText = "Delete from Ball_bowler";

        da.DeleteCommand = cmd;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception e2)
        {
            Response.Write(e2.Message);
        }

        cmd.CommandText = "UPDATE Save_image SET varify_bit = 0 WHERE id = 2";
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

        cmd.CommandText = "UPDATE Save_image SET varify_bit = 1 WHERE id = 2";

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
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Beehive.aspx");
    }
>>>>>>> 9fe402ac798ef393c9b39749c089c6f74b01984d
}