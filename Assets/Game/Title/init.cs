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
        sw = new StreamWriter(Application.persistentDataPath + "/Photo" + "/PhotoData.csv", true);
        sw.WriteLine("path");
        sw.Flush();
        sw = new StreamWriter(Application.persistentDataPath + "/Camera" + "/CameraData.csv", true, Encoding.GetEncoding("UTF-8"));
        sw.WriteLine("path");
        sw.Flush();
        sw = new StreamWriter(Application.persistentDataPath + "/Decide" + "/SelectData.csv", true, Encoding.GetEncoding("UTF-8"));
        sw.WriteLine("path");
        sw.Flush();
#endif

#if UNITY_EDITOR
        // �V����csv�t�@�C�����쐬���āA{}�̒��̗v�f��csv�ɒǋL������(Unity��ł̏���)
        sw = new StreamWriter("PhotoData.csv", true, Encoding.GetEncoding("shift-jis"));
#endif
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
