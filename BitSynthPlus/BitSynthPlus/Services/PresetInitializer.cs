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

            presetOne.Name = "Lazy";
            presetOne.SoundBankSetIndexes = new int[,]
            {
                {
                    1,0,1,0
                },
                {
                    2,1,0,0
                },
                {
                    0,0,0,0
                },
                {
                    0,0,0,0
                }
            };

            presetTwo.Name = "Bangin";
            presetTwo.SoundBankSetIndexes = new int[,]
            {
                {
                    1,0,1,0
                },
                {
                    0,0,0,0
                },
                {
                    2,0,1,0
                },
                {
                    0,0,0,0
                }
            };

            presetThree.Name = "Funky";
            presetThree.SoundBankSetIndexes = new int[,]
            {
                {
                    0,0,0,0
                },
                {
                    1,0,0,1
                },
                {
                    1,0,1,0
                },
                {
                    2,1,0,0
                }
            };

            presetFour.Name = "Accident";
            presetFour.SoundBankSetIndexes = new int[,]
            {
                {
                    1,0,0,0
                },
                {
                    1,1,0,0
                },
                {
                    2,0,1,0
                },
                {
                    1,0,0,0
                }
            };

            presetFive.Name = "I want to believe";
            presetFive.SoundBankSetIndexes = new int[,]
            {
                {
                    1,1,1,1
                },
                {
                    0,0,0,0
                },
                {
                    2,1,1,1
                },
                {
                    0,0,0,0
                }
            };

            presetSix.Name = "Supercommuter";
            presetSix.SoundBankSetIndexes = new int[,]
            {
                {
                    1,0,0,0
                },
                {
                    0,0,0,0
                },
                {
                    1,1,1,1
                },
                {
                    2,0,0,0
                }
            };

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
