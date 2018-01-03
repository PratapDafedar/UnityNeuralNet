using UnityEngine;

namespace NeuralNetwork
{
    public static class Sigmoid
    {
        public static float Output(float x)
        {
            return x < -45.0f ? 0.0f : x > 45.0f ? 1.0f : 1.0f / (1.0f + Mathf.Exp((float)-x));
        }

        public static float Derivative(float x)
        {
            return x * (1 - x);
        }
    }
}