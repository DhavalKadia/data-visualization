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

public partial class Scientific_MathAlgo : System.Web.UI.Page
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

        Program.dirTheta = 1;
        Program.dirPhi = 110;
        Program.dirRadii = 1000;
        Program.dTheta = 15;
        Program.dPhi = 15;
        Program.dRadii = 100;
        Program.planeCount = 0;

        ///////////////////////////////////////////

        int[][][] rect = new int[900000][][];//4,2
        Color[] rect_color = new Color[1000];

        Program.a = new double[3];// { -200.1, 0.1, 150 };//  -20000.1, 1000.1, 6000   //0.1, 2200.1, 50 //-800.1, 2000.1, 60
        Program.d = new double[3];// { 1.001, 0.0001, 0.0001 };


        /* Database */

        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        DataTable dt = new DataTable();

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM TEMP2";

        con.Open();

        reader = cmd.ExecuteReader();
        dt.Load(reader);

        double[] rtp = new double[3];

        rtp[0] = Convert.ToDouble(dt.Rows[0][1].ToString());
        rtp[1] = Convert.ToDouble(dt.Rows[0][2].ToString());
        rtp[2] = Convert.ToDouble(dt.Rows[0][3].ToString());

        //}
        //catch (Exception ex)
        //{
        //}

        con.Close();
        //reader.Close();

        // End Database Code

        //-2000.1, 1000.1, 1050

        //Program.dirRadii = rtp[0];
        //Program.dirTheta = rtp[1];
        //Program.dirPhi = rtp[2];
        Program.dirRadii = 40;
        Program.dirTheta = 0;
        Program.dirPhi = 120;

        Program.d[0] = Program.dirRadii * Math.Sin(Program.toRadians(Program.dirPhi)) * Math.Cos(Program.toRadians(Program.dirTheta));    // sinphi costheta
        Program.d[1] = Program.dirRadii * Math.Sin(Program.toRadians(Program.dirPhi)) * Math.Sin(Program.toRadians(Program.dirTheta));    // sinphi sintheta
        Program.d[2] = Program.dirRadii * Math.Cos(Program.toRadians(Program.dirPhi)); // cosphi

        Program.a[0] = -Program.d[0];
        Program.a[1] = -Program.d[1] + 1;
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

        g.TextRenderingHint = TextRenderingHint.SystemDefault;
        g.Clear(Color.White);
        g.DrawRectangle(Pens.Black, 0, 0, 1920, 1080);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////

        MathGraph mg = new MathGraph(20, 20, 0.5);   //(1000, 1000, 40)

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        mg.initMathGraph();       
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

        bmp.Save(Response.OutputStream, ImageFormat.Jpeg);
        g.Dispose();
        bmp.Dispose();
    }
}