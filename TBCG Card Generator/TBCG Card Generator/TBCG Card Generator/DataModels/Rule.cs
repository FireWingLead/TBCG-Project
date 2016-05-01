using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBCG_Card_Generator.DataModels
{
    public class Rule : ObservableObject
    {
        string text;


        public string Text { get { return text; } set { text = value; OnPropertyChanged("Text"); } }
    }
}
