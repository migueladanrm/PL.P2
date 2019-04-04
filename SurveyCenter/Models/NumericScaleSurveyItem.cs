using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyCenter.Models
{
    public class NumericScaleSurveyItem : SurveyItem
    {

        public Dictionary<NumericScale, string> NumericScale { get; set; }


    }
}