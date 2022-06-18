using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Set : MonoBehaviour
{
    /*
    void Start()
    {
        SaveText(
            Application.persistentDataPath,
            "SampleFile",
            "保存したいテキストデータ"
        );
    }



    private void Update()
    {
        Debug.Log(Application.persistentDataPath);
    }
    */
    void Start()
    {
        SaveText(
            GetInternalStoragePath(),
            "SampleFile",
            "保存したいテキストデータ"
        );
    }

    public void SaveText(string filePath, string fileName, string textToSave)
    {
        var combinedPath = Path.Combine(filePath, fileName);
        using (var streamWriter = new StreamWriter(combinedPath))
        {
            streamWriter.WriteLine(textToSave);
        }
    }

    private string GetInternalStoragePath()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        using (var unityPlayer = new AndroidJavaClass("com.Pampukin.EducationGame"))
        using (var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
        using (var getFilesDir = currentActivity.Call<AndroidJavaObject>("getFilesDir"))
        {
            string secureDataPath = getFilesDir.Call<string>("getCanonicalPath");
            return secureDataPath;
        }
#else
        return Application.persistentDataPath;
#endif
    }
}
