using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Take()
    {
        var mediaActionSound = new AndroidJavaObject("android.media.MediaActionSound");
        mediaActionSound.Call("play", mediaActionSound.GetStatic<int>("SHUTTER_CLICK"));
    }
}
