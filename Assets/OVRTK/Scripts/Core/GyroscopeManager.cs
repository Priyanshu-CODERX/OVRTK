using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeManager : MonoBehaviour
{
    private GyroscopeStateManager gyroscopeStateManager;
    public GyroscopeStateManager GyroscopeStateManager
    {
        get
        {
            return gyroscopeStateManager;
        }
        set
        {
            gyroscopeStateManager = value;
        }
    }

    private bool GyroEnabled;
    private Gyroscope _Gyro;

    [SerializeField]
    private GameObject RigContainer;
    private Quaternion _Rot;

    private void Start()
    {
        GyroscopeStateManager = GetComponentInParent<GyroscopeStateManager>();
        EnableGyroscope();
    }

    private void EnableGyroscope()
    {
        if (GyroscopeStateManager.CheckGyroSupport())
        {
            _Gyro = Input.gyro;
            _Gyro.enabled = true;

            RigContainer.transform.rotation = Quaternion.Euler(90f, 0f, 90f);
            _Rot = new Quaternion(0, 0, 1, 0);
        }
    }

    private void Update()
    {
        if (GyroscopeStateManager.CheckGyroSupport())
            transform.localRotation = _Gyro.attitude * _Rot;
    }
}
