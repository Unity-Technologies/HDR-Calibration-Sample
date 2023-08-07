using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDRCalibrationSample
{
    public class UI_ShowHide : MonoBehaviour
    {
        public GameObject menu;
        public GameObject navigationShowUIButton;
        public RectTransform navigation;
        public UI_SampleImagesNavigation sampleImagesNavigation;

        private float navigationOriginalPositionX = 0f;

        void Start()
        {
            //for positioning the navigation panel when Menu UI is hidden
            navigationOriginalPositionX = navigation.anchoredPosition.x;

            ShowMenu();
        }

        public void ShowMenu()
        {
            menu.SetActive(true);
            navigationShowUIButton.SetActive(false);
            navigation.anchoredPosition = new Vector2(navigationOriginalPositionX, navigation.anchoredPosition.y);
            sampleImagesNavigation.SetRawImageOriginalRect();
        }

        public void HideMenu()
        {
            menu.SetActive(false);
            navigationShowUIButton.SetActive(true);
            navigation.anchoredPosition = new Vector2(0f, navigation.anchoredPosition.y);
            sampleImagesNavigation.SetRawImageDefaultRect();
        }

        //For input control
        public void ToggleMenu()
        {
            if(menu.activeSelf)
            {
                HideMenu();
            }
            else
            {
                ShowMenu();
            }
        }
    }
}