using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class shootButton : MonoBehaviour
{
    public RawImage RawImage;
    public GameObject Text;
    WebCamTexture webCam;

    // Start is called before the first frame update
    void Start()
    {
        // WebCamTextureのインスタンスを生成
        webCam = new WebCamTexture();

        //RawImageのテクスチャにWebCamTextureのインスタンスを設定
        RawImage.texture = webCam;

        //90度回転
        Vector3 angles = RawImage.GetComponent<RectTransform>().eulerAngles;
        angles.z = -90;
        RawImage.GetComponent<RectTransform>().eulerAngles = angles;
        Vector2 size;
        size = RawImage.GetComponent<RectTransform>().sizeDelta;
        size.x = RawImage.GetComponent<RectTransform>().sizeDelta.y;
        size.y = RawImage.GetComponent<RectTransform>().sizeDelta.x;
        RawImage.GetComponent<RectTransform>().sizeDelta = size;


        //縦横のサイズを要求
        webCam.requestedWidth = 3024;
        webCam.requestedHeight = 4032;

        //カメラ表示開始
        webCam.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClick()
    {

        // インスタンス取得
        //webCam = shootButton.GetComponent<shootButton>().webCam;
        // Texture2Dを新規作成
        Texture2D texture = new Texture2D(webCam.width, webCam.height, TextureFormat.ARGB32, false);
        // カメラのピクセルデータを設定
        texture.SetPixels(webCam.GetPixels());
        // TextureをApply
        texture.Apply();

        // Encode
        byte[] bin = texture.EncodeToJPG();
        // Encodeが終わったら削除
        Object.Destroy(texture);

        // ファイルを保存
#if UNITY_ANDROID
        File.WriteAllBytes(Application.persistentDataPath + "/test.jpg", bin);
#else
        File.WriteAllBytes(Application.dataPath + "/test.jpg", bin);
#endif

        webCam.Stop();
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