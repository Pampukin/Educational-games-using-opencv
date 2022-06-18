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
        // WebCamTexture�̃C���X�^���X�𐶐�
        webCam = new WebCamTexture();

        //RawImage�̃e�N�X�`����WebCamTexture�̃C���X�^���X��ݒ�
        RawImage.texture = webCam;

        //90�x��]
        Vector3 angles = RawImage.GetComponent<RectTransform>().eulerAngles;
        angles.z = -90;
        RawImage.GetComponent<RectTransform>().eulerAngles = angles;
        Vector2 size;
        size = RawImage.GetComponent<RectTransform>().sizeDelta;
        size.x = RawImage.GetComponent<RectTransform>().sizeDelta.y;
        size.y = RawImage.GetComponent<RectTransform>().sizeDelta.x;
        RawImage.GetComponent<RectTransform>().sizeDelta = size;


        //�c���̃T�C�Y��v��
        webCam.requestedWidth = 3024;
        webCam.requestedHeight = 4032;

        //�J�����\���J�n
        webCam.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClick()
    {

        // �C���X�^���X�擾
        //webCam = shootButton.GetComponent<shootButton>().webCam;
        // Texture2D��V�K�쐬
        Texture2D texture = new Texture2D(webCam.width, webCam.height, TextureFormat.ARGB32, false);
        // �J�����̃s�N�Z���f�[�^��ݒ�
        texture.SetPixels(webCam.GetPixels());
        // Texture��Apply
        texture.Apply();

        // Encode
        byte[] bin = texture.EncodeToJPG();
        // Encode���I�������폜
        Object.Destroy(texture);

        // �t�@�C����ۑ�
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