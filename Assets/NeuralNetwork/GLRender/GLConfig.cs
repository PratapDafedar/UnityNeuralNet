using System;

namespace NeuralNetwork.GLRendering
{
    [Serializable]
    public class GLConfig
    {
        public float layerDistance;
        public float neuronDistance;
        public float neuronSize;
        public float synapseWidth;

        public GLConfig ()
        {
            layerDistance = 10f;
            neuronDistance = 2f;
            neuronSize = 0.3f;
            synapseWidth = 0.1f;
        }
    }
}
