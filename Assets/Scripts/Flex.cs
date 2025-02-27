using UnityEngine;

public class Flex : MonoBehaviour
{
    [Header("Force (N/mm)")]
    public float OutFlexResistance = 0.0f;
    public GameObject hapticManager;
    private float hapticDisplacement;

    [Header("Bar dimensions (mm)")]
    public float h = 1.0f;
    public float w = 10.0f;
    public float L = 100.0f;

    [Header("Flexural modulus (N/mm^2)")]
    public float E = 72000.0f; 

    private SkinnedMeshRenderer mRenderer;
    private float currentForce = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Set blendshapes and collider for bar dimensions
        mRenderer.SetBlendShapeWeight(0, h);
        mRenderer.SetBlendShapeWeight(1, w);
        mRenderer.SetBlendShapeWeight(2, L);

        hapticDisplacement = hapticManager.GetComponent<SampleSceneHM>().outDisplacement;

        currentForce = hapticDisplacement * 10.0f; //10mm

        //float ldeflection = deflection(currentForce, h, w, L, E);
        float ldeflection = hapticDisplacement * 10.0f; //10mm deflection

        mRenderer.SetBlendShapeWeight(3, ldeflection);
        
        OutFlexResistance = flexForce(ldeflection, h, w, L, E);

        hapticManager.GetComponent<SampleSceneHM>().inBarResistanceForce = OutFlexResistance;

        ///*
        Debug.Log("Current force: " + Mathf.Round(currentForce*100.0f) / 100.0f + " N/mm" +
                  " | deflection: " + Mathf.Round(ldeflection * 100.0f) / 100.0f + " mm" +
                  " | resistance force: " + Mathf.Round(OutFlexResistance * 100.0f) / 100.0f + " N");
        //*/
    }

    float deflection(float pF, float ph, float pw, float pL,float pE) //from flexural modulus equation
    {
        return (Mathf.Pow(pL, 3.0f) * pF) / (4.0f * pw * Mathf.Pow(ph, 3.0f) * pE);
    }

    float flexForce(float pD, float ph, float pw, float pL, float pE) //from flexural modulus equation
    {
        return (4.0f * pw * Mathf.Pow(ph, 3.0f) * pE * pD) / Mathf.Pow(pL, 3.0f);
    }

}
