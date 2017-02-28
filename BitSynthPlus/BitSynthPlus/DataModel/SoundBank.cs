using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitSynthPlus.DataModel
{
    public class SoundBank
    {
        /// <summary>
        /// The name of a SoundBank
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The total number of sound sets in a SoundBank
        /// </summary>
        //public int SetsNum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<List<string>> FileNames { get; set; }

        /// <summary>
        /// Optional list of  Samples in a SoundBank
        /// </summary>
        //public List<string> FileNamesNormal { get; set; }

        //public List<string> FileNamesLooping { get; set; }
    }
}
