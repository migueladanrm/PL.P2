using System.Windows;

namespace SurveyCenter
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Workspace.SetupWorkspace();
            Workspace.InitializeClojureRuntime();

            // Carga la ventana de inicio.
            new UI.Home().Show();
        }


    }
}