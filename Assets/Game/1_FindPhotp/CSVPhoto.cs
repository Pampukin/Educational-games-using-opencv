using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class CSVPhoto : MonoBehaviour
{
    private StreamWriter sw;

    void Start()
    {

        string s1 = null;
        string FilePath = "/PhotoData.csv";
#if UNITY_ANDROID
            // csv�t�@�C�����쐬���āA{}�̒��̗v�f��csv�ɒǋL������(Android�̏���)
            sw = new StreamWriter(Application.persistentDataPath+ "/Photo" + FilePath, true);
#endif

#if UNITY_EDITOR
            // �V����csv�t�@�C�����쐬���āA{}�̒��̗v�f��csv�ɒǋL������(Unity��ł̏���)
            sw = new StreamWriter("PhotoData.csv", true, Encoding.GetEncoding("shift-jis"));
#endif

        /**
         * s2�������csv�t�@�C���֏�������
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
        string[] s1 = {txt1};
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);
        sw.Flush();
    }

    private void OnApplicationQuit()
    {
        sw.Close();
    }
}