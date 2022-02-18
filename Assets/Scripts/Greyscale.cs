using UnityEngine;
using System.Collections;

public class Greyscale : MonoBehaviour
{
    private Material mat;
    public Shader shader;

    void Start()
    {
        mat = new Material(shader);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, mat);
    }
}