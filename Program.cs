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
/// Summary description for Program
/// </summary>
public class Program
{
    public static double[] a;
    public static double[] d;

    public static double[][] v;    //2
    public static Color[] p_color;

    public static double[][] p; //3

    public static double[][] COM;//3
    public static double[][] comDist;//2

    

    public static int pointCount;

    public static int succ_plane;//4c    

    public static double dirTheta, dirPhi, dirRadii;
    public static double dTheta, dPhi, dRadii;

    public static int planeCount;

    /// <summary>
    /// /////////////////////
    /// </summary>

    static double zoom = 500;   
    double[] loc = new double[900000];
    static double tx, ty;    

    Program()
    { }

    public static double toRadians(double angle)
    {
        return 3.14 * angle / 180.0;
    }

    public static double toDegrees(double angle)
    {
        return angle * (180.0 / 3.14);
    }

    public static double dot(double[] a, double[] b)
    {
        return (a[0] * b[0] + a[1] * b[1] + a[2] * b[2]);
    }

    public static double[] cross(double[] a, double[] b)
    {
        double[] crs = { 0, 0, 0 };

        crs[0] = a[1] * b[2] - b[1] * a[2];
        crs[1] = b[0] * a[2] - a[0] * b[2];
        crs[2] = a[0] * b[1] - b[0] * a[1];

        return crs;
    }

    public static double mod(double[] v)
    {
        double ln = (double)Math.Sqrt(v[0] * v[0] + v[1] * v[1] + v[2] * v[2]);

        return ln;
    }

    public static double[] diff(double[] a, double[] b)
    {
        double[] d = { 0, 0, 0, };

        d[0] = b[0] - a[0];
        d[1] = b[1] - a[1];
        d[2] = b[2] - a[2];

        return d;
    }


    public static double per_dist(double[] p, double[] a, double[] d)
    {
        double width = 0;

        width = (mod(cross(diff(p, a), d))) / (mod(d));

        return width;
    }

    public static double per_dist_plane(double[] p, double[] a, double[] d)//w
    {
        double D;

        D = a[0] * d[0] + a[1] * d[1] + a[2] * d[2];

        return ((Math.Abs(dot(p, d) - D)) / (mod(d)));
    }

    public static double depth(double[] p, double[] a, double[] d)
    {
        return (Math.Abs(dot(diff(p, a), d) / (mod(d))));
    }

    public static double displ(double[] p, double[] a, double[] d)
    {
        double dtemp = depth(p, a, d);

        if (dtemp == 0)
            dtemp = 0.0000001;	//ANY SMALL

        return (per_dist(p, a, d)) / (dtemp);
    }

    public static double[] unit(double[] v)
    {
        double md = mod(v);
        double[] unt = { 0, 0, 0 };

        unt[0] = v[0] / md;
        unt[1] = v[1] / md;
        unt[2] = v[2] / md;

        return unt;
    }

    public static double get_phi(double[] v)
    {
        double phi = 0;

        phi = toDegrees(Math.Acos(v[2] / (Math.Sqrt(v[0] * v[0] + v[1] * v[1] + v[2] * v[2]))));

        return phi;
    }

    public static double get_theta(double[] v)
    {
        double theta = 0;

        if (v[0] == 0 & v[1] == 0)
            return theta;	//exception

        theta = toDegrees(Math.Acos(v[0] / (Math.Sqrt(v[0] * v[0] + v[1] * v[1]))));

        if (v[1] < 0)
            theta = 360 - theta;

        return theta;
    }


