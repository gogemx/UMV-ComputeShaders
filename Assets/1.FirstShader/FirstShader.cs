using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class FirstShader : MonoBehaviour
{
    [SerializeField] private ComputeShader computeShader = null;
    [SerializeField] private RenderTexture renderTexture = null;

    public DefaultFormat ARGB32 { get; private set; }

    private void Start()
    {
        var rt = renderTexture = new RenderTexture(512, 256, 16, ARGB32);
        rt.enableRandomWrite = true;
        rt.Create();
        int kernel = computeShader.FindKernel("CSMain");
        computeShader.SetTexture(kernel, "Result", rt);
        computeShader.Dispatch(kernel, rt.width / 16, rt.height / 8, 1);
    }
}
