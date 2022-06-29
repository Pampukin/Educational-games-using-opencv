using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHint : MonoBehaviour
{
    public GameObject canvas;
    public Text Hint;
    private bool isSet = false;

    public void showHint()
    {
        canvas.SetActive(true);
        if (ShowSelected.hint != "")
        {
            Hint.text = ShowSelected.hint;
        }
        else
        {
            Hint.text = "ƒqƒ“ƒg‚Í‚ ‚è‚Ü‚¹‚ñ";
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
                Hint.text = ShowSelected.hint;
            }
            else
            {
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
