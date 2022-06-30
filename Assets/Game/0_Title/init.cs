using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class init : MonoBehaviour
{
    private StreamWriter sw;
    public static int amount=0;
    public Text am;

    // Start is called before the first frame update
    void Start()
    {
        amount = PlayerPrefs.GetInt("amount", 0);
        am.text = "みつけたこすう\n" + amount.ToString() + "こ";
#if UNITY_ANDROID
        Directory.CreateDirectory(Application.persistentDataPath + "/Photo");
        Directory.CreateDirectory(Application.persistentDataPath + "/Camera");
        Directory.CreateDirectory(Application.persistentDataPath + "/Decide");
        // csvファイルを作成して、{}の中の要素分csvに追記をする(Androidの処理)


#endif

#if UNITY_EDITOR
        // 新しくcsvファイルを作成して、{}の中の要素分csvに追記をする(Unity上での処理)
        sw = new StreamWriter("PhotoData.csv", false, Encoding.GetEncoding("shift-jis"));
#endif

        if (!File.Exists(Application.persistentDataPath + "/Photo" + "/PhotoData.csv"))
        {
            sw = new StreamWriter(Application.persistentDataPath + "/Photo" + "/PhotoData.csv", true);
            sw.WriteLine("path");
            sw.Flush();
        }



        if(!File.Exists(Application.persistentDataPath + "/Camera" + "/CameraData.csv"))
        {
            sw = new StreamWriter(Application.persistentDataPath + "/Camera" + "/CameraData.csv", true, System.Text.Encoding.UTF8);
            sw.WriteLine("path");
            sw.Flush();
        }



        if(!File.Exists(Application.persistentDataPath + "/Decide" + "/SelectData.csv"))
        {
            sw = new StreamWriter(Application.persistentDataPath + "/Decide" + "/SelectData.csv", true);
            sw.WriteLine("path,hint");
            sw.Flush();
        }



        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
