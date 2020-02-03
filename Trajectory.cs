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
/// Summary description for Trajectory
/// </summary>
public class Trajectory
{
    Plane3D[] shell = new Plane3D[40];
    double[][] path;
    double[] pt_release;
    double velocity, theta, phi, spin_angle;
    public Color color;

    public Trajectory(double[] pt_release, double velocity, double theta, double phi, double spin_angle, Color color)
    {
        this.pt_release = pt_release;
        this.velocity = velocity;
        this.theta = theta;
        this.phi = phi;
        path = new double[41][];//3
        this.spin_angle = spin_angle;

        this.color = color;
    }

    public void initTrajectory()
    {
        double y = 0, p = 0, q = 0;
        double t;
        double x = 0;
        double a;
        double b, c;

        double theta_release = phi * (3.14 / 180);
        double temp = Math.Cos(theta_release);
        double init_vel = 280;
        double theta_point;// in radians
        double vy, vx;
        double cos_of;
        double vel_after;
        double COR = 0.6;   //Coefficient of Restitution	//dhaval
        double vybyvx;

        //ball color settings (start)

        //ball color settings.(end)


        //fuction to get a,b,c from v and theta.(start)

        a = -0.5 * 9.8 / (init_vel * init_vel * temp * temp);
        b = Math.Tan(theta_release);
        c = 0;

        //function end.
        int index = 0;
        for (x = 0; x < 2050; x += 50)
        {
            y = a * x * x + b * x + c;

            path[index] = new double[3];
            path[index][0] = 0;
            path[index][1] = x;
            path[index][2] = y + 240;

            index++;

            if (y <= -240)
                break;
        }

        p = x; q = y;        // p holds the value of x for which height = 0.

        int spin_index = (int)p / 50;
        double[] spin_point = new double[3];
        double[][] spin_point_temp = new double[4][];//3

        //function to get a,b and c from v and theta.(start)

        //function to find theta for path after landing.(start)
        t = p / (init_vel * Math.Cos(theta_release));
        vy = init_vel * Math.Sin(theta_release) - 2 * 9.8 * t;
        vx = init_vel * Math.Cos(theta_release);
        vybyvx = vy / vx;
        theta_point = Math.Atan((vybyvx > 0) ? vybyvx : (-1) * vybyvx);

        if (p > 1000)
            theta_point *= 0.6;


        cos_of = Math.Cos(theta_point);
        vel_after = COR * (Math.Sqrt((init_vel * init_vel) - (2 * 9.8 * p * Math.Tan(theta_point)) + (9.8 * 9.8 * p * p) / (init_vel * init_vel * cos_of * cos_of)));

        // vel_after is the initial velocity for the path after landing.

        a = -0.5 * 9.8 / (vel_after * vel_after * cos_of * cos_of);
        b = Math.Tan(theta_point);
        c = 0;             //c = y_zero.

        // (end)

        x = 0; y = 0;
        p += 50;

        for (; p < 2050; p += 50)
        {
            y = (a * x * x + b * x + c);

            //pt = y + 240;
            //putpixel(p,480-y,GREEN);
            path[index] = new double[3];
            path[index][0] = 0;
            path[index][1] = p;
            path[index][2] = y;

            index++;
            x += 50;

        }

        /////////////////////////////////////////////////////////////////////////

        double[][] segment = new double[4][];

        for (int w = 0; w < 40; w++)
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

            shell[w] = new Plane3D(segment, color);


            shell[w].rotateZ(theta, new double[] { 0, 0, 0 });

            if (w == spin_index)
            {
                spin_point_temp = shell[w].getPlane3D();
                spin_point[0] = spin_point_temp[1][0];
                spin_point[1] = spin_point_temp[1][1];
                spin_point[2] = 0;
            }

            if (w > spin_index)
                shell[w].rotateZ(spin_angle, spin_point);

            shell[w].seek(pt_release);

            Program.addPlane(shell[w].initPlane3D(), color);
        }

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