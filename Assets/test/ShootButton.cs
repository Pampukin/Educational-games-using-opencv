using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class ShootButton : MonoBehaviour
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

    public void Pause()
    {
        // カメラを停止
        webCam.Pause();
    }

    public void Save()
    {
        // インスタンス取得
        //webCam = ShootButton.GetComponent<ShootButton>().webCam;
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
    }
}
