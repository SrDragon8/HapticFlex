using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_and_animationManager : MonoBehaviour
{
    public GameObject Bar;
    public Slider HSlider;
    public TMP_InputField HInput;
    public Slider WSlider;
    public TMP_InputField WInput;
    public Slider LSlider;
    public TMP_InputField LInput;

    public TMP_InputField EInput;

    public TMP_Text outD;
    public TMP_Text outF;

    private SkinnedMeshRenderer BarmRenderer;
    private SkinnedMeshRenderer RigmRenderer;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BarmRenderer = Bar.GetComponent<SkinnedMeshRenderer>();
        RigmRenderer = GetComponent<SkinnedMeshRenderer>();
        changeH();
        changeW();
        changeL();
        changeEInput();
        changeE();
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

        outDeflection();
        outResistance();
    }

    public void changeHSlider()
    {
        HSlider.value = Mathf.RoundToInt(float.Parse(HInput.text));
    }
    public void changeH()
    {
        Bar.GetComponent<Flex>().h = HSlider.value;
        HInput.text = Mathf.RoundToInt(HSlider.value).ToString();
    }


    public void changeWSlider()
    {
        WSlider.value = Mathf.RoundToInt(float.Parse(WInput.text));
    }
    public void changeW()
    {
        Bar.GetComponent<Flex>().w = WSlider.value;
        WInput.text = Mathf.RoundToInt(WSlider.value).ToString();
    }


    public void changeLSlider()
    {
        LSlider.value = Mathf.RoundToInt(float.Parse(LInput.text));
    }
    public void changeL()
    {
        Bar.GetComponent<Flex>().L = LSlider.value;
        LInput.text = Mathf.RoundToInt(LSlider.value).ToString();
    }

    public void changeEInput()
    {
        EInput.text = Mathf.RoundToInt(Bar.GetComponent<Flex>().E).ToString();
    }

    public void changeE()
    {
        Bar.GetComponent<Flex>().E = Mathf.RoundToInt(float.Parse(EInput.text));
    }


    public void outDeflection()
    {
        float ldeflection = Bar.GetComponent<Flex>().hapticManager.GetComponent<SampleSceneHM>().outDisplacement * 10.0f;

        outD.text = "Deflection: " + Mathf.Round(ldeflection * 100.0f) / 100.0f + " mm";
    }

    public void outResistance()
    {
        float lResistanceForce = Bar.GetComponent<Flex>().hapticManager.GetComponent<SampleSceneHM>().inBarResistanceForce;

        outF.text = "Force: " + Mathf.Round(lResistanceForce * 100.0f) / 100.0f + " N";
    }
}
