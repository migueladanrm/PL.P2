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
    /// Lógica de interacción para SurveyEditorWizard.xaml
    /// </summary>
    public partial class SurveyEditorWizard : Window
    {
        private string currentSurvey;
        private JObject survey;

        public SurveyEditorWizard()
        {
            InitializeComponent();

            LayoutSecondary.Visibility = Visibility.Collapsed;
        }

        public SurveyEditorWizard(string surveyId)
        {
            InitializeComponent();

            LayoutSecondary.Visibility = Visibility.Collapsed;

            currentSurvey = surveyId;
            LoadData();
        }

        private void LoadData()
        {
            survey = JObject.Parse(Workspace.SurveyGet(currentSurvey).ToString());
            TxtSurveyName.Text = (string)survey["name"];
        }

        private void BtnNewItem_Click(object sender, RoutedEventArgs e)
        {
            LayoutSecondary.Visibility = Visibility.Visible;
            SurveyItemTitle.Focus();

            SetupSurveyItemEditor();
        }

        private void SetupSurveyItemEditor()
        {
            SurveyItemTitle.Focus();

            SurveyItemTitle.Clear(); 
            RbnSurveyItemModeChoice.IsChecked = true;

            DlgNewItemNS1Caption.Text = string.Empty;
            DlgNewItemNS2Caption.Text = string.Empty;
            DlgNewItemNS3Caption.Text = string.Empty;
            DlgNewItemNS4Caption.Text = string.Empty;
            DlgNewItemNS5Caption.Text = string.Empty;
        }

        private void BtnHideLS_Click(object sender, RoutedEventArgs e)
        {
            HideLS();
        }

        private void HideLS()
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

        private void DlgNewItemBtnCreateItem_Click(object sender, RoutedEventArgs e)
        {
            if (RbnSurveyItemModeChoice.IsChecked.Value) {

            }

            if (RbnSurveyItemModeNumericScale.IsChecked.Value) {
                var item = new JObject {
                    { "caption", SurveyItemTitle.Text },
                    { "item_type", 2 },
                    { "content", new JArray {
                        string.IsNullOrWhiteSpace(DlgNewItemNS1Caption.Text) ? DlgNewItemNS1Caption.Watermark.ToString() : DlgNewItemNS1Caption.Text,
                        string.IsNullOrWhiteSpace(DlgNewItemNS2Caption.Text) ? DlgNewItemNS2Caption.Watermark.ToString() : DlgNewItemNS2Caption.Text,
                        string.IsNullOrWhiteSpace(DlgNewItemNS3Caption.Text) ? DlgNewItemNS3Caption.Watermark.ToString() : DlgNewItemNS3Caption.Text,
                        string.IsNullOrWhiteSpace(DlgNewItemNS4Caption.Text) ? DlgNewItemNS4Caption.Watermark.ToString() : DlgNewItemNS4Caption.Text,
                        string.IsNullOrWhiteSpace(DlgNewItemNS5Caption.Text) ? DlgNewItemNS5Caption.Watermark.ToString() : DlgNewItemNS5Caption.Text
                        }
                    }
                };

                //Workspace.SurveyItemAdd(item);

                var control = new SurveyEditorItem(item);

                StkSurveyItems.Children.Add(control);
            }

            HideLS();
        }
    }
}