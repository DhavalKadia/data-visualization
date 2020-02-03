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

/// <summary>
/// Summary description for MagneticField
/// </summary>
public class MagneticField
{
        Plane3D[] shell = new Plane3D[200000];
        double[][] path = new double[200000][];//3
        int index = 0, number, space;
        double acc;

        public MagneticField(int number, int space, double acc)
        {
            this.number = number;
            this.space = space;
            this.acc = acc;
        }

        public void initMagneticField()
        {
            int count = 0, count_shell = 0;
            double x;

            for (double t = -1.47; t < 1.57 - 0.018 / acc; t += 0.018 / acc)
                count++;

            for (double theta = 0; theta < 360; theta += 15)
                for (x = -1.47; x < 1.57 - 0.018 / acc; x += 0.018 / acc)
                    count_shell++;


            //count_shell /= 24;

            double[][] segment = new double[4][];
            int start;
            Color c = Color.Cyan;

            for (int n = 1; n <= number; n++)
            {
                if (n == 1)
                    c = Color.DarkBlue;
                else if (n == 2)
                    c = Color.MediumBlue;
                else if (n == 3)
                    c = Color.SlateBlue;
                else if (n == 4)
                    c = Color.CornflowerBlue;
                else if (n == 5)
                    c = Color.SkyBlue;
                else if (n == 6)
                    c = Color.LightSkyBlue;
                else if (n == 7)
                    c = Color.LightBlue;

                start = index;

                for (double theta = 0; theta < 360; theta += 15)
                    for (x = -1.47; x < 1.57 - 0.018 / acc; x += 0.018 / acc)
                    {
                        path[index] = new double[3];
                        path[index][0] = n * space * Math.Sin(1.57 + x) * Math.Cos(Program.toRadians(theta)) * Math.Abs((Math.Pow(1.57, 5) - Math.Abs(Math.Pow(x, 5))) * Math.Cos(0.4 * x));
                        path[index][1] = n * space * Math.Sin(Program.toRadians(theta)) * Math.Abs((Math.Pow(1.57, 5) - Math.Abs(Math.Pow(x, 5))) * Math.Cos(0.4 * x));
                        path[index][2] = -n * space * Math.Cos(1.57 + x) * Math.Abs((Math.Pow(1.57, 5) - Math.Abs(Math.Pow(x, 5))) * Math.Cos(0.4 * x));

                        index++;
                    }

                for (int w = start; w < start + count_shell - 1; w++)
                {
                    segment[0] = new double[3];
                    segment[1] = new double[3];
                    segment[2] = new double[3];
                    segment[3] = new double[3];

                    segment[0][0] = segment[1][0] = path[w][0];
                    segment[0][1] = segment[1][1] = path[w][1];
                    segment[0][2] = segment[1][2] = path[w][2];

                    segment[2][0] = segment[3][0] = path[w + 1][0];
                    segment[2][1] = segment[3][1] = path[w + 1][1];
                    segment[2][2] = segment[3][2] = path[w + 1][2];

                    shell[w] = new Plane3D(segment, c);
                    Program.addPlane(shell[w].initPlane3D(), shell[w].color);

                }
            }
            //( Math.Abs((Math.Pow(1.57, 5) - Math.Abs(Math.Pow(x, 5)))*Math.Cos(0.4*x)))

            /////////////////////////////////////////////////////////////////////////
            /*
            double[][] segment = new double[4][];

            Color c = Color.Cyan;

            for (int w = 0; w < index - 1; w++)
            {
                segment[0] = new double[3];
                segment[1] = new double[3];
                segment[2] = new double[3];
                segment[3] = new double[3];

                segment[0][0] = segment[1][0] = path[w][0];
                segment[0][1] = segment[1][1] = path[w][1];
                segment[0][2] = segment[1][2] = path[w][2];

                segment[2][0] = segment[3][0] = path[w + 1][0];
                segment[2][1] = segment[3][1] = path[w + 1][1];
                segment[2][2] = segment[3][2] = path[w + 1][2];
            
                shell[w] = new Plane3D(segment, c);
                Program.addPlane(shell[w].initPlane3D(), shell[w].color);

                if (w % count == (count - 2))
                    w++;

                if (w % count_shell == (count_shell-1))
                    c = Color.Red;
            }*/

        }

        public void seek(double[] move)
        {
            int i, j;

            for (i = 0; i < index - 1; i++)
                shell[i].seek(move);
        }

        public void rotateX(double degree, double[] COM)
        {
            int i, j;

            for (i = 0; i < index - 1; i++)
                shell[i].rotateX(degree, COM);
        }

        public void rotateY(double degree, double[] COM)
        {
            int i, j;

            for (i = 0; i < index - 1; i++)
                shell[i].rotateY(degree, COM);
        }

        public void rotateZ(double degree, double[] COM)
        {
            int i, j;

            for (i = 0; i < index - 1; i++)
                shell[i].rotateZ(degree, COM);
        }
    
}