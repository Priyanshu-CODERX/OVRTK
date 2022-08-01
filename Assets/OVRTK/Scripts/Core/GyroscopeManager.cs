using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeManager : MonoBehaviour
{
    private bool GyroEnabled;
    private Gyroscope _Gyro;

    [SerializeField]
    private GameObject _CameraContainer;
    private Quaternion _Rot;

    public GameObject _GyroNotSupportedPopUp;

    public bool onDesktop;
    public bool canvasAvailable;

    private void Start()
    {
        _CameraContainer = new GameObject("Camera Container");
        _CameraContainer.transform.position = transform.position;
        transform.SetParent(_CameraContainer.transform);

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

        if (!onDesktop && !canvasAvailable)
        {
            Instantiate(_GyroNotSupportedPopUp);
            canvasAvailable = true;
        }

        return false;
    }

    private void Update()
    {
        if (GyroEnabled)
            transform.localRotation = _Gyro.attitude * _Rot;
    }
}
