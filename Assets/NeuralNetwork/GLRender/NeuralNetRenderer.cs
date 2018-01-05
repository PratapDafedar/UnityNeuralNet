using UnityEngine;

namespace NeuralNetwork.GLRendering
{
    public class NeuralNetRenderer : MonoBehaviour
    {
        [Header("Rendering shader")]
        public Shader shader;

        private IGLRender glRender;
        private Material material;

        private bool initialised = false;

        public void InitRender(IGLRender neuralNet)
        {
            this.glRender = neuralNet;
            Init ();
            initialised = true;
        }

        public void UpdateRender()
        {

        }

        private void Init()
        {
            material = new Material(shader);
            material.hideFlags = HideFlags.HideAndDontSave;
            material.shader.hideFlags = HideFlags.HideAndDontSave;
        }

        void OnRenderObject()
        {
            if (!initialised)
            {
                return;
            }

            material.SetPass(0);
            GLHelper.BeginRendering(transform.localToWorldMatrix);

			glRender.Render(transform.position);

            GLHelper.EndRendering();
        }
    }
}