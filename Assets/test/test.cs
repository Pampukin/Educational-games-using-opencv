using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp.Demo;
using OpenCvSharp;
using OpenCvSharp.Aruco;

public class test : MonoBehaviour
{
    WebCamTexture webCamTexture;
    RawImage RawImage;
    private Texture2D texture;

    // Start is called before the first frame update
    void Start()
    {
        webCamTexture = new WebCamTexture();
        //texture = webCamTexture;
        RawImage.texture = webCamTexture;
        webCamTexture.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
