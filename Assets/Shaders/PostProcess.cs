using UnityEngine;

public class PostProcess : MonoBehaviour
{
    public Shader shader;
    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = new Material(shader); 
    }

    // Update is called once per frame
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
