using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitSynthPlus.DataModel
{
    public class Preset
    {
        /// <summary>
        /// The public facing name of a preset
        /// </summary>
        public string Name { get; set; }

        public bool IsActive { get; set; }


        /// <summary>
        /// 2D array of indexes for each toggle in each SoundBank
        /// Each array row represents SoundBank, in this order: 
        /// [x,0] Volume: 0 = off, 1 = low volume, 2 = high volume (there can be only one...two!)
        /// [x,1] Looping: 0 = off, 1 = on (can only be one if Volume != zero)
        /// </summary>
        public int[,] SoundBankSetIndexes { get; set; }

        public bool IsDelayOn { get; set; }

        public bool IsReverbOn { get; set; }
    }
}
