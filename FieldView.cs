using System;
using System.IO;
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

public class FieldView
{
    Plane3D[] team = new Plane3D[11];

    double[,] position; /*= new double[,]
    {
            {2000, 90},
            {3000, 80},
            {2000, 60},
            {5000, 0},
            {7500, 30},
            {7500, 180},//
            {3000, 220},
            {7000, 250},
            {2000, 270},
            {7600, 310},
            {2000, 350}
    };*/

        public Color color;

        public FieldView(double[,] d)
        {
            this.position = d;
        }

        public void initFieldView()
        {
            double[][] segment = new double[4][];

            for (int w = 0; w < 11; w++)
            {
                segment[0] = new double[3];
                segment[1] = new double[3];
                segment[2] = new double[3];
                segment[3] = new double[3];

                segment[0][0] = segment[1][0] = segment[2][0] = segment[3][0] = position[w, 0] * Math.Cos(Program.toRadians(position[w, 1]));
                segment[0][1] = segment[1][1] = segment[2][1] = segment[3][1] = position[w, 0] * Math.Sin(Program.toRadians(position[w, 1])) + 1000;
                segment[0][2] = segment[1][2] = segment[2][2] = segment[3][2] = -15;

                team[w] = new Plane3D(segment, color);

                Program.addPlane(team[w].initPlane3D(), Color.Snow);
            }
        }
        public static double toRadians(double angle)
        {
            return 3.14 * angle / 180.0;
        }

        public static double toDegrees(double angle)
        {
            return angle * (180.0 / 3.14);
        }

        public void seek(double[] move)
        {
            int i, j;

            for (i = 0; i < 40; i++)
                team[i].seek(move);
        }

        public void rotateX(double degree, double[] COM)
        {
            int i, j;

            for (i = 0; i < 40; i++)
                team[i].rotateX(degree, COM);
        }

        public void rotateY(double degree, double[] COM)
        {
            int i, j;

            for (i = 0; i < 40; i++)
                team[i].rotateY(degree, COM);
        }

        public void rotateZ(double degree, double[] COM)
        {
            int i, j;

            for (i = 0; i < 40; i++)
                team[i].rotateZ(degree, COM);
        }

        double[] mulScaler(double scale, double[] vector)
        {
            double[] scaledV = new double[3];

            scaledV[0] = scale * vector[0];
            scaledV[1] = scale * vector[1];
            scaledV[2] = scale * vector[2];

            return scaledV;
        }
    }