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
    private string CSVPath = "SelectData.csv";
    public static List<string> SelectedPhotoPath = new List<string>();
    //public static List<string> hint = new List<string>();
    public Text hintText;
    private int id = 1;
    public static string decide;
    public static string hint;
    public Text FileName;
    string path;

    // Start is called before the first frame update
    void Start()
    {
        path = Application.persistentDataPath + "/Decide/";

        if (!File.Exists(path + CSVPath))
        {
            return;
        }

        try
        {
            StreamReader reader = new StreamReader(path + CSVPath);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                SelectedPhotoPath.Add(line);
                //hint.Add("");
            }
            
            setTex();

        }
        catch
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
        setTex();

    }

    public void left()
    {
        id = idCheck(--id);
        setTex();

    }

    private void setTex()
    {
        Texture2D tex = new Texture2D(0, 0);
        tex.LoadImage(LoadBytes(split(SelectedPhotoPath[id])[0]));
        photo.texture = tex;

        FileName.text = split(SelectedPhotoPath[id].Substring(path.Length))[0].Split('.')[0];
        hintText.text = split(SelectedPhotoPath[id].Substring(path.Length))[1];


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
        decide = split(SelectedPhotoPath[id])[0];
        hint = split(SelectedPhotoPath[id])[1];
        SceneManager.LoadScene("Camera");
    }

    public void toFindPhoto()
    {
        SceneManager.LoadScene("FindPhoto");
    }

    private string[] split(string data)
    {
        return data.Split(',');
    }
}
