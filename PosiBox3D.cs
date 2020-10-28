<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PosiBox3D
/// </summary>
public class PosiBox3D
{
        double[][] PosBox3D = new double[8][];//3
        int initPoint;

        public PosiBox3D(double[][] PosBox3D)
        {
            this.PosBox3D = PosBox3D;
        }

        public double[][] initPosiBox3D()
        {
            initPoint = Program.pointCount;

            return this.PosBox3D;
        }

        double[][] getPosiBox3D()
        {
            return this.PosBox3D;
        }

        void seek(double[] move)
        {
            for (int i = 0; i < 8; i++)
            {
                PosBox3D[i] = new double[3];

                PosBox3D[i][0] += move[0];
                PosBox3D[i][0] += move[0];
                PosBox3D[i][0] += move[0];

                PosBox3D[i][1] += move[1];
                PosBox3D[i][1] += move[1];
                PosBox3D[i][1] += move[1];

                PosBox3D[i][2] += move[2];
                PosBox3D[i][2] += move[2];
                PosBox3D[i][2] += move[2];
            }
        }

        double[] getCorner(int n)
        {
            return PosBox3D[n];
        }

        double[] getCOM()
        {
            double[] COM = new double[3];

            COM[0] = (PosBox3D[0][0] + PosBox3D[1][0] + PosBox3D[2][0] + PosBox3D[3][0] + PosBox3D[4][0] + PosBox3D[5][0] + PosBox3D[6][0] + PosBox3D[7][0]) / 8;
            COM[1] = (PosBox3D[0][1] + PosBox3D[1][1] + PosBox3D[2][1] + PosBox3D[3][1] + PosBox3D[4][1] + PosBox3D[5][1] + PosBox3D[6][1] + PosBox3D[7][1]) / 8;
            COM[2] = (PosBox3D[0][2] + PosBox3D[1][2] + PosBox3D[2][2] + PosBox3D[3][2] + PosBox3D[4][2] + PosBox3D[5][2] + PosBox3D[6][2] + PosBox3D[7][2]) / 8;

            return COM;
        }
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PosiBox3D
/// </summary>
public class PosiBox3D
{
        double[][] PosBox3D = new double[8][];//3
        int initPoint;

        public PosiBox3D(double[][] PosBox3D)
        {
            this.PosBox3D = PosBox3D;
        }

        public double[][] initPosiBox3D()
        {
            initPoint = Program.pointCount;

            return this.PosBox3D;
        }

        double[][] getPosiBox3D()
        {
            return this.PosBox3D;
        }

        void seek(double[] move)
        {
            for (int i = 0; i < 8; i++)
            {
                PosBox3D[i] = new double[3];

                PosBox3D[i][0] += move[0];
                PosBox3D[i][0] += move[0];
                PosBox3D[i][0] += move[0];

                PosBox3D[i][1] += move[1];
                PosBox3D[i][1] += move[1];
                PosBox3D[i][1] += move[1];

                PosBox3D[i][2] += move[2];
                PosBox3D[i][2] += move[2];
                PosBox3D[i][2] += move[2];
            }
        }

        double[] getCorner(int n)
        {
            return PosBox3D[n];
        }

        double[] getCOM()
        {
            double[] COM = new double[3];

            COM[0] = (PosBox3D[0][0] + PosBox3D[1][0] + PosBox3D[2][0] + PosBox3D[3][0] + PosBox3D[4][0] + PosBox3D[5][0] + PosBox3D[6][0] + PosBox3D[7][0]) / 8;
            COM[1] = (PosBox3D[0][1] + PosBox3D[1][1] + PosBox3D[2][1] + PosBox3D[3][1] + PosBox3D[4][1] + PosBox3D[5][1] + PosBox3D[6][1] + PosBox3D[7][1]) / 8;
            COM[2] = (PosBox3D[0][2] + PosBox3D[1][2] + PosBox3D[2][2] + PosBox3D[3][2] + PosBox3D[4][2] + PosBox3D[5][2] + PosBox3D[6][2] + PosBox3D[7][2]) / 8;

            return COM;
        }
>>>>>>> 9fe402ac798ef393c9b39749c089c6f74b01984d
}