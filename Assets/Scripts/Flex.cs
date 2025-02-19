using UnityEngine;

public class Flex : MonoBehaviour
{
    [Header("Force (N/mm)")]
    public float forcePerSecond = 5.0f;
    public float reboundMult = 5.0f;
    public float maxForce = 10.0f;

    [Header("Bar dimensions (mm)")]
    public float h = 1.0f;
    public float w = 10.0f;
    public float L = 100.0f;

    [Header("Flexural modulus (N/mm^2)")]
    public float E = 72000.0f; 

    private SkinnedMeshRenderer mRenderer;
    private BoxCollider mCollider;
    private float currentForce = 0.0f;
    private float collisionForce = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mRenderer = GetComponent<SkinnedMeshRenderer>();
        mCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //Set blendshapes and collider for bar dimensions
        mRenderer.SetBlendShapeWeight(0, h);
        mRenderer.SetBlendShapeWeight(1, w);
        mRenderer.SetBlendShapeWeight(2, L);

        mCollider.size = new Vector3(L, h, w) / 1000.0f;


        if (Input.GetKey(KeyCode.F))
        {
            currentForce += forcePerSecond * Time.deltaTime;
        }
        else
        {
            currentForce -= forcePerSecond * reboundMult * Time.deltaTime;
        }

        currentForce = Mathf.Clamp(currentForce, 0.0f, maxForce);

        //Use object
        //currentForce = collisionForce;



        float ldeflection = deflection(currentForce, h, w, L, E);

        mRenderer.SetBlendShapeWeight(3, ldeflection);
       
        mCollider.center = new Vector3(0, -ldeflection, 0) / 1000.0f;

        Debug.Log("Current force: " + Mathf.Round(currentForce*100.0f) / 100.0f + " N/mm" +
                  " | deflection: " + Mathf.Round(ldeflection * 100.0f) / 100.0f + " mm");
    }

    float deflection(float pF, float ph, float pw, float pL,float pE) //from flexural modulus equation
    {
        return (Mathf.Pow(pL, 3.0f) * pF) / (4.0f * pw * Mathf.Pow(ph, 3.0f) * pE);
    }

    private void OnCollisionStay(Collision collision)
    {
        collisionForce = Mathf.Abs(collision.gameObject.GetComponent<Rigidbody>().mass * Physics.gravity.y);
    }
}
