using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp
{
    public class SplineDataItem
    {

		public double X { get; set; }
        public double Y { get; set; }
        public double dY { get; set; }
        public double ddY { get; set; }

        public SplineDataItem(double _X, double _Y, double _dY, double _ddY ) 
        {
            X = _X;
            Y = _Y;
            dY = _dY;
            ddY = _ddY;
        }

        public override string ToString()
        {
            return X.ToString("F2") + " " + Y.ToString("F2") + " " + dY.ToString("F2") + " " + ddY.ToString("F2");
        }
    }
}
