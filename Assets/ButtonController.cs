using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void toParentSelected()
    {
        SceneManager.LoadScene("ParentSelected");
    }

    public void toCamera()
    {
        SceneManager.LoadScene("Camera");
    }

    public void toSelected()
    {
        SceneManager.LoadScene("Selected");
    }

    public void toLookPhoto()
    {
        SceneManager.LoadScene("LookPhoto");
    }
}
