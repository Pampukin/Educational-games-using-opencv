using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class init : MonoBehaviour
{
    private StreamWriter sw;

    // Start is called before the first frame update
    void Start()
    {

#if UNITY_ANDROID
        Directory.CreateDirectory(Application.persistentDataPath + "/Photo");
        Directory.CreateDirectory(Application.persistentDataPath + "/Camera");
        Directory.CreateDirectory(Application.persistentDataPath + "/Decide");
        // csv�t�@�C�����쐬���āA{}�̒��̗v�f��csv�ɒǋL������(Android�̏���)


#endif

#if UNITY_EDITOR
        // �V����csv�t�@�C�����쐬���āA{}�̒��̗v�f��csv�ɒǋL������(Unity��ł̏���)
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
