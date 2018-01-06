using System;

namespace NeuralNetwork
{
    [Serializable]
    public class Config
    {
        public int inputSize;
        public int hiddenSize;
        public int outputSize;
        public int numHiddenLayers;
        public float learnRate;
        public float momentum;
        public int maxEpoch;
        
        public Config()
        {
            numHiddenLayers = 1;
            learnRate = 0.4f;
            momentum = 0.9f;
            maxEpoch = 1000;
        }

        public Config(int inputSize, int hiddenSize, int outputSize) : this ()
        {
            this.inputSize = inputSize;
            this.hiddenSize = hiddenSize;
            this.outputSize = outputSize;
        }
    }    
}