<<<<<<< HEAD
﻿using System;
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

public class BiologyDNA
{
 Plane3D[] shell = new Plane3D[4000];
    Plane3D[] shell2 = new Plane3D[4000];
    Plane3D[] strip = new Plane3D[8000];

    double[][] path = new double[4100][];
    int index = 0;

    string gnome;
    public BiologyDNA(String gnome)
    {
        this.gnome = gnome;
    }

    public void initDNA()
    {
        //function end.
        double z;
        double inc = 0.2;

        for (z = -(gnome.Length/2); z < gnome.Length/2; z += inc)
        {
            path[index] = new double[3];

            path[index][0] = 50 * Math.Cos(z);
            path[index][1] = 50 * Math.Sin(z);
            path[index][2] = 25 * z;
           
            index++;
        }

        /////////////////////////////////////////////////////////////////////////

        double[][] segment = new double[4][];
        double[][] segment2 = new double[4][];
        double[][] segment3 = new double[4][];
        double[][] segment4 = new double[4][];
        double dTheta = 0.26;
        double cos = Math.Cos(dTheta);
        double sin = Math.Sin(dTheta);

        Random r = new Random();
        int flag = 0;
        int stripCount = 0;

        for (int w = 0; stripCount < gnome.Length - 1; w++)
        {
            segment[0] = new double[3];
            segment[0][0] = path[w][0];
            segment[0][1] = path[w][1];
            segment[0][2] = path[w][2];

            segment[1] = new double[3];
            segment[1][0] = path[w][0];// * cos - segment[0][1] * sin;
            segment[1][1] = path[w][1];// * sin + segment[0][1] * cos;
            segment[1][2] = path[w][2];

            segment[2] = new double[3];
            segment[2][0] = path[w + 1][0];
            segment[2][1] = path[w + 1][1];
            segment[2][2] = path[w + 1][2];

            segment[3] = new double[3];
            segment[3][0] = path[w + 1][0];// * cos - segment[2][1] * sin;
            segment[3][1] = path[w + 1][1];// * sin + segment[2][1] * cos;
            segment[3][2] = path[w + 1][2];

            shell[w] = new Plane3D(segment, Color.DarkTurquoise);

            Program.addPlane(shell[w].initPlane3D(), shell[w].color);

            segment2[0] = new double[3];
            segment2[1] = new double[3];
            segment2[2] = new double[3];
            segment2[3] = new double[3];

            segment2[0][0] = -segment[0][0];
            segment2[0][1] = -segment[0][1];
            segment2[0][2] = segment[0][2];

            segment2[1][0] = -segment[1][0];
            segment2[1][1] = -segment[1][1];
            segment2[1][2] = segment[1][2];

            segment2[2][0] = -segment[2][0];
            segment2[2][1] = -segment[2][1];
            segment2[2][2] = segment[2][2];

            segment2[3][0] = -segment[3][0];
            segment2[3][1] = -segment[3][1];
            segment2[3][2] = segment[3][2];

            shell2[w] = new Plane3D(segment2, Color.DarkCyan);

            Program.addPlane(shell2[w].initPlane3D(),  shell2[w].color);

            segment3[0] = new double[3];
            segment3[1] = new double[3];
            segment3[2] = new double[3];
            segment3[3] = new double[3];

            segment4[0] = new double[3];
            segment4[1] = new double[3];
            segment4[2] = new double[3];
            segment4[3] = new double[3];

            double gap = 0.1;

            segment3[0][0] = segment[0][0];
            segment3[0][1] = segment[0][1];
            segment3[0][2] = segment[0][2];

            segment3[1][0] = segment[1][0];
            segment3[1][1] = segment[1][1];
            segment3[1][2] = segment[1][2];

            segment3[2][0] = gap * segment[1][0];
            segment3[2][1] = gap * segment[1][1];
            segment3[2][2] = segment[1][2];

            segment3[3][0] = gap * segment[0][0];
            segment3[3][1] = gap * segment[0][1];
            segment3[3][2] = segment[0][2];

            /*
             * A - Pink
             * T - MistyRose
             * C - LightGreen
             * G - LightSeaGreen
            */

            if (w % 2 == 0)
            {
                
                if (gnome.Substring(stripCount, 1) == "A")
                    flag = 0;
                else if (gnome.Substring(stripCount, 1) == "T")
                    flag = 1;
                else if (gnome.Substring(stripCount, 1) == "C")
                    flag = 2;
                else if (gnome.Substring(stripCount, 1) == "G")
                    flag = 3;
                
                //flag = r.Next()%4;

                if (flag == 0)
                    strip[w * 2] = new Plane3D(segment3, Color.Pink);
                else if (flag == 1)
                    strip[w * 2] = new Plane3D(segment3, Color.MistyRose);
                else if (flag == 2)
                    strip[w * 2] = new Plane3D(segment3, Color.LightGreen);
                else if (flag == 3)
                    strip[w * 2] = new Plane3D(segment3, Color.LightSeaGreen);

                Program.addPlane(strip[w * 2].initPlane3D(), strip[w * 2].color);
            }

            segment4[0][0] = segment2[0][0];
            segment4[0][1] = segment2[0][1];
            segment4[0][2] = segment2[0][2];

            segment4[1][0] = segment2[1][0];
            segment4[1][1] = segment2[1][1];
            segment4[1][2] = segment2[1][2];

            segment4[2][0] = gap * segment2[1][0];
            segment4[2][1] = gap * segment2[1][1];
            segment4[2][2] = segment2[1][2];

            segment4[3][0] = gap * segment2[0][0];
            segment4[3][1] = gap * segment2[0][1];
            segment4[3][2] = segment2[0][2];

            if (w % 2 == 0)
            {
                if (flag == 0)
                    strip[w * 2 + 1] = new Plane3D(segment4, Color.MistyRose);
                else if (flag == 1)
                    strip[w * 2 + 1] = new Plane3D(segment4, Color.Pink);
                else if (flag == 2)
                    strip[w * 2 + 1] = new Plane3D(segment4, Color.LightSeaGreen);
                else if (flag == 3)
                    strip[w * 2 + 1] = new Plane3D(segment4, Color.LightGreen);

                Program.addPlane(strip[w * 2 + 1].initPlane3D(), strip[w * 2 + 1].color);
            }
            if (w % (int)(1 / inc) == 0)
                stripCount++;
        }
    }

