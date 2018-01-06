using UnityEngine;

namespace NeuralNetwork.GLRendering
{
	public class GLHelper
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

        public static void DrawLine(Vector3 pointA, Vector3 pointB, Color color, float thickness = 1)
        {
            GL.Color(color);

            var perpendicular = (new Vector3(pointB.y, pointA.x, pointA.z) -
                                     new Vector3(pointA.y, pointB.x, pointA.z)).normalized * thickness;
            var v1 = new Vector3(pointA.x, pointA.y, pointA.z);
            var v2 = new Vector3(pointB.x, pointB.y, pointA.z);
            GL.Vertex(v1 - perpendicular);
            GL.Vertex(v1 + perpendicular);
            GL.Vertex(v2 + perpendicular);
            GL.Vertex(v2 - perpendicular);
        }

        public static void DrawCube(Vector3 point, float scale, Color color)
		{
			DrawCuboid (point, new Vector3 (scale, scale, scale), color);
		}

		public static void DrawCuboid(Vector3 p, Vector3 s, Color color)
		{//purposefully NOT reverse faced cuboid.
			GL.Color(color);
			GL.Vertex3(p.x - s.x, p.y - s.y, p.z - s.z);
			GL.Vertex3(p.x + s.x, p.y - s.y, p.z - s.z);
			GL.Vertex3(p.x + s.x, p.y + s.y, p.z - s.z);
			GL.Vertex3(p.x - s.x, p.y + s.y, p.z - s.z);

			GL.Vertex3(p.x + s.x, p.y - s.y, p.z - s.z);
			GL.Vertex3(p.x + s.x, p.y - s.y, p.z + s.z);
			GL.Vertex3(p.x + s.x, p.y + s.y, p.z + s.z);
			GL.Vertex3(p.x + s.x, p.y + s.y, p.z - s.z);

			GL.Vertex3(p.x + s.x, p.y - s.y, p.z + s.z);
			GL.Vertex3(p.x - s.x, p.y - s.y, p.z + s.z);
			GL.Vertex3(p.x - s.x, p.y + s.y, p.z + s.z);
			GL.Vertex3(p.x + s.x, p.y + s.y, p.z + s.z);

			GL.Vertex3(p.x - s.x, p.y - s.y, p.z + s.z);
			GL.Vertex3(p.x - s.x, p.y - s.y, p.z - s.z);
			GL.Vertex3(p.x - s.x, p.y + s.y, p.z - s.z);
			GL.Vertex3(p.x - s.x, p.y + s.y, p.z + s.z);

			GL.Vertex3(p.x - s.x, p.y - s.y, p.z + s.z);
			GL.Vertex3(p.x + s.x, p.y - s.y, p.z + s.z);
			GL.Vertex3(p.x + s.x, p.y - s.y, p.z - s.z);
			GL.Vertex3(p.x - s.x, p.y - s.y, p.z - s.z);

			GL.Vertex3(p.x - s.x, p.y + s.y, p.z - s.z);
			GL.Vertex3(p.x + s.x, p.y + s.y, p.z - s.z);
			GL.Vertex3(p.x + s.x, p.y + s.y, p.z + s.z);
			GL.Vertex3(p.x - s.x, p.y + s.y, p.z + s.z);
		}

        public static Color ColorFromUnitFloat(float val)
        {//val - varies between -1f to 1f.
            return (val >= 0) ? new Color(val, val, val) :
                new Color(-val, 0, 0);
        }
	}
}