using BitSynthPlus.DataModel;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace BitSynthPlus.Services
{
    /// <summary>
    /// Initializes all six Presets
    /// </summary>
    public class PresetInitializer
    {
        static readonly double echoDelayMinVal = (double)Application.Current.Resources["EchoDelayMin"];
        static readonly double echoFeedbackMinVal = (double)Application.Current.Resources["EchoFeedbackMin"];
        static readonly double reverbDecayMinVal = (double)Application.Current.Resources["ReverbDecayMin"];
        static readonly double reverbDensityMinVal = (double)Application.Current.Resources["ReverbDensityMin"];
        static readonly double reverbGainMinVal = (double)Application.Current.Resources["ReverbGainMin"];

        private Preset presetOne;
        private Preset presetTwo;
        private Preset presetThree;
        private Preset presetFour;
        private Preset presetFive;
        private Preset presetSix;

        public List<Preset> allPresets;

        public PresetInitializer()
        {
            presetOne = new Preset(
                "Default",
                new int[,]
                {
                    {2,0,1},
                    {0,0,1},
                    {0,0,1},
                    {0,0,1}
                },
                false,
                false,
                (int)echoDelayMinVal,
                echoFeedbackMinVal,
                (int)reverbDecayMinVal,
                (int)reverbDensityMinVal,
                (int)reverbGainMinVal
                );

            presetTwo = new Preset(
                "All You Need Is Kill",
                new int[,]
                {
                    {1,1,3},
                    {0,0,1},
                    {0,0,1},
                    {2,0,1}
                },
                false,
                false,
                (int)echoDelayMinVal,
                echoFeedbackMinVal,
                (int)reverbDecayMinVal,
                (int)reverbDensityMinVal,
                (int)reverbGainMinVal
                );

            presetThree = new Preset(
                "Something",
                new int[,]
                {
                    {0,0,1},
                    {0,0,1},
                    {2,1,1},
                    {1,1,1}
                },
                true,
                false,
                (int)echoDelayMinVal + 100,
                echoFeedbackMinVal + 0.4,
                (int)reverbDecayMinVal,
                (int)reverbDensityMinVal,
                (int)reverbGainMinVal
                );

            presetFour = new Preset(
                "Accident",
                new int[,]
                {
                    {1,0,2},
                    {1,1,1},
                    {2,1,0},
                    {0,0,1}
                },
                false,
                false,
                (int)echoDelayMinVal,
                echoFeedbackMinVal,
                (int)reverbDecayMinVal,
                (int)reverbDensityMinVal,
                (int)reverbGainMinVal
                );

            presetFive = new Preset(
                "I Want To Believe",
                new int[,]
                {
                    {2,0,1},
                    {1,0,3},
                    {1,0,1},
                    {0,0,1}
                },
                false,
                true,
                (int)echoDelayMinVal,
                echoFeedbackMinVal,
                (int)reverbDecayMinVal + 10,
                (int)reverbDensityMinVal + 3,
                (int)reverbGainMinVal + 2
                );

            presetSix = new Preset(
                "Supercommuter",
                new int[,]
                {
                    {2,1,1},
                    {0,0,1},
                    {1,0,1},
                    {1,0,1}
                },
                true,
                false,
                (int)echoDelayMinVal + 400,
                echoFeedbackMinVal + 0.1,
                (int)reverbDecayMinVal,
                (int)reverbDensityMinVal,
                (int)reverbGainMinVal
                );

            allPresets = new List<Preset>();
            allPresets.Add(presetOne);
            allPresets.Add(presetTwo);
            allPresets.Add(presetThree);
            allPresets.Add(presetFour);
            allPresets.Add(presetFive);
            allPresets.Add(presetSix);

            foreach (Preset preset in allPresets)
                preset.IsActive = false;
        }
    }
}
