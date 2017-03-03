using BitSynthPlus.DataModel;
using System.Collections.Generic;

namespace BitSynthPlus.Services
{
    /// <summary>
    /// Initializes all six Presets
    /// </summary>
    public class PresetInitializer
    {
        private Preset presetOne;
        private Preset presetTwo;
        private Preset presetThree;
        private Preset presetFour;
        private Preset presetFive;
        private Preset presetSix;

        public List<Preset> allPresets;

        public PresetInitializer()
        {
            presetOne = new Preset();
            presetTwo = new Preset();
            presetThree = new Preset();
            presetFour = new Preset();
            presetFive = new Preset();
            presetSix = new Preset();

            allPresets = new List<Preset>();

            //PRESET ONE
            presetOne.Name = "Default";
            presetOne.SoundBankSetIndexes = new int[,]
            {
                {2,0},
                {0,0},
                {0,0},
                {0,0}
            };
            presetOne.IsDelayOn = false;
            presetOne.IsReverbOn = false;

            //PRESET TWO
            presetTwo.Name = "Bangin";
            presetTwo.SoundBankSetIndexes = new int[,]
            {
                {1,1},
                {0,0},
                {0,0},
                {2,0}
            };
            presetTwo.IsDelayOn = false;
            presetTwo.IsReverbOn = true;

            //PRESET THREE
            presetThree.Name = "Funky";
            presetThree.SoundBankSetIndexes = new int[,]
            {
                {0,0},
                {0,0},
                {2,1},
                {1,1}
            };
            presetThree.IsDelayOn = true;
            presetThree.IsReverbOn = false;

            //PRESET FOUR
            presetFour.Name = "Accident";
            presetFour.SoundBankSetIndexes = new int[,]
            {
                {0,0},
                {1,1},
                {2,1},
                {0,0}
            };
            presetFour.IsDelayOn = true;
            presetFour.IsReverbOn = false;

            //PRESET FIVE
            presetFive.Name = "I want to believe";
            presetFive.SoundBankSetIndexes = new int[,]
            {
                {2,0},
                {1,0},
                {1,0},
                {0,0}
            };
            presetFive.IsDelayOn = false;
            presetFive.IsReverbOn = true;

            //PRESET SIX
            presetSix.Name = "Supercommuter";
            presetSix.SoundBankSetIndexes = new int[,]
            {
                {2,1},
                {0,0},
                {1,0},
                {1,0}
            };
            presetSix.IsDelayOn = true;
            presetSix.IsReverbOn = false;

            allPresets.Add(presetOne);
            allPresets.Add(presetTwo);
            allPresets.Add(presetThree);
            allPresets.Add(presetFour);
            allPresets.Add(presetFive);
            allPresets.Add(presetSix);

            foreach (Preset preset in allPresets)
                preset.IsActive = false;

            allPresets[0].IsActive = true;
        }
    }
}
