using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultButton2 : MonoBehaviour
{
    public Button myButton2;


    public void OnClickMyButton() // Private for declaring in the code, but public for the button to access it in the editor
    {
        Debug.Log("Button Clicked!");
    }
}
