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

namespace ApStatisticsApplicationUI
{
    /// <summary>
    /// Interaction logic for StartUpWindow.xaml
    /// </summary>
    public partial class StartUpWindow : Window
    {
        public StartUpWindow()
        {
            InitializeComponent();
        }

        private void ChiSquButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newmain = new MainWindow();
            newmain.Show();
            Close();
        }

        private void BinomialProbability_Click(object sender, RoutedEventArgs e)
        {
            AllWindows.BinomialProbabilityQuestionSheet newbinom = new AllWindows.BinomialProbabilityQuestionSheet();
            newbinom.Show();
            Close();
        }

        private void ZIntervals_Click(object sender, RoutedEventArgs e)
        {
            ZIntervalQuestionSheet newquest = new ZIntervalQuestionSheet();
            newquest.Show();
            Close();
        }

        private void TIntervals_Click(object sender, RoutedEventArgs e)
        {
            TIntervalQuestionSheet newT = new TIntervalQuestionSheet();
            newT.Show();
            Close();
        }

        private void ZTests_Click(object sender, RoutedEventArgs e)
        {
            ZTestsQuestionSheet newZ = new ZTestsQuestionSheet();
            newZ.Show();
            Close();
        }

        private void TTests_Click(object sender, RoutedEventArgs e)
        {
            TTestsQuestionSheet newt = new TTestsQuestionSheet();
            newt.Show();
            Close();
        }

        private void GeometricProbability_Click(object sender, RoutedEventArgs e)
        {
            GeometricProbabilityQuestionSheet newg = new GeometricProbabilityQuestionSheet();
            newg.Show();
            Close();
        }
    }
}
