using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeManager : MonoBehaviour
{
    public GyroscopeStateNotifier notifier = null;

    private bool GyroEnabled;
    private Gyroscope _Gyro;

    [SerializeField]
    private GameObject _CameraContainer;
    private Quaternion _Rot;
    [SerializeField]
    private GameObject OVRTKCameraRig;

    public bool SimulatorEnabled = false;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private void Start()
    {
        notifier = GameObject.Find("OVRTK Camera Rig").GetComponent<GyroscopeStateNotifier>();
        OVRTKCameraRig = GameObject.Find("OVRTK Camera Rig");

        _CameraContainer = new GameObject("Camera Container");
        _CameraContainer.transform.position = transform.position;
        transform.SetParent(_CameraContainer.transform);
        _CameraContainer.transform.SetParent(OVRTKCameraRig.transform);

        GyroEnabled = EnableGyro();
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            _Gyro = Input.gyro;
            _Gyro.enabled = true;

            _CameraContainer.transform.rotation = Quaternion.Euler(90f, 0f, 90f);
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
        yaw += notifier.horizontalSpeed * Input.GetAxis("Mouse X");
        pitch -= notifier.verticalSpeed * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
