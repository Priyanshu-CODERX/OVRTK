using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeStateManager : MonoBehaviour
{
    private bool GyroEnabled;
    private Gyroscope _Gyro;
    public GyroscopeManager[] GManager;

    private Quaternion _Rot;

    public GameObject _GyroNotSupportedPopUp;

    public bool enableSimulator;
    private bool canvasAvailable;

    public float verticalSpeed = 2.0f;
    public float horizontalSpeed = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private void Start()
    {
        GyroEnabled = EnableGyro();
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            return true;
        }

        if (!enableSimulator && !canvasAvailable)
        {
            GameObject NotifierCanvas = Instantiate(_GyroNotSupportedPopUp);
            NotifierCanvas.transform.SetParent(transform);
            canvasAvailable = true;
        }

        return false;
    }

    private void Update()
    {
        if (GyroEnabled)
            transform.localRotation = _Gyro.attitude * _Rot;

        if (enableSimulator)
        {
            GManager[0].SimulatorEnabled = true;
            GManager[1].SimulatorEnabled = true;
        }
        else
        {
            GManager[0].SimulatorEnabled = false;
            GManager[1].SimulatorEnabled = false;
        }
    }

    public void Simulate()
    {
        yaw += horizontalSpeed * Input.GetAxis("Mouse X");
        pitch -= verticalSpeed * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
