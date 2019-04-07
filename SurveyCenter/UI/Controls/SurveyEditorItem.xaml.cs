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
    /// Lógica de interacción para SurveyEditorItem.xaml
    /// </summary>
    public partial class SurveyEditorItem : UserControl
    {
        public SurveyEditorItem()
        {
            InitializeComponent();
        }

        public SurveyEditorItem(SurveyItem item)
        {
            InitializeComponent();
        }

        public int ItemId { get; set; }
        public string ItemCaption { get; set; }
        public SurveyItemKind ItemKind { get; set; }

    }
}
