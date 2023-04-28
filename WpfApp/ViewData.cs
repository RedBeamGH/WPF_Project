using C_Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp
{
    public class ViewData : IDataErrorInfo
    {
        // RawData 

        public double left { get; set; }
        public double right { get; set; }
        public int nRaw { get; set; }
        public bool isUniform { get; set; }
        public FRaw func { get; set; }
        public FRawEnum fRawEnum { get; set; }
        public RawData? rawData;

        // SplineData
        public int nSpline { get; set; }
        public double[]? Dirs { get; set; }
        public double rightD { get; set; }
        public SplineData? splineData { get; set; }


        public ViewData() {}

        public ViewData(RawData r) 
        {
            left = r.left;
            right = r.right;
            nRaw = r.n;
            func = r.func;
            nSpline = 5;
            Dirs = new double[2] {0, 0};
        }

        public void ConstructSplines() 
        {
            try
            {
                if (fRawEnum == FRawEnum.Linear) func = RawData.Linear;
                else if (fRawEnum == FRawEnum.Cubic) func = RawData.Cubic;
                else func = RawData.Rand;
                rawData = new RawData(left, right, nRaw, isUniform, func);
                splineData = new SplineData(rawData, Dirs[0], Dirs[1], nSpline);
                int res = splineData.MakeSpline();
                if (res != 0) 
                {
                    MessageBox.Show(res.ToString());
                }
            }
            catch (DllNotFoundException ex) 
            {
                MessageBox.Show(ex.Message);
                foreach (var item in ex.Data.Values)
                {
                    MessageBox.Show(item.ToString());
                }
            } 
        }

        public void Save(string filename) 
        {
            try 
            {
                if (rawData == null) 
                {
                    MessageBox.Show("Перед сохранением необходимо посчитать сплайн");
                    return;
                }

                rawData.Save(filename);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Load(string filename) 
        {
            try
            {
                RawData.Load(filename, out rawData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public override string ToString()
        {
            return $"RawData = {rawData}\n" +
                   $"SplineData = {splineData}\n" +
                   $"left = {left}\n" +
                   $"right = {right}\n" +
                   $"nRaw = {nRaw}\n" +
                   $"nRaw = {nSpline}\n" +
                   //$"DirsL = {Dirs[0]}\n" +
                   //$"DirsR = {Dirs[1]}\n" +
                   $"fRaw = {fRawEnum}\n";
        }

        public string Error => string.Empty;

        public string this[string columnName]
        {
            get
            {
                string errorMessage = string.Empty;

                switch (columnName)
                {
                    case "nRaw":
                        if (nRaw < 3)
                        {
                            errorMessage = "The number of nodes in the grid must be at least 3";
                        }
                        break;

                    case "nSpline":
                        if (nSpline < 2)
                        {
                            errorMessage = "The number of nodes in the grid must be at least 2";
                        }
                        break;

                    case "left":
                    case "right":
                        if (left >= right)
                        {
                            errorMessage = "The left endpoint must be less than the right endpoint";
                        }
                        break;
                }

                return errorMessage;
            }
        }

    }
}
