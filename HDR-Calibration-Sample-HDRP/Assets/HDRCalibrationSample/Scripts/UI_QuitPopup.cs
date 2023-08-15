using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDRCalibrationSample
{
    public class UI_QuitPopup : MonoBehaviour
    {
        [HideInInspector] public bool quitButtonActive = false;
        
        public void Quit()
        {
            if(quitButtonActive) gameObject.SetActive(true);
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
}