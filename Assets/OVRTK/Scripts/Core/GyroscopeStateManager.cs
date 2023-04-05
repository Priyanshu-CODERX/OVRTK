using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool CheckGyroSupport()
    {
        if (SystemInfo.supportsGyroscope)
        {
            return true;
        }

        return false;
    }

    private void enableGyroNotSupportedWarning()
    {
        if (!isGyroSupported && !SimulationManager.EnableSimulator)
        {
            GameObject NotifierCanvas = Instantiate(_GyroNotSupportedPopUp);
            NotifierCanvas.transform.SetParent(transform);
        }
    }
}
