using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioFramework
{
    public static class AMath
    {
        public static double Sine(int Index, double frequency) => Math.Sin(frequency * Index);

        private static double Saw(int index, double frequency) =>
             2.0 * (index * frequency - Math.Floor(index * frequency)) - 1.0;

        public static double Triangle(int index, double frequency) =>
             2.0 * Math.Abs(2.0 * (index * frequency - Math.Floor(index * frequency + 0.5))) - 1.0;

        public static double Flat(int index, double frequency) =>
            Math.Sin(frequency * index) > 0 ? 1 : 0;

        public static double Length(double compressor, double frequency, double position, double length, int sampleRate) =>
            Math.Exp(((compressor / sampleRate) * frequency * sampleRate * (position / sampleRate)) / (length / sampleRate));

        public static double GetNote(int key, int octave) =>
            27.5 * Math.Pow(2, (key + octave * 12.0) / 12.0);
    }
}
