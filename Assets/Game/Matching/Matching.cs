using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;
using UnityEngine.UI;
using System.IO;

public class Matching : MonoBehaviour
{
    //AKAZE
    private AKAZE Akaze = AKAZE.Create();

    //�e���B�e�������f�[�^
    private Texture2D original;
    private string originalPath;
    private Mat originalMat;
    private KeyPoint[] originalKeyPoint;
    private Mat originalDescriptor = new Mat();
    //�f���ꏊ
    public RawImage originalRawImage;


    //�q�����B�e�����f�[�^
    private Texture2D sub;
    private string subPath;
    private Mat subMat;
    private KeyPoint[] subKeyPoint;
    private Mat subDescriptor = new Mat();
    public RawImage subRawImage;

    //�쐬�������̂�����Mat
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

    // Start is called before the first frame update
    void Start()
    {
        originalPath = ShowSelected.decide;
        original = new Texture2D(0, 0);
        original.LoadImage(LoadBytes(originalPath));
        originalMat = OpenCvSharp.Unity.TextureToMat(this.original);
        Akaze.DetectAndCompute(originalMat, null, out originalKeyPoint, originalDescriptor);
        //�e���B�e�����f�[�^�̓����_
        Cv2.DrawKeypoints(originalMat, originalKeyPoint, output1);

        subPath = Camera.testId;
        sub = new Texture2D(0, 0);
        sub.LoadImage(LoadBytes(subPath));
        subMat = OpenCvSharp.Unity.TextureToMat(this.sub);
        Akaze.DetectAndCompute(subMat, null, out subKeyPoint, subDescriptor);
        //�e���B�e�����f�[�^�̓����_
        Cv2.DrawKeypoints(subMat, subKeyPoint, output2);

        match();
        originalRawImage.texture = OpenCvSharp.Unity.MatToTexture(output3);
        subRawImage.texture = OpenCvSharp.Unity.MatToTexture(output4);
    }

    /**
* �p�X�w��ŉ摜��ǂݍ���
* SD�J�[�h�̃p�X = /mnt/sdcard/...
* �悭����G���[(UnauthorizedAccessException: Access to the path "/mnt/sdcard/image.jpg" is denied.)
* ���o����APlayerSetting �� Force SD-Card Permission ���I���ɂ���I
**/
    byte[] LoadBytes(string path)
    {
        FileStream fs = new FileStream(path, FileMode.Open);
        BinaryReader bin = new BinaryReader(fs);
        byte[] result = bin.ReadBytes((int)bin.BaseStream.Length);
        bin.Close();
        return result;
    }

    public void match()
    {
        match1();
        match2();
    }
    public void match1()
    {

        //�����_�}�b�`���O
        matches = matcher.Match(originalDescriptor, subDescriptor);
        Cv2.DrawMatches(originalMat, originalKeyPoint, subMat, subKeyPoint, matches, output3);
        //Cv2.ImShow("output3", output3);
    }

    //��萸�x�̍��������_�}�b�`���O
    public void match2()
    {


        matches = matcher.Match(originalDescriptor, subDescriptor);
        for (int i = 0; i < originalKeyPoint.Length && i < subKeyPoint.Length; ++i)
        {
            if (matches[i].Distance < threshold)
            {
                ++good_match_length;
            }
        }
        DMatch[] good_matchse = new DMatch[good_match_length];

        int j = 0;
        for (int i = 0; i < originalKeyPoint.Length && i < subKeyPoint.Length; i++)
        {
            if (matches[i].Distance < threshold)
            {
                good_matchse[j] = matches[i];
                ++j;
            }
        }
        Cv2.DrawMatches(originalMat, originalKeyPoint, subMat, subKeyPoint, good_matchse, output4);
        //Cv2.ImShow("output4", output4);

        //Debug.Log(good_match_length);
    }

    /*
    //RawImage��texture�ɕϊ�
    private void raw2Tex(RawImage raw)
    {
        var tex = raw.texture;
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
    */

}
