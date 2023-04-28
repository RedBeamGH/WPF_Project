using System.Text.Json;

namespace C_Sharp
{
    [Serializable]
    public class RawData
    {
        public double left { get; set; }
        public double right { get; set; }
        public int n { get; set; }
        [NonSerialized]
        public FRaw func;
        public double[] Grid { get; set; }
        public double[] Data { get; set; }

        public RawData(double _left, double _right, int _n, bool isUniform, FRaw _func) 
        {
            left = _left;
            right = _right;
            n = _n;
            func = _func;
            Grid = new double[n];
            Data = new double[n];
            if (isUniform)
            {
                for (int i = 0; i < n; i++)
                {
                    double x = (left * (n - i - 1) + right * i) / (n - 1);
                    Grid[i] = x;
                    Data[i] = func(x);
                }
            }
            else 
            {
                var r = new Random();
                for (int i = 0; i < n; i++)
                {
                    Grid[i] = r.NextDouble()*(right - left) + left;
                }
                Array.Sort(Grid);
                for (int i = 0; i < n; i++)
                {
                    Data[i] = func(Grid[i]);
                }
            }
        }

        public RawData(string filename) 
        {
            RawData rawData;
            Load(filename, out rawData);

            left = rawData.left;
            right = rawData.right;
            n = rawData.n;
            func = rawData.func;
            Grid = rawData.Grid;
            Data = rawData.Data;
        }
        public RawData() { }

        public static double Linear(double x) => 3 * x - 1;

        public static double Cubic(double x) => 2 * x * x * x - 3 * x * x + 4 * x - 5;

        public static double Rand(double x) => (new Random()).NextDouble()*50-25;

        public void Save(string filename) 
        {
            string jsonString = JsonSerializer.Serialize(this);
            File.WriteAllText(filename, jsonString);
        }

        public static void Load(string filename, out RawData rawData) 
        {
            string jsonString = File.ReadAllText(filename);
            rawData = JsonSerializer.Deserialize<RawData>(jsonString);
        }

        public override string ToString()
        {
            return left.ToString() + " " + right.ToString() + " " + n.ToString();
        }
    }
}