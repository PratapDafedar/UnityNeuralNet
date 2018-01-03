using UnityEngine;

namespace NeuralNetwork.GLRendering
{
	public static class GLHelper
	{
		public static void BeginRendering(Matrix4x4 matrix)
		{
			GL.PushMatrix(); 
			GL.MultMatrix(matrix);
		}

		public static void EndRendering()
		{
			GL.PopMatrix();
		}

		public static void DrawLine(Vector3 pointA, Vector3 pointB, Color color)
		{
			GL.Color(color);
			GL.Vertex(pointA); 
			GL.Vertex(pointB);
		}

		public static void DrawCube(Vector3 point, float scale, Color color)
		{
			DrawCuboid (point, new Vector3 (scale, scale, scale), color);
		}

		public static void DrawCuboid(Vector3 p, Vector3 s, Color color)
		{//purposefully reverse faced cuboid.
			GL.Color(color);
			GL.Vertex3(p.x - s.x, p.y + s.y, p.z - s.z);
			GL.Vertex3(p.x + s.x, p.y + s.y, p.z - s.z);
			GL.Vertex3(p.x + s.x, p.y - s.y, p.z - s.z);
			GL.Vertex3(p.x - s.x, p.y - s.y, p.z - s.z);
			GL.Vertex3(p.x + s.x, p.y + s.y, p.z - s.z);
			GL.Vertex3(p.x + s.x, p.y + s.y, p.z + s.z);
			GL.Vertex3(p.x + s.x, p.y - s.y, p.z + s.z);
			GL.Vertex3(p.x + s.x, p.y - s.y, p.z - s.z);
			GL.Vertex3(p.x + s.x, p.y + s.y, p.z + s.z);
			GL.Vertex3(p.x - s.x, p.y + s.y, p.z + s.z);
			GL.Vertex3(p.x - s.x, p.y - s.y, p.z + s.z);
			GL.Vertex3(p.x + s.x, p.y - s.y, p.z + s.z);
			GL.Vertex3(p.x - s.x, p.y + s.y, p.z + s.z);
			GL.Vertex3(p.x - s.x, p.y + s.y, p.z - s.z);
			GL.Vertex3(p.x - s.x, p.y - s.y, p.z - s.z);
			GL.Vertex3(p.x - s.x, p.y - s.y, p.z + s.z);
			GL.Vertex3(p.x - s.x, p.y - s.y, p.z - s.z);
			GL.Vertex3(p.x + s.x, p.y - s.y, p.z - s.z);
			GL.Vertex3(p.x + s.x, p.y - s.y, p.z + s.z);
			GL.Vertex3(p.x - s.x, p.y - s.y, p.z + s.z);
			GL.Vertex3(p.x - s.x, p.y + s.y, p.z + s.z);
			GL.Vertex3(p.x + s.x, p.y + s.y, p.z + s.z);
			GL.Vertex3(p.x + s.x, p.y + s.y, p.z - s.z);
			GL.Vertex3(p.x - s.x, p.y + s.y, p.z - s.z);
		}
	}
}