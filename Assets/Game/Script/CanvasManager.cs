using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject canvas;
    private bool isSet = false;
    public void trueManager()
    {
        canvas.SetActive(true);
        isSet = true;
    }

    public void falseManager()
    {
        canvas.SetActive(false);
        isSet = false;
    }

    public void manager()
    {
        if (!isSet)
        {
            canvas.SetActive(true);
            isSet = true;
        }else if (isSet)
        {
            canvas.SetActive(false);
            isSet = false;
        }
    }
}
