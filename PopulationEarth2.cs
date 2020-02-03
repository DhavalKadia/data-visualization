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
using System.Threading.Tasks;
using System.IO;
using System.Drawing.Drawing2D;

/// <summary>
/// Summary description for PopulationEarth2
/// </summary>
public class PopulationEarth2
{
    Plane3D[][] shell;
    double radii, hInc, vInc;
    double[] center;
    int initPoint, hDiv, vDiv;
    Color c = Color.White;

    String pString;
    List<string> pData;
    double lonMin, lonMax, latMin, latMax;

    public PopulationEarth2(double radii, double[] center, int horizontalDiv, int verticalDiv, double lonmin, double lonmax, double latmin, double latmax)
    {
        this.radii = radii;
        this.center = center;
        this.hDiv = horizontalDiv;
        this.vDiv = verticalDiv;
        shell = new Plane3D[vDiv][];//hDiv

        pString = File.ReadAllText("C:\\inetpub\\wwwroot\\Datavisu28\\js\\population.json");

        pData = pString.Split(',').ToList<string>();

/*
        lonMin = -180; lonMax = 180;
        latMin = -90; latMax = 90;

        lonMin = 67; lonMax = 97;//India
        latMin = 8; latMax = 38;

        lonMin = 73; lonMax = 136;//India
        latMin = 20; latMax = 53;

        //lonMin = -180; lonMax = 180;
        //latMin = -90; latMax = 90;

        lonMin = -30; lonMax = 55;//India
        latMin = -80; latMax = 40;

        lonMin = -180;
        lonMax = 180;
        latMin = -90;
        latMax = 90;
*/
        this.lonMin = lonmin;
        this.lonMax = lonmax;
        this.latMin = latmin;
        this.latMax = latmax;

        Program.dirTheta = (lonMin + lonMax + 0.5) / 2;
        hInc = 360 / horizontalDiv;
        vInc = 360 / verticalDiv;
    }

