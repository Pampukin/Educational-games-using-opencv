using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Select : MonoBehaviour
{
    public RawImage photo;
    public Text input;
    private Texture2D tex;
    public GameObject CSVObject;
    private CSVSelect csv;

    private string id;
    private string preId;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID
        Directory.CreateDirectory(Application.persistentDataPath + "/Decide");

#endif

        csv = CSVObject.GetComponent<CSVSelect>();
        try
        {
            tex = new Texture2D(0, 0);
            tex.LoadImage(LoadBytes(LookPhoto.select));
            photo.texture = tex;


        }
        catch (IOException e)
        {
            // ファイルを読み込めない場合エラーメッセージを表示
            //t1.text = "ファイルを読み込めませんでした";

        }

        byte[] LoadBytes(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            BinaryReader bin = new BinaryReader(fs);
            byte[] result = bin.ReadBytes((int)bin.BaseStream.Length);
            bin.Close();
            return result;
        }
    }



    public void OnClick()
    {
        if(input.text != "")
        {
            id = Application.persistentDataPath + "/Decide" + "/" + input.text + ".jpg";
            if (preId != id)
            {

#if UNITY_ANDROID
                File.WriteAllBytes(Application.persistentDataPath + "/Decide" + "/" + input.text + ".jpg", tex.EncodeToPNG());

                csv.SaveData(id);
#else
        File.WriteAllBytes(Application.dataPath + "/test.jpg", bin);
#endif
                preId = id;
            }
        }
        
    }
}
