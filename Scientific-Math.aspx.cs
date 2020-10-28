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

public partial class Scientific_Math : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
        cmd.ExecuteNonQuery();
        con.Close();

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
        cmd.ExecuteNonQuery();
        con.Close();
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
        cmd.ExecuteNonQuery();
        con.Close();
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
        cmd.ExecuteNonQuery();
        con.Close();

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
        cmd.ExecuteNonQuery();
        con.Close();

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
        cmd.ExecuteNonQuery();
        con.Close();
    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {
        Response.Redirect("3DBarGraphAlgo.aspx");
    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
     
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

public partial class Scientific_Math : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
        cmd.ExecuteNonQuery();
        con.Close();

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
        cmd.ExecuteNonQuery();
        con.Close();
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
        cmd.ExecuteNonQuery();
        con.Close();
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
        cmd.ExecuteNonQuery();
        con.Close();

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
        cmd.ExecuteNonQuery();
        con.Close();

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
        cmd.ExecuteNonQuery();
        con.Close();
    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {
        Response.Redirect("3DBarGraphAlgo.aspx");
    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
     
    }
>>>>>>> 9fe402ac798ef393c9b39749c089c6f74b01984d
}