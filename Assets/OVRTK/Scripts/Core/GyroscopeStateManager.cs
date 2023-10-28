using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The code is for managing gyroscope support in Unity. 
/// It checks whether the device supports a gyroscope, and if it doesn't, it enables a warning popup. 
/// The script also has a reference to a SimulationManager component and a GyroscopeManager array, as well as a Quaternion variable for storing rotation data. 
/// The SimulationManager component is set in the Start() method, and the enableGyroNotSupportedWarning() method is called to show the popup if necessary.
/// </summary>
public class GyroscopeStateManager : MonoBehaviour
{
    private bool isGyroSupported;
    public GyroscopeManager[] gyroscopeManager;
    public GameObject _GyroNotSupportedPopUp;

    private Quaternion _Rot;
    private SimulationManager simulationManager;
    public SimulationManager SimulationManager
    {
        get
        {
            return simulationManager;
        }
        set
        {
            simulationManager = value;
        }
    }

    private void Start()
    {
        SimulationManager = GetComponent<SimulationManager>();

        isGyroSupported = CheckGyroSupport();
        enableGyroNotSupportedWarning();
    }

    //Checks for Gyroscope Support
    public bool CheckGyroSupport()
    {
        if (SystemInfo.supportsGyroscope)
        {
            return true;
        }

        return false;
    }

    // Enables Gyroscope Not Supported Prompt
    private void enableGyroNotSupportedWarning()
    {
        if (!isGyroSupported && !SimulationManager.EnableSimulator)
        {
            GameObject NotifierCanvas = Instantiate(_GyroNotSupportedPopUp);
            NotifierCanvas.transform.SetParent(transform);
        }
    }
}
