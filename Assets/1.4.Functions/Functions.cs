using UnityEngine;

public class Functions : MonoBehaviour
{
    [SerializeField] private ComputeShader computeShader;
    [SerializeField] private Vector2Int resolution = new Vector2Int(512, 256);

    private RenderTexture renderTexture = null;
    private int kernel = 0;

    private void Start()
    {
        if (renderTexture == null)
        {
            renderTexture = new RenderTexture(resolution.x, resolution.y, 16, RenderTextureFormat.ARGB32);
            renderTexture.enableRandomWrite = true;
            renderTexture.Create();
        }

        int kernel = computeShader.FindKernel("Functions");

        computeShader.SetVector("resolution", new Vector4(resolution.x, resolution.y));
        computeShader.SetTexture(kernel, "target", renderTexture);
    }

    private void Update()
    {
        computeShader.SetFloat("time", Time.realtimeSinceStartup);
        computeShader.Dispatch(kernel, renderTexture.width / 32, renderTexture.height / 32, 1);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(renderTexture, destination);
    }
}
