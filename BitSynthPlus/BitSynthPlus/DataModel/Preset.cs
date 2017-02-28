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
        /// List of volume indexes for each SoundBank, in order of each SoundBank's index
        /// 0 = off, 1 = low volume, 2 = high volume
        /// there can be only one... two ;)
        /// </summary>
        //public List<int> SoundBankVolumeIndexes { get; set; }

        /// <summary>
        /// 2D array of indexes for each toggle in each SoundBank
        /// Each array row represents SoundBank, in this order: 
        /// [x,0] Volume: 0 = off, 1 = low volume, 2 = high volume (there can be only one...two)
        /// [x,1] Looping: 0 = off, 1 = on
        /// [x,2] EffectSaw: 0 = off, 1 = on 
        /// [x,3] EffectSquare: 0 = off, 1 = on
        /// </summary>
        public int[,] SoundBankSetIndexes { get; set; }
    }
}
