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
                    break;
                case "Action.CreateSurvey":
                    new SurveyEditorWizard().Show();
                    Close();
                    break;
                case "Action.ViewStats":
                    var hello = RT.var("surveycenter", "hello");
                    hello.invoke();
                    break;
            }
        }
    }
}