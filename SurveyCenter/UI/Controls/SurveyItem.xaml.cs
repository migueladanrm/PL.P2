using Newtonsoft.Json.Linq;
using SurveyCenter.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SurveyCenter.UI.Controls
{
    /// <summary>
    /// Lógica de interacción para SurveyItem.xaml
    /// </summary>
    public partial class SurveyItem : UserControl
    {
        private int _itemId;
        private SurveyItemKind itemType;

        public SurveyItem()
        {
            InitializeComponent();
        }

        public SurveyItem(int itemNumber, JObject item)
        {
            InitializeComponent();

            _itemId = itemNumber;

            LoadItem(item);
        }

        public int SelectedValue {
            get {
                if (itemType.Equals(SurveyItemKind.ScaleChoice)) {
                    if (MScaleOpt1.IsChecked.Value)
                        return 0;
                    if (MScaleOpt2.IsChecked.Value)
                        return 1;
                    if (MScaleOpt3.IsChecked.Value)
                        return 2;
                    if (MScaleOpt4.IsChecked.Value)
                        return 3;
                    if (MScaleOpt5.IsChecked.Value)
                        return 4;
                } else {
                    foreach(RadioButton opt in SSingleChoice.Children) {
                        if (opt.IsChecked.Value)
                            return Convert.ToInt32(opt.Tag.ToString());
                    }
                }

                return -1;
            }
        }

        private void LoadItem(JObject item)
        {
            TxtItemNumber.Text = $"{_itemId}";
            TxtItemCaption.Text = (string)item["caption"];

            itemType = (SurveyItemKind)Enum.Parse(typeof(SurveyItemKind), item["item_type"].ToString());

            if (itemType.Equals(SurveyItemKind.SingleChoice)) {
                SSingleChoice.Visibility = Visibility.Visible;
                SNumericScale.Visibility = Visibility.Collapsed;

                SSingleChoice.Children.Clear();

                int idx = 0;

                foreach (string opt in item["content"]) {
                    SSingleChoice.Children.Add(new RadioButton() {
                        Content = opt,
                        Tag=idx
                    });
                    idx++;
                }
            }

            if (itemType.Equals(SurveyItemKind.ScaleChoice)) {
                SNumericScale.Visibility = Visibility.Visible;
                SSingleChoice.Visibility = Visibility.Collapsed;

                MScaleOptCaption1.Text = (string)item["content"][0];
                MScaleOptCaption2.Text = (string)item["content"][1];
                MScaleOptCaption3.Text = (string)item["content"][2];
                MScaleOptCaption4.Text = (string)item["content"][3];
                MScaleOptCaption5.Text = (string)item["content"][4];
            }
        }
    }
}