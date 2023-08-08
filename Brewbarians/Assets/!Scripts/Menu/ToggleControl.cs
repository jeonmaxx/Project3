using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleControl : MonoBehaviour
{
    public Toggle otherToggle;
    private Toggle thisToggle;

    public void Start()
    {
        thisToggle = GetComponent<Toggle>();
    }

    public void Update()
    {
        thisToggle.onValueChanged.AddListener(delegate { OnToggleExec(); });
    }

    private void OnToggleExec()
    {
        if(thisToggle.isOn)
        {
            otherToggle.isOn = false;
        }
    }
}
