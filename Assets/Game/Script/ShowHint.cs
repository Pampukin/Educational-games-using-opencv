using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHint : MonoBehaviour
{
    public GameObject canvas;
    public Text Hint;

    public void showHint()
    {
        canvas.SetActive(true);
        if(ShowSelected.hint != "")
        {
            Hint.text = ShowSelected.hint;
        }
        else
        {
            Hint.text = "�q���g�͂���܂���";
        }

    }

    public void falseCanvas()
    {
        canvas.SetActive(false);
    }
}
