using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleSceneHM : HapticManager
{
    public GameObject Bar;
    public float resistanceMinForce = 1.0f;
    public float resistanceMaxForce = 10.0f;
    public float outDisplacement;
    public float inBarResistanceForce;


    protected override void ApplyForceToHapticDevice(int index, bool[,] buttons)
    {
        //hapticDevices[index]
        Vector3 currentPos = HapticPluginImport.GetHapticsPositions(hapticPlugin, index);

        Vector3 force = new Vector3(0, 0, 0);

        if(currentPos.y < 0.07f)
        {
            force = new Vector3(0, 1, 0);
        }

        outDisplacement = changeRange(currentPos.y, -0.07f, 0.07f, 1.0f, 0.0f);

        outDisplacement = decay(outDisplacement);

        //force = force * changeRange(outDisplacement,0.0f, 1.0f, resistanceMinForce, resistanceMaxForce);
        force = force * inBarResistanceForce;

        HapticPluginImport.SetHapticsForce(hapticPlugin, index, force);

        HapticPluginImport.UpdateHapticDevices(hapticPlugin, index);
    }
    float changeRange(float v, float inMin, float inMax, float outMin, float outMax)
    {
        return ((v - inMin) / (inMax - inMin)) * (outMax - outMin) + outMin;
    }
    float decay(float x)
    {
        return Mathf.Pow(x, 1.0f/2.0f);

        //return 1.0f / Mathf.Pow(1 + Mathf.Pow(x-0.5f,2.0f),30.0f);
    }
}
