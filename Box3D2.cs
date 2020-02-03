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
/// Summary description for Box3D2
/// </summary>
public class Box3D2	//////////////////////////////////////////////////	BOX
{
    double[][] box3D = new double[8][];//3
    int initPoint;
    public Color color;

    public Box3D2(double[][] box3D, Color color)
    {
        this.box3D = box3D;
        this.color = color;
    }

    public static double toRadians(double angle)
    {
        return 3.14 * angle / 180.0;
    }

    public double[][] initBox3D()
    {
        initPoint = Program.pointCount;

        return this.box3D;
    }

    double[][] getBox3D()
    {
        return this.box3D;
    }

    void seek(double[] move)
    {
        for (int i = 0; i < 8; i++)
        {
            box3D[i][0] += move[0];
            box3D[i][0] += move[0];
            box3D[i][0] += move[0];

            box3D[i][1] += move[1];
            box3D[i][1] += move[1];
            box3D[i][1] += move[1];

            box3D[i][2] += move[2];
            box3D[i][2] += move[2];
            box3D[i][2] += move[2];
        }
    }

    double[] getCorner(int n)
    {
        return box3D[n];
    }

    double[] getCOM()
    {
        double[] COM = new double[3];

        COM[0] = (box3D[0][0] + box3D[1][0] + box3D[2][0] + box3D[3][0] + box3D[4][0] + box3D[5][0] + box3D[6][0] + box3D[7][0]) / 8;
        COM[1] = (box3D[0][1] + box3D[1][1] + box3D[2][1] + box3D[3][1] + box3D[4][1] + box3D[5][1] + box3D[6][1] + box3D[7][1]) / 8;
        COM[2] = (box3D[0][2] + box3D[1][2] + box3D[2][2] + box3D[3][2] + box3D[4][2] + box3D[5][2] + box3D[6][2] + box3D[7][2]) / 8;

        return COM;
    }

    void rotateX(double degree, double[] COM)
    {
        int i;

        double[][] relV = new double[8][];//3

        for (i = 0; i < 8; i++)
        {
            relV[i][0] = box3D[i][0] - COM[0];
            relV[i][1] = box3D[i][1] - COM[1];
            relV[i][2] = box3D[i][2] - COM[2];
        }

        double radian = toRadians(degree);

        double[][] relNewV = new double[8][];//3

        for (i = 0; i < 8; i++)
        {
            relNewV[i][1] = relV[i][1] * Math.Cos(radian) - relV[i][2] * Math.Sin(radian);
            relNewV[i][2] = relV[i][1] * Math.Sin(radian) + relV[i][2] * Math.Cos(radian);
            relNewV[i][0] = relV[i][0];
        }

        for (i = 0; i < 8; i++)
        {
            box3D[i][0] = COM[0] + relNewV[i][0];
            box3D[i][1] = COM[1] + relNewV[i][1];
            box3D[i][2] = COM[2] + relNewV[i][2];
        }
    }

    void rotateY(double degree, double[] COM)
    {
        int i;

        double[][] relV = new double[8][];//3

        for (i = 0; i < 8; i++)
        {
            relV[i][0] = box3D[i][0] - COM[0];
            relV[i][1] = box3D[i][1] - COM[1];
            relV[i][2] = box3D[i][2] - COM[2];
        }

        double radian = toRadians(degree);

        double[][] relNewV = new double[8][];//3

        for (i = 0; i < 8; i++)
        {
            relNewV[i][2] = relV[i][2] * Math.Cos(radian) - relV[i][0] * Math.Sin(radian);
            relNewV[i][0] = relV[i][2] * Math.Sin(radian) + relV[i][0] * Math.Cos(radian);
            relNewV[i][1] = relV[i][1];
        }

        for (i = 0; i < 8; i++)
        {
            box3D[i][0] = COM[0] + relNewV[i][0];
            box3D[i][1] = COM[1] + relNewV[i][1];
            box3D[i][2] = COM[2] + relNewV[i][2];
        }
    }

    void rotateZ(double degree, double[] COM)
    {
        int i;

        double[][] relV = new double[8][];//3

        for (i = 0; i < 8; i++)
        {
            relV[i][0] = box3D[i][0] - COM[0];
            relV[i][1] = box3D[i][1] - COM[1];
            relV[i][2] = box3D[i][2] - COM[2];
        }

        double radian = toRadians(degree);

        double[][] relNewV = new double[8][];//3

        for (i = 0; i < 8; i++)
        {
            relNewV[i][0] = relV[i][0] * Math.Cos(radian) - relV[i][1] * Math.Sin(radian);
            relNewV[i][1] = relV[i][0] * Math.Sin(radian) + relV[i][1] * Math.Cos(radian);
            relNewV[i][2] = relV[i][2];
        }

        for (i = 0; i < 8; i++)
        {
            box3D[i][0] = COM[0] + relNewV[i][0];
            box3D[i][1] = COM[1] + relNewV[i][1];
            box3D[i][2] = COM[2] + relNewV[i][2];
        }

    }
}