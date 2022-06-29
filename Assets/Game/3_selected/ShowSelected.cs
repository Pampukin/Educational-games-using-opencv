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
    //private string[] PhotoPath;
    public static List<string> SelectedPhotoPath = new List<string>();
    private int id = 1;
    public static string decide;
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
            }

            setTex();

        }
        catch
        {
            // ファイルを読み込めない場合エラーメッセージを表示
            FileName.text = "ファイルを読み込めませんでした";

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
        tex.LoadImage(LoadBytes(SelectedPhotoPath[id]));
        photo.texture = tex;

        FileName.text = SelectedPhotoPath[id].Substring(path.Length);
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

    public void toFindPhoto()
    {
        SceneManager.LoadScene("FindPhoto");
    }

}
