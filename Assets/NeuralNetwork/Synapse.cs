using System;

namespace NeuralNetwork
{
    [Serializable]
    public class Synapse
    {
        [NonSerialized]
        public Neuron InputNeuron;
        [NonSerialized]
        public Neuron OutputNeuron;
        public float Weight;
        public float WeightDelta;

        public Synapse(Neuron inputNeuron, Neuron outputNeuron)
        {
            InputNeuron = inputNeuron;
            OutputNeuron = outputNeuron;
            Weight = NeuralNet.GetRandom();
        }
    }
}