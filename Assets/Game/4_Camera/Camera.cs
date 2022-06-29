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
        // WebCamTexture�̃C���X�^���X�𐶐�
        webCam = new WebCamTexture();
        //RawImage�̃e�N�X�`����WebCamTexture�̃C���X�^���X��ݒ�
        RawImage.texture = webCam;
        //�J�����\���J�n
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
            //�V���b�^�[��
            var mediaActionSound = new AndroidJavaObject("android.media.MediaActionSound");
            mediaActionSound.Call("play", mediaActionSound.GetStatic<int>("SHUTTER_CLICK"));

            // Texture2D��V�K�쐬
            Texture2D texture = new Texture2D(webCam.width, webCam.height, TextureFormat.ARGB32, false);

            // �J�����̃s�N�Z���f�[�^��ݒ�
            texture.SetPixels(webCam.GetPixels());
            // Texture��Apply
            texture.Apply();

            // Encode
            byte[] bin = texture.EncodeToJPG();
            // Encode���I�������폜
            Destroy(texture);


            // �t�@�C����ۑ�
#if UNITY_ANDROID
            File.WriteAllBytes(path + "/" + now + ".jpg", bin);
            // �f�[�^����������
            csv.SaveData(id);
#else
        File.WriteAllBytes(Application.dataPath + "/test.jpg", bin);
#endif

            preId = id;
            testId = path + "/" + now + ".jpg";
            SceneManager.LoadScene("Matching");
        }
    }
}
