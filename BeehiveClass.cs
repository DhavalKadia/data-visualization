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

public class BeehiveClass
{
    Plane3D[] shell = new Plane3D[40];
    double[][] path;
    double[] pt_release;
    double velocity, theta, phi, spin_angle;
    public Color color;

    public BeehiveClass(double[] pt_release, double velocity, double theta, double phi, double spin_angle, Color color)
    {
        this.pt_release = pt_release;
        this.velocity = velocity;
        this.theta = theta;
        this.phi = phi;
        path = new double[41][];//3
        this.spin_angle = spin_angle;

        this.color = color;
    }

    public void initBeehive()
    {
        double y = 0, p = 0, q = 0;
        double t;
        double x = 0;
        double a;
        double b, c;

        double theta_release = phi * (3.14 / 180);
        double temp = Math.Cos(theta_release);
        double init_vel = 280;
       
        a = -0.5 * 9.8 / (init_vel * init_vel * temp * temp);
        b = Math.Tan(theta_release);
        c = 0;
        for (x = 0; x < 2050; x += 50)
        {
            y = a * x * x + b * x + c;

            if (y <= -240)
                break;
        }

        p = x; q = y;        // p holds the value of x for which height = 0.

        double[][] pointG = new double[4][];
        Plane3D pointE;

        pointG[0] = new double[3];
        pointG[1] = new double[3];
        pointG[2] = new double[3];
        pointG[3] = new double[3];

        pointG[0][0] = pointG[1][0] = pointG[2][0] = pointG[3][0] = 0;
        pointG[0][1] = pointG[1][1] = pointG[2][1] = pointG[3][1] = p;
        pointG[0][2] = pointG[1][2] = pointG[2][2] = pointG[3][2] = q + 240;

        pointE = new Plane3D(pointG, color);

        pointE.rotateZ(theta, pt_release);

        Program.addPlane(pointE.initPlane3D(), Color.Azure);
    }

    public void seek(double[] move)
    {
        int i, j;

        for (i = 0; i < 40; i++)
            shell[i].seek(move);
    }

    public void rotateX(double degree, double[] COM)
    {
        int i, j;

        for (i = 0; i < 40; i++)
            shell[i].rotateX(degree, COM);
    }

    public void rotateY(double degree, double[] COM)
    {
        int i, j;

        for (i = 0; i < 40; i++)
            shell[i].rotateY(degree, COM);
    }

    public void rotateZ(double degree, double[] COM)
    {
        int i, j;

        for (i = 0; i < 40; i++)
            shell[i].rotateZ(degree, COM);
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