    public static double[] get_diff_phi(double[] d, double[] p)
    {
        double phi_d = get_phi(d);
        double phi_p = get_phi(p);

        double[] diff_phi = { 0, 0 };

        double[] use_diff_theta = get_diff_theta(d, p);

        if (phi_d < 90 && phi_p < 90)
        {
            if (Math.Abs(use_diff_theta[0]) > 90)
            {
                diff_phi[0] = phi_d + phi_p;
                diff_phi[1] = 1;
            }
            else if (Math.Abs(use_diff_theta[0]) < 90)
            {
                diff_phi[0] = phi_d - phi_p;

                if (diff_phi[0] > 0)
                    diff_phi[1] = 1;
                else
                    diff_phi[1] = -1;
            }
        }
        else if (phi_d > 90 && phi_p > 90)
        {
            if (use_diff_theta[0] > 90)
            {
                diff_phi[0] = 360 - phi_d - phi_p;
                diff_phi[1] = -1;
            }
            else if (Math.Abs(use_diff_theta[0]) < 90)
            {
                diff_phi[0] = phi_d - phi_p;

                if (diff_phi[0] > 0)
                    diff_phi[1] = 1;
                else
                    diff_phi[1] = -1;
            }
        }
        else
        {
            if (Math.Abs(use_diff_theta[0]) > 90)
            {
                diff_phi[0] = phi_d + phi_p;
                diff_phi[1] = 1;//or-1
            }
            else if (Math.Abs(use_diff_theta[0]) < 90)
            {
                diff_phi[0] = phi_d - phi_p;

                if (diff_phi[0] > 0)
                    diff_phi[1] = 1;
                else
                    diff_phi[1] = -1;
            }
        }

        return diff_phi;
    }


    public static double[] get_diff_theta(double[] d, double[] p)
    {
        double[] diff_theta = { 0, 0 };

        double theta_d = get_theta(d);
        double theta_p = get_theta(p);

        if (theta_d < 90 && theta_p > 270)
        {
            diff_theta[0] = theta_d + 360 - theta_p;
            diff_theta[1] = 1;
        }
        else if (theta_d > 270 && theta_p < 90)
        {
            diff_theta[0] = (theta_p + 360 - theta_d) * (-1);
            diff_theta[1] = -1;
        }
        else
        {
            diff_theta[0] = theta_d - theta_p;

            if (diff_theta[0] > 0)
                diff_theta[1] = 1;
            else
                diff_theta[1] = -1;
        }

        return diff_theta;
    }

