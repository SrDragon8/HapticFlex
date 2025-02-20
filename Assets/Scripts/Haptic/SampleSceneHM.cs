using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleSceneHM : HapticManager
{
    public GameObject Bar;
    public float limitForce = 4.0f;

    public float resistanceMinForce = 1.0f;
    public float resistanceMaxForce = 10.0f;
    public float outDisplacement;


    protected override void ApplyForceToHapticDevice(int index, bool[,] buttons)
    {
        //hapticDevices[index]
        Vector3 currentPos = HapticPluginImport.GetHapticsPositions(hapticPlugin, index);

        Vector3 force = new Vector3(0, 0, 0);

        if(currentPos.y < 0.07f)
        {
            force = new Vector3(0, 1, 0);
        }

        outDisplacement = changeRange(currentPos.y, -0.08f, 0.08f, 1.0f, 0.0f);

        force = force * changeRange(outDisplacement,0.0f,1.0f, resistanceMinForce, resistanceMaxForce);

        HapticPluginImport.SetHapticsForce(hapticPlugin, index, force);

        HapticPluginImport.UpdateHapticDevices(hapticPlugin, index);
    }
    float changeRange(float v, float inMin, float inMax, float outMin, float outMax)
    {
        return ((v - inMin) / (inMax - inMin)) * (outMax - outMin) + outMin;
    }
}
