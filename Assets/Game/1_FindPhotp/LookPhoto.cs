using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LookPhoto : MonoBehaviour
{
    public RawImage photo;
    //private string[] PhotoPath;
    public static List<string> PhotoPath = new List<string>();
    private int id = 1;
    public Text t1;
    public Text t2;
    public static string select;
    // Start is called before the first frame update
    void Start()
    {

        if (!File.Exists(Application.persistentDataPath + "/Photo" + "/PhotoData.csv"))
        {
            return;
        }

        try
        {
            StreamReader reader = new StreamReader(Application.persistentDataPath + "/Photo" + "/PhotoData.csv");
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                PhotoPath.Add(line);
            }

            Texture2D tex = new Texture2D(0, 0);
            tex.LoadImage(LoadBytes(PhotoPath[id]));
            photo.texture = tex;


        }
        catch (IOException e)
        {
            // �t�@�C����ǂݍ��߂Ȃ��ꍇ�G���[���b�Z�[�W��\��
            t1.text = "�t�@�C����ǂݍ��߂܂���ł���";

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
        t1.text = id.ToString();
        Texture2D tex = new Texture2D(0, 0);
        tex.LoadImage(LoadBytes(PhotoPath[id]));
        photo.texture = tex;

    }

    public void left()
    {
        id = idCheck(--id);
        t1.text = id.ToString();
        Texture2D tex = new Texture2D(0, 0);
        tex.LoadImage(LoadBytes(PhotoPath[id]));
        photo.texture = tex;
 
    }

    private int idCheck(int id)
    {
        if(id > PhotoPath.Count-1)
        {
            return 1;
        }else if(id < 1)
        {
            return PhotoPath.Count-1;
        }
        else
        {
            return id;
        }
    }

    public void Select()
    {
        select = PhotoPath[id];

        SceneManager.LoadScene("Select");
    }
}
