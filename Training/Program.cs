using System;

using LostTech.Gradient;
using LostTech.TensorFlow;


using LostTech.TensorFlow.GPT;


namespace Training
{
    class Program
    {
        static void Main(string[] args)
        {

             Console.Title = "GPT-2";

            var bach = 10;
            var input = Gpt2Sampler.SampleSequence(Gpt2Model.DefaultHParams, 512, "Привет");
            var output = Gpt2Model.Model(Gpt2Model.DefaultHParams, input);


            foreach (var o in output)
                Console.WriteLine(o.Key);
        }
    }
}
