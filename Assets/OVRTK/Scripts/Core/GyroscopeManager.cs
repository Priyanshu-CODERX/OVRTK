using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeManager : MonoBehaviour
{
    public GyroscopeStateManager StateManager = null;

    private bool GyroEnabled;
    private Gyroscope _Gyro;

    [SerializeField]
    private GameObject RigContainer;
    private Quaternion _Rot;

    [HideInInspector]
    public bool SimulatorEnabled = false;

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
            _Gyro = Input.gyro;
            _Gyro.enabled = true;

            RigContainer.transform.rotation = Quaternion.Euler(90f, 0f, 90f);
            _Rot = new Quaternion(0, 0, 1, 0);

            return true;
        }

        return false;
    }

    private void Update()
    {
        if (GyroEnabled)
            transform.localRotation = _Gyro.attitude * _Rot;

        if (SimulatorEnabled)
            SimulateVR();
    }

    void SimulateVR()
    {
        yaw += StateManager.horizontalSpeed * Input.GetAxis("Mouse X");
        pitch -= StateManager.verticalSpeed * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
