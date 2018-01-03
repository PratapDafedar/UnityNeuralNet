using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork
{
	public class NeuralNet
	{
		public float LearnRate { get; set; }
		public float Momentum { get; set; }
		public List<Neuron> InputLayer { get; set; }
		public List<List<Neuron>> HiddenLayers { get; set; }
		public List<Neuron> OutputLayer { get; set; }

		private static readonly System.Random Random = new System.Random();

		public NeuralNet(int inputSize, int hiddenSize, int outputSize, int numHiddenLayers = 1, float? learnRate = null, float? momentum = null)
		{
			LearnRate = learnRate ?? .4f;
			Momentum = momentum ?? .9f;
			InputLayer = new List<Neuron>();
			HiddenLayers = new List<List<Neuron>>();
			OutputLayer = new List<Neuron>();

			for (var i = 0; i < inputSize; i++)
				InputLayer.Add(new Neuron());

		for (int i = 0; i < numHiddenLayers; i++)
		{
			HiddenLayers.Add(new List<Neuron>());
			for (var j = 0; j < hiddenSize; j++)
				HiddenLayers[i].Add(new Neuron(i==0?InputLayer:HiddenLayers[i-1]));
		}

			for (var i = 0; i < outputSize; i++)
				OutputLayer.Add(new Neuron(HiddenLayers[numHiddenLayers-1]));
		}

		public void Train(List<DataSet> dataSets, int numEpochs)
		{
			for (var i = 0; i < numEpochs; i++)
			{
				foreach (var dataSet in dataSets)
				{
					ForwardPropagate(dataSet.Values);
					BackPropagate(dataSet.Targets);
				}
			}
		}

		public void Train(List<DataSet> dataSets, float minimumError)
		{
			var error = 1.0;
			var numEpochs = 0;

			while (error > minimumError && numEpochs < int.MaxValue)
			{
				var errors = new List<float>();
				foreach (var dataSet in dataSets)
				{
					ForwardPropagate(dataSet.Values);
					BackPropagate(dataSet.Targets);
					errors.Add(CalculateError(dataSet.Targets));
				}
				error = errors.Average();
				numEpochs++;
			}
		}

		private void ForwardPropagate(params float[] inputs)
		{
			var i = 0;
			InputLayer.ForEach(a => a.Value = inputs[i++]);
			foreach (var layer in HiddenLayers)
				layer.ForEach(a => a.CalculateValue());
			OutputLayer.ForEach(a => a.CalculateValue());
		}

		private void BackPropagate(params float[] targets)
		{
			var i = 0;
			OutputLayer.ForEach(a => a.CalculateGradient(targets[i++]));
			foreach(var layer in HiddenLayers.AsEnumerable<List<Neuron>>().Reverse() )
			{
				layer.ForEach(a => a.CalculateGradient());
				layer.ForEach(a => a.UpdateWeights(LearnRate, Momentum));
			}
			OutputLayer.ForEach(a => a.UpdateWeights(LearnRate, Momentum));
		}

		public float[] Compute(params float[] inputs)
		{
			ForwardPropagate(inputs);
			return OutputLayer.Select(a => a.Value).ToArray();
		}

		private float CalculateError(params float[] targets)
		{
			var i = 0;
			return OutputLayer.Sum(a => Mathf.Abs((float)a.CalculateError(targets[i++])));
		}

		public static float GetRandom()
		{
			return 2 * (float)Random.NextDouble() - 1;
		}
	}
}