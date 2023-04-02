using UnityEngine;
using TMPro;

namespace Utils
{
    public enum LOGS { 
        MESSAGE,
        ERROR,
        WARNING
    }

   public class LoggerUtility : MonoBehaviour
    {
        public TMP_Text LOGGER;
        private float DeltaTime;

        [SerializeField] bool LogFPS;
        [SerializeField] bool ViewSystemMemory;
        [SerializeField] bool ViewGraphicsMemory;
        [SerializeField] bool ViewDeviceModel;

        // Makes use of Enums to switch between logs
        public void LogMessage(LOGS LogType, string Message)
        {
            if (LOGGER.text == "[LOGGER]") 
                LOGGER.text = "";

            // Logs Messages, Erros and Warnings to TextMeshPro
            if (LogType == LOGS.MESSAGE)
                LOGGER.text += $"MESSAGE: {Message}\n";

            if (LogType == LOGS.WARNING)
                LOGGER.text += $"WARNING: {Message}\n";

            if (LogType == LOGS.ERROR)
                LOGGER.text += $"ERROR: {Message}\n";
        }

        // Logs Frames Per Second
        public void GetFramesPerSecond()
        {
            if (LogFPS)
            {
                DeltaTime += (Time.deltaTime - DeltaTime) * 0.1f;
                float fps = 1.0f / DeltaTime;
                LOGGER.text = $"FPS:{Mathf.Ceil(fps)}\n";
            }
        }

        // Get Available System Memory Size
        public void GetSystemMemorySize()
        {
            if (ViewSystemMemory)
            {
                float DeviceMemory = SystemInfo.systemMemorySize;
                LOGGER.text = $"Available Device Memory: {DeviceMemory} MB\n";
            }
        }

        // Get Available Graphics Memory Size
        public void GetGraphicsMemorySize()
        {
            if (ViewGraphicsMemory)
            {
                float GraphicsMemory = SystemInfo.graphicsMemorySize;
                LOGGER.text = $"Graphics Memory: {GraphicsMemory} MB\n";
            }
        }

        // Get Current Device Model
        public void GetDeviceModel()
        {
            if (ViewDeviceModel)
            {
                string DeviceModel = SystemInfo.deviceModel;
                LOGGER.text = $"Device Model: {DeviceModel}\n";
            }
        }
    }
}