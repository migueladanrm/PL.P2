using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using clojure.lang;

namespace SurveyCenter
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Ejecución de prueba de entorno de Clojure.
            RT.load("sc.core");
            var hello = RT.var("surveycenter", "hello");
            hello.invoke();

            // Carga la ventana de inicio.
            new UI.Home().Show();
        }
    }
}
