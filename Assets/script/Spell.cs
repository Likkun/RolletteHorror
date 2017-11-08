using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.script
{
    class Spell
    {
        public List<KeyValuePair<string,string>> spellString { get; set; }
        public bool cast { get; set; }
        public string element { get; set; }
        public string monsterID { get; set; }
        public string allyID { get; set; }

        public float spellDamage { get; set; }
        public float timerDelta { get; set; }
        public float symbolSheldDamage { get; set; }
        public float SymbolSpeedDelat { get; set; }

        

    }
}
