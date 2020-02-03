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

public partial class BiologyAlgo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Program.dirTheta = 0; Program.dirPhi = 90; Program.dirRadii = 300;
        Program.dTheta = 5; Program.dPhi = 5; Program.dRadii = 1000;
        int i, j;

        Program.v = new double[900000][];    //2
        Program.p_color = new Color[900000];

        Program.p = new double[900000][]; //3

        Program.COM = new double[900000][];//3
        Program.comDist = new double[900000][];//2

        Program.pointCount = 0;

        Program.succ_plane = 0;//4c    

        Program.planeCount = 0;

        // Main Database

        double[] d = new double[3];

        /* Database */
        SqlConnection con = new SqlConnection();
        SqlDataReader reader;
        SqlCommand cmdtemp = new SqlCommand();
        DataTable dttemp = new DataTable();

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        cmdtemp.Connection = con;
        cmdtemp.CommandText = "SELECT * FROM DNA";

        con.Open();
        try
        {
            reader = cmdtemp.ExecuteReader();
            dttemp.Load(reader);
        }
        catch (Exception e1)
        {
            Response.Write(e1.Message);
        }

        string genome;

        genome = dttemp.Rows[0][1].ToString();
        
        con.Close();
        /////////
        int[][][] rect = new int[900000][][];//4,2
        Color[] rect_color = new Color[10000000];
        
        //Population dirRadii = 2600

        Program.a = new double[] { -60.1, 2200.1, 150 };//  -20000.1, 1000.1, 6000   //0.1, 2200.1, 50 //-800.1, 2000.1, 60
        Program.d = new double[] { 0.001, -1.0001, 0.0001 };
        //-2000.1, 1000.1, 1050

        Program.d[0] = Program.dirRadii * Math.Sin(Program.toRadians(Program.dirPhi)) * Math.Cos(Program.toRadians(Program.dirTheta));    // sinphi costheta
        Program.d[1] = Program.dirRadii * Math.Sin(Program.toRadians(Program.dirPhi)) * Math.Sin(Program.toRadians(Program.dirTheta));    // sinphi sintheta
        Program.d[2] = Program.dirRadii * Math.Cos(Program.toRadians(Program.dirPhi)); // cosphi

        Program.a[0] = -Program.d[0];
        Program.a[1] = -Program.d[1];
        Program.a[2] = -Program.d[2];

        Response.Clear();
        int h = 1080;
        int w = 1920;

        Bitmap bmp = new Bitmap(w, h, PixelFormat.Format24bppRgb);
        Graphics g = Graphics.FromImage(bmp);
        SolidBrush sb = new SolidBrush(Color.Cyan);
        Pen pen = new Pen(Color.DarkCyan);
        Pen p = new Pen(Color.Red, 5);
        //PaleTurquoise for Population

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

        //BiologyDNA dna1 = new BiologyDNA("ATCGGCAATCGTACATCGCATGGCAATCTCG");
        BiologyDNA dna1 = new BiologyDNA(genome);

        //boundary.initPartialSphere();
        //boards.initPartialSphere();
        //stadium.initPartialSphere();
        //Program.addPlane(pitch.initPlane3D(), Color.Gray);
        //Program.makePitch();
        /*
        Program.addBox(batStump1.initBox3D(), batStump1.color);
        Program.addBox(batStump2.initBox3D(), batStump2.color);
        Program.addBox(batStump3.initBox3D(), batStump3.color);
        Program.addBox(ballStump1.initBox3D(), ballStump1.color);
        Program.addBox(ballStump2.initBox3D(), ballStump1.color);
        Program.addBox(ballStump3.initBox3D(), ballStump1.color);
        */
        //for (i = 0; i < 6; i++)
        //    hawk[i].initTrajectory();

        //for (i = 0; i < 6; i++)
        //    ww[i].initTrajectory();

        //br.initBarGraph();
        //mg.initMathGraph();       
        //mf.initMagneticField();
        //earth.initPopulationEarth();
        //earth2.initPopulationEarth2();
        dna1.initDNA();

        //for (i = 0; i < 6; i++)
        //    bh[i].initBeehive();

        //fv.initFieldView();
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

            if (Program.p_color[x] == Color.Azure)
            {
                Pen myPen = new Pen(Color.Firebrick, 5);
                Rectangle myRectangle = new Rectangle(rect[x][0][0], rect[x][0][1], 10, 2);
                g.DrawEllipse(myPen, myRectangle);
            }
            else if (Program.p_color[x] == Color.Snow)
            {
                Pen myPen = new Pen(Color.DeepSkyBlue, 5);
                Rectangle myRectangle = new Rectangle(rect[x][0][0], rect[x][0][1], 20, 4);
                g.DrawEllipse(myPen, myRectangle);
            }
            else if ((rect[x][0][0] == rect[x][1][0]) &&
                (rect[x][0][1] == rect[x][1][1]) &&
                (rect[x][2][0] == rect[x][3][0]) &&
                (rect[x][2][1] == rect[x][3][1]))
            {
                g.DrawLine(new Pen(Program.p_color[x], 2), rect[x][0][0], rect[x][0][1], rect[x][2][0], rect[x][2][1]);
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

        bmp.Save(Response.OutputStream, ImageFormat.Jpeg);
        g.Dispose();
        bmp.Dispose();
    }
}