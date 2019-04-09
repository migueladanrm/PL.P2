using Newtonsoft.Json.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SurveyCenter.Models;

namespace SurveyCenter.UI.Controls
{
    /// <summary>
    /// Lógica de interacción para SurveyItem.xaml
    /// </summary>
    public partial class SurveyItem : UserControl
    {
        private int itemId;

        public SurveyItem()
        {
            InitializeComponent();
        }

        public SurveyItem(JObject item)
        {
            InitializeComponent();

            LoadItem(item);
        }

        private void LoadItem(JObject item)
        {
            itemId = (int)item["item_id"];

            TxtItemCaption.Text = (string)item["caption"];

            var itemType = (SurveyItemKind)Enum.Parse(typeof(SurveyItemKind), item["item_type"].ToString());

            if (itemType.Equals(SurveyItemKind.SingleChoice)) {
                SSingleChoice.Visibility = Visibility.Visible;
                SNumericScale.Visibility = Visibility.Collapsed;

                SSingleChoice.Children.Clear();
                foreach (string opt in item["content"]) {

                    SSingleChoice.Children.Add(new RadioButton() {
                        Content = opt,
                    });
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
