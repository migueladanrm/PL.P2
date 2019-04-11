using Newtonsoft.Json.Linq;
using SurveyCenter.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SurveyCenter.UI.Controls
{
    /// <summary>
    /// Lógica de interacción para SurveyEditorItem.xaml
    /// </summary>
    public partial class SurveyEditorItem : UserControl
    {
        private JObject surveyItem;

        public SurveyEditorItem()
        {
            InitializeComponent();
        }

        public SurveyEditorItem(JObject item)
        {
            InitializeComponent();

            BtnDelete.Visibility = Visibility.Collapsed;

            surveyItem = item;

            LoadItem(item);
        }

        private void LoadItem(JObject item)
        {
            TxtItemCaption.Text = (string)item["caption"];

            var itemType = (SurveyItemKind)Enum.Parse(typeof(SurveyItemKind), item["item_type"].ToString());

            if (itemType.Equals(SurveyItemKind.SingleChoice)) {
                ModeSingleChoice.Visibility = Visibility.Visible;
                ModeScaleChoice.Visibility = Visibility.Collapsed;

                foreach (string str in item["content"]) {
                    ModeSingleChoice.Children.Add(new TextBlock {
                        Text = str
                    });
                }
            }

            if (itemType.Equals(SurveyItemKind.ScaleChoice)) {
                ModeScaleChoice.Visibility = Visibility.Visible;
                ModeSingleChoice.Visibility = Visibility.Collapsed;

                MScaleOptCaption1.Text = (string)item["content"][0];
                MScaleOptCaption2.Text = (string)item["content"][1];
                MScaleOptCaption3.Text = (string)item["content"][2];
                MScaleOptCaption4.Text = (string)item["content"][3];
                MScaleOptCaption5.Text = (string)item["content"][4];
            }
        }
    }
}