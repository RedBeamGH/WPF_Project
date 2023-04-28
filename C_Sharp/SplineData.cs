using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp
{
    public class SplineData
    {
        public RawData rawData;
        public int n;
        public List<SplineDataItem> Items;
        public double IntegralValue;
        public double leftD;
        public double rightD;

        public SplineData(RawData _rawData, double _leftD, double _rightD, int _n) 
        {
            rawData = _rawData;
            n = _n;
            Items = new List<SplineDataItem>();
            IntegralValue = 0;
            leftD = _leftD;
            rightD = _rightD;
        }

        public int MakeSpline() 
        {
            int ret = 0;
            double[] y = rawData.Data;

            int nx = rawData.n;

            double[] scoeff = new double[4 * (nx - 1)];

            int nsite = n;

            double[] bc = new double[2] { leftD, rightD };

            double[] site = new double[2] { rawData.left, rawData.right };

            int[] dorder = new int[3] { 1, 1, 1 };

            double[] result = new double[3 * nsite];

            double[] x = rawData.Grid;

            double[] leftLim = new double[1] { rawData.left };
            double[] rightLim = new double[1] { rawData.right };

            double[] intRes = new double[1];

            CubicInterpolate(nx, 1, x, y, bc, scoeff, nsite, site, 3, dorder, result, ref ret, leftLim, rightLim, intRes);
            if (ret != 0)
            {
                return ret;
            }

            IntegralValue = intRes[0];

            for (int i = 0; i < n; i++)
            {
                int idx = 3 * i;
                double X = (rawData.left * (n - i - 1) + rawData.right * i) / (n - 1);
                Items.Add(new SplineDataItem(X, result[idx], result[idx + 1], result[idx + 2]));
            }
            return 0;
        }

        [DllImport("..\\..\\..\\..\\x64\\DEBUG\\DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void CubicInterpolate(int nx, int ny, double[] x, double[] y, double[] bc, double[] scoeff,
            int nsite, double[] site, int ndorder, int[] dorder, double[] result, ref int ret,
            double[] leftLim, double[] rightLim, double[] intRes);

    }
}
