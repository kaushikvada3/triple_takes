using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class CaptureImage : MonoBehaviour
{
    public KeyCode captureKey = KeyCode.Tab;
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(captureKey))
        {
            StartCoroutine(CaptureAndSave());
        }
    }

    IEnumerator CaptureAndSave()
    {
        yield return new WaitForEndOfFrame();

        Texture2D screenImage = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenImage.Apply();

        byte[] imageBytes = screenImage.EncodeToPNG();

        string downloadsPath = GetDownloadsPath();
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string fileName = $"scene_{timestamp}.png";

        string filePath = Path.Combine(downloadsPath, fileName);

        File.WriteAllBytes(filePath, imageBytes);

        Debug.Log($"Screenshot saved to: {filePath}");

        Destroy(screenImage);
    }

    string GetDownloadsPath()
    {
        string downloadsPath = "";

#if UNITY_STANDALONE_WIN
        // On Windows, the Downloads folder is typically at %USERPROFILE%\Downloads
        downloadsPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile), "Downloads");
#elif UNITY_STANDALONE_OSX
        // On macOS, the Downloads folder is typically at ~/Downloads
        downloadsPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Downloads");
#else
        // For other platforms, default to persistent data path
        downloadsPath = Application.persistentDataPath;
#endif

        return downloadsPath;
    }
}
