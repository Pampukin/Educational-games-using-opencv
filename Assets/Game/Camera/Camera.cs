using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Camera : MonoBehaviour
{
    public RawImage RawImage;
    WebCamTexture webCam;
    DateTime dt;
    private string path;
    string id = null;
    string preId = null;
    public GameObject CSVObject;
    CSVCamera csv;
    public static string testId;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID
        Directory.CreateDirectory(Application.persistentDataPath + "/Camera");

#endif

        path = Application.persistentDataPath + "/Camera";
        // WebCamTextureのインスタンスを生成
        webCam = new WebCamTexture();
        //RawImageのテクスチャにWebCamTextureのインスタンスを設定
        RawImage.texture = webCam;
        //カメラ表示開始
        webCam.Play();
        csv = CSVObject.GetComponent<CSVCamera>();

    }

    public void OnClick()
    {
        dt = DateTime.Now;
        String now = dt.ToString("yyyy_MM_dd_HH_mm_ss");
        id = path + "/" + now + ".jpg";
        if (preId != id)
        {
            //シャッター音
            var mediaActionSound = new AndroidJavaObject("android.media.MediaActionSound");
            mediaActionSound.Call("play", mediaActionSound.GetStatic<int>("SHUTTER_CLICK"));

            // Texture2Dを新規作成
            Texture2D texture = new Texture2D(webCam.width, webCam.height, TextureFormat.ARGB32, false);

            // カメラのピクセルデータを設定
            texture.SetPixels(webCam.GetPixels());
            // TextureをApply
            texture.Apply();

            // Encode
            byte[] bin = texture.EncodeToJPG();
            // Encodeが終わったら削除
            Destroy(texture);


            // ファイルを保存
#if UNITY_ANDROID
            File.WriteAllBytes(path + "/" + now + ".jpg", bin);
            // データを書き込む
            csv.SaveData(id);
#else
        File.WriteAllBytes(Application.dataPath + "/test.jpg", bin);
#endif

            preId = id;
            testId = path + "/" + now + ".jpg";
            SceneManager.LoadScene("Matching");
        }
    }

    private Color[] _rotateImg(Color[] coler, int width, int height, int rotate)
    {
        Color[] rotatepix = new Color[width * height];
        int startposi = width * height - width;
        int posi = 0;

        if (rotate == 0)
        {
            for (int i = 0; i < rotatepix.Length; i++)
            {
                rotatepix[i] = coler[i];
            }
        }
        else if (rotate == 90)
        {
            startposi = width - 1;
            for (int j = 0; j < width; j++)
            {
                for (int i = startposi; i < rotatepix.Length; i += width)
                {
                    rotatepix[posi] = coler[i];
                    posi++;
                }
                startposi--;
            }
        }
        else if (rotate == 180)
        {
            for (int i = (rotatepix.Length - 1); i >= 0; i--)
            {
                rotatepix[rotatepix.Length - 1 - i] = coler[i];
            }
        }
        else if (rotate == 270)
        {
            startposi = width * height - width;
            for (int j = 0; j < width; j++)
            {
                for (int i = startposi; i >= 0; i -= width)
                {
                    rotatepix[posi] = coler[i];
                    posi++;
                }
                startposi++;
            }
        }

        return rotatepix;
    }


}