    void seek(double[] move)
    {
        int i, j;

        for (i = 0; i < 40; i++)
            shell[i].seek(move);
    }

    public void rotateX(double degree, double[] COM)
    {
        int i;

        for (i = 0; i < 20 - 1; i++)
        {
            shell[i].rotateX(degree, COM);
            shell2[i].rotateX(degree, COM);
            strip[i*2].rotateX(degree, COM);
            strip[i * 2 + 1].rotateX(degree, COM);
        }
        
    }

    void rotateY(double degree, double[] COM)
    {
        int i, j;

        for (i = 0; i < 40; i++)
            shell[i].rotateY(degree, COM);
    }

    void rotateZ(double degree, double[] COM)
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
=======
﻿using System;
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

public class BiologyDNA
{
 Plane3D[] shell = new Plane3D[4000];
    Plane3D[] shell2 = new Plane3D[4000];
    Plane3D[] strip = new Plane3D[8000];

    double[][] path = new double[4100][];
    int index = 0;

    string gnome;
    public BiologyDNA(String gnome)
    {
        this.gnome = gnome;
    }

    public void initDNA()
    {
        //function end.
        double z;
        double inc = 0.2;

        for (z = -(gnome.Length/2); z < gnome.Length/2; z += inc)
        {
            path[index] = new double[3];

            path[index][0] = 50 * Math.Cos(z);
            path[index][1] = 50 * Math.Sin(z);
            path[index][2] = 25 * z;
           
            index++;
        }

        /////////////////////////////////////////////////////////////////////////

        double[][] segment = new double[4][];
        double[][] segment2 = new double[4][];
        double[][] segment3 = new double[4][];
        double[][] segment4 = new double[4][];
        double dTheta = 0.26;
        double cos = Math.Cos(dTheta);
        double sin = Math.Sin(dTheta);

        Random r = new Random();
        int flag = 0;
        int stripCount = 0;

        for (int w = 0; stripCount < gnome.Length - 1; w++)
        {
            segment[0] = new double[3];
            segment[0][0] = path[w][0];
            segment[0][1] = path[w][1];
            segment[0][2] = path[w][2];

            segment[1] = new double[3];
            segment[1][0] = path[w][0];// * cos - segment[0][1] * sin;
            segment[1][1] = path[w][1];// * sin + segment[0][1] * cos;
            segment[1][2] = path[w][2];

            segment[2] = new double[3];
            segment[2][0] = path[w + 1][0];
            segment[2][1] = path[w + 1][1];
            segment[2][2] = path[w + 1][2];

            segment[3] = new double[3];
            segment[3][0] = path[w + 1][0];// * cos - segment[2][1] * sin;
            segment[3][1] = path[w + 1][1];// * sin + segment[2][1] * cos;
            segment[3][2] = path[w + 1][2];

            shell[w] = new Plane3D(segment, Color.DarkTurquoise);

            Program.addPlane(shell[w].initPlane3D(), shell[w].color);

            segment2[0] = new double[3];
            segment2[1] = new double[3];
            segment2[2] = new double[3];
            segment2[3] = new double[3];

            segment2[0][0] = -segment[0][0];
            segment2[0][1] = -segment[0][1];
            segment2[0][2] = segment[0][2];

            segment2[1][0] = -segment[1][0];
            segment2[1][1] = -segment[1][1];
            segment2[1][2] = segment[1][2];

            segment2[2][0] = -segment[2][0];
            segment2[2][1] = -segment[2][1];
            segment2[2][2] = segment[2][2];

            segment2[3][0] = -segment[3][0];
            segment2[3][1] = -segment[3][1];
            segment2[3][2] = segment[3][2];

            shell2[w] = new Plane3D(segment2, Color.DarkCyan);

            Program.addPlane(shell2[w].initPlane3D(),  shell2[w].color);

            segment3[0] = new double[3];
            segment3[1] = new double[3];
            segment3[2] = new double[3];
            segment3[3] = new double[3];

            segment4[0] = new double[3];
            segment4[1] = new double[3];
            segment4[2] = new double[3];
            segment4[3] = new double[3];

            double gap = 0.1;

            segment3[0][0] = segment[0][0];
            segment3[0][1] = segment[0][1];
            segment3[0][2] = segment[0][2];

            segment3[1][0] = segment[1][0];
            segment3[1][1] = segment[1][1];
            segment3[1][2] = segment[1][2];

            segment3[2][0] = gap * segment[1][0];
            segment3[2][1] = gap * segment[1][1];
            segment3[2][2] = segment[1][2];

            segment3[3][0] = gap * segment[0][0];
            segment3[3][1] = gap * segment[0][1];
            segment3[3][2] = segment[0][2];

            /*
             * A - Pink
             * T - MistyRose
             * C - LightGreen
             * G - LightSeaGreen
            */

            if (w % 2 == 0)
            {
                
                if (gnome.Substring(stripCount, 1) == "A")
                    flag = 0;
                else if (gnome.Substring(stripCount, 1) == "T")
                    flag = 1;
                else if (gnome.Substring(stripCount, 1) == "C")
                    flag = 2;
                else if (gnome.Substring(stripCount, 1) == "G")
                    flag = 3;
                
                //flag = r.Next()%4;

                if (flag == 0)
                    strip[w * 2] = new Plane3D(segment3, Color.Pink);
                else if (flag == 1)
                    strip[w * 2] = new Plane3D(segment3, Color.MistyRose);
                else if (flag == 2)
                    strip[w * 2] = new Plane3D(segment3, Color.LightGreen);
                else if (flag == 3)
                    strip[w * 2] = new Plane3D(segment3, Color.LightSeaGreen);

                Program.addPlane(strip[w * 2].initPlane3D(), strip[w * 2].color);
            }

            segment4[0][0] = segment2[0][0];
            segment4[0][1] = segment2[0][1];
            segment4[0][2] = segment2[0][2];

            segment4[1][0] = segment2[1][0];
            segment4[1][1] = segment2[1][1];
            segment4[1][2] = segment2[1][2];

            segment4[2][0] = gap * segment2[1][0];
            segment4[2][1] = gap * segment2[1][1];
            segment4[2][2] = segment2[1][2];

            segment4[3][0] = gap * segment2[0][0];
            segment4[3][1] = gap * segment2[0][1];
            segment4[3][2] = segment2[0][2];

            if (w % 2 == 0)
            {
                if (flag == 0)
                    strip[w * 2 + 1] = new Plane3D(segment4, Color.MistyRose);
                else if (flag == 1)
                    strip[w * 2 + 1] = new Plane3D(segment4, Color.Pink);
                else if (flag == 2)
                    strip[w * 2 + 1] = new Plane3D(segment4, Color.LightSeaGreen);
                else if (flag == 3)
                    strip[w * 2 + 1] = new Plane3D(segment4, Color.LightGreen);

                Program.addPlane(strip[w * 2 + 1].initPlane3D(), strip[w * 2 + 1].color);
            }
            if (w % (int)(1 / inc) == 0)
                stripCount++;
        }
    }

    void seek(double[] move)
    {
        int i, j;

        for (i = 0; i < 40; i++)
            shell[i].seek(move);
    }

    public void rotateX(double degree, double[] COM)
    {
        int i;

        for (i = 0; i < 20 - 1; i++)
        {
            shell[i].rotateX(degree, COM);
            shell2[i].rotateX(degree, COM);
            strip[i*2].rotateX(degree, COM);
            strip[i * 2 + 1].rotateX(degree, COM);
        }
        
    }

    void rotateY(double degree, double[] COM)
    {
        int i, j;

        for (i = 0; i < 40; i++)
            shell[i].rotateY(degree, COM);
    }

    void rotateZ(double degree, double[] COM)
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
>>>>>>> 9fe402ac798ef393c9b39749c089c6f74b01984d
}