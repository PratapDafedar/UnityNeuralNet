using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using NeuralNetwork.GLRendering;

namespace NeuralNetwork
{
    [Serializable]
	public class NeuralNet : IGLRender
    {
		private static readonly System.Random Random = new System.Random();

        public List<Neuron> InputLayer;
        public List<List<Neuron>> HiddenLayers;
        public List<Neuron> OutputLayer;

        private Config config;
        private GLConfig visualisationConfig;
        [SerializeField]
        private List<float> errors;

		public NeuralNet(Config config, GLConfig visualisationConfig)
		{
            this.config = config;
            this.visualisationConfig = visualisationConfig;

            InputLayer = new List<Neuron>();
			HiddenLayers = new List<List<Neuron>>();
			OutputLayer = new List<Neuron>();
            errors = new List<float>();

            for (var i = 0; i < config.inputSize; i++)
				InputLayer.Add(new Neuron());

			for (int i = 0; i < config.numHiddenLayers; i++)
			{
				HiddenLayers.Add(new List<Neuron>());
				for (var j = 0; j < config.hiddenSize; j++)
					HiddenLayers[i].Add(new Neuron(i==0?InputLayer:HiddenLayers[i-1]));
			}

			for (var i = 0; i < config.outputSize; i++)
				OutputLayer.Add(new Neuron(HiddenLayers[config.numHiddenLayers - 1]));
		}

		public void Train(List<DataSet> dataSets, int numEpochs)
		{
			for (var i = 0; i < numEpochs; i++)
			{
                for (int j = 0; j < dataSets.Count; j++)
                {
                    var dataSet = dataSets[i];
                    ForwardPropagate(dataSet.Values);
					BackPropagate(dataSet.Targets);
				}
			}
		}

		public void Train(List<DataSet> dataSets, float minimumError)
		{
			var error = 1.0f;
			var numEpochs = 0;

			while (error > minimumError && numEpochs < config.maxEpoch)
			{
                errors.Clear();
				for (int i = 0; i < dataSets.Count; i++)
				{
                    var dataSet = dataSets[i];
                    ForwardPropagate(dataSet.Values);
					BackPropagate(dataSet.Targets);
					errors.Add(CalculateError(dataSet.Targets));
				}
				error = errors.Average();
				numEpochs++;
			}
            Debug.LogFormat("Training result:: error:{0}; epoch:{1}", error, numEpochs);
		}

		private void ForwardPropagate(params float[] inputs)
		{
			var i = 0;
			InputLayer.ForEach(a => a.Value = inputs[i++]);
            for (int j = 0; j < HiddenLayers.Count; j++)
            {
                var layer = HiddenLayers[j];
                layer.ForEach(a => a.CalculateValue());
            }
			OutputLayer.ForEach(a => a.CalculateValue());
		}

		private void BackPropagate(params float[] targets)
		{
			var i = 0;
			OutputLayer.ForEach(a => a.CalculateGradient(targets[i++]));
			//foreach(var layer in HiddenLayers.AsEnumerable<List<Neuron>>().Reverse())
            for (int j = HiddenLayers.Count - 1; j >= 0; j--)
			{
                var layer = HiddenLayers[j];
				layer.ForEach(a => a.CalculateGradient());
				layer.ForEach(a => a.UpdateWeights(config.learnRate, config.momentum));
			}
			OutputLayer.ForEach(a => a.UpdateWeights(config.learnRate, config.momentum));
		}

		public float[] Compute(params float[] inputs)
		{
			ForwardPropagate(inputs);
			return OutputLayer.Select(a => a.Value).ToArray();
		}

		private float CalculateError(params float[] targets)
		{
            //return OutputLayer.Sum(a => Mathf.Abs((float)a.CalculateError(targets[i++])));
            float sum = 0f;
            for (int i = 0; i < OutputLayer.Count; i++)
            {
                var layer = OutputLayer[i];
                sum += Mathf.Abs((float)layer.CalculateError(targets[i]));
            }
            return sum;
        }

		public static float GetRandom()
		{
			return 2 * (float)Random.NextDouble() - 1;
		}


        #region IGLRender implementation

        public void Render(Vector3 position)
        {
            RenderLayer(InputLayer, position, visualisationConfig.neuronDistance);

            foreach (List<Neuron> hiddenLayer in HiddenLayers)
            {
                position.x += visualisationConfig.layerDistance;
                RenderLayer(hiddenLayer, position, visualisationConfig.neuronDistance);
            }
            
            position.x += visualisationConfig.layerDistance;
            RenderLayer(OutputLayer, position, visualisationConfig.neuronDistance);
        }

        private void RenderLayer(List<Neuron> layer, Vector3 position, float neuronDistance)
        {
            GL.Begin(GL.QUADS);
            Vector3 curPos = position;
            Vector3 endPos = new Vector3(curPos.x + visualisationConfig.layerDistance, position.y, curPos.z);
            foreach (Neuron neuron in layer)
            {
                Color neuronCol = GLHelper.ColorFromUnitFloat(neuron.Value);
                GLHelper.DrawCube(curPos, visualisationConfig.neuronSize, neuronCol);
                neuron.RenderSynapses(curPos, endPos, visualisationConfig.neuronDistance, visualisationConfig.synapseWidth);
                curPos.y -= neuronDistance;
            }
            GL.End();            
        }

        #endregion
    }
}