using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LookPhoto : MonoBehaviour
{
    public RawImage photo;
    private string CSVPath = "/PhotoData.csv";
    //private string[] PhotoPath;
    List<string> PhotoPath = new List<string>();
    private int id = 1;
    private int input = 0;
    public Text t1;
    public Text t2;
    private int length = 0;
    // Start is called before the first frame update
    void Start()
    {

        if (!File.Exists(Application.persistentDataPath + "/PhotoData.csv"))
        {
            return;
        }

        try
        {
            StreamReader reader = new StreamReader(Application.persistentDataPath + "/PhotoData.csv");
            t2.text = "aaa";
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                PhotoPath.Add(line);
                t1.text = "bbb";
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
}