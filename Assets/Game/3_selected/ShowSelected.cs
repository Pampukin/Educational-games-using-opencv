using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShowSelected : MonoBehaviour
{
    public RawImage photo;
    private string CSVPath = "/Decide/SelectData.csv";
    //private string[] PhotoPath;
    public static List<string> SelectedPhotoPath = new List<string>();
    private int id = 1;
    public static string decide;
    public Text FileName;
    string path;
    private string text;
    // Start is called before the first frame update
    void Start()
    {
        path = Application.persistentDataPath + "/Decide/";

        text = SelectedPhotoPath[id].Substring(path.Length, 4);
        FileName.text = text;

        if (!File.Exists(Application.persistentDataPath + CSVPath))
        {
            return;
        }

        try
        {
            StreamReader reader = new StreamReader(Application.persistentDataPath + CSVPath);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                SelectedPhotoPath.Add(line);
            }

            Texture2D tex = new Texture2D(0, 0);
            tex.LoadImage(LoadBytes(SelectedPhotoPath[id]));
            photo.texture = tex;
            FileName.text = SelectedPhotoPath[id];

        }
        catch (IOException e)
        {
            // �t�@�C����ǂݍ��߂Ȃ��ꍇ�G���[���b�Z�[�W��\��
            FileName.text = "�t�@�C����ǂݍ��߂܂���ł���";

        }
    }
    /*


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

    public void right()
    {
        id = idCheck(++id);
        //t2.text = PhotoPath[id];
        //t1.text = id.ToString();
        setTex();

    }

    private void setTex()
    {
        Texture2D tex = new Texture2D(0, 0);
        tex.LoadImage(LoadBytes(SelectedPhotoPath[id]));
        photo.texture = tex;


        text = SelectedPhotoPath[id].Substring(path.Length, 4);
        FileName.text = text;
    }

    public void left()
    {
        id = idCheck(--id);
        //t1.text = id.ToString();
        setTex();

    }

    private int idCheck(int id)
    {
        if (id > SelectedPhotoPath.Count - 1)
        {
            return 1;
        }
        else if (id < 1)
        {
            return SelectedPhotoPath.Count - 1;
        }
        else
        {
            return id;
        }
    }

    public void Decide()
    {
        decide = SelectedPhotoPath[id];
        SceneManager.LoadScene("Camera");
    }

}