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
    /// Lógica de interacción para SurveyEditorWizardSingleChoiceItemOption.xaml
    /// </summary>
    public partial class SurveyEditorWizardSingleChoiceItemOption : UserControl
    {
        public SurveyEditorWizardSingleChoiceItemOption()
        {
            InitializeComponent();
        }

        public event Action<object> RequestDelete;

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            RequestDelete?.Invoke(this);
        }

        public string OptionCaption => TbxCaption.Text;
    }
}