using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;
using UnityEngine.UI;
using System.IO;
using System;
using DG.Tweening;

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
    int threshold = 600;

    DescriptorMatcher matcher = DescriptorMatcher.Create("BruteForce");
    DMatch[] matches;

    public Text persent;

    private Tween tween;
    float ave;
    double matchP;
    public Text result;
    // Start is called before the first frame update
    void Start()
    {

        init();
        match();

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
        originalRawImage.texture = OpenCvSharp.Unity.MatToTexture(output3);
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
        subRawImage.texture = OpenCvSharp.Unity.MatToTexture(output4);

        ave = (originalKeyPoint.Length + subKeyPoint.Length)/2f;
        matchP = 100*good_match_length /ave ;
        matchP = Math.Round(matchP, 1);

        setMatchP(matchP);
        persentCheck();
    }

    private void init()
    {
        initOriginal();
        initSub();
    }

    private void initOriginal()
    {
        originalPath = ShowSelected.decide;
        original = new Texture2D(0, 0);
        original.LoadImage(LoadBytes(originalPath));
        originalMat = OpenCvSharp.Unity.TextureToMat(this.original);
        Akaze.DetectAndCompute(originalMat, null, out originalKeyPoint, originalDescriptor);
        //�e���B�e�����f�[�^�̓����_
        Cv2.DrawKeypoints(originalMat, originalKeyPoint, output1);
    }

    private void initSub()
    {
        subPath = Camera.testId;
        sub = new Texture2D(0, 0);
        sub.LoadImage(LoadBytes(subPath));
        subMat = OpenCvSharp.Unity.TextureToMat(this.sub);
        Akaze.DetectAndCompute(subMat, null, out subKeyPoint, subDescriptor);
        //�e���B�e�����f�[�^�̓����_
        Cv2.DrawKeypoints(subMat, subKeyPoint, output2);
    }

    private double currentDispCoin = 0;

    private void setMatchP(double num)
    {

        DOTween.Kill(tween);
        tween = DOTween.To(() => 0, (val) =>
        {
            currentDispCoin = val;
            persent.text = string.Format("{0:#,0}", val) + " %";
        }, num, 1f);
    }

    private void persentCheck()
    {
        if(matchP >=70)
        {
            result.text = ("���S��v");
        }else if(30 < matchP &&matchP < 70)
        {
            result.text = ("��v??");
        }
        else if (30 >matchP)
        {
            result.text = ("????????");
        }
    }
}
