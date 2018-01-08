using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputeShaderTest : MonoBehaviour {

    public ComputeShader shader;

    struct VecMatPair
    {
        public Vector3 point;
        public Matrix4x4 matrix;
    }

    void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            RunShader();
        }
    }

    void RunShader()
    {
        VecMatPair[] data = new VecMatPair[5];
        VecMatPair[] output = new VecMatPair[5];

        //INITIALIZE DATA HERE

        ComputeBuffer buffer = new ComputeBuffer(data.Length, 76);
        buffer.SetData(data);
        int kernel = shader.FindKernel("Multiply");
        shader.SetBuffer(kernel, "dataBuffer", buffer);
        shader.Dispatch(kernel, data.Length, 1, 1);
        buffer.GetData(output);
        buffer.Dispose();
    }
}
