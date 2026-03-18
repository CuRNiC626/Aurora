using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioFramework
{
    public class ABuffer
    {
        public int Size { get; private set; }

        private readonly short[] content;
        private int index;


        public int sampleRate = 8000; // наша частота дискретизации.
        //double frequency = Math.PI * 2 * 440.0 / sampleRate; // Рассчитываем требующуюся частоту.

        public ABuffer(int Size, int SampleRate)
        {
            content = new short[Size];
            this.Size = Size;

            this.sampleRate = SampleRate;
        }


        public void AutoFill(double frequency)
        {
            for (int i = 0; i < Size; i++)
                content[i] = (short)(AMath.Sine(index, frequency) * AMath.Length(-0.0015, frequency, index, 1.0, sampleRate) * short.MaxValue);
        }

        public void SaveWave(Stream stream, int sampleRate)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            short frameSize = (short)(16 / 8); // Количество байт в блоке (16 бит делим на 8).
            writer.Write(0x46464952); // Заголовок "RIFF".
            writer.Write(36 + content.Length * frameSize); // Размер файла от данной точки.
            writer.Write(0x45564157); // Заголовок "WAVE".
            writer.Write(0x20746D66); // Заголовок "frm ".
            writer.Write(16); // Размер блока формата.
            writer.Write((short)1); // Формат 1 значит PCM.
            writer.Write((short)1); // Количество дорожек.
            writer.Write(sampleRate); // Частота дискретизации.
            writer.Write(sampleRate * frameSize); // Байтрейт (Как битрейт только в байтах).
            writer.Write(frameSize); // Количество байт в блоке.
            writer.Write((short)16); // разрядность.
            writer.Write(0x61746164); // Заголовок "DATA".
            writer.Write(content.Length * frameSize); // Размер данных в байтах.
            for (int index = 0; index < content.Length; index++)
            { // Начинаем записывать данные из нашего массива.
                foreach (byte element in BitConverter.GetBytes(content[index]))
                { // Разбиваем каждый элемент нашего массива на байты.
                    stream.WriteByte(element); // И записываем их в поток.
                }
            }
        }
    }
}
