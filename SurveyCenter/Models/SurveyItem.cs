using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyCenter.Models
{
    /// <summary>
    /// Abstracción de item de encuesta.
    /// </summary>
    public abstract class SurveyItem
    {
        public int Id { get; set; }
        public string Statement { get; set; }
    }
}