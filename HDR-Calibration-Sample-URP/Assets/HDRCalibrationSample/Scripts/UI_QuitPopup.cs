using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_QuitPopup : MonoBehaviour
{
    public void Quit()
    {
        gameObject.SetActive(true);
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }

    public void DoQuit()
    {
        Application.Quit();
    }
}
