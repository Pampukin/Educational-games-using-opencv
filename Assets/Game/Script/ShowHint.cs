using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHint : MonoBehaviour
{
    public GameObject canvas;
    public Text Hint;
    public Text file;
    private bool isSet = false;
    private string path;

    private void Start()
    {
        path = Application.persistentDataPath + "/Decide/";
    }
    public void showHint()
    {
        canvas.SetActive(true);
        if (ShowSelected.hint != "")
        {
            Hint.text = ShowSelected.decide+"/n"+ShowSelected.hint;
        }
        else
        {
            Hint.text = ShowSelected.decide + "/n" + "ƒqƒ“ƒg‚Í‚ ‚è‚Ü‚¹‚ñ";
        }

    }

    public void falseCanvas()
    {
        canvas.SetActive(false);
        isSet = false;
    }

    public void hint()
    {
        if (!isSet)
        {
            canvas.SetActive(true);
            if (ShowSelected.hint != "")
            {
                file.text = ShowSelected.decide.Substring(path.Length).Split('.')[0];
                Hint.text = ShowSelected.hint;
            }
            else
            {
                file.text = ShowSelected.decide.Substring(path.Length).Split('.')[0];
                Hint.text = "ƒqƒ“ƒg‚Í‚ ‚è‚Ü‚¹‚ñ";
            }
            isSet = true;
        }else if (isSet)
        {
            canvas.SetActive(false);
            isSet = false;
        }
    }
}
