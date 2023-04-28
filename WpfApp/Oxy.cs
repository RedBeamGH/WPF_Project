using C_Sharp;
using OxyPlot;
using OxyPlot.Legends;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp
{
    public class OxyPlotModel
    {
        public PlotModel plotModel { get; private set; }
        SplineData data;
        RawData rawData;
        public OxyPlotModel(SplineData data, RawData rawData)
        {
            this.data = data;
            this.rawData = rawData;
            plotModel = new PlotModel { Title = "Spline Interpolation result" };
            AddSeries();
        }
        public void AddSeries()
        {
            plotModel.Series.Clear();
            Legend legend = new Legend();
            LineSeries lineSeries = new LineSeries();
            lineSeries.Color = OxyColors.Green;
            lineSeries.Title = "Spline interpolation";
            for (int i = 0; i < data.n; i++)
            {
                lineSeries.Points.Add(new DataPoint(data.Items[i].X, data.Items[i].Y));
            }
            plotModel.Legends.Add(legend);
            plotModel.Series.Add(lineSeries);
            Legend legend_rd = new Legend();
            LineSeries lineSeries_rd = new LineSeries();
            lineSeries_rd.Title = "Original function";
            lineSeries_rd.Color = OxyColors.Red;

            lineSeries_rd.MarkerType = MarkerType.Circle;
            lineSeries_rd.MarkerSize = 4;
            lineSeries_rd.MarkerStroke = OxyColors.Red;
            lineSeries_rd.MarkerFill = OxyColors.Red;
            for (int js = 0; js < rawData.n; js++)
            {
                lineSeries_rd.Points.Add(new DataPoint(rawData.Grid[js], rawData.Data[js]));
            }
            plotModel.Legends.Add(legend_rd);
            plotModel.Series.Add(lineSeries_rd);
        }
    }
}