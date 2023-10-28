using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used to create a stereoscopic effect for a virtual reality experience. 
/// It sets up two cameras in the scene, one for the left eye and one for the right eye, and positions them to match the distance between the user's eyes, known as the interpupillary distance. 
/// It also includes variables for eye height and eye depth, which can be adjusted to match the user's physical characteristics.
/// </summary>
public class StereoscopeManager : MonoBehaviour
{
    [SerializeField]
    private Camera leftCamera;

    [SerializeField]
    private Camera rightCamera;

    [SerializeField]
    private GameObject SterescopicPanel;

    [SerializeField]
    private float interpupillaryDistance = 0.064f;

    [SerializeField]
    private float eyeHeight = 0.064f;

    [SerializeField]
    private float eyeDepth = 0.064f;

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

    private void Awake()
    {
        GyroscopeStateManager = GetComponent<GyroscopeStateManager>();
        SimulationManager = GetComponent<SimulationManager>();
    }

    private void Start()
    {
        if (GyroscopeStateManager.CheckGyroSupport())
        {
            SterescopicPanel.SetActive(true);
        }
        else
        {
            SterescopicPanel.SetActive(false);
        }

        if (leftCamera == null || rightCamera == null)
        {
            CreateChildCameras();
        }
        else
        {
            leftCamera.transform.SetParent(transform);
            rightCamera.transform.SetParent(transform);
        }

        ConfigureCameras();
    }

    // This function is used to create two child cameras in the scene, one for the left eye and one for the right eye. It calculates the position and rotation of the cameras based on the interpupillary distance and assigns them to the leftCamera and rightCamera variables.
    private void CreateChildCameras()
    {
        // Calculate the position of the left and right cameras.
        Vector3 leftCameraPosition = transform.position - transform.right * interpupillaryDistance / 2;
        Vector3 rightCameraPosition = transform.position + transform.right * interpupillaryDistance / 2;

        // Calculate the rotation of the left and right cameras.
        Quaternion leftCameraRotation = transform.rotation;
        Quaternion rightCameraRotation = transform.rotation;

        // Set up the left camera.
        GameObject leftCameraObject = new GameObject("Left Camera");

        leftCameraObject.transform.parent = transform;
        leftCamera = leftCameraObject.AddComponent<Camera>();
        leftCameraObject.AddComponent<GyroscopeManager>();

        leftCamera.transform.position = leftCameraPosition;
        leftCamera.transform.rotation = leftCameraRotation;

        // Set up the right camera.
        GameObject rightCameraObject = new GameObject("Right Camera");

        rightCameraObject.transform.parent = transform;
        rightCamera = rightCameraObject.AddComponent<Camera>();
        rightCameraObject.AddComponent<GyroscopeManager>();

        rightCamera.transform.position = rightCameraPosition;
        rightCamera.transform.rotation = rightCameraRotation;
    }

    // This function is used to configure the cameras for the stereoscopic effect. It sets the viewport rectangles of the left and right cameras, sets their depth, modifies their projection matrices to create the stereoscopic effect, and adjusts their position and rotation to account for the user's eye height and depth.
    private void ConfigureCameras()
    {
        // Set the viewport rectangles of the left and right cameras.
        leftCamera.rect = new Rect(0, 0, 0.5f, 1);
        rightCamera.rect = new Rect(0.5f, 0, 0.5f, 1);

        // Set the depth of the left and right cameras.
        // Set the depth of the left and right cameras.
        leftCamera.depth = -1;
        rightCamera.depth = -1;


        // Modify the projection matrices of the left and right cameras to create the stereoscopic effect.
        Matrix4x4 leftMatrix = leftCamera.projectionMatrix;
        Matrix4x4 rightMatrix = rightCamera.projectionMatrix;

        float ipdOffset = interpupillaryDistance / 4 / (leftCamera.nearClipPlane + rightCamera.nearClipPlane);
        leftMatrix[0, 2] = ipdOffset;
        rightMatrix[0, 2] = -ipdOffset;
        leftCamera.projectionMatrix = leftMatrix;
        rightCamera.projectionMatrix = rightMatrix;

        // Modify the position and rotation of the left and right cameras to account for the eye height and depth.
        leftCamera.transform.position += transform.up * eyeHeight;
        leftCamera.transform.position -= transform.forward * eyeDepth;

        rightCamera.transform.position += transform.up * eyeHeight;
        rightCamera.transform.position -= transform.forward * eyeDepth;
    }
}
