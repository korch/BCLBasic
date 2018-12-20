using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCLProject.Models
{
    class RuleModel
    {
        public string FilePatternName { get; set; }
        public bool Counter { get; set; }
        public bool MovementDate { get; set; }
        public string FolderToSave { get; set; }
        public int Count { get; set; }
    }
}
