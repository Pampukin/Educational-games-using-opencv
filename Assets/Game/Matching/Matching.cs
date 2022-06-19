using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;
using UnityEngine.UI;

public class Matching : MonoBehaviour
{
    private AKAZE Akaze = AKAZE.Create();

    //�e���Ƃ������f�[�^
    private Texture2D original;
    //�q�����Ƃ����f�[�^
    private Texture2D sub;
    private Mat mat;
    private Mat temp;
    private Mat output1 = new Mat();
    private Mat output2 = new Mat();
    private Mat output3 = new Mat();
    private Mat output4 = new Mat();

    //�����}�b�`���O������
    int good_match_length = 0;
    //�}�b�`���O��臒l
    int threshold = 400;

    DescriptorMatcher matcher = DescriptorMatcher.Create("BruteForce");
    DMatch[] matches;
    private KeyPoint[] keyPoint1;
    private KeyPoint[] keyPoint2;
    private Mat descriptor1 = new Mat();
    private Mat descriptor2 = new Mat();

    //�f���ꏊ
    public RawImage RawImage;


    // Start is called before the first frame update
    void Start()
    {
        mat = OpenCvSharp.Unity.TextureToMat(this.original);
        Akaze.DetectAndCompute(mat, null, out keyPoint1, descriptor1);
        //���f���f�[�^�̓����_
        Cv2.DrawKeypoints(mat, keyPoint1, output1);
        //Cv2.ImShow("output1", output1);
    }

    // Update is called once per frame
    void Update()
    {
        raw2Tex();
        akaze();
    }

    private void akaze()
    {
        temp = OpenCvSharp.Unity.TextureToMat(this.sub);
        Akaze.DetectAndCompute(temp, null, out keyPoint2, descriptor2);
        //����̓����_
        Cv2.DrawKeypoints(temp, keyPoint2, output2);
        Cv2.ImShow("output2", output2);
    }

    public void match()
    {
        match1();
        match2();
    }
    public void match1()
    {

        //�����_�}�b�`���O
        matches = matcher.Match(descriptor1, descriptor2);
        Cv2.DrawMatches(mat, keyPoint1, temp, keyPoint2, matches, output3);
        Cv2.ImShow("output3", output3);
    }

    //��萸�x�̍��������_�}�b�`���O
    public void match2()
    {


        matches = matcher.Match(descriptor1, descriptor2);
        for (int i = 0; i < keyPoint1.Length && i < keyPoint2.Length; ++i)
        {
            if (matches[i].Distance < threshold)
            {
                ++good_match_length;
            }
        }
        DMatch[] good_matchse = new DMatch[good_match_length];

        int j = 0;
        for (int i = 0; i < keyPoint1.Length && i < keyPoint2.Length; i++)
        {
            if (matches[i].Distance < threshold)
            {
                good_matchse[j] = matches[i];
                ++j;
            }
        }
        Cv2.DrawMatches(mat, keyPoint1, temp, keyPoint2, good_matchse, output4);
        Cv2.ImShow("output4", output4);

        Debug.Log(good_match_length);
    }
    private void raw2Tex()
    {
        var tex = RawImage.texture;
        var sw = tex.width;
        var sh = tex.height;
        var result = new Texture2D(sw, sh, TextureFormat.RGBA32, false);
        var currentRT = RenderTexture.active;
        var rt = new RenderTexture(sw, sh, 32);

        // RawImage��Texture��RenderTexture�ɃR�s�[
        Graphics.Blit(tex, rt);
        RenderTexture.active = rt;

        // RenderTexture�̃s�N�Z������Texture2D�ɃR�s�[
        result.ReadPixels(new UnityEngine.Rect(0, 0, rt.width, rt.height), 0, 0);
        result.Apply();
        RenderTexture.active = currentRT;

        // PNG�ɃG���R�[�h����
        //var texture = result.EncodeToPNG();

        this.sub = result;
    }

}
