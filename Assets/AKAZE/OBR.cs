using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;
using UnityEngine.UI;

public class OBR : MonoBehaviour
{
    public Texture2D original;
    public Texture2D sub;
    private Mat mat;
    private Mat temp;
    private Mat output1 = new Mat();
    private Mat output2 = new Mat();
    private Mat output3 = new Mat();
    private Mat output4 = new Mat();

    int good_match_length = 0;
    int threshold = 400;
    /*
    private ORB detector;
    private ORB extractor;
    private ORB orb;*/

    DescriptorMatcher matcher;
    DMatch[] matches;
    private KeyPoint[] keyPoint1;
    private KeyPoint[] keyPoint2;
    private Mat descriptor1 = new Mat();
    private Mat descriptor2 = new Mat();
    

    // Start is called before the first frame update
    void Start()
    {
        
        AKAZE akaze = AKAZE.Create();
        mat = OpenCvSharp.Unity.TextureToMat(this.original);
        temp = OpenCvSharp.Unity.TextureToMat(this.sub);
        akaze.DetectAndCompute(mat, null, out keyPoint1, descriptor1);
        akaze.DetectAndCompute(temp, null, out keyPoint2, descriptor2);

        Cv2.DrawKeypoints(mat, keyPoint1, output1);
        Cv2.ImShow("output1", output1);
        Cv2.DrawKeypoints(temp, keyPoint2, output2);
        Cv2.ImShow("output2", output2);

        matcher = DescriptorMatcher.Create("BruteForce");
        matches = matcher.Match(descriptor1, descriptor2);

        Cv2.DrawMatches(mat, keyPoint1, temp, keyPoint2, matches, output3);
        Cv2.ImShow("output3", output3);

        for(int i = 0; i < keyPoint1.Length && i <keyPoint2.Length; ++i)
        {
            if(matches[i].Distance < threshold)
            {
                ++good_match_length;
            }
        }

        DMatch[] good_matchse = new DMatch[good_match_length];

        int j = 0;
        for (int i = 0; i < keyPoint1.Length && i<keyPoint2.Length; i++)
        {
            if (matches[i].Distance < threshold)
            {
                good_matchse[j] = matches[i];
                ++j;
            }
        }

        Cv2.DrawMatches(mat, keyPoint1, temp, keyPoint2, good_matchse, output4);
        Cv2.ImShow("output4", output4);
        //mat = Cv2.ImRead(original);
        /*
        orb = ORB.Create();
        orb.DetectAndCompute(mat,null,out keyPoint1,null);
        */
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
