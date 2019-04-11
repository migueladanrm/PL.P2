using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Windows.Controls;

namespace SurveyCenter.UI.Controls
{
    /// <summary>
    /// Lógica de interacción para SurveyStatisticsItem.xaml
    /// </summary>
    public partial class SurveyStatisticsItem : UserControl
    {
        public SurveyStatisticsItem()
        {
            InitializeComponent();

            DataContext = this;
        }

        public SurveyStatisticsItem(JObject surveyItem, List<int> values)
        {
            InitializeComponent();
            DataContext = this;

            SeriesCollection = new SeriesCollection();

            var content = (JArray)surveyItem["content"];

            for (int i = 0; i < content.Count; i++) {
                SeriesCollection.Add(new PieSeries {
                    Title = (string)content[i],
                    Values = new ChartValues<ObservableValue> { new ObservableValue(values[i]) },
                    DataLabels = true
                });
            }

        }

        public SeriesCollection SeriesCollection { get; set; }
    }
}