using BitSynthPlus.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitSynthPlus.Services
{
    public class PresetInitializer
    {
        private Preset presetOne;
        private Preset presetTwo;
        private Preset presetThree;
        private Preset presetFour;
        private Preset presetFive;
        private Preset presetSix;

        public PresetInitializer()
        {
            presetOne = new Preset();
            presetTwo = new Preset();
            presetThree = new Preset();
            presetFour = new Preset();
            presetFive = new Preset();
            presetSix = new Preset();

            presetOne.Name = "Lazy";
            presetOne.SoundBankSetIndexes = new List<List<int>>();

            presetOne.SoundBankVolumeIndexes = new List<int>()
            {
                1,0,2,0
            };
        }
    }
}
