using UnityEngine;

public class HandsOnBlit : MonoBehaviour
{
    [SerializeField] private ComputeShader computeShader;
    [SerializeField] private Vector2Int resolution = new Vector2Int(512, 256);

    private RenderTexture renderTexture = null;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (renderTexture == null)
        {
            renderTexture = new RenderTexture(resolution.x, resolution.y, 16, RenderTextureFormat.ARGB32);
            renderTexture.enableRandomWrite = true;
            renderTexture.Create();
        }

        int kernel = 0;

        computeShader.SetVector("resolution", new Vector4(resolution.x, resolution.y));
        computeShader.SetTexture(kernel, "target", renderTexture);
        computeShader.Dispatch(kernel, renderTexture.width / 8, renderTexture.height / 8, 1);

        Graphics.Blit(renderTexture, destination);
    }
}
