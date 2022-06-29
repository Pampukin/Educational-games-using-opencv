using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToSelected : MonoBehaviour
{
    public void toSelected()
    {
        SceneManager.LoadScene("Selected");
    }
}
