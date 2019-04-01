using clojure.lang;
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
            //InitializeClojureRuntime();

            // Carga la ventana de inicio.
            new UI.Home().Show();
        }

        private static void InitializeClojureRuntime()
        {
            RT.load("sc.core");
            var hello = RT.var("surveycenter", "hello");
            hello.invoke();
        }
    }
}