using System.Collections.Generic;
using System.Linq;
using System;
using NeuralNetwork.GLRendering;
using UnityEngine;

namespace NeuralNetwork
{
    [Serializable]
	public class Neuron
	{
        public List<Synapse> InputSynapses;
        public List<Synapse> OutputSynapses;
        public float Bias;
        public float BiasDelta;
        public float Gradient;
        public float Value;

		public Neuron()
		{
			InputSynapses = new List<Synapse>();
			OutputSynapses = new List<Synapse>();
			Bias = NeuralNet.GetRandom();
		}

		public Neuron(IEnumerable<Neuron> inputNeurons) : this()
		{
			foreach (var inputNeuron in inputNeurons)
			{
				var synapse = new Synapse(inputNeuron, this);
				inputNeuron.OutputSynapses.Add(synapse);
				InputSynapses.Add(synapse);
			}
		}

		public virtual float CalculateValue()
		{
            //return Value = Sigmoid.Output(InputSynapses.Sum(a => a.Weight * a.InputNeuron.Value) + Bias);
            float sum = 0f;
            for (int i = 0; i < InputSynapses.Count; i++)
            {
                var synapse = InputSynapses[i];
                sum += synapse.Weight * synapse.InputNeuron.Value;
            }
            return Value = Sigmoid.Output(sum + Bias);
        }

		public float CalculateError(float target)
		{
			return target - Value;
		}

        public float CalculateGradient(float? target = null)
        {
            if (target == null)
            {
                //return Gradient = OutputSynapses.Sum(a => a.OutputNeuron.Gradient * a.Weight) * Sigmoid.Derivative(Value);
                float sum = 0f;
                for (int i = 0; i < OutputSynapses.Count; i++)
                {
                    var synapse = OutputSynapses[i];
                    sum += synapse.OutputNeuron.Gradient * synapse.Weight;
                }
                return Gradient = sum * Sigmoid.Derivative(Value);
            }
			return Gradient = CalculateError(target.Value) * Sigmoid.Derivative(Value);
		}

		public void UpdateWeights(float learnRate, float momentum)
		{
			var prevDelta = BiasDelta;
			BiasDelta = learnRate * Gradient;
			Bias += BiasDelta + momentum * prevDelta;

			for (int i = 0; i < InputSynapses.Count; i++)
			{
                var synapse = InputSynapses[i];
                prevDelta = synapse.WeightDelta;
				synapse.WeightDelta = learnRate * Gradient * synapse.InputNeuron.Value;
				synapse.Weight += synapse.WeightDelta + momentum * prevDelta;
			}
		}


        #region GLRender implementation

        public void RenderSynapses(Vector3 startPos, Vector3 endPos, float neuronDist, float width)
        {
            for(int i = 0; i< OutputSynapses.Count; i++)
            {
                Synapse synapse = OutputSynapses[i];
                float unitWeight = synapse.Weight / 10f;
                Color neuronCol = GLHelper.ColorFromUnitFloat(unitWeight);
                GLHelper.DrawLine(startPos, endPos, neuronCol, width);
                endPos.y -= neuronDist;
            }
        }

        #endregion
    }
}
