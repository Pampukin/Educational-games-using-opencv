using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp.Demo;
using OpenCvSharp;
using OpenCvSharp.Aruco;

public class test : MonoBehaviour
{
    RawImage RawImage;
    private Texture2D texture;

    // Start is called before the first frame update
    void Start()
    {
        WebCamTexture webCamTexture = new WebCamTexture();
        RawImage = GetComponent<RawImage>();
        RawImage.texture = webCamTexture;
        webCamTexture.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
