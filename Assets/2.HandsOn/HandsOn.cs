using UnityEngine;

public class HandsOn : MonoBehaviour
{
    [SerializeField] private ComputeShader computeShader = null;
    [SerializeField] private RenderTexture renderTexture = null;

    [SerializeField] private Vector2Int resolution = new Vector2Int(512, 256);

    private void Start()
    {
        if (renderTexture == null)
        {
            renderTexture = new RenderTexture(resolution.x, resolution.y, 16, RenderTextureFormat.ARGB32);
            renderTexture.enableRandomWrite = true;
            renderTexture.Create();
        }

        int kernel = computeShader.FindKernel("HandsOn");

        computeShader.SetVector("resolution", new Vector4(resolution.x, resolution.y));
        computeShader.SetTexture(kernel, "target", renderTexture);
        computeShader.Dispatch(kernel, renderTexture.width / 8, renderTexture.height / 8, 1);
    }
}
