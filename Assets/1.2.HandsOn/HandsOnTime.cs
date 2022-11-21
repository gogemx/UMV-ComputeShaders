using UnityEngine;

public class HandsOnTime : MonoBehaviour
{
    [SerializeField] private ComputeShader computeShader;
    [SerializeField] private Vector2Int resolution = new Vector2Int(512, 256);
    [SerializeField] private float timeScale = 1f;

    private RenderTexture renderTexture = null;

    private void Start()
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
    }

    private void Update()
    {
        computeShader.SetFloat("time", timeScale * Time.realtimeSinceStartup);
        computeShader.Dispatch(0, renderTexture.width / 8, renderTexture.height / 8, 1);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(renderTexture, destination);
    }
}

