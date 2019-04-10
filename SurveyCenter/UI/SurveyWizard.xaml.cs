using Newtonsoft.Json.Linq;
using SurveyCenter.UI.Controls;
using System.Windows;

namespace SurveyCenter.UI
{
    /// <summary>
    /// Lógica de interacción para SurveyWizard.xaml
    /// </summary>
    public partial class SurveyWizard : Window
    {
        private JObject survey;

        public SurveyWizard()
        {
            InitializeComponent();
        }

        public SurveyWizard(string surveyId)
        {
            InitializeComponent();

            LayoutSecondary.Visibility = Visibility.Collapsed;

            LoadSurvey(surveyId);
        }

        private void LoadSurvey(string surveyId)
        {
            StkSurveyItems.Children.Clear();

            survey = Workspace.SurveyGet(surveyId);
            TxtSurveyName.Text = (string)survey["name"];

            int count = 1;

            foreach(JObject item in survey["items"]) {
                var ctrl = new SurveyItem(count,item);
                StkSurveyItems.Children.Add(ctrl);

                count++;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new Home().Show();
        }
    }
}
