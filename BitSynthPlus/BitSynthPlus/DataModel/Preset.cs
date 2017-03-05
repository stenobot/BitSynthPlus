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
        /// [x,1] Looping: 0 = off, 1 = on (can only be set to one if Volume != zero)
        /// [x,2] Pitch: 0 = 0.5, 1 = 1.0 (default), 2 = 1.5, 3 = 2.0
        /// </summary>
        public int[,] SoundBankSetIndexes { get; set; }

        public bool IsEchoOn { get; set; }

        public bool IsReverbOn { get; set; }

        public int EchoDelayValue { get; set; }

        public double EchoFeedbackValue { get; set; }

        public int ReverbDecayValue { get; set; }

        public int ReverbDensityValue { get; set; }

        public int ReverbGainValue { get; set; }

        public Preset(
            string name, 
            int[,] soundBankSetIndexes, 
            bool isEchoOn, 
            bool isReverbOn, 
            int echoDelayValue, 
            double echoFeedbackValue, 
            int reverbDecayValue, 
            int reverbDensityValue, 
            int reverbGainValue)
        {
            Name = name;
            SoundBankSetIndexes = soundBankSetIndexes;
            IsEchoOn = isEchoOn;
            IsReverbOn = isReverbOn;
            EchoDelayValue = echoDelayValue;
            EchoFeedbackValue = echoFeedbackValue;
            ReverbDecayValue = reverbDecayValue;
            ReverbDensityValue = reverbDensityValue;
            ReverbGainValue = reverbGainValue;
        }
    }
}
