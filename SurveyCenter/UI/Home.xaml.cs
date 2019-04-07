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
using clojure.lang;
using Newtonsoft.Json.Linq;

namespace SurveyCenter.UI
{
    /// <summary>
    /// Lógica de interacción para Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();

            LayoutSecondary.Visibility = Visibility.Collapsed;
        }

        private void BtnStartAction_Click(object sender, RoutedEventArgs e)
        {
            if (!(e.Source is Button))
                return;

            var tag = (e.Source as Button).Tag.ToString();

            switch (tag) {
                default:
                    break;
                case "Action.AnswerSurvey":
                    ShowLSDialog(nameof(LSAnswerSurvey));
                    break;
                case "Action.CreateSurvey":
                    ShowLSDialog(nameof(LSNewSurvey));
                    break;
                case "Action.ViewStats":
                    Console.WriteLine("No action implemented!");
                    break;
            }
        }

        private void LSHide(object sender, RoutedEventArgs e)
        {
            LayoutSecondary.Visibility = Visibility.Collapsed;
        }

        private void ShowLSDialog(string id)
        {
            LayoutSecondary.Visibility = Visibility.Visible;

            foreach (FrameworkElement child in LayoutSecondary.Children)
                child.Visibility = Visibility.Collapsed;

            switch (id) {
                case nameof(LSNewSurvey):
                    LSNewSurvey.Visibility = Visibility.Visible;
                    LSNewSurveyTbxSurveyName.Clear();
                    LSNewSurveyTbxSurveyName.Focus();
                    break;
                case nameof(LSAnswerSurvey):
                    LSAnswerSurvey.Visibility = Visibility.Visible;
                    LSAnswerSurveyStkAvailableSurveys.Children.Clear();

                    foreach (JObject survey in Workspace.SurveyLibraryGet()) {
                        var lbl = new Label() {
                            Content = (string)survey["name"],
                            Style = (Style)FindResource("LSAnswerSurveyLblSurvey"),
                            Tag = (string)survey["id"]
                        };
                        lbl.MouseLeftButtonDown += LSNewSurveyLblSurvey_MouseLeftButtonDown;

                        LSAnswerSurveyStkAvailableSurveys.Children.Add(lbl);
                    }
                    break;
            }
        }

        private void LSDialogNewSurveyBtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (LSNewSurveyTbxSurveyName.Text.Length == 0 || string.IsNullOrWhiteSpace(LSNewSurveyTbxSurveyName.Text)) {
                MessageBox.Show("Introdúzca un nombre válido para la encuesta.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                LSNewSurveyTbxSurveyName.Focus();
                return;
            }

            var surveyId = Workspace.SurveyNew(LSNewSurveyTbxSurveyName.Text);
            new SurveyEditorWizard(surveyId).Show();

            Close();
        }

        private void LSNewSurveyLblSurvey_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var surveyId = (e.Source as FrameworkElement).Tag.ToString();
            Console.WriteLine(surveyId);
        }
    }
}