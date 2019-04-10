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
using Newtonsoft.Json.Linq;
using SurveyCenter.UI.Controls;

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
    }
}
