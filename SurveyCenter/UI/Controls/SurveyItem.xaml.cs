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

namespace SurveyCenter.UI.Controls
{
    /// <summary>
    /// Lógica de interacción para SurveyItem.xaml
    /// </summary>
    public partial class SurveyItem : UserControl
    {
        public SurveyItem()
        {
            InitializeComponent();
        }

        public SurveyItem(JObject item)
        {
            InitializeComponent();
        }
    }
}
