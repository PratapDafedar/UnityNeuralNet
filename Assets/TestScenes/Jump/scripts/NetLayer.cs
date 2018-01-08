using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeuralNetwork;
using UnityEngine.UI;
using System.Threading;
using NeuralNetwork.GLRendering;

public class NetLayer : MonoBehaviour
{
    public Config config;
    public GLConfig visualisationConfig;

    public NeuralNetRenderer neuralNetRenderer;

    //Neural Network Variables
    private const float MinimumError = 0.1f;
    public NeuralNet net;
    public static List<DataSet> dataSets;

    private int collectedDatasets = 0;
    private const int maxNumberOfDatasets = 60;

    public Player player;

    void Start()
    {
        net = new NeuralNet(config, visualisationConfig);
        dataSets = new List<DataSet>();
        neuralNetRenderer.InitRender(net);
    }

    void Update()
    {
		//Let the network decide if the player should jump
		float result = compute(new float[] { player.distanceInPercent, player.canJump });
		if (result > 0.5f)
		{
			player.jump();
			Debug.Log ("Jump : " + player.distanceInPercent);
		}
    }

    public void Train(float canJump, float jumped)
    {
        float[] C = { player.distanceInPercent, canJump };
        float[] v = { jumped };
        dataSets.Add(new DataSet(C, v));

        collectedDatasets++;
		net.Train(dataSets, MinimumError);
    }

    float compute(float[] vals)
    {
        float[] result = net.Compute(vals);
        return result[0];
    }
}