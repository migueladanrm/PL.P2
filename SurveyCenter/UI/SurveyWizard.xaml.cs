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

            foreach (JObject item in survey["items"]) {
                var ctrl = new SurveyItem(count, item);
                StkSurveyItems.Children.Add(ctrl);

                count++;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new Home().Show();
        }

        private void BtnFinish_Click(object sender, RoutedEventArgs e)
        {
            var surveyResponse = new JObject {
                { "survey", survey["id"] },
                { "results", null }
            };

            var results = new JArray();

            for (int i = 0; i < StkSurveyItems.Children.Count; i++) {
                var val = ((SurveyItem)StkSurveyItems.Children[i]).SelectedValue;
                if (val == -1) {
                    MessageBox.Show("¡Debe rellenar todos los campos de la encuesta!", "Encuesta incompleta", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                results.Add(val);
            }

            surveyResponse["results"] = results;

            Workspace.SurveyResponseSave(surveyResponse);

            MessageBox.Show("¡Gracias por su respuesta!", "Ha finalizado.", MessageBoxButton.OK, MessageBoxImage.Information);

            Close();
        }
    }
}