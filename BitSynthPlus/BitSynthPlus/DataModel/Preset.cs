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

        /// <summary>
        /// List of volume indexes for each SoundBank, in order of each SoundBank's index
        /// 0 = off, 1 = low volume, 2 = high volume
        /// </summary>
        public List<int> SoundBankVolumeIndexes { get; set; }

        /// <summary>
        /// List of sound set indexes that should be active for each SoundBank,
        /// corresponding to a SoundBank's SetNum
        /// General order: Normal, Looping, EffectSaw, EffectSquare 
        /// </summary>
        public List<int> SoundBankSetIndexes { get; set; }
    }
}
