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


/// <summary>
/// Summary description for BarGraph
/// </summary>
public class BarGraph
{
    Box3D[][] box = new Box3D[5][];//5
    int xNumber, yNumber;
    double scale;
    int initPoint;
    Color color;

    
    public double[,] data = new double[,]
    {
            {0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0}
    };
     

    public BarGraph(int xNumber, int yNumber, double scale)
    {
        this.xNumber = xNumber;
        this.yNumber = yNumber;
        this.scale = scale;
    }

    public void initBarGraph()
    {
        this.initPoint = Program.pointCount;

        double[] position = new double[3];

        double[][][][] basee = new double[5][][][];

        int i = 0;
        /* Data Fetching */

        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        DataTable dt = new DataTable();

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM Bar_Graph";

        con.Open();

        reader = cmd.ExecuteReader();

        dt.Load(reader);

        int team_id = 0;
        int batsman_position = 0;
        int runs = 0;

        
        int j ;
        for (j = 0; j < dt.Rows.Count; j++ )
        {
            team_id = Convert.ToInt32(dt.Rows[j][0].ToString());
            batsman_position = Convert.ToInt32(dt.Rows[j][1].ToString());
            runs = Convert.ToInt32(dt.Rows[j][2].ToString());

            team_id--;
            batsman_position--;

            data[team_id, batsman_position] = runs;
        }

        con.Close();
        reader.Close();
    

        // End Fetching
        for (i = 0; i < 5; i++)
        {
            box[i] = new Box3D[12];

            basee[i] = new double[6][][];

            for (j = 0; j < 5; j++)
            {
                position[0] = scale * i;
                position[1] = scale * j;
                position[2] = 0;

                if (data[i, j] >= 200)
                    color = Color.DarkBlue;
                else if (data[i, j] > 100 && data[i, j] < 200)
                    color = Color.Green;
                else if (data[i, j] <= 100)
                    color = Color.Red;

                box[i][j] = new Box3D(position, 10, 10, data[i, j], color);
                Program.addBox(box[i][j].initBox3D(), box[i][j].color);

                basee[i][j] = new double[4][];
                basee[i][j][0] = new double[3];
                basee[i][j][1] = new double[3];
                basee[i][j][2] = new double[3];
                basee[i][j][3] = new double[3];

                basee[i][j][0][0] = scale * i;
                basee[i][j][0][1] = scale * j;
                basee[i][j][0][2] = 0;

                basee[i][j][1][0] = scale * (i + 1);
                basee[i][j][1][1] = scale * j;
                basee[i][j][1][2] = 0;

                basee[i][j][2][0] = scale * (i + 1);
                basee[i][j][2][1] = scale * (j + 1);
                basee[i][j][2][2] = 0;

                basee[i][j][3][0] = scale * i;
                basee[i][j][3][1] = scale * (j + 1);
                basee[i][j][3][2] = 0;

                Program.addPlane(basee[i][j], Color.LightCyan);
            }
        }
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


/// <summary>
/// Summary description for BarGraph
/// </summary>
public class BarGraph
{
    Box3D[][] box = new Box3D[5][];//5
    int xNumber, yNumber;
    double scale;
    int initPoint;
    Color color;

    
    public double[,] data = new double[,]
    {
            {0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0}
    };
     

    public BarGraph(int xNumber, int yNumber, double scale)
    {
        this.xNumber = xNumber;
        this.yNumber = yNumber;
        this.scale = scale;
    }

    public void initBarGraph()
    {
        this.initPoint = Program.pointCount;

        double[] position = new double[3];

        double[][][][] basee = new double[5][][][];

        int i = 0;
        /* Data Fetching */

        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        DataTable dt = new DataTable();

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM Bar_Graph";

        con.Open();

        reader = cmd.ExecuteReader();

        dt.Load(reader);

        int team_id = 0;
        int batsman_position = 0;
        int runs = 0;

        
        int j ;
        for (j = 0; j < dt.Rows.Count; j++ )
        {
            team_id = Convert.ToInt32(dt.Rows[j][0].ToString());
            batsman_position = Convert.ToInt32(dt.Rows[j][1].ToString());
            runs = Convert.ToInt32(dt.Rows[j][2].ToString());

            team_id--;
            batsman_position--;

            data[team_id, batsman_position] = runs;
        }

        con.Close();
        reader.Close();
    

        // End Fetching
        for (i = 0; i < 5; i++)
        {
            box[i] = new Box3D[12];

            basee[i] = new double[6][][];

            for (j = 0; j < 5; j++)
            {
                position[0] = scale * i;
                position[1] = scale * j;
                position[2] = 0;

                if (data[i, j] >= 200)
                    color = Color.DarkBlue;
                else if (data[i, j] > 100 && data[i, j] < 200)
                    color = Color.Green;
                else if (data[i, j] <= 100)
                    color = Color.Red;

                box[i][j] = new Box3D(position, 10, 10, data[i, j], color);
                Program.addBox(box[i][j].initBox3D(), box[i][j].color);

                basee[i][j] = new double[4][];
                basee[i][j][0] = new double[3];
                basee[i][j][1] = new double[3];
                basee[i][j][2] = new double[3];
                basee[i][j][3] = new double[3];

                basee[i][j][0][0] = scale * i;
                basee[i][j][0][1] = scale * j;
                basee[i][j][0][2] = 0;

                basee[i][j][1][0] = scale * (i + 1);
                basee[i][j][1][1] = scale * j;
                basee[i][j][1][2] = 0;

                basee[i][j][2][0] = scale * (i + 1);
                basee[i][j][2][1] = scale * (j + 1);
                basee[i][j][2][2] = 0;

                basee[i][j][3][0] = scale * i;
                basee[i][j][3][1] = scale * (j + 1);
                basee[i][j][3][2] = 0;

                Program.addPlane(basee[i][j], Color.LightCyan);
            }
        }
    }
>>>>>>> 9fe402ac798ef393c9b39749c089c6f74b01984d
}