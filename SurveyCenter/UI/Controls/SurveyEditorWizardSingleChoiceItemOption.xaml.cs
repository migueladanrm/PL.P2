using System;
using System.Windows;
using System.Windows.Controls;

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