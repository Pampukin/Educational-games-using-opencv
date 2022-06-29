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
            // ファイルを読み込めない場合エラーメッセージを表示
            t1.text = "ファイルを読み込めませんでした";

        }
    } 
        /*

        
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

    public void right()
    {
        id = idCheck(++id);
        Texture2D tex = new Texture2D(0, 0);
        tex.LoadImage(LoadBytes(PhotoPath[id]));
        photo.texture = tex;

    }

    public void left()
    {
        id = idCheck(--id);
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
