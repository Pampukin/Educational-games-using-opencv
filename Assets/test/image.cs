using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using OpenCvSharp;

public class image : MonoBehaviour
{
    public Texture2D imgTexture1;
    public RawImage gray;

    // Start is called before the first frame update
    void Start()
    {
        // “ñ‚Â‚Ì‰æ‘œ‚ð—pˆÓ
        Mat srcMat1 = OpenCvSharp.Unity.TextureToMat(imgTexture1);

        Mat grayMat = new Mat();
        Cv2.CvtColor(srcMat1, grayMat, ColorConversionCodes.RGB2GRAY);

        Texture2D dstTexture = OpenCvSharp.Unity.MatToTexture(grayMat);

        this.gray.texture = imgTexture1;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
