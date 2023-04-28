using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using C_Sharp;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ViewData viewData = new ViewData(new  RawData(1, 2, 3, true, RawData.Cubic));

        public OxyPlotModel oxyPlotModel;

        public static RoutedCommand LoadFromControlsCommand = new RoutedCommand("LoadFromControlsCommand", typeof(MainWindow));
        public static RoutedCommand LoadFromFileCommand = new RoutedCommand("LoadFromFileCommand", typeof(MainWindow));
        public static RoutedCommand SaveCommand = new RoutedCommand("SaveCommand", typeof(MainWindow));


        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewData;
            comboBox_Enum.ItemsSource = Enum.GetValues(typeof(FRawEnum));
        }

        public void ShowData() 
        {
            try
            {
                ListBox_RawData.Items.Clear();
                for (int i = 0; i < viewData.nRaw; i++)
                {
                    ListBox_RawData.Items.Add($"x={viewData.rawData.Grid[i]:F2}; f(x)={viewData.rawData.Data[i]:F2}");
                }

                ListBox_SplineData.ItemsSource = viewData.splineData.Items.ToArray();

                TextBlock_Integral.Text = $"Значение интеграла: {viewData.splineData.IntegralValue:F2}";
                drawSpline();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void RadioButton_U(object sender, RoutedEventArgs e)
        {
            RB_NU.IsChecked = false;
            viewData.isUniform = true;
        }

        void RadioButton_NU(object sender, RoutedEventArgs e)
        {
            RB_U.IsChecked = false;
            viewData.isUniform = false;
        }

        private void SaveCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "RawData";
                dlg.DefaultExt = ".json";

                bool? result = dlg.ShowDialog();

                if (result == true)
                {
                    string filename = dlg.FileName;
                    viewData.Save(filename);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CanSaveCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            if (nRaw != null)
                e.CanExecute = !Validation.GetHasError(nRaw) && (viewData.rawData != null);
            else 
                e.CanExecute = true;
        }

        private void LoadFromControlsCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            viewData.ConstructSplines();
            ShowData();
        }

        private void CanLoadFromControlsCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            if (nRaw != null && nSpline != null)
                e.CanExecute = !(Validation.GetHasError(nRaw) || Validation.GetHasError(nSpline));
            else
                e.CanExecute = true;
        }

        private void LoadFromFileCommandHandler(object sender, ExecutedRoutedEventArgs e) 
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                bool? result = dlg.ShowDialog();

                if (result == true)
                {
                    string filename = dlg.FileName;
                    viewData.Load(filename);
                    viewData.left = viewData.rawData.left;
                    viewData.right = viewData.rawData.right;
                    viewData.nRaw = viewData.rawData.n;
                    viewData.splineData = new SplineData(viewData.rawData, viewData.Dirs[0], viewData.Dirs[1], viewData.nSpline);
                    int res = viewData.splineData.MakeSpline();
                    if (res != 0)
                    {
                        MessageBox.Show(res.ToString());
                    }
                    ShowData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CanLoadFromFileCommandHandler(object sender, CanExecuteRoutedEventArgs e) 
        {
            if (nSpline != null)
                e.CanExecute = !Validation.GetHasError(nSpline);
            else
                e.CanExecute = true;
        }

        private void drawSpline()
        {
            try
            {
                oxyPlotModel = new OxyPlotModel(viewData.splineData, viewData.rawData);
                SplinePlot.Model = oxyPlotModel.plotModel;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}