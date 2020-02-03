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


/// <summary>
/// Summary description for Plane3D
/// </summary>
public class Plane3D	//////////////////////////////////////////////////	Plane
{
    double[][] plane3d = new double[4][];//3
    int initPoint;
    public Color color;

    public Plane3D(double[][] plane3d, Color color)
    {
        this.plane3d = plane3d;
        this.color = color;
    }


    public static double toRadians(double angle)
    {
        return 3.14 * angle / 180.0;
    }

    public static double toDegrees(double angle)
    {
        return angle * (180.0 / 3.14);
    }

    public double[][] initPlane3D()
    {
        initPoint = Program.pointCount;

        return this.plane3d;
    }

    public double[][] getPlane3D()
    {
        return this.plane3d;
    }

    public void seek(double[] move)
    {
        for (int i = 0; i < 4; i++)
        {
            plane3d[i][0] += move[0];
            plane3d[i][0] += move[0];
            plane3d[i][0] += move[0];

            plane3d[i][1] += move[1];
            plane3d[i][1] += move[1];
            plane3d[i][1] += move[1];

            plane3d[i][2] += move[2];
            plane3d[i][2] += move[2];
            plane3d[i][2] += move[2];
        }
    }

    double[] getCorner(int n)
    {
        return plane3d[n];
    }

    double[] getCOM()
    {
        double[] COM = new double[3];

        COM[0] = (plane3d[0][0] + plane3d[1][0] + plane3d[2][0] + plane3d[3][0]) / 4;
        COM[1] = (plane3d[0][1] + plane3d[1][1] + plane3d[2][1] + plane3d[3][1]) / 4;
        COM[2] = (plane3d[0][2] + plane3d[1][2] + plane3d[2][2] + plane3d[3][2]) / 4;

        return COM;
    }

    public void rotateX(double degree, double[] COM)
    {
        int i;

        double[][] relV = new double[4][];//3 //4c

        for (i = 0; i < 4; i++)
        {
            relV[i] = new double[3];
            relV[i][0] = plane3d[i][0] - COM[0];
            relV[i][1] = plane3d[i][1] - COM[1];
            relV[i][2] = plane3d[i][2] - COM[2];
        }

        double radian = toRadians(degree);

        double[][] relNewV = new double[4][];//3 //4 

        for (i = 0; i < 4; i++)
        {
            relNewV[i] = new double[3];
            relNewV[i][1] = relV[i][1] * Math.Cos(radian) - relV[i][2] * Math.Sin(radian);
            relNewV[i][2] = relV[i][1] * Math.Sin(radian) + relV[i][2] * Math.Cos(radian);
            relNewV[i][0] = relV[i][0];
        }

        for (i = 0; i < 4; i++)//4c
        {
            plane3d[i][0] = COM[0] + relNewV[i][0];
            plane3d[i][1] = COM[1] + relNewV[i][1];
            plane3d[i][2] = COM[2] + relNewV[i][2];
        }
    }

    public void rotateY(double degree, double[] COM)
    {
        int i;

        double[][] relV = new double[4][];//3

        for (i = 0; i < 4; i++)
        {
            relV[i] = new double[3];
            relV[i][0] = plane3d[i][0] - COM[0];
            relV[i][1] = plane3d[i][1] - COM[1];
            relV[i][2] = plane3d[i][2] - COM[2];
        }

        double radian = toRadians(degree);

        double[][] relNewV = new double[4][];//3

        for (i = 0; i < 4; i++)
        {
            relNewV[i] = new double[3];
            relNewV[i][2] = relV[i][2] * Math.Cos(radian) - relV[i][0] * Math.Sin(radian);
            relNewV[i][0] = relV[i][2] * Math.Sin(radian) + relV[i][0] * Math.Cos(radian);
            relNewV[i][1] = relV[i][1];
        }

        for (i = 0; i < 4; i++)
        {
            plane3d[i][0] = COM[0] + relNewV[i][0];
            plane3d[i][1] = COM[1] + relNewV[i][1];
            plane3d[i][2] = COM[2] + relNewV[i][2];
        }
    }

    public void rotateZ(double degree, double[] COM)
    {
        int i;

        double[][] relV = new double[4][];//3

        for (i = 0; i < 4; i++)
        {
            relV[i] = new double[3];
            relV[i][0] = plane3d[i][0] - COM[0];
            relV[i][1] = plane3d[i][1] - COM[1];
            relV[i][2] = plane3d[i][2] - COM[2];
        }

        double radian = toRadians(degree);

        double[][] relNewV = new double[4][];//3

        for (i = 0; i < 4; i++)
        {
            relNewV[i] = new double[3];
            relNewV[i][0] = relV[i][0] * Math.Cos(radian) - relV[i][1] * Math.Sin(radian);
            relNewV[i][1] = relV[i][0] * Math.Sin(radian) + relV[i][1] * Math.Cos(radian);
            relNewV[i][2] = relV[i][2];
        }

        for (i = 0; i < 4; i++)
        {
            plane3d[i][0] = COM[0] + relNewV[i][0];
            plane3d[i][1] = COM[1] + relNewV[i][1];
            plane3d[i][2] = COM[2] + relNewV[i][2];
        }
    }


}