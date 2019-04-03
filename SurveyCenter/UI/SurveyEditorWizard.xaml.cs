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
    /// Lógica de interacción para SurveyEditorWizard.xaml
    /// </summary>
    public partial class SurveyEditorWizard : Window
    {
        public SurveyEditorWizard()
        {
            InitializeComponent();

            LayoutSecondary.Visibility = Visibility.Collapsed;
            TbxSurveyTitle.Focus();
        }

        private void BtnNewItem_Click(object sender, RoutedEventArgs e)
        {
            LayoutSecondary.Visibility = Visibility.Visible;
            SurveyItemTitle.Focus();

            SetupSurveyItemEditor();
        }

        private void SetupSurveyItemEditor()
        {
            SurveyItemTitle.Clear();
            RbnSurveyItemModeChoice.IsChecked = true;
        }

        private void BtnHideLS_Click(object sender, RoutedEventArgs e)
        {
            LayoutSecondary.Visibility = Visibility.Collapsed;
        }

        private void RbnSurveyItemMode_Checked(object sender, RoutedEventArgs e)
        {
            string id = (e.Source as RadioButton).Tag.ToString();

            if (id.Equals("opt.choice")) {
                SurveyItemChoiceMode.Visibility = Visibility.Visible;
                SurveyItemNumericScaleMode.Visibility = Visibility.Collapsed;
            }

            if (id.Equals("opt.numericscale")) {
                SurveyItemNumericScaleMode.Visibility = Visibility.Visible;
                SurveyItemChoiceMode.Visibility = Visibility.Collapsed;
            }
        }

        private void RbnSurveyItemMode_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}