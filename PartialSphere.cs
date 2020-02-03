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
/// Summary description for PartialSphere
/// </summary>
public class PartialSphere
{
    Plane3D[][] shell;
    double radii, hInc, vInc, phiStart, phiEnd, thetaStart, thetaEnd, currPhi, currTheta;
    double[] center, normal = { 0, 0, 1 };
    int initPoint, hDiv, vDiv;
    public Color color;

    public PartialSphere(double radii, double[] center, int verticalDiv, int horizontalDiv, double phiStart, double phiEnd, double thetaStart, double thetaEnd, Color color)
    {
        this.radii = radii;
        this.center = center;
        this.hDiv = horizontalDiv;
        this.vDiv = verticalDiv;
        shell = new Plane3D[vDiv][];//hDiv

        this.phiStart = phiStart;
        this.phiEnd = phiEnd;
        this.thetaStart = thetaStart;
        this.thetaEnd = thetaEnd;

        currPhi = 0;
        currTheta = 0;

        this.color = color;
    }

    public void initPartialSphere()
    {
        this.initPoint = Program.pointCount;

        hInc = 360 / (double)hDiv;
        vInc = 180 / (double)vDiv;

        for (double phi = phiStart, i = 0; phi < phiEnd; phi = phi + vInc, i++)
        {
            double upperPhi = phi;
            double lowerPhi = phi - vInc;

            for (double theta = thetaStart, j = 0; theta < thetaEnd; theta = theta + hInc, j++)
            {
                double startTheta = theta;
                double endTheta = theta + hInc;

                double[] point1 = new double[3];
                point1[0] = center[0] + radii * Math.Cos(toRadians(theta)) * Math.Sin(toRadians(phi));
                point1[1] = center[1] + radii * Math.Sin(toRadians(theta)) * Math.Sin(toRadians(phi));
                point1[2] = center[2] + radii * Math.Cos(toRadians(phi));

                double[] point2 = new double[3];
                point2[0] = center[0] + radii * Math.Cos(toRadians(theta + hInc)) * Math.Sin(toRadians(phi));
                point2[1] = center[1] + radii * Math.Sin(toRadians(theta + hInc)) * Math.Sin(toRadians(phi));
                point2[2] = center[2] + radii * Math.Cos(toRadians(phi));

                double[] point3 = new double[3];
                point3[0] = center[0] + radii * Math.Cos(toRadians(theta + hInc)) * Math.Sin(toRadians(phi + vInc));
                point3[1] = center[1] + radii * Math.Sin(toRadians(theta + hInc)) * Math.Sin(toRadians(phi + vInc));
                point3[2] = center[2] + radii * Math.Cos(toRadians(phi + vInc));

                double[] point4 = new double[3];
                point4[0] = center[0] + radii * Math.Cos(toRadians(theta)) * Math.Sin(toRadians(phi + vInc));
                point4[1] = center[1] + radii * Math.Sin(toRadians(theta)) * Math.Sin(toRadians(phi + vInc));
                point4[2] = center[2] + radii * Math.Cos(toRadians(phi + vInc));

                double[][] planeElement = { point1, point2, point3, point4 };

                shell[(int)i] = new Plane3D[hDiv];

                shell[(int)i][(int)j] = new Plane3D(planeElement, color);

                Program.addPlane(shell[(int)i][(int)j].initPlane3D(), color);
            }
        }
    }

    public double[] getVector(double radii, double[] center, double phi, double theta)
    {
        double[] point = new double[3];

        point[0] = center[0] + radii * Math.Cos(toRadians(theta)) * Math.Sin(toRadians(phi));
        point[1] = center[1] + radii * Math.Sin(toRadians(theta)) * Math.Sin(toRadians(phi));
        point[2] = center[2] + radii * Math.Cos(toRadians(phi));

        return point;
    }

    public double[] mulScaler(double scale, double[] vector)
    {
        double[] scaledV = new double[3];

        scaledV[0] = scale * vector[0];
        scaledV[1] = scale * vector[1];
        scaledV[2] = scale * vector[2];

        return scaledV;
    }

    public void seek(double[] move)
    {
        center[0] += 3 * move[0];
        center[1] += 3 * move[1];
        center[2] += 3 * move[2];       //	HOW !???

        for (double phi = phiStart, i = 0; phi < phiEnd; phi = phi + vInc, i++)
            for (double theta = thetaStart, j = 0; theta < thetaEnd; theta = theta + hInc, j++)
                shell[(int)i][(int)j].seek(move);
    }

    public void rotateX(double degree, double[] COM)
    {
        currPhi -= degree;

        if (currPhi > 180)
            currPhi -= 180;
        else if (currPhi < 0)
            currPhi = -currPhi;

        normal = getVector(1, new double[] { 0, 0, 0 }, currPhi, currTheta);

        for (double phi = phiStart, i = 0; phi < phiEnd; phi = phi + vInc, i++)
            for (double theta = thetaStart, j = 0; theta < thetaEnd; theta = theta + hInc, j++)
                shell[(int)i][(int)j].rotateX(degree, COM);

        ////#//^System.out.println("phi="+currPhi+" theta="+currTheta);
    }

    public void rotateY(double degree, double[] COM)
    {
        currPhi -= degree;

        if (currPhi > 180)
            currPhi -= 180;
        else if (currPhi < 0)
            currPhi = -currPhi;

        normal = getVector(1, new double[] { 0, 0, 0 }, currPhi, currTheta);

        for (double phi = phiStart, i = 0; phi < phiEnd; phi = phi + vInc, i++)
            for (double theta = thetaStart, j = 0; theta < thetaEnd; theta = theta + hInc, j++)
                shell[(int)i][(int)j].rotateY(degree, COM);

        ////#//^System.out.println("phi="+currPhi+" theta="+currTheta);
    }

    public void rotateZ(double degree, double[] COM)
    {
        currTheta -= degree;

        if (currTheta > 360)
            currPhi -= 360;
        else if (currTheta < 0)
            currTheta = 360 + currTheta;

        normal = getVector(1, new double[] { 0, 0, 0 }, currPhi, currTheta);

        for (double phi = phiStart, i = 0; phi < phiEnd; phi = phi + vInc, i++)
            for (double theta = thetaStart, j = 0; theta < thetaEnd; theta = theta + hInc, j++)
                shell[(int)i][(int)j].rotateZ(degree, COM);

        ////#//^System.out.println("phi="+currPhi+" theta="+currTheta);
    }

    public static double toRadians(double angle)
    {
        return 3.14 * angle / 180.0;
    }

    public static double toDegrees(double angle)
    {
        return angle * (180.0 / 3.14);
    }
}