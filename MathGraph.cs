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
using System.ComponentModel;
using System.Text;

public class MathGraph
{
    double[][][][] mesh;
    int xNumber, yNumber;
    double scaleD;
    int scale;
    int initPoint;

    double[][] data;

    public MathGraph(int xNumber, int yNumber, double scale)
    {
        this.xNumber = xNumber;
        this.yNumber = yNumber;
        this.scaleD = scale;
        this.scale = (int)scale;
        mesh = new double[(int)(2 * xNumber / scale)][][][];
        data = new double[(int)(2 * xNumber / scale)][];
    }

    public void initMathGraph()
    {
        this.initPoint = Program.pointCount;

        double i, j;
        int x, y;

        for (i = -xNumber, x = 0; i < xNumber - 1; i += scaleD, x++)
        {
            data[x] = new double[(int)(2 * xNumber / scaleD)];

            for (j = -yNumber, y = 0; j < yNumber - 1; j += scaleD, y++)
            {
                data[x][y] = Math.Abs(Math.Pow(100, -Math.Pow(0.0008 * i, 2) - Math.Pow(0.0008 * j, 2)) * 5 * Math.Cos(Math.Pow(0.16 * i, 2) + Math.Pow(0.16 * j, 2))); ;
                //SIN...Math.Abs(Math.Pow(100, -Math.Pow(0.001 * i, 2) - Math.Pow(0.001 * j, 2)) * 5 * Math.Cos(1.57 + Math.Pow(0.16 * i, 2) + Math.Pow(0.16 * j, 2)));
                //COS...Math.Abs(Math.Pow(100, -Math.Pow(0.0008 * i, 2) - Math.Pow(0.0008 * j, 2)) * 5 * Math.Cos(Math.Pow(0.16 * i, 2) + Math.Pow(0.16 * j, 2)));
                //Math.Abs(Math.Pow(100, -Math.Pow(0.0008 * i, 2) - Math.Pow(0.0008 * j, 2)) * 200 * Math.Cos(Math.Pow(0.16 * i, 2) + Math.Pow(0.16 * j, 2)));
                //(10000000000 * i * Math.Exp(-(x ^ 2) - (y ^ 2))) % 1000; 
                //0.000001 *i * Math.Exp(-(x ^ 2) - (y ^ 2));
            }
        }
        //(1+Math.Pow(-Math.Pow(0.5*i, 2) - -Math.Pow(0.5*j,2), 2) )*
        //Math.Pow(100, -Math.Pow(0.8*i, 2) - Math.Pow(0.8*j,2)) * 500*Math.Cos(Math.Pow(0.6*i,2) + Math.Pow(0.6*j, 2));
        //10+500*Math.pow(1000, -Math.abs(0.02*i)-Math.abs(0.02*j)) *Math.Cos(Math.abs(i) + Math.abs(j));
        //on 2nd sub:data[x][y] = Math.Abs(Math.Pow(100, -Math.Pow(0.0008 * i, 2) - Math.Pow(0.0008 * j, 2)) * 200 * Math.Cos(Math.Pow(0.16 * i, 2) + Math.Pow(0.16 * j, 2)));

        double iD, jD;
        for (iD = -xNumber, x = 0; iD < xNumber - scaleD - 2; iD += scaleD, x++)
        {
            mesh[x] = new double[(int)(2 * xNumber / scaleD)][][];

            for (jD = -yNumber, y = 0; jD < yNumber - scaleD - 2; jD += scaleD, y++)
            {
                mesh[x][y] = new double[4][];
                mesh[x][y][0] = new double[3];
                mesh[x][y][1] = new double[3];
                mesh[x][y][2] = new double[3];
                mesh[x][y][3] = new double[3];

                mesh[x][y][0][0] = iD;
                mesh[x][y][0][1] = jD;
                mesh[x][y][0][2] = data[x][y];

                mesh[x][y][1][0] = (iD + scaleD);
                mesh[x][y][1][1] = jD;
                mesh[x][y][1][2] = data[x + 1][y];

                mesh[x][y][2][0] = (iD + scaleD);
                mesh[x][y][2][1] = (jD + scaleD);
                mesh[x][y][2][2] = data[x + 1][y + 1];

                mesh[x][y][3][0] = iD;
                mesh[x][y][3][1] = (jD + scaleD);
                mesh[x][y][3][2] = data[x][y + 1];

                Program.addPlane(mesh[x][y], Color.Cyan);
            }
        }
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
using System.ComponentModel;
using System.Text;

public class MathGraph
{
    double[][][][] mesh;
    int xNumber, yNumber;
    double scaleD;
    int scale;
    int initPoint;

    double[][] data;

    public MathGraph(int xNumber, int yNumber, double scale)
    {
        this.xNumber = xNumber;
        this.yNumber = yNumber;
        this.scaleD = scale;
        this.scale = (int)scale;
        mesh = new double[(int)(2 * xNumber / scale)][][][];
        data = new double[(int)(2 * xNumber / scale)][];
    }

    public void initMathGraph()
    {
        this.initPoint = Program.pointCount;

        double i, j;
        int x, y;

        for (i = -xNumber, x = 0; i < xNumber - 1; i += scaleD, x++)
        {
            data[x] = new double[(int)(2 * xNumber / scaleD)];

            for (j = -yNumber, y = 0; j < yNumber - 1; j += scaleD, y++)
            {
                data[x][y] = Math.Abs(Math.Pow(100, -Math.Pow(0.0008 * i, 2) - Math.Pow(0.0008 * j, 2)) * 5 * Math.Cos(Math.Pow(0.16 * i, 2) + Math.Pow(0.16 * j, 2))); ;
                //SIN...Math.Abs(Math.Pow(100, -Math.Pow(0.001 * i, 2) - Math.Pow(0.001 * j, 2)) * 5 * Math.Cos(1.57 + Math.Pow(0.16 * i, 2) + Math.Pow(0.16 * j, 2)));
                //COS...Math.Abs(Math.Pow(100, -Math.Pow(0.0008 * i, 2) - Math.Pow(0.0008 * j, 2)) * 5 * Math.Cos(Math.Pow(0.16 * i, 2) + Math.Pow(0.16 * j, 2)));
                //Math.Abs(Math.Pow(100, -Math.Pow(0.0008 * i, 2) - Math.Pow(0.0008 * j, 2)) * 200 * Math.Cos(Math.Pow(0.16 * i, 2) + Math.Pow(0.16 * j, 2)));
                //(10000000000 * i * Math.Exp(-(x ^ 2) - (y ^ 2))) % 1000; 
                //0.000001 *i * Math.Exp(-(x ^ 2) - (y ^ 2));
            }
        }
        //(1+Math.Pow(-Math.Pow(0.5*i, 2) - -Math.Pow(0.5*j,2), 2) )*
        //Math.Pow(100, -Math.Pow(0.8*i, 2) - Math.Pow(0.8*j,2)) * 500*Math.Cos(Math.Pow(0.6*i,2) + Math.Pow(0.6*j, 2));
        //10+500*Math.pow(1000, -Math.abs(0.02*i)-Math.abs(0.02*j)) *Math.Cos(Math.abs(i) + Math.abs(j));
        //on 2nd sub:data[x][y] = Math.Abs(Math.Pow(100, -Math.Pow(0.0008 * i, 2) - Math.Pow(0.0008 * j, 2)) * 200 * Math.Cos(Math.Pow(0.16 * i, 2) + Math.Pow(0.16 * j, 2)));

        double iD, jD;
        for (iD = -xNumber, x = 0; iD < xNumber - scaleD - 2; iD += scaleD, x++)
        {
            mesh[x] = new double[(int)(2 * xNumber / scaleD)][][];

            for (jD = -yNumber, y = 0; jD < yNumber - scaleD - 2; jD += scaleD, y++)
            {
                mesh[x][y] = new double[4][];
                mesh[x][y][0] = new double[3];
                mesh[x][y][1] = new double[3];
                mesh[x][y][2] = new double[3];
                mesh[x][y][3] = new double[3];

                mesh[x][y][0][0] = iD;
                mesh[x][y][0][1] = jD;
                mesh[x][y][0][2] = data[x][y];

                mesh[x][y][1][0] = (iD + scaleD);
                mesh[x][y][1][1] = jD;
                mesh[x][y][1][2] = data[x + 1][y];

                mesh[x][y][2][0] = (iD + scaleD);
                mesh[x][y][2][1] = (jD + scaleD);
                mesh[x][y][2][2] = data[x + 1][y + 1];

                mesh[x][y][3][0] = iD;
                mesh[x][y][3][1] = (jD + scaleD);
                mesh[x][y][3][2] = data[x][y + 1];

                Program.addPlane(mesh[x][y], Color.Cyan);
            }
        }
    }
>>>>>>> 9fe402ac798ef393c9b39749c089c6f74b01984d
}