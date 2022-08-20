using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StereoscopicView : MonoBehaviour
{
    public GameObject SterescopeSeparatorCanvas;

    private GameObject OVRTKCameraRig;

    public const float DefaultSeparation = 0.067f;
    public const float MinSeparation = 0.01f;
    public const float MaxSeparation = 0.10f;
    public const float DefaultConvergence = 5.0f;
    public const float MinConvergence = 0.01f;
    public const float MaxConvergence = 10.0f;

    public Camera leftCamera;
    public Camera rightCamera;
    public float separation = DefaultSeparation;
    public float convergence = DefaultConvergence;

    float previousSeparation;
    float previousConvergence;

    // Use this for initialization
    void Start()
    {
        OVRTKCameraRig = GameObject.Find("OVRTK Camera Rig");

        Debug.Assert(rightCamera != null, "rightCamera can't be null!");
        Debug.Assert(leftCamera != null, "leftCamera can't be null!");

        GameObject stereoscopicCanvas = Instantiate(SterescopeSeparatorCanvas);

        // apply fixed properties
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        // handle property changes
        if (previousSeparation != separation || previousConvergence != convergence)
            ApplyLayout();
    }

    void Reset()
    {
        separation = DefaultSeparation;
        convergence = DefaultConvergence;
    }

    void OnValidate()
    {
        // clamp values
        separation = Mathf.Clamp(separation, MinSeparation, MaxSeparation);
        convergence = Mathf.Clamp(convergence, MinConvergence, MaxConvergence);
    }

    public void Initialize()
    {
        leftCamera.orthographic = false;
        rightCamera.orthographic = false;

        leftCamera.rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
        rightCamera.rect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);

        var aspect = Screen.height == 0 ? 1 : Screen.width / (float)Screen.height;
        leftCamera.aspect = aspect;
        rightCamera.aspect = aspect;

        // apply other configurations
        ApplyLayout();
    }

    void ApplyLayout()
    {
        var halfSeparation = separation / 2.0f;
        leftCamera.transform.localPosition = new Vector3(-separation, 0.0f, 0.0f);
        rightCamera.transform.localPosition = new Vector3(separation, 0.0f, 0.0f);

        var angle = 90.0f - Mathf.Atan2(convergence, halfSeparation) * Mathf.Rad2Deg;
        leftCamera.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        rightCamera.transform.rotation = Quaternion.AngleAxis(-angle, Vector3.up);

        // keep values
        previousSeparation = separation;
        previousConvergence = convergence;
    }
}
