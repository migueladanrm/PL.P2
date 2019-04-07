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
                    var hello = RT.var("surveycenter", "hello");
                    hello.invoke();
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
    }
}