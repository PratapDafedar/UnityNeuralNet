using System.Collections.Generic;
using System.Linq;
using NeuralNetwork.GLRendering;
using UnityEngine;

namespace NeuralNetwork
{
	public class Neuron
	{
		public List<Synapse> InputSynapses { get; set; }
		public List<Synapse> OutputSynapses { get; set; }
		public float Bias { get; set; }
		public float BiasDelta { get; set; }
		public float Gradient { get; set; }
		public float Value { get; set; }

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
			return Value = Sigmoid.Output(InputSynapses.Sum(a => a.Weight * a.InputNeuron.Value) + Bias);
		}

		public float CalculateError(float target)
		{
			return target - Value;
		}

		public float CalculateGradient(float? target = null)
		{
			if(target == null)
				return Gradient = OutputSynapses.Sum(a => a.OutputNeuron.Gradient * a.Weight) * Sigmoid.Derivative(Value);

			return Gradient = CalculateError(target.Value) * Sigmoid.Derivative(Value);
		}

		public void UpdateWeights(float learnRate, float momentum)
		{
			var prevDelta = BiasDelta;
			BiasDelta = learnRate * Gradient;
			Bias += BiasDelta + momentum * prevDelta;

			foreach (var synapse in InputSynapses)
			{
				prevDelta = synapse.WeightDelta;
				synapse.WeightDelta = learnRate * Gradient * synapse.InputNeuron.Value;
				synapse.Weight += synapse.WeightDelta + momentum * prevDelta;
			}
		}


        #region GLRender implementation

        public void RenderSynapses(Vector3 startPos, Vector3 endPos, float neuronDist)
        {
            for(int i = 0; i< OutputSynapses.Count; i++)
            {
                Synapse synapse = OutputSynapses[i];
                Color neuronCol = GLHelper.ColorFromUnitFloat(synapse.Weight / 10f);

                GLHelper.DrawLine(startPos, endPos, neuronCol);
                endPos.y -= neuronDist;
            }
        }

        #endregion
    }
}
