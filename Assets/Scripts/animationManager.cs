using UnityEngine;
using UnityEngine.UIElements;

public class animationManager : MonoBehaviour
{
    public GameObject Bar;

    private SkinnedMeshRenderer BarmRenderer;
    private SkinnedMeshRenderer RigmRenderer;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BarmRenderer = Bar.GetComponent<SkinnedMeshRenderer>();
        RigmRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = BarmRenderer.GetBlendShapeWeight(0);
        float w = BarmRenderer.GetBlendShapeWeight(1);
        float L = BarmRenderer.GetBlendShapeWeight(2);
        float d = BarmRenderer.GetBlendShapeWeight(3);


        RigmRenderer.SetBlendShapeWeight(0, h);
        RigmRenderer.SetBlendShapeWeight(1, w);
        RigmRenderer.SetBlendShapeWeight(2, L);
        RigmRenderer.SetBlendShapeWeight(3, d);
    }
}
