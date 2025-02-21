﻿using Newtonsoft.Json.Linq;
using SurveyCenter.UI.Controls;
using System.Windows;
using System.Windows.Controls;

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

            StkSurveyEditorItemChoiceModeItemsContainer.Children.Clear();

            var item1 = new SurveyEditorWizardSingleChoiceItemOption();
            var item2 = new SurveyEditorWizardSingleChoiceItemOption();
            item1.RequestDelete += ItemModeChoiceOptionRequestDelete;
            item2.RequestDelete += ItemModeChoiceOptionRequestDelete;

            StkSurveyEditorItemChoiceModeItemsContainer.Children.Add(item1);
            StkSurveyEditorItemChoiceModeItemsContainer.Children.Add(item2);
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

        private void ItemModeChoiceOptionRequestDelete(object sender)
        {
            if (StkSurveyEditorItemChoiceModeItemsContainer.Children.Count < 3) {
                MessageBox.Show("El item debe tener al menos dos opciones.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            } else {
                if (!(sender is SurveyEditorWizardSingleChoiceItemOption))
                    return;

                foreach (FrameworkElement child in StkSurveyEditorItemChoiceModeItemsContainer.Children) {
                    if (child == (FrameworkElement)sender) {
                        StkSurveyEditorItemChoiceModeItemsContainer.Children.Remove(child);
                        break;
                    }
                }
            }
        }

        private void DlgNewItemBtnCreateItem_Click(object sender, RoutedEventArgs e)
        {
            var item = new JObject {
                    { "caption", SurveyItemTitle.Text },
                    { "item_type", RbnSurveyItemModeChoice.IsChecked.Value ? 1:2 },
                    { "content", null }
            };

            if (RbnSurveyItemModeChoice.IsChecked.Value) {
                var tmpContent = new JArray();
                foreach (FrameworkElement child in StkSurveyEditorItemChoiceModeItemsContainer.Children) {
                    tmpContent.Add(((SurveyEditorWizardSingleChoiceItemOption)child).OptionCaption);
                }

                item["content"] = tmpContent;
            }

            if (RbnSurveyItemModeNumericScale.IsChecked.Value) {

                item["content"] = new JArray {
                    string.IsNullOrWhiteSpace(DlgNewItemNS1Caption.Text) ? DlgNewItemNS1Caption.Watermark.ToString() : DlgNewItemNS1Caption.Text,
                    string.IsNullOrWhiteSpace(DlgNewItemNS2Caption.Text) ? DlgNewItemNS2Caption.Watermark.ToString() : DlgNewItemNS2Caption.Text,
                    string.IsNullOrWhiteSpace(DlgNewItemNS3Caption.Text) ? DlgNewItemNS3Caption.Watermark.ToString() : DlgNewItemNS3Caption.Text,
                    string.IsNullOrWhiteSpace(DlgNewItemNS4Caption.Text) ? DlgNewItemNS4Caption.Watermark.ToString() : DlgNewItemNS4Caption.Text,
                    string.IsNullOrWhiteSpace(DlgNewItemNS5Caption.Text) ? DlgNewItemNS5Caption.Watermark.ToString() : DlgNewItemNS5Caption.Text
                };
            }

            Workspace.SurveyItemAdd(currentSurvey, item);

            var control = new SurveyEditorItem(item);
            StkSurveyItems.Children.Add(control);

            HideLS();
        }

        private void BtnAddItemOption_Click(object sender, RoutedEventArgs e)
        {
            if (StkSurveyEditorItemChoiceModeItemsContainer.Children.Count >= 8)
                MessageBox.Show("Ha excedido la cantidad de opciones.", "No se puede agregar la opción", MessageBoxButton.OK, MessageBoxImage.Warning);
            else {
                var itemOption = new SurveyEditorWizardSingleChoiceItemOption();
                itemOption.RequestDelete += ItemModeChoiceOptionRequestDelete;

                StkSurveyEditorItemChoiceModeItemsContainer.Children.Add(itemOption);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new Home().Show();
        }

        private void BtnPublish_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}