
namespace NeuralNetwork
{
    public class DataSet
    {
        public float[] Values { get; set; }
        public float[] Targets { get; set; }

        public DataSet(float[] values, float[] targets)
        {
            Values = values;
            Targets = targets;
        }
    }
}