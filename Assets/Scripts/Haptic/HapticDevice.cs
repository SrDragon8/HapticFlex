using System;
using Unity.VisualScripting;
using UnityEngine;

public class HapticDevice : MonoBehaviour
{
    [Header("Haptic Device Settings")]
    // Haptic device settings
    public HapticManager hapticManager;
    public GameObject hip;
    public GameObject ihip;
    protected int deviceNumber;

    // Haptic device transform
    protected Vector3 position;
    protected Quaternion rotation;

    // Haptic device physics
    protected float mass;
    protected Vector3 force;
    protected Vector3 torque;

    // Button states
    [SerializeField]
    protected bool [] buttons;

    // Material, Rigidbody, and Collider properties
    protected Material material;
    protected Rigidbody rb;
    protected Collider col;

    // Editor properties
    public int DeviceNumber => deviceNumber;
    public Vector3 Position => position;
    public Quaternion Rotation => rotation;
    public float Mass => mass;
    public Vector3 Force => force;
    public Vector3 Torque => torque;
    public bool[] ButtonStates => buttons; // Assuming buttonStates is your private array

    // Define here any additional properties you want to use in the script

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Get the material and rigidbody components
        material = ihip.GetComponent<Renderer>().material;
        rb = hip.GetComponent<Rigidbody>();
        col = hip.GetComponent<Collider>();

        // Set the parameters of the haptic device
        position = Vector3.zero;
        rotation = Quaternion.identity;
        force = Vector3.zero;
        torque = Vector3.zero;
        buttons = new bool[4];
        mass = rb.mass;
    }

    // Start is called before the first frame update
    protected void Start()
    {
        if (hapticManager == null)
        {
            Debug.LogError("HapticManager is not assigned on " + gameObject.name);
            return;
        }

        deviceNumber = hapticManager.GetIndexOfGameObject(this);

        if (deviceNumber == -1)
        {
            Debug.LogError("HapticDevice not found in HapticManager's devices array for " + gameObject.name);
        }

    }

    // Update is called once per frame
    protected void Update()
    {
        // Update the position, rotation, and button states of the haptic device
        position = hapticManager.GetPosition(deviceNumber);
        rotation = hapticManager.GetRotation(deviceNumber);
        buttons = hapticManager.GetButtonStates(deviceNumber);

        // update positions of HIP and IHIP
        hip.transform.position = position;
        hip.transform.rotation = rotation;

        // update rotation of IHIP
        ihip.transform.position = position;
        ihip.transform.rotation = rotation;

        // change material color based on button states
        ChangeMaterialColor(buttons);
    }

    // Method that changes the color of the material based on the button states
    protected void ChangeMaterialColor(bool[] buttons)
    {
        
        if (buttons[0])
        {
            material.color = Color.red;
        }
        else if (buttons[1])
        {
            material.color = Color.green;
        }
        else if (buttons[2])
        {
            material.color = Color.blue;
        }
        else if (buttons[3])
        {
            material.color = Color.yellow;
        }
        else
        {
            material.color = Color.white;
        }
    }

    public float GetMass()
    {
        return mass;
    }   
}
