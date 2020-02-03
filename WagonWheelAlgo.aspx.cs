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
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Text;

public partial class WagonWheelAlgo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Program.v = new double[900000][];    //2
        Program.p_color = new Color[900000];

        Program.p = new double[900000][]; //3

        Program.COM = new double[900000][];//3
        Program.comDist = new double[900000][];//2
        Program.pointCount = 0;

        Program.succ_plane = 0;//4c    

        //Program.dirTheta = 1;
        //Program.dirPhi = 110;
        //Program.dirRadii = 10000;
        Program.dTheta = 5;
        Program.dPhi = 5;
        Program.dRadii = 1000;
        Program.planeCount = 0;
        ///////
        /* Data base Main */
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        DataTable dt = new DataTable();

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM Ball_Batsman";

        con.Open();

        try
        {
            reader = cmd.ExecuteReader();
            dt.Load(reader);

            double vel = new double();
            double theta_1 = new double();
            double phi_1 = new double();
            double endP = new double();
            
            int j = 0;
            WagonWheel[] ww = new WagonWheel[6];
            for (j = 0; j < 6; j++)
            {
                vel = Convert.ToDouble(dt.Rows[j][4].ToString());
                phi_1 = Convert.ToDouble(dt.Rows[j][3].ToString());
                theta_1 = Convert.ToDouble(dt.Rows[j][2].ToString());
                endP = Convert.ToDouble(dt.Rows[j][5].ToString());

                //Response.Write(vel);
                    ww[j] = new WagonWheel(vel, theta_1, phi_1, endP);
                    ww[j].initTrajectory();
            }
            /*WagonWheel[] ww = new WagonWheel[6];
            ww[0] = new WagonWheel(120, -20, 30, 4020, Color.Blue);
            ww[1] = new WagonWheel(220, 40, 40, 9000, Color.Yellow);
            ww[2] = new WagonWheel(120, -40, 50, 3000, Color.Blue);
            ww[3] = new WagonWheel(160, 160, 40, 7500, Color.Yellow);
            ww[4] = new WagonWheel(300, 50, 50, 9000, Color.Red);
            ww[5] = new WagonWheel(320, -60, 60, 9000, Color.Red);
            */
            for (int mn = 0; mn < 6; mn++)
                ww[mn].initTrajectory();

            con.Close();
            reader.Close();
        }
        catch (Exception e2)
        {
            Response.Write(e2.Message);
        }

        // End Main Database

        int i;
        int[][][] rect = new int[900000][][];//4,2
        Color[] rect_color = new Color[1000];

        Program.a = new double[3];// { -60.1, 2200.1, 150 };//  -20000.1, 1000.1, 6000   //0.1, 2200.1, 50 //-800.1, 2000.1, 60
        Program.d = new double[3];// { 0.001, -1.0001, 0.0001 };
        //-2000.1, 1000.1, 1050
        /*
        Program.d[0] = Program.dirRadii * Math.Sin(Program.toRadians(Program.dirPhi)) * Math.Cos(Program.toRadians(Program.dirTheta));    // sinphi costheta
        Program.d[1] = Program.dirRadii * Math.Sin(Program.toRadians(Program.dirPhi)) * Math.Sin(Program.toRadians(Program.dirTheta));    // sinphi sintheta
        Program.d[2] = Program.dirRadii * Math.Cos(Program.toRadians(Program.dirPhi)); // cosphi

        Program.a[0] = -Program.d[0];
        Program.a[1] = -Program.d[1] + 1000;
        Program.a[2] = -Program.d[2];
        */
        /* Database */
        con.Close();

        //SqlConnection con = new SqlConnection();
        //SqlDataReader reader;
        SqlCommand cmdtemp = new SqlCommand();
        DataTable dttemp = new DataTable();

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        cmdtemp.Connection = con;
        cmdtemp.CommandText = "SELECT * FROM TEMP3";

        con.Open();
        try
        {
            reader = cmdtemp.ExecuteReader();
            dttemp.Load(reader);
        }
        catch (Exception e2)
        {
            Response.Write(e2.Message);
        }

        double[] rtp = new double[3];

        rtp[0] = Convert.ToDouble(dttemp.Rows[0][1].ToString());
        rtp[1] = Convert.ToDouble(dttemp.Rows[0][2].ToString());
        rtp[2] = Convert.ToDouble(dttemp.Rows[0][3].ToString());

        //}
        //catch (Exception ex)
        //{
        //}

        con.Close();
        //reader.Close();
         
        // End Database Code

        //-2000.1, 1000.1, 1050

        Program.dirRadii = rtp[0];
        Program.dirTheta = rtp[1];
        Program.dirPhi = rtp[2];

        Program.d[0] = Program.dirRadii * Math.Sin(Program.toRadians(Program.dirPhi)) * Math.Cos(Program.toRadians(Program.dirTheta));    // sinphi costheta
        Program.d[1] = Program.dirRadii * Math.Sin(Program.toRadians(Program.dirPhi)) * Math.Sin(Program.toRadians(Program.dirTheta));    // sinphi sintheta
        Program.d[2] = Program.dirRadii * Math.Cos(Program.toRadians(Program.dirPhi)); // cosphi

        Program.a[0] = -Program.d[0];
        Program.a[1] = -Program.d[1] + 1000;
        Program.a[2] = -Program.d[2];


        ///////////////////////////////////
        
        Response.Clear();
        int h = 1080;
        int w = 1920;

        Bitmap bmp = new Bitmap(w, h, PixelFormat.Format24bppRgb);
        Graphics g = Graphics.FromImage(bmp);
        SolidBrush sb = new SolidBrush(Color.Cyan);
        Pen pen = new Pen(Color.Black);
        Pen p = new Pen(Color.Red, 5);

        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = TextRenderingHint.SystemDefault;
        g.Clear(Color.White);
        g.DrawRectangle(Pens.Black, 0, 0, 1920, 1080);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////

        PartialSphere boundary = new PartialSphere(8000, new double[] { 0, 1000, -15 }, 200, 100, 89.999, 90, 0, 360, Color.Yellow);
        PartialSphere boards = new PartialSphere(8300, new double[] { 0, 1000, -15 }, 80, 100, 89, 90, 0, 360, Color.LightBlue);
        PartialSphere stadium = new PartialSphere(20000, new double[] { 0, 1000, 18000 }, 100, 100, 135, 150, 0, 360, Color.Cyan);

        Plane3D pitch = new Plane3D(new double[][] {
            new double[] { -200,0,-15 },
            new double[] { -200,2000,-15 },
            new double[] { 200,2000,-15 },
            new double[] { 200,0,-15 }    }, Color.LightGray);

        Box3D batStump1 = new Box3D(new double[] { -11, 1990, -15 }, 4.4, 4.4, 71, Color.Gray);
        Box3D batStump2 = new Box3D(new double[] { 0, 1990, -15 }, 4.4, 4.4, 71, Color.Gray);
        Box3D batStump3 = new Box3D(new double[] { 11, 1990, -15 }, 4.4, 4.4, 71, Color.Gray);

        Box3D ballStump1 = new Box3D(new double[] { -11, 0, -15 }, 4.4, 4.4, 71, Color.Gray);
        Box3D ballStump2 = new Box3D(new double[] { 0, 0, -15 }, 4.4, 4.4, 71, Color.Gray);
        Box3D ballStump3 = new Box3D(new double[] { 11, 0, -15 }, 4.4, 4.4, 71, Color.Gray);

        /*WagonWheel[] ww = new WagonWheel[6];
        ww[0] = new WagonWheel(120, -20, 30, 4020, Color.Blue);
        ww[1] = new WagonWheel(220, 40, 40, 9000, Color.Yellow);
        ww[2] = new WagonWheel(120, -40, 50, 3000, Color.Blue);
        ww[3] = new WagonWheel(160, 160, 40, 7500, Color.Yellow);
        ww[4] = new WagonWheel(300, 50, 50, 9000, Color.Red);
        ww[5] = new WagonWheel(320, -60, 60, 9000, Color.Red);
        */
        BarGraph br = new BarGraph(5, 5, 100);
        MathGraph mg = new MathGraph(1000, 1000, 40);

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        boundary.initPartialSphere();
        boards.initPartialSphere();
        stadium.initPartialSphere();
        //Program.addPlane(pitch.initPlane3D(), Color.Gray);
        Program.makePitch();

        Program.addBox(batStump1.initBox3D(), batStump1.color);
        Program.addBox(batStump2.initBox3D(), batStump2.color);
        Program.addBox(batStump3.initBox3D(), batStump3.color);
        Program.addBox(ballStump1.initBox3D(), ballStump1.color);
        Program.addBox(ballStump2.initBox3D(), ballStump1.color);
        Program.addBox(ballStump3.initBox3D(), ballStump1.color);

        //for (i = 0; i < 6; i++)
        //hawk[i].initTrajectory();

        // for (i = 0; i < 6; i++)
        // ww[i].initTrajectory();

        //br.initBarGraph();
        //mg.initMathGraph();       


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Program.trans();

        int m;
        for (int t = 0; t < Program.succ_plane; t++)
        {
            rect[t] = new int[4][];
            rect[t][0] = new int[2];
            rect[t][1] = new int[2];
            rect[t][2] = new int[2];
            rect[t][3] = new int[2];

            m = t * 4;

            rect[t][0][0] = (int)Program.v[m][0]; rect[t][0][1] = (int)Program.v[m][1];
            rect[t][1][0] = (int)Program.v[m + 1][0]; rect[t][1][1] = (int)Program.v[m + 1][1];
            rect[t][2][0] = (int)Program.v[m + 2][0]; rect[t][2][1] = (int)Program.v[m + 2][1];
            rect[t][3][0] = (int)Program.v[m + 3][0]; rect[t][3][1] = (int)Program.v[m + 3][1];
        }

        Program.preCalc();

        for (int z = 0; z < Program.succ_plane; z++)
        {
            int x = (int)Program.comDist[z][1];

            if ((rect[x][0][0] == rect[x][1][0]) &&
                (rect[x][0][1] == rect[x][1][1]) &&
                (rect[x][2][0] == rect[x][3][0]) &&
                (rect[x][2][1] == rect[x][3][1]))
            {
                g.DrawLine(new Pen(Program.p_color[x], 5), rect[x][0][0], rect[x][0][1], rect[x][2][0], rect[x][2][1]);
            }
            else
            {

                g.FillPolygon(new SolidBrush(Program.p_color[x]), new Point[] { new Point(rect[x][0][0], rect[x][0][1]), 
                new Point(rect[x][1][0], rect[x][1][1]), 
                new Point(rect[x][2][0], rect[x][2][1]),
                new Point(rect[x][3][0], rect[x][3][1])   });

                g.DrawPolygon(pen, new Point[] { new Point(rect[x][0][0], rect[x][0][1]), 
                new Point(rect[x][1][0], rect[x][1][1]), 
                new Point(rect[x][2][0], rect[x][2][1]),
                new Point(rect[x][3][0], rect[x][3][1])   });
            }
        }

        //New Data
        con.Close();

        var bit = 0;

        // SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt3 = new DataTable();

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        cmd.Connection = con;

        cmd.CommandText = "Select * from Save_image where id = 3";

        cmd.CommandType = CommandType.Text;

        da.SelectCommand = cmd;

        da.Fill(dt3);

        bit = Convert.ToInt32(dt3.Rows[0].ItemArray[1]);
        con.Open();
        try
        {
            cmd.ExecuteNonQuery();
        }
        catch (Exception ee)
        {
        }
        //End Database 

        if (bit == 1)
        {
            const string imagePath = @"C:\Users\PM\Downloads\WagonwheelGraph.jpg";
            var imageobject = bmp;
            //imageobject.Save(imagePath, ImageFormat.Jpeg);
        }
        //End New Data

        bmp.Save(Response.OutputStream, ImageFormat.Jpeg);
        g.Dispose();
        bmp.Dispose();

    }
}