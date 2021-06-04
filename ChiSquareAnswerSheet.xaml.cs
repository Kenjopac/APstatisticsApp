using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using ApStatToolsLibrary;
namespace ApStatisticsApplicationUI
{
    /// <summary>
    /// Interaction logic for ChiSquareAnswerSheet.xaml
    /// </summary>
    public partial class ChiSquareAnswerSheet : Window
    {
        
        public ChiSquareAnswerSheet(ChiSquareStats newstatistic)
        {
            InitializeComponent();
            TestTypeDisplay.Text = $"Chi Square {newstatistic.TestType} test";
            
            NullHypothesisDisplay.Text = newstatistic.NullHypothesis;
            AltHypothesisDisplay.Text = newstatistic.AlternativeHypothesis;
            WriteChiSquareWork(newstatistic.Observed, newstatistic.Expected);
            ChiSquareStatisticDisplay.Text = $"Chi Square Statistic: {newstatistic.ChiSquareStatistic}";
            DegreesOfFreedomDisplay.Text = $"Degrees of Freedom: {newstatistic.DegreesOfFreedom}";
            AlphaLevelDisplay.Text = $"Alpha level: {newstatistic.SignificanceLevel}";
            PValueDisplay.Text = $"P value: {newstatistic.Pval}";
            ConclusionDisplay.Text = newstatistic.Conclusion.ToString();

        }
        private void WriteChiSquareWork(List<double> Observed, List<double> Alternative)
        {
            StringBuilder str = new StringBuilder();
            if ( Alternative.Count == 1) { 
                foreach (double obs in Observed)
                {
                str.Append($" ({obs} - {Alternative[0]})^2 / {Alternative[0]} +");
                }
                ChiSquStatWorkDisplay.Text = str.Remove(str.Length - 1, 1).ToString();
            } else
            {
                for (int i = 0; i < Observed.Count; i++)
                {
                    str.Append($" ({Observed[i]} - {Alternative[i]})^2 / {Alternative[i]} +");
                    
                }
                ChiSquStatWorkDisplay.Text = str.Remove(str.Length - 1, 1).ToString();
            }
        }

        private void ReOpen_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Close();
            mainWindow.Show();
        }
    }
}
