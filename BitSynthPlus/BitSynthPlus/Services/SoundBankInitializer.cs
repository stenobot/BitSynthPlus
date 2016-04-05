using BitSynthPlus.DataModel;
using System;
using System.Collections.Generic;
using BitSynthPlus.Services;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace BitSynthPlus.Services
{
    public class SoundBanksInitializer
    {
        private string[,] pOneSampleFileNames;
        private string[,] pTwoSampleFileNames;
        private string[,] wOneSampleFileNames;
        private string[,] wTwoSampleFileNames;


        private SoundBank pOne;
        private SoundBank pTwo;
        private SoundBank wOne;
        private SoundBank wTwo;

        private int soundBankIndex;


        public ObservableCollection<SoundBank> SoundBanks;

        public SoundBanksInitializer()
        {

            pOneSampleFileNames = new string[,]
            {
                {
                    "p1-1f.mp3","p1-1fsharp.mp3","p1-1g.mp3", "p1-1gsharp.mp3",
                    "p1-2a.mp3", "p1-2asharp.mp3", "p1-2b.mp3", "p1-2c.mp3", "p1-2csharp.mp3", "p1-2d.mp3", "p1-2dsharp.mp3", "p1-2e.mp3", "p1-2f.mp3", "p1-2fsharp.mp3", "p1-2g.mp3", "p1-2gsharp.mp3",
                    "p1-3a.mp3", "p1-3asharp.mp3", "p1-3b.mp3", "p1-3c.mp3", "p1-3csharp.mp3", "p1-3d.mp3", "p1-3dsharp.mp3", "p1-3e.mp3"
                },
                {
                    "p1-1f-loop.mp3","p1-1fsharp-loop.mp3","p1-1g-loop.mp3", "p1-1gsharp-loop.mp3",
                    "p1-2a-loop.mp3", "p1-2asharp-loop.mp3", "p1-2b-loop.mp3", "p1-2c-loop.mp3", "p1-2csharp-loop.mp3", "p1-2d-loop.mp3", "p1-2dsharp-loop.mp3", "p1-2e-loop.mp3", "p1-2f-loop.mp3", "p1-2fsharp-loop.mp3", "p1-2g-loop.mp3", "p1-2gsharp-loop.mp3",
                    "p1-3a-loop.mp3", "p1-3asharp-loop.mp3", "p1-3b-loop.mp3", "p1-3c-loop.mp3", "p1-3csharp-loop.mp3", "p1-3d-loop.mp3", "p1-3dsharp-loop.mp3", "p1-3e-loop.mp3"
                }
            };

            pTwoSampleFileNames = new string[,]
            {
                {
                    "p2-1f.mp3","p2-1fsharp.mp3","p2-1g.mp3", "p2-1gsharp.mp3",
                    "p2-2a.mp3", "p2-2asharp.mp3", "p2-2b.mp3", "p2-2c.mp3", "p2-2csharp.mp3", "p2-2d.mp3", "p2-2dsharp.mp3", "p2-2e.mp3", "p2-2f.mp3", "p2-2fsharp.mp3", "p2-2g.mp3", "p2-2gsharp.mp3",
                    "p2-3a.mp3", "p2-3asharp.mp3", "p2-3b.mp3", "p2-3c.mp3", "p2-3csharp.mp3", "p2-3d.mp3", "p2-3dsharp.mp3", "p2-3e.mp3"
                },
                {
                    "p2-1f-loop.wav","p2-1fsharp-loop.wav","p2-1g-loop.wav", "p2-1gsharp-loop.wav",
                    "p2-2a-loop.wav", "p2-2asharp-loop.wav", "p2-2b-loop.wav", "p2-2c-loop.wav", "p2-2csharp-loop.wav", "p2-2d-loop.wav", "p2-2dsharp-loop.wav", "p2-2e-loop.wav", "p2-2f-loop.wav", "p2-2fsharp-loop.wav", "p2-2g-loop.wav", "p2-2gsharp-loop.wav",
                    "p2-3a-loop.wav", "p2-3asharp-loop.wav", "p2-3b-loop.wav", "p2-3c-loop.wav", "p2-3csharp-loop.wav", "p2-3d-loop.wav", "p2-3dsharp-loop.wav", "p2-3e-loop.wav"
                }
            };

            wOneSampleFileNames = new string[,]
            {
                {
                    "w1-1f.wav","w1-1fsharp.wav","w1-1g.wav", "w1-1gsharp.wav",
                    "w1-2a.wav", "w1-2asharp.wav", "w1-2b.wav", "w1-2c.wav", "w1-2csharp.wav", "w1-2d.wav", "w1-2dsharp.wav", "w1-2e.wav", "w1-2f.wav", "w1-2fsharp.wav", "w1-2g.wav", "w1-2gsharp.wav",
                    "w1-3a.wav", "w1-3asharp.wav", "w1-3b.wav", "w1-3c.wav", "w1-3csharp.wav", "w1-3d.wav", "w1-3dsharp.wav", "w1-3e.wav"
                },
                {
                    "w1-1f-loop.wav","w1-1fsharp-loop.wav","w1-1g-loop.wav", "w1-1gsharp-loop.wav",
                    "w1-2a-loop.wav", "w1-2asharp-loop.wav", "w1-2b-loop.wav", "w1-2c-loop.wav", "w1-2csharp-loop.wav", "w1-2d-loop.wav", "w1-2dsharp-loop.wav", "w1-2e-loop.wav", "w1-2f-loop.wav", "w1-2fsharp-loop.wav", "w1-2g-loop.wav", "w1-2gsharp-loop.wav",
                    "w1-3a-loop.wav", "w1-3asharp-loop.wav", "w1-3b-loop.wav", "w1-3c-loop.wav", "w1-3csharp-loop.wav", "w1-3d-loop.wav", "w1-3dsharp-loop.wav", "w1-3e-loop.wav"
                }
            };

            wTwoSampleFileNames = new string[,]
            {
                {
                    "w2-1f.wav","w2-1fsharp.wav","w2-1g.wav", "w2-1gsharp.wav",
                    "w2-2a.wav", "w2-2asharp.wav", "w2-2b.wav", "w2-2c.wav", "w2-2csharp.wav", "w2-2d.wav", "w2-2dsharp.wav", "w2-2e.wav", "w2-2f.wav", "w2-2fsharp.wav", "w2-2g.wav", "w2-2gsharp.wav",
                    "w2-3a.wav", "w2-3asharp.wav", "w2-3b.wav", "w2-3c.wav", "w2-3csharp.wav", "w2-3d.wav", "w2-3dsharp.wav", "w2-3e.wav"
                },
                {
                    "w2-1f-loop.wav","w2-1fsharp-loop.wav","w2-1g-loop.wav", "w2-1gsharp-loop.wav",
                    "w2-2a-loop.wav", "w2-2asharp-loop.wav", "w2-2b-loop.wav", "w2-2c-loop.wav", "w2-2csharp-loop.wav", "w2-2d-loop.wav", "w2-2dsharp-loop.wav", "w2-2e-loop.wav", "w2-2f-loop.wav", "w2-2fsharp-loop.wav", "w2-2g-loop.wav", "w2-2gsharp-loop.wav",
                    "w2-3a-loop.wav", "w2-3asharp-loop.wav", "w2-3b-loop.wav", "w2-3c-loop.wav", "w2-3csharp-loop.wav", "w2-3d-loop.wav", "w2-3dsharp-loop.wav", "w2-3e-loop.wav"
                }
            };

            pOne = new SoundBank();
            pTwo = new SoundBank();
            wOne = new SoundBank();
            wTwo = new SoundBank();

            SoundBanks = new ObservableCollection<SoundBank>();

            InitializeSoundBank(pOneSampleFileNames, pOne, "P1");
            InitializeSoundBank(pTwoSampleFileNames, pTwo, "P2");
            InitializeSoundBank(wOneSampleFileNames, wOne, "W1");
            InitializeSoundBank(wTwoSampleFileNames, wTwo, "W2");
        }


        private void InitializeSoundBank(string[,] samples, SoundBank soundBank, string name)
        {
            soundBank.Name = name;

            soundBank.Index = soundBankIndex;
            soundBankIndex++;

            //TODOsee if we can get rid of looping and normal lists, just have hte list if lists

            soundBank.FileNames = new List<List<string>>();

            for (var i = 0; i < samples.Rank; i++)
            {
                List<string> list = new List<string>();
                soundBank.FileNames.Add(list);
                soundBank.FileNames[i].AddRange(Utilities.Current.GetArrayColumn(samples, i));
            }

            //if (samples.Rank > 0)
            //{
            //    List<string> FileNamesNormal = new List<string>();
            //    soundBank.FileNames.Add(FileNamesNormal);
            //    soundBank.FileNames[0].AddRange(Utilities.Current.GetArrayColumn(samples, 0));

            //    if (samples.Rank > 1)
            //    {
            //        List<string> FileNamesLooping = new List<string>();

            //        soundBank.FileNames.Add(FileNamesLooping);
            //        soundBank.FileNames[1].AddRange(Utilities.Current.GetArrayColumn(samples, 1));

            //    }
            //}

            //soundBank.FileNamesNormal = new List<string>();
            //soundBank.FileNamesLooping = new List<string>();



            SoundBanks.Add(soundBank);
        }
    }
}
