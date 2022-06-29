using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToParentSelected : MonoBehaviour
{
    public void toParentSelected()
    {
        SceneManager.LoadScene("ParentSelected");
    }
}
