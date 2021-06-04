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
    /// Interaction logic for GeometricProbabilityAnswerSheet.xaml
    /// </summary>
    public partial class GeometricProbabilityAnswerSheet : Window
    {
        public GeometricProbabilityAnswerSheet(GeometricProbability AnswerSheet)
        {
            InitializeComponent();
            WorkDisplay.Text = $"{AnswerSheet.Data}\n{AnswerSheet.Work}";
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            StartUpWindow newstart = new StartUpWindow();
            newstart.Show();
            Close();
        }
    }
}
