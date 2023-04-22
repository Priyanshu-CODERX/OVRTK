using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    /// <summary>
    /// The above code defines a simulation manager that simulates a 3D experience using the mouse as input. 
    /// It has three public properties for adjusting the horizontal and vertical speed of the mouse movement and a boolean flag for enabling the simulator.
    /// </summary>
    public float horizontalSpeed = 2f;
    public float verticalSpeed = 2f;
    public bool EnableSimulator;


    private float pitch = 0f;
    private float yaw = 0f;

    private void Update()
    {
        if (EnableSimulator)
        {
            SimulateExperience();
        }
    }

    void SimulateExperience()
    {
        yaw += horizontalSpeed * Input.GetAxis("Mouse X");
        pitch -= verticalSpeed * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
