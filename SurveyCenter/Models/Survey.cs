using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyCenter.Models
{
    /// <summary>
    /// Encuesta.
    /// </summary>
    public class Survey
    {
        public string Name { get; set; }
        public List<SurveyItem> Content { get; set; }
    }
}