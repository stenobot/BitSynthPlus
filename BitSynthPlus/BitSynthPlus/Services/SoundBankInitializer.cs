using BitSynthPlus.DataModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BitSynthPlus.Services
{
    /// <summary>
    /// Initializes all four SoundBanks and creates filenames
    /// </summary>
    public class SoundBanksInitializer
    {
        private string[] audioFileNotes;

        private const string FILENAME_NORMAL_TEMPLATE = @"{0}-{1}.wav";
        private const string FILENAME_LOOPED_TEMPLATE = @"{0}-{1}-loop.wav";

        private SoundBank pOne;
        private SoundBank pTwo;
        private SoundBank wOne;
        private SoundBank wTwo;

        public ObservableCollection<SoundBank> SoundBanks;

        public SoundBanksInitializer()
        {
            audioFileNotes = new string[]
            {
                "1f", "1fsharp", "1g", "1gsharp",
                "2a", "2asharp", "2b", "2c", "2csharp", "2d", "2dsharp", "2e", "2f", "2fsharp", "2g", "2gsharp",
                "3a", "3asharp", "3b", "3c", "3csharp", "3d", "3dsharp", "3e", "3f", "3fsharp", "3g", "3gsharp",
                "4a", "4asharp", "4b", "4c", "4csharp", "4d", "4dsharp", "4e", "4f", "4fsharp", "4g"
            };

            SoundBanks = new ObservableCollection<SoundBank>();

            pOne = new SoundBank();
            InitializeSoundBank(pOne, "p1");

            pTwo = new SoundBank();
            InitializeSoundBank(pTwo, "p2");

            wOne = new SoundBank();
            InitializeSoundBank(wOne, "w1");

            wTwo = new SoundBank();
            InitializeSoundBank(wTwo, "w2");
        }

        /// <summary>
        /// Initialize an individual SoundBank with two sets of filenames:
        /// One for regular audio files, and one for looping audio files
        /// </summary>
        /// <param name="soundBank">The SoundBank to initialize</param>
        /// <param name="name">The name of the SoundBank, which will be prepended to each filename</param>
        private void InitializeSoundBank(SoundBank soundBank, string name)
        {
            soundBank.Name = name;

            // create Lists for filenames
            soundBank.FileNames = new List<List<string>>();
            soundBank.FileNames.Add(new List<string>());
            soundBank.FileNames.Add(new List<string>());

            // loop through note names array
            // create normal and looped file names based on templates
            // add filenames to SoundBank Lists
            foreach (string noteName in audioFileNotes)
            {
                soundBank.FileNames[0].Add(string.Format(FILENAME_NORMAL_TEMPLATE, soundBank.Name.ToLower(), noteName.ToLower()));
                soundBank.FileNames[1].Add(string.Format(FILENAME_LOOPED_TEMPLATE, soundBank.Name.ToLower(), noteName.ToLower()));
            }
        
            //for (var i = 0; i < samples.Rank; i++)
            //{
            //    List<string> list = new List<string>();
            //    soundBank.FileNames.Add(list);
            //    soundBank.FileNames[i].AddRange(Utilities.Current.GetArrayColumn(samples, i));
            //}

            SoundBanks.Add(soundBank);
        }
    }
}
