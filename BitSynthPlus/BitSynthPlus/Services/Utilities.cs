using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitSynthPlus.Services
{
    public class Utilities
    {
        private static Utilities current;

        public static Utilities Current
        {
            get
            {
                if (current == null)
                    current = new Utilities();

                return current;
            }

            set
            {
                current = value;
            }
        }


        public string[] GetArrayColumn(string[,] array, int columnNum)
        {
            string[] result = new string[array.GetLength(1)];

            for (int i = 0; i < array.GetLength(1); i++)
            {
                result[i] = array[columnNum, i];
            }
            return result;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="samples"></param>
        public void AddSamplesToList(List<string> list, params string[] samples)
        {
            for (int i = 0; i < samples.Length; i++)
            {
                list.Add(samples[i]);
            }
        }
    }
}
