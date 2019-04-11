using Newtonsoft.Json.Linq;
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

namespace SurveyCenter.UI
{
    /// <summary>
    /// Lógica de interacción para SurveyStatistics.xaml
    /// </summary>
    public partial class SurveyStatistics : Window
    {
        private JObject survey;
        private JArray surveyResponses;

        public SurveyStatistics()
        {
            InitializeComponent();
        }

        public SurveyStatistics(string surveyId)
        {
            InitializeComponent();

            survey = Workspace.SurveyGet(surveyId);
            surveyResponses = Workspace.SurveyResponsesGet(surveyId);
        }

        private void LoadStatistics()
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new Home().Show();
        }
    }
}
