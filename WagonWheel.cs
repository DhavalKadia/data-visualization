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
/// Summary description for WagonWheel
/// </summary>
public class WagonWheel
{
    Plane3D[] shell = new Plane3D[2000];
    double[][] path;
    double velocity, theta, phi, endPoint;
    int index = 0;
    public Color color;

    public WagonWheel(double velocity, double theta, double phi, double endPoint)
    {
        this.velocity = velocity;
        this.theta = theta;
        this.phi = phi;
        this.endPoint = endPoint;
        path = new double[2000][];//3

        //this.color = color;
    }

    public void initTrajectory()
    {
        double end_pt_x = endPoint; //end_pt_y = 0;
        //wagon wheel
        double y = 0, p = 0, q = 0;
        double t;   //dhaval
        double x = 0;
        double a;       //a = -1/1000;
        double b, c;   //b=0.2, c=0;

        double theta1 = phi * (3.14 / 180);
        double temp = Math.Cos(theta1);
        double magnitude = velocity;
        double theta_point;// in radians
        double vy, vx;
        double cos_of;
        double vel_after;
        double vybyvx;
        double COR = 0.4;
    
        a = -0.5 * 9.8 / (magnitude * magnitude * temp * temp);
        b = Math.Tan(theta1);
        c = 0;

        //function end.
        color = Color.Red;
        for (x = 0; x < 8000; x += 50)
        {
            y = a * x * x + b * x + c;

            path[index] = new double[3];
            path[index][0] = 0;
            path[index][1] = x;
            path[index][2] = y;
            //System.out.println(index+" : "+ path[index][1] +", "+ path[index][2]);
            index++;

            if (y < 0)
            {
                break;
            }
        }

        double temp_ex = end_pt_x;
        p = x; q = y;
        if (end_pt_x < 8000 && y < 0)
        {
            // p holds the value of x for which height = 0.

            t = p / (magnitude * Math.Cos(theta1));
            vy = magnitude * Math.Sin(theta1) - 2 * 9.8 * t;
            vx = magnitude * Math.Cos(theta1);
            vybyvx = vy / vx;
            theta_point = Math.Atan((vybyvx > 0) ? vybyvx : (-1) * vybyvx);

            cos_of = Math.Cos(theta_point);
            vel_after = COR * Math.Sqrt((magnitude * magnitude) - (2 * 9.8 * p * Math.Tan(theta_point)) + (9.8 * 9.8 * p * p) / (magnitude * magnitude * cos_of * cos_of));

            // vel_after is the initial velocity for the path after landing.

            a = -0.5 * 9.8 / (vel_after * vel_after * cos_of * cos_of);
            b = Math.Tan(theta_point);
            c = 0;             //c = y_zero.

            // (end)


            p += 50;
            for (; p < temp_ex; p += 50)
            {//System.out.println("$$$index="+index);
                path[index] = new double[3];

                path[index][0] = 0;
                path[index][1] = p;
                path[index][2] = 0;

                //System.out.println(index+" ~ : "+ path[index][1] +", "+ path[index][2]);
                x++;
                index++;
                //pt = y + 240;
                //circle(p,480-y,5);
                //if(y<0 || (x >= end_pt_x))
                //  break;
            }
            //if(end_pt_x < 8000)
            color = Color.Yellow;
        }
        else if (end_pt_x == 8000 && y < 0)
        {


            p += 50;
            for (; p < 8000; p += 50)
            {//System.out.println("$$$index="+index);
                path[index] = new double[3];

                path[index][0] = 0;
                path[index][1] = p;
                path[index][2] = 0;

                //System.out.println(index+" ~ : "+ path[index][1] +", "+ path[index][2]);
                x++;
                index++;
                //pt = y + 240;
                //circle(p,480-y,5);
                //if(y<0 || (x >= end_pt_x))
                //  break;
            }
            color = Color.Green;
        }
        /////////////////////////////////////////////////////////////////////////

        double[][] segment = new double[4][];
        //System.out.println("index="+index);
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

            shell[w] = new Plane3D(segment, color);

            shell[w].rotateZ(180 + theta, new double[] { 0, 0, 0 });
            shell[w].seek(new double[] { 0, 645, 0 });

            Program.addPlane(shell[w].initPlane3D(), color);
        }

    }

    void seek(double[] move)
    {
        int i, j;

        for (i = 0; i < index - 1; i++)
            shell[i].seek(move);
    }

    void rotateX(double degree, double[] COM)
    {
        int i, j;

        for (i = 0; i < index - 1; i++)
            shell[i].rotateX(degree, COM);
    }

    void rotateY(double degree, double[] COM)
    {
        int i, j;

        for (i = 0; i < index - 1; i++)
            shell[i].rotateY(degree, COM);
    }

    void rotateZ(double degree, double[] COM)
    {
        int i, j;

        for (i = 0; i < index - 1; i++)
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