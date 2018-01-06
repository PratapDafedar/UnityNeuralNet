using UnityEngine;
using System.Collections;
using NeuralNetwork;
using UnityEngine.UI;
using System.Collections.Generic;
using NeuralNetwork.GLRendering;

public class ColorPicking : MonoBehaviour {

    public Config config;
    public GLConfig visualisationConfig;

    //Neural Network Variables
    private const float MinimumError = 0.01f;
    [SerializeField]
	private NeuralNet net;
	private static List<DataSet> dataSets; 

	public Image I1;
	public Image I2;

	public GameObject pointer1;
	public GameObject pointer2;

    public NeuralNetRenderer neuralNetRenderer;

	bool trained;

	int i = 0;

	// Use this for initialization
	void Start () 
	{
        //Input - 3 (r,g,b) -- Output - 1 (Black/White)
        net = new NeuralNet(config, visualisationConfig);// (3, 4, 1, 1, 0.3f, 0.8f, 100);
		dataSets = new List<DataSet>();
		Next();
        
        neuralNetRenderer.InitRender(net);
    }

	void Next()
	{
		Color c = new Color(Random.Range(0,1f), Random.Range(0,1f), Random.Range(0,1f));
		I1.color = c;
		I2.color = c;
		float[] C = {(float)I1.color.r, (float)I1.color.g, (float)I1.color.b};
		if(trained)
		{
			float d = tryValues(C);
			if(d > 0.5)
			{
				pointer1.SetActive(false);
				pointer2.SetActive(true);
			}
			else
			{
				pointer1.SetActive(true);
				pointer2.SetActive(false);
			}
		}
	}
	
	public void Train(float val)
	{ 
		float[] C = {(float)I1.color.r, (float)I1.color.g, (float)I1.color.b};
		float[] v = {(float)val};
		dataSets.Add(new DataSet(C, v));

		i++;
		//if(!trained && i%10 == 9)
			Train();

		Next();
	}

	private void Train()
	{
		net.Train(dataSets, MinimumError);
        trained = true;
	}

	float tryValues(float[] vals)
	{
	 	float[] result = net.Compute(vals);
	 	return result[0];
	}


}
