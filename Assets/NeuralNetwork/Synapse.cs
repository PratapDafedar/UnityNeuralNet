
namespace NeuralNetwork
{
    public class Synapse
    {
        public Neuron InputNeuron;
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