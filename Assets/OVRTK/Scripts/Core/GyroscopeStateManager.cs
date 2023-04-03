using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GyroscopeStateManager : MonoBehaviour
{
    public LoggerUtility logUtils;

    public bool GyroEnabled;

    public GyroscopeManager[] gyroscopeManager;

    private Quaternion _Rot;

    public GameObject _GyroNotSupportedPopUp;

    public bool enableSimulator;

    public float verticalSpeed = 2.0f;
    public float horizontalSpeed = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private void Start()
    {
        GyroEnabled = EnableGyro();

        if (gyroscopeManager != null)
        {
            if (enableSimulator)
            {
                gyroscopeManager[0].SimulatorEnabled = true;
                gyroscopeManager[1].SimulatorEnabled = true;
            }
            else
            {
                gyroscopeManager[0].SimulatorEnabled = false;
                gyroscopeManager[1].SimulatorEnabled = false;
            }
        }
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            return true;
        }

        if (!enableSimulator)
        {
            GameObject NotifierCanvas = Instantiate(_GyroNotSupportedPopUp);
            NotifierCanvas.transform.SetParent(transform);
        }

        return false;
    }
}