    public void initPopulationEarth2()
    {
        this.initPoint = Program.pointCount;

        int boxCount = 0, v;

        double[][][] bx = new double[100000][][];//8 3
        PosiBox3D[] pb = new PosiBox3D[100000];

        for (v = 0; v < 30000; v++)
        {
            bx[v] = new double[8][];

            bx[v][0] = new double[3];
            bx[v][1] = new double[3];
            bx[v][2] = new double[3];
            bx[v][3] = new double[3];
            bx[v][4] = new double[3];
            bx[v][5] = new double[3];
            bx[v][6] = new double[3];
            bx[v][7] = new double[3];
        }
        for (v = 0; v < 28000; v += 3)
        {
            if (double.Parse(pData[v + 1]) >= lonMin && double.Parse(pData[v + 1]) <= lonMax && double.Parse(pData[v]) >= latMin && double.Parse(pData[v]) <= latMax)
            {
                if (double.Parse(pData[v + 2]) > 0.07 && double.Parse(pData[v + 2]) < 0.07)
                    c = Color.Navy;
                else if (double.Parse(pData[v + 2]) > 0.05 && double.Parse(pData[v + 2]) < 0.06)
                    c = Color.MediumBlue;
                else if (double.Parse(pData[v + 2]) > 0.04 && double.Parse(pData[v + 2]) < 0.05)
                    c = Color.DarkSlateBlue;
                else if (double.Parse(pData[v + 2]) > 0.03 && double.Parse(pData[v + 2]) < 0.04)
                    c = Color.SlateBlue;
                else if (double.Parse(pData[v + 2]) > 0.02 && double.Parse(pData[v + 2]) < 0.03)
                    c = Color.DodgerBlue;
                else if (double.Parse(pData[v + 2]) > 0.03 && double.Parse(pData[v + 2]) < 0)
                    c = Color.CornflowerBlue;

                bx[boxCount][0][0] = radii * Math.Cos(Program.toRadians(180 + double.Parse(pData[v + 1]))) * Math.Sin(Program.toRadians(90 - double.Parse(pData[v])));
                bx[boxCount][0][1] = radii * Math.Sin(Program.toRadians(180 + double.Parse(pData[v + 1]))) * Math.Sin(Program.toRadians(90 - double.Parse(pData[v])));
                bx[boxCount][0][2] = radii * Math.Cos(Program.toRadians(90 - double.Parse(pData[v])));

                bx[boxCount][1][0] = radii * Math.Cos(Program.toRadians(180 + double.Parse(pData[v + 1]))) * Math.Sin(Program.toRadians(89.6 - double.Parse(pData[v])));
                bx[boxCount][1][1] = radii * Math.Sin(Program.toRadians(180 + double.Parse(pData[v + 1]))) * Math.Sin(Program.toRadians(89.6 - double.Parse(pData[v])));
                bx[boxCount][1][2] = radii * Math.Cos(Program.toRadians(89.6 - double.Parse(pData[v])));

                bx[boxCount][2][0] = radii * Math.Cos(Program.toRadians(180.4 + double.Parse(pData[v + 1]))) * Math.Sin(Program.toRadians(89.6 - double.Parse(pData[v])));
                bx[boxCount][2][1] = radii * Math.Sin(Program.toRadians(180.4 + double.Parse(pData[v + 1]))) * Math.Sin(Program.toRadians(89.6 - double.Parse(pData[v])));
                bx[boxCount][2][2] = radii * Math.Cos(Program.toRadians(89.6 - double.Parse(pData[v])));

                bx[boxCount][3][0] = radii * Math.Cos(Program.toRadians(180.4 + double.Parse(pData[v + 1]))) * Math.Sin(Program.toRadians(90 - double.Parse(pData[v])));
                bx[boxCount][3][1] = radii * Math.Sin(Program.toRadians(180.4 + double.Parse(pData[v + 1]))) * Math.Sin(Program.toRadians(90 - double.Parse(pData[v])));
                bx[boxCount][3][2] = radii * Math.Cos(Program.toRadians(90 - double.Parse(pData[v])));
                //////////////////////////////
                bx[boxCount][4][0] = (double.Parse(pData[v + 2]) + 1) * radii * Math.Cos(Program.toRadians(180 + double.Parse(pData[v + 1]))) * Math.Sin(Program.toRadians(90 - double.Parse(pData[v])));
                bx[boxCount][4][1] = (double.Parse(pData[v + 2]) + 1) * radii * Math.Sin(Program.toRadians(180 + double.Parse(pData[v + 1]))) * Math.Sin(Program.toRadians(90 - double.Parse(pData[v])));
                bx[boxCount][4][2] = (double.Parse(pData[v + 2]) + 1) * radii * Math.Cos(Program.toRadians(90 - double.Parse(pData[v])));

                bx[boxCount][5][0] = (double.Parse(pData[v + 2]) + 1) * radii * Math.Cos(Program.toRadians(180 + double.Parse(pData[v + 1]))) * Math.Sin(Program.toRadians(89.6 - double.Parse(pData[v])));
                bx[boxCount][5][1] = (double.Parse(pData[v + 2]) + 1) * radii * Math.Sin(Program.toRadians(180 + double.Parse(pData[v + 1]))) * Math.Sin(Program.toRadians(89.6 - double.Parse(pData[v])));
                bx[boxCount][5][2] = (double.Parse(pData[v + 2]) + 1) * radii * Math.Cos(Program.toRadians(89.6 - double.Parse(pData[v])));

                bx[boxCount][6][0] = (double.Parse(pData[v + 2]) + 1) * radii * Math.Cos(Program.toRadians(180.4 + double.Parse(pData[v + 1]))) * Math.Sin(Program.toRadians(89.6 - double.Parse(pData[v])));
                bx[boxCount][6][1] = (double.Parse(pData[v + 2]) + 1) * radii * Math.Sin(Program.toRadians(180.4 + double.Parse(pData[v + 1]))) * Math.Sin(Program.toRadians(89.6 - double.Parse(pData[v])));
                bx[boxCount][6][2] = (double.Parse(pData[v + 2]) + 1) * radii * Math.Cos(Program.toRadians(89.6 - double.Parse(pData[v])));

                bx[boxCount][7][0] = (double.Parse(pData[v + 2]) + 1) * radii * Math.Cos(Program.toRadians(180.4 + double.Parse(pData[v + 1]))) * Math.Sin(Program.toRadians(90 - double.Parse(pData[v])));
                bx[boxCount][7][1] = (double.Parse(pData[v + 2]) + 1) * radii * Math.Sin(Program.toRadians(180.4 + double.Parse(pData[v + 1]))) * Math.Sin(Program.toRadians(90 - double.Parse(pData[v])));
                bx[boxCount][7][2] = (double.Parse(pData[v + 2]) + 1) * radii * Math.Cos(Program.toRadians(90 - double.Parse(pData[v])));

                pb[boxCount] = new PosiBox3D(bx[boxCount]);

                Program.addBox(pb[boxCount].initPosiBox3D(), c);

                boxCount++;
            }
        }

        hInc = 360 / (double)hDiv;
        vInc = 180 / (double)vDiv;

        for (double phi = 0, i = 0; i < vDiv; phi = phi + vInc, i++)
        {
            double upperPhi = phi;
            double lowerPhi = phi - vInc;

            shell[(int)i] = new Plane3D[hDiv];

            for (double theta = 0, j = 0; j < hDiv; theta = theta + hInc, j++)
            {
                double startTheta = theta;
                double endTheta = theta + hInc;

                double[] point1 = new double[3];
                point1[0] = center[0] + radii * Math.Cos(Program.toRadians(theta)) * Math.Sin(Program.toRadians(phi));
                point1[1] = center[1] + radii * Math.Sin(Program.toRadians(theta)) * Math.Sin(Program.toRadians(phi));
                point1[2] = center[2] + radii * Math.Cos(Program.toRadians(phi));

                double[] point2 = new double[3];
                point2[0] = center[0] + radii * Math.Cos(Program.toRadians(theta + hInc)) * Math.Sin(Program.toRadians(phi));
                point2[1] = center[1] + radii * Math.Sin(Program.toRadians(theta + hInc)) * Math.Sin(Program.toRadians(phi));
                point2[2] = center[2] + radii * Math.Cos(Program.toRadians(phi));

                double[] point3 = new double[3];
                point3[0] = center[0] + radii * Math.Cos(Program.toRadians(theta + hInc)) * Math.Sin(Program.toRadians(phi + vInc));
                point3[1] = center[1] + radii * Math.Sin(Program.toRadians(theta + hInc)) * Math.Sin(Program.toRadians(phi + vInc));
                point3[2] = center[2] + radii * Math.Cos(Program.toRadians(phi + vInc));

                double[] point4 = new double[3];
                point4[0] = center[0] + radii * Math.Cos(Program.toRadians(theta)) * Math.Sin(Program.toRadians(phi + vInc));
                point4[1] = center[1] + radii * Math.Sin(Program.toRadians(theta)) * Math.Sin(Program.toRadians(phi + vInc));
                point4[2] = center[2] + radii * Math.Cos(Program.toRadians(phi + vInc));

                double[][] planeElement = { point1, point2, point3, point4 };

                shell[(int)i][(int)j] = new Plane3D(planeElement, Color.LightCyan);

                Program.addPlane(shell[(int)i][(int)j].initPlane3D(), Color.LightCyan);
            }
        }
    }

    void seek(double[] move)
    {
        int i, j;

        center[0] += 3 * move[0];
        center[1] += 3 * move[1];
        center[2] += 3 * move[2];       //	HOW !???

        for (i = 0; i < vDiv; i++)
            for (j = 0; j < hDiv; j++)
                shell[i][j].seek(move);
    }

    void rotateX(double degree, double[] COM)
    {
        int i, j;

        for (i = 0; i < vDiv; i++)
            for (j = 0; j < hDiv; j++)
                shell[i][j].rotateX(degree, COM);
    }

    void rotateY(double degree, double[] COM)
    {
        int i, j;

        for (i = 0; i < vDiv; i++)
            for (j = 0; j < hDiv; j++)
                shell[i][j].rotateY(degree, COM);
    }

    void rotateZ(double degree, double[] COM)
    {
        int i, j;

        for (i = 0; i < vDiv; i++)
            for (j = 0; j < hDiv; j++)
                shell[i][j].rotateZ(degree, COM);
    }

}