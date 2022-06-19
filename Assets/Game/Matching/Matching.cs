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

    //親が撮影した元データ
    private Texture2D original;
    private string originalPath;
    private Mat originalMat;
    private KeyPoint[] originalKeyPoint;
    private Mat originalDescriptor = new Mat();
    //映す場所
    public RawImage originalRawImage;


    //子供が撮影したデータ
    private Texture2D sub;
    private string subPath;
    private Mat subMat;
    private KeyPoint[] subKeyPoint;
    private Mat subDescriptor = new Mat();
    public RawImage subRawImage;

    //作成したものを入れるMat
    private Mat output1 = new Mat();
    private Mat output2 = new Mat();
    private Mat output3 = new Mat();
    private Mat output4 = new Mat();

    //いくつマッチングしたか
    int good_match_length = 0;
    //マッチングの閾値
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
        //親が撮影したデータの特徴点
        Cv2.DrawKeypoints(originalMat, originalKeyPoint, output1);

        subPath = Camera.testId;
        sub = new Texture2D(0, 0);
        sub.LoadImage(LoadBytes(subPath));
        subMat = OpenCvSharp.Unity.TextureToMat(this.sub);
        Akaze.DetectAndCompute(subMat, null, out subKeyPoint, subDescriptor);
        //親が撮影したデータの特徴点
        Cv2.DrawKeypoints(subMat, subKeyPoint, output2);

        match();
        originalRawImage.texture = OpenCvSharp.Unity.MatToTexture(output3);
        subRawImage.texture = OpenCvSharp.Unity.MatToTexture(output4);
    }

    /**
* パス指定で画像を読み込む
* SDカードのパス = /mnt/sdcard/...
* よくあるエラー(UnauthorizedAccessException: Access to the path "/mnt/sdcard/image.jpg" is denied.)
* が出たら、PlayerSetting で Force SD-Card Permission をオンにする！
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

        //特徴点マッチング
        matches = matcher.Match(originalDescriptor, subDescriptor);
        Cv2.DrawMatches(originalMat, originalKeyPoint, subMat, subKeyPoint, matches, output3);
        //Cv2.ImShow("output3", output3);
    }

    //より精度の高い特徴点マッチング
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
    //RawImageをtextureに変換
    private void raw2Tex(RawImage raw)
    {
        var tex = raw.texture;
        var sw = tex.width;
        var sh = tex.height;
        var result = new Texture2D(sw, sh, TextureFormat.RGBA32, false);
        var currentRT = RenderTexture.active;
        var rt = new RenderTexture(sw, sh, 32);

        // RawImageのTextureをRenderTextureにコピー
        Graphics.Blit(tex, rt);
        RenderTexture.active = rt;

        // RenderTextureのピクセル情報をTexture2Dにコピー
        result.ReadPixels(new UnityEngine.Rect(0, 0, rt.width, rt.height), 0, 0);
        result.Apply();
        RenderTexture.active = currentRT;

        // PNGにエンコード完了
        //var texture = result.EncodeToPNG();

        this.sub = result;
    }
    */

}
