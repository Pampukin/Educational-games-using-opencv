using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class CSVCamera : MonoBehaviour
{
    private StreamWriter sw;

    void Start()
    {

        string s1 = null;
        string path = Application.persistentDataPath + "/Camera";
        string FilePath = "/CameraData.csv";

        // CSV1行目のカラムで、StreamWriter オブジェクトへ書き込む
        if (!File.Exists(path + FilePath))
        {
            s1 = "path";
        }
#if UNITY_ANDROID
        // csvファイルを作成して、{}の中の要素分csvに追記をする(Androidの処理)
        sw = new StreamWriter(path + FilePath, true);
#endif

#if UNITY_EDITOR
        // 新しくcsvファイルを作成して、{}の中の要素分csvに追記をする(Unity上での処理)
        sw = new StreamWriter("CameraData.csv", true, Encoding.GetEncoding("shift-jis"));
#endif

        /**
         * s2文字列をcsvファイルへ書き込む
         * @see https://docs.microsoft.com/ja-jp/dotnet/api/system.io.streamwriter.writeline?view=net-6.0#System_IO_StreamWriter_WriteLine_System_String_
         */
        if (s1 != null)
        {
            sw.WriteLine(s1);
            sw.Flush();
        }


    }

    public void SaveData(string txt1)
    {
        string[] s1 = { txt1 };
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);
        sw.Flush();
    }

    private void OnApplicationQuit()
    {
        sw.Close();
    }
}