    public static double[] get_rel_vect(double[] d, double[] p)
    {
        double[] rel_vect = { 0, 0, 0 };

        double phi_d = get_phi(d);
        double phi_p = get_phi(p);

        double[] diff_phi = { 0, 0 };

        double[] use_diff_theta = get_diff_theta(d, p);

        double[] rel_unit_vect = { 0, 0, 0 };

        double limit = 90;

        if (phi_d < 90 && phi_p < 90)
        {
            if (Math.Abs(use_diff_theta[0]) > limit)
            {
                rel_vect[0] = 180 - Math.Abs(use_diff_theta[0]);//ts_6_1

                rel_vect[0] = use_diff_theta[1] * rel_vect[0];

                rel_vect[1] = phi_d + phi_p;

                rel_unit_vect[2] = -3;
            }
            else if (Math.Abs(use_diff_theta[0]) < 90)
            {
                diff_phi[0] = phi_d - phi_p;

                if (diff_phi[0] > 0)
                    diff_phi[1] = 1;
                else
                    diff_phi[1] = -1;

                rel_vect[1] = diff_phi[0];

                rel_vect[0] = use_diff_theta[0];
            }
        }
        else if (phi_d > 90 && phi_p > 90)
        {
            if (Math.Abs(use_diff_theta[0]) > limit)
            {
                rel_vect[1] = (360 - phi_d - phi_p) * (-1);

                rel_vect[0] = 180 - use_diff_theta[0];

                rel_vect[0] = use_diff_theta[1] * rel_vect[0];

                rel_unit_vect[2] = -3;
            }
            else if (Math.Abs(use_diff_theta[0]) < 90)
            {
                diff_phi[0] = phi_d - phi_p;

                if (diff_phi[0] > 0)
                    diff_phi[1] = 1;
                else
                    diff_phi[1] = -1;

                rel_vect[1] = diff_phi[0];

                rel_vect[0] = use_diff_theta[0];
            }
        }
        else
        {
            if (Math.Abs(use_diff_theta[0]) > limit)
            {

                diff_phi[0] = phi_d + phi_p;
                diff_phi[1] = 1;

                rel_unit_vect[2] = -3;
            }
            else if (Math.Abs(use_diff_theta[0]) < 90)
            {
                diff_phi[0] = phi_d - phi_p;

                if (diff_phi[0] > 0)
                    diff_phi[1] = 1;
                else
                    diff_phi[1] = -1;
            }

            rel_vect[1] = diff_phi[0];

            rel_vect[0] = use_diff_theta[0];
        }

        double rel_vect_magn = Math.Sqrt(rel_vect[0] * rel_vect[0] + rel_vect[1] * rel_vect[1]);

        rel_unit_vect[0] = rel_vect[0] / rel_vect_magn;
        rel_unit_vect[1] = rel_vect[1] / rel_vect_magn;

        return rel_unit_vect;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////
    public static void trans()
    {
        int new_count = -1;
        double radii;

        succ_plane = 0;//4c
        double[] w = { 0, 0, 0 };

        for (int i = 0; i < pointCount; i += 4, succ_plane++)//4c
        {
            radii = displ(w, w, w);

            radii = displ(p[i], a, d);

            double[] rel_v = get_rel_vect(d, diff(a, p[i]));

            tx = zoom * radii * rel_v[0];
            ty = zoom * radii * rel_v[1];

            if (rel_v[2] == 0)
            {
                new_count++;

                v[new_count] = new double[2];
                v[new_count][0] = 960 + tx;
                v[new_count][1] = 540 - ty;

                //Console.WriteLine("{0},{1}", v[new_count][0], v[new_count][1]);

                radii = displ(p[i + 1], a, d);
                rel_v = get_rel_vect(d, diff(a, p[i + 1]));

                tx = zoom * radii * rel_v[0];
                ty = zoom * radii * rel_v[1];

                new_count++;
                v[new_count] = new double[2];
                v[new_count][0] = 960 + tx;
                v[new_count][1] = 540 - ty;

                //Console.WriteLine("{0},{1}", v[new_count][0], v[new_count][1]);

                radii = displ(p[i + 2], a, d);
                rel_v = get_rel_vect(d, diff(a, p[i + 2]));

                tx = zoom * radii * rel_v[0];
                ty = zoom * radii * rel_v[1];

                new_count++;
                v[new_count] = new double[2];
                v[new_count][0] = 960 + tx;
                v[new_count][1] = 540 - ty;

                //Console.WriteLine("{0},{1}", v[new_count][0], v[new_count][1]);

                radii = displ(p[i + 3], a, d);
                rel_v = get_rel_vect(d, diff(a, p[i + 3]));

                tx = zoom * radii * rel_v[0];
                ty = zoom * radii * rel_v[1];

                new_count++;
                v[new_count] = new double[2];
                v[new_count][0] = 960 + tx;
                v[new_count][1] = 540 - ty;
            }
            else
            {
                new_count++;
                v[new_count] = new double[2];
                v[new_count][0] = 0;
                v[new_count][1] = 0;

                new_count++;
                v[new_count] = new double[2];
                v[new_count][0] = 0;
                v[new_count][1] = 0;

                new_count++;
                v[new_count] = new double[2];
                v[new_count][0] = 0;
                v[new_count][1] = 0;

                new_count++;
                v[new_count] = new double[2];
                v[new_count][0] = 0;
                v[new_count][1] = 0;
            }
        }
    }

    public static void addPlane(double[][] plane, Color c)//4c
    {
        p_color[planeCount] = c;

        p[pointCount] = new double[3];
        p[pointCount + 1] = new double[3];
        p[pointCount + 2] = new double[3];
        p[pointCount + 3] = new double[3];

        p[pointCount] = plane[0];
        p[pointCount + 1] = plane[1];
        p[pointCount + 2] = plane[2];
        p[pointCount + 3] = plane[3];

        pointCount += 4;
        planeCount += 1;
    }


    public static void addBox(double[][] box, Color c)
    {
        double[][] plane = new double[4][];//3
        plane[0] = new double[3];
        plane[1] = new double[3];
        plane[2] = new double[3];
        plane[3] = new double[3];

        //base
        plane[0] = box[0];
        plane[1] = box[1];
        plane[2] = box[2];
        plane[3] = box[3];

        addPlane(plane, c);

        //top
        plane[0] = box[4];
        plane[1] = box[5];
        plane[2] = box[6];
        plane[3] = box[7];

        addPlane(plane, c);

        //YZ near	
        plane[0] = box[3];
        plane[1] = box[0];
        plane[2] = box[4];
        plane[3] = box[7];

        addPlane(plane, c);

        //YZ far
        plane[0] = box[2];
        plane[1] = box[1];
        plane[2] = box[5];
        plane[3] = box[6];

        addPlane(plane, c);

        // XZ near
        plane[0] = box[0];
        plane[1] = box[1];
        plane[2] = box[5];
        plane[3] = box[4];

        addPlane(plane, c);

        //XZ far
        plane[0] = box[3];
        plane[1] = box[2];
        plane[2] = box[6];
        plane[3] = box[7];

        addPlane(plane, c);
    }

    public static void sortDist()
    {
        int c, d;
        double swap;

        for (c = 0; c < (succ_plane - 1); c++)//4c
        {
            for (d = 0; d < succ_plane - c - 1; d++)//4c
            {
                if (comDist[d][0] < comDist[d + 1][0]) /* For descending order use < */
                {
                    swap = comDist[d][0];
                    comDist[d][0] = comDist[d + 1][0];
                    comDist[d + 1][0] = swap;

                    swap = comDist[d][1];
                    comDist[d][1] = comDist[d + 1][1];
                    comDist[d + 1][1] = swap;
                }
            }
        }
    }

    public static void calcDist()
    {
        double[] comass = new double[3];

        for (int i = 0; i < succ_plane; i++)//4c
        {
            comass[0] = COM[i][0];
            comass[1] = COM[i][1];
            comass[2] = COM[i][2];

            comDist[i] = new double[2];

            comDist[i][0] = mod(diff(a, comass));
            comDist[i][1] = i;
        }
    }

    public static void calcCOM()
    {
        for (int i = 0; i < succ_plane; i++)//4c
        {
            COM[i] = new double[3];

            COM[i][0] = (p[i * 4][0] + p[i * 4 + 1][0] + p[i * 4 + 2][0] + p[i * 4 + 3][0]) / 4;
            COM[i][1] = (p[i * 4][1] + p[i * 4 + 1][1] + p[i * 4 + 2][1] + p[i * 4 + 3][1]) / 4;
            COM[i][2] = (p[i * 4][2] + p[i * 4 + 1][2] + p[i * 4 + 2][2] + p[i * 4 + 3][2]) / 4;
        }
    }

    public static void preCalc()
    {
        calcCOM();
        calcDist();
        sortDist();
    }


    public static void makePitch()
    {
        double[][][][] basee = new double[10][][][];

        double scale = 40;

        for (int i = 0; i < 10; i++)
        {
            basee[i] = new double[50][][];

            for (int j = 0; j < 50; j++)
            {
                basee[i][j] = new double[4][];
                basee[i][j][0] = new double[3];
                basee[i][j][1] = new double[3];
                basee[i][j][2] = new double[3];
                basee[i][j][3] = new double[3];

                basee[i][j][0][0] = scale * i - 200;
                basee[i][j][0][1] = scale * j;
                basee[i][j][0][2] = -15;

                basee[i][j][1][0] = scale * (i + 1) - 200;
                basee[i][j][1][1] = scale * j;
                basee[i][j][1][2] = -15;

                basee[i][j][2][0] = scale * (i + 1) - 200;
                basee[i][j][2][1] = scale * (j + 1);
                basee[i][j][2][2] = -15;

                basee[i][j][3][0] = scale * i - 200;
                basee[i][j][3][1] = scale * (j + 1);
                basee[i][j][3][2] = -15;

                Program.addPlane(basee[i][j], Color.LightCyan);
            }
        }
    }
}
