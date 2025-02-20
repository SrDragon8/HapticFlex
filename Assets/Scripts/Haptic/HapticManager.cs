using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract class HapticManager : MonoBehaviour
{
    // Plugin import
    protected IntPtr hapticPlugin;

    // Haptic thread
    private Thread hapticThread;
    private bool hapticThreadRunning;

    // Haptic devices in the scene
    [Header("Haptic Devices")]
    public float hapticDeviceWorkspace = 100;
    private int hapticDevicesDetected;
    private int numHapDev; // Max number of haptic devices
    public HapticDevice[] hapticDevices;
    protected Vector3[] hapticPositions;
    protected Quaternion[] hapticRotations;
    protected bool[,] hapticButtons;


    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        hapticThreadRunning = false;
        numHapDev = hapticDevices.Length;
        hapticPositions = new Vector3[numHapDev];
        hapticRotations = new Quaternion[numHapDev];
        hapticButtons = new bool[numHapDev, 4];
    }

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Starting Haptic Devices");
        Debug.Log("Haptic Devices Currently Assigned in the Haptic Manager: " + hapticDevices.Length);
        hapticPlugin = HapticPluginImport.CreateHapticDevices();
        hapticDevicesDetected = HapticPluginImport.GetHapticsDetected(hapticPlugin);
        if (hapticDevicesDetected > 0)
        {
            Debug.Log("Haptic Devices Found: " + hapticDevicesDetected);
            hapticThreadRunning = true;
            hapticThread = new Thread(HapticThread);
            hapticThread.IsBackground = true;
            hapticThread.Priority = System.Threading.ThreadPriority.Highest;
            hapticThread.Start();
        }
        else
        {
            Debug.Log("Haptic Devices Cannot be Found");
            Application.Quit();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // Exit application
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // OnApplicationQuit is called when the application is quitting
    private void OnApplicationQuit()
    {
        if (hapticThreadRunning != false)
        {
            // close haptic thread
            EndHapticThread();
            // delete haptic plugin
            HapticPluginImport.DeleteHapticDevices(hapticPlugin);
            Debug.Log("Application Ended correctly");

        }
    }

    private void HapticThread()
    {
        while (hapticThreadRunning)
        {
            for (int i = 0; i < hapticDevices.Length; i++)
            {
                // get haptic positions and convert them into scene positions
                hapticPositions[i] = hapticDeviceWorkspace * HapticPluginImport.GetHapticsPositions(hapticPlugin, i);
                // hapticRotations[i] = HapticPluginImport.GetHapticsOrientations(hapticPlugin, i);

                // get haptic buttons
                hapticButtons[i,0] = HapticPluginImport.GetHapticsButtons(hapticPlugin, i, 1);
                hapticButtons[i,1] = HapticPluginImport.GetHapticsButtons(hapticPlugin, i, 2);
                hapticButtons[i,2] = HapticPluginImport.GetHapticsButtons(hapticPlugin, i, 3);
                hapticButtons[i,3] = HapticPluginImport.GetHapticsButtons(hapticPlugin, i, 4);

                ApplyForceToHapticDevice(i, hapticButtons);
                HapticPluginImport.UpdateHapticDevices(hapticPlugin, i);
            }
        }
    }

    protected virtual void ApplyForceToHapticDevice(int index, bool[,] buttons)
    {
        
    }

    // Closes the thread that was created
    void EndHapticThread()
    {
        hapticThreadRunning = false;
        Thread.Sleep(100);

        // variables for checking if thread hangs
        bool isHung = false; // could possibely be hung during shutdown
        int timepassed = 0;  // how much time has passed in milliseconds
        int maxwait = 10000; // 10 seconds
        Debug.Log("Shutting Down Haptic Thread");
        try
        {
            // loop until haptic thread is finished
            while (hapticThread.IsAlive && timepassed <= maxwait)
            {
                Thread.Sleep(10);
                timepassed += 10;
            }

            if (timepassed >= maxwait)
            {
                isHung = true;
            }
            // Unity tries to end all threads associated or attached
            // to the parent threading model, if this happens, the 
            // created one is already stopped; therefore, if we try to 
            // abort a thread that is stopped, it will throw a mono error.
            if (isHung)
            {
                Debug.Log("Haptic Thread is Hung, Checking IsLive State");
                if (hapticThread.IsAlive)
                {
                    Debug.Log("Haptic Thread Object IsLive, Forcing Abort mode");
                    hapticThread.Abort();
                }
            }
            Debug.Log("Shutdown of Haptic Thread Completed.");
        }
        catch (Exception e)
        {
            // lets let the user know the error, Unity will end normally
            Debug.Log("ERROR During OnApplicationQuit: " + e.ToString());
        }
    }


    // Method to find the index of a GameObject
    public int GetIndexOfGameObject(HapticDevice obj)
    {
        for (int i = 0; i < hapticDevices.Length; i++)
        {
            if (hapticDevices[i] == obj)
            {
                return i; // Return the index of the GameObject
            }
        }
        return -1; // Return -1 if the GameObject is not found
    }

    // Method to get the position of a haptic device
    public Vector3 GetPosition(int deviceNumber)
    {
        return hapticPositions[deviceNumber];
    }

    // Method to get the rotation of a haptic device
    public Quaternion GetRotation(int deviceNumber)
    {
        return hapticRotations[deviceNumber];
    }

    // Method to get the button states of a haptic device
    public bool[] GetButtonStates(int deviceNumber)
    {
        return new bool[] { hapticButtons[deviceNumber, 0], hapticButtons[deviceNumber, 1], hapticButtons[deviceNumber, 2], hapticButtons[deviceNumber, 3] };
    }

    // Method to get the haptic device information
    public float GetHapticDeviceInfo(int numHapDev, int parameter)
    {
        // Haptic information variables
        // 0 - m_maxLinearForce
        // 1 - m_maxAngularTorque
        // 2 - m_maxGripperForce 
        // 3 - m_maxLinearStiffness
        // 4 - m_maxAngularStiffness
        // 5 - m_maxGripperLinearStiffness;
        // 6 - m_maxLinearDamping
        // 7 - m_maxAngularDamping
        // 8 - m_maxGripperAngularDamping

        float temp;
        switch (parameter)
        {
            case 1:
                temp = (float)HapticPluginImport.GetHapticsDeviceInfo(hapticPlugin, numHapDev, 1);
                break;
            case 2:
                temp = (float)HapticPluginImport.GetHapticsDeviceInfo(hapticPlugin, numHapDev, 2);
                break;
            case 3:
                temp = (float)HapticPluginImport.GetHapticsDeviceInfo(hapticPlugin, numHapDev, 3);
                break;
            case 4:
                temp = (float)HapticPluginImport.GetHapticsDeviceInfo(hapticPlugin, numHapDev, 4);
                break;
            case 5:
                temp = (float)HapticPluginImport.GetHapticsDeviceInfo(hapticPlugin, numHapDev, 5);
                break;
            case 6:
                temp = (float)HapticPluginImport.GetHapticsDeviceInfo(hapticPlugin, numHapDev, 6);
                break;
            case 7:
                temp = (float)HapticPluginImport.GetHapticsDeviceInfo(hapticPlugin, numHapDev, 7);
                break;
            case 8:
                temp = (float)HapticPluginImport.GetHapticsDeviceInfo(hapticPlugin, numHapDev, 8);
                break;
            default:
                temp = (float)HapticPluginImport.GetHapticsDeviceInfo(hapticPlugin, numHapDev, 0);
                break;
        }
        return temp;
    }

}
