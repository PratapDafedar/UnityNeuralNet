using UnityEngine;
using System.Collections;
using NeuralNetwork.GLRendering;
using System.Collections.Generic;

[ExecuteInEditMode]
public class WireFrame : MonoBehaviour {

	public Color lineColor; 
	public Color backgroundColor; 
	public bool ZWrite = true; 
	public bool AWrite = true; 
	public bool blend = true;

    public Shader lineShader;
	
	private Vector3[] lines; 
	private ArrayList linesArray; 
	private Material lineMaterial;

	private List<Vector3> randomPoints;

	// Use this for initialization
	void Start () {
		lineMaterial = new Material(lineShader); 
		
		lineMaterial.hideFlags = HideFlags.HideAndDontSave; 
		lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave; 
		
		linesArray = new ArrayList(); 
		MeshFilter filter = GetComponent<MeshFilter>(); 
		Mesh mesh = filter.sharedMesh; 
		Vector3[] vertices = mesh.vertices; 
		int[] triangles = mesh.triangles; 
		
		for (int i = 0; i < triangles.Length / 3; i++) 
		{ 
			linesArray.Add(vertices[triangles[i * 3]]); 
			linesArray.Add(vertices[triangles[i * 3 + 1]]); 
			linesArray.Add(vertices[triangles[i * 3 + 2]]); 
		} 

		lines = new Vector3[triangles.Length];
		for ( int i = 0 ; i < triangles.Length ; i ++ ){
			lines[i] = (Vector3)linesArray[i];
		}

		randomPoints = new List<Vector3> ();
	}

//	void OnRenderObject() 
//	{    
//		//meshRenderer.sharedMaterial.color = backgroundColor; 
//		lineMaterial.SetPass(0); 
//		
//		GL.PushMatrix(); 
//		GL.MultMatrix(transform.localToWorldMatrix); 
//		GL.Begin(GL.LINES); 
//		
//		for (int i = 0; i < lines.Length / 3; i++) 
//		{ 
//			GL.Color(lineColor); 
//			GL.Vertex(lines[i * 3]); 
//			GL.Vertex(lines[i * 3 + 1]); 
//
//			GL.Color(lineColor * 2);
//			GL.Vertex(lines[i * 3 + 1]); 
//			GL.Vertex(lines[i * 3 + 2]); 
//
//			GL.Color(backgroundColor);
//			GL.Vertex(lines[i * 3 + 2]); 
//			GL.Vertex(lines[i * 3]); 
//		} 
//		
//		GL.End(); 
//		GL.PopMatrix(); 
//	}

	void OnRenderObject() 
	{    
		lineMaterial.SetPass(0); 

		GLHelper.BeginRendering (transform.localToWorldMatrix);

		GL.Begin (GL.LINES);
		for (int i = 0; i < lines.Length / 3; i++) 
		{
			GLHelper.DrawLine(lines[i * 3], lines[i * 3 + 1], lineColor);
			GLHelper.DrawLine(lines[i * 3 + 1], lines[i * 3 + 2], lineColor);
			GLHelper.DrawLine(lines[i * 3 + 2], lines[i * 3], backgroundColor);
		}
		GL.End ();
//
		GL.Begin (GL.QUADS);
//		foreach (Vector3 p in randomPoints) {
//			GLHelper.DrawCube (p, 0.1f, Color.green);
//		}
		GLHelper.DrawCuboid (transform.position, transform.localScale, Color.green);
		GL.End ();
		GLHelper.EndRendering ();
	}
		
//	void Update()
//	{
//		if (Input.GetKey (KeyCode.Space)) {
//			randomPoints.Add (new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f)));
//		}
//	}
}
