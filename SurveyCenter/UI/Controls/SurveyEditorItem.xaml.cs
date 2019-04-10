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
using Newtonsoft.Json.Linq;
using SurveyCenter.Models;

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

            surveyItem = item;

            LoadItem(item);
        }

        private void LoadItem(JObject item)
        {
            TxtItemCaption.Text = (string)item["caption"];

            var itemType = (SurveyItemKind)Enum.Parse(typeof(SurveyItemKind), item["item_type"].ToString());

            if (itemType.Equals(SurveyItemKind.SingleChoice)) {

            }

            if (itemType.Equals(SurveyItemKind.ScaleChoice)) {
                ModeScaleChoice.Visibility = Visibility.Visible;

                MScaleOptCaption1.Text = (string)item["content"][0];
                MScaleOptCaption2.Text = (string)item["content"][1];
                MScaleOptCaption3.Text = (string)item["content"][2];
                MScaleOptCaption4.Text = (string)item["content"][3];
                MScaleOptCaption5.Text = (string)item["content"][4];
            }
        }
    }
}