using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace HDRCalibrationSample
{
    public class UI_FullScreenCalibration : MonoBehaviour
    {
        [Header("UI Pages")]
        public GameObject page1_ui;
        public GameObject page2_ui;
        public GameObject page3_ui;

        [Header("Object Pages")]
        public GameObject page1;
        public GameObject page2;
        public GameObject page3;

        [Header("Settings")]
        public UI_SetBrightness page1_ui_settings;
        public UI_SetBrightness page2_ui_settings;
        public UI_SetBrightness page3_ui_settings;

        [Header("Menu Settings UI")]
        public UI_SetBrightness menu_ui_settings;
        public UI_SetBrightness menu_min_settings;
        public UI_SetBrightness menu_max_settings;

        public UI_TonemappingValues tmv;
        public GameObject menuUI;
        public GameObject calibrationUI;

        public InputActionAsset actions;
        private InputActionMap actionMap_FullScreenCalibration;
        private InputAction action_brightnessAdjust;
        private int currentPage = 1;

        private float tmvValuesBeforeCalibration_paperWhite = 0f;
        private float tmvValuesBeforeCalibration_maxNits = 0f;
        private float tmvValuesBeforeCalibration_minNits = 0f;

        void Init()
        {
            actionMap_FullScreenCalibration = actions.FindActionMap("FullScreenCalibration");
            actionMap_FullScreenCalibration.Enable();

            tmvValuesBeforeCalibration_paperWhite = tmv.GetPaperWhite();
            tmvValuesBeforeCalibration_maxNits = tmv.GetMaxNits();
            tmvValuesBeforeCalibration_minNits = tmv.GetMinNits();
        }

        void Start()
        {
            actionMap_FullScreenCalibration.FindAction("BrightnessIncrease").performed += BrightnessIncrease;
            actionMap_FullScreenCalibration.FindAction("BrightnessIncrease").canceled += StopBrightnessAdjust;
            actionMap_FullScreenCalibration.FindAction("BrightnessDecrease").performed += BrightnessDecrease;
            actionMap_FullScreenCalibration.FindAction("BrightnessDecrease").canceled += StopBrightnessAdjust;
            actionMap_FullScreenCalibration.FindAction("Confirm").performed += Confirm;
            actionMap_FullScreenCalibration.FindAction("Back").performed += Back;
        }

        void OnEnable()
        {
            Init();
        }

        void OnDisable()
        {
            actionMap_FullScreenCalibration.Disable();
        }

        private void Confirm(InputAction.CallbackContext context)
        {
            switch(currentPage)
            {
                case 1:
                    Page2();
                    break;
                case 2:
                    Page3();
                    break;
                case 3:
                    EndTest();
                    break;
            }
        }

        private void Back(InputAction.CallbackContext context)
        {
            switch(currentPage)
            {
                case 1:
                    BackToMenu();
                    break;
                case 2:
                    Page1();
                    break;
                case 3:
                    Page2();
                    break;
            }
        }

        private void BrightnessIncrease(InputAction.CallbackContext context)
        {
            bool isTap = context.interaction is TapInteraction;
            ControlDir(1f, isTap);
        }

        private void BrightnessDecrease(InputAction.CallbackContext context)
        {
            bool isTap = context.interaction is TapInteraction;
            ControlDir(-1f, isTap);
        }

        private void ControlDir(float direction, bool isTap)
        {
            switch(currentPage)
            {
                case 1:
                    page1_ui_settings.InputControl(direction,isTap);
                    break;
                case 2:
                    page2_ui_settings.InputControl(direction,isTap);
                    break;
                case 3:
                    page3_ui_settings.InputControl(direction,isTap);
                    break;
            }
        }

        private void StopBrightnessAdjust(InputAction.CallbackContext context)
        {
            page1_ui_settings.PointerUp();
            page2_ui_settings.PointerUp();
            page3_ui_settings.PointerUp();
        }

        public void BackToMenu()
        {
            //Apply values before calibration
            menu_max_settings.value = tmvValuesBeforeCalibration_maxNits; menu_max_settings.SetValue();
            menu_min_settings.value = tmvValuesBeforeCalibration_minNits; menu_min_settings.SetValue();
            menu_ui_settings.value = tmvValuesBeforeCalibration_paperWhite; menu_ui_settings.SetValue();

            menuUI.SetActive(true);
            calibrationUI.SetActive(false);

            page1.SetActive(false);
            page2.SetActive(false);
            page3.SetActive(false);

            page1_ui.SetActive(false);
            page2_ui.SetActive(false);
            page3_ui.SetActive(false);
        }

        public void Page1()
        {
            currentPage = 1;
            
            menuUI.SetActive(false);
            calibrationUI.SetActive(true);

            page1.SetActive(true);
            page2.SetActive(false);
            page3.SetActive(false);

            page1_ui.SetActive(true);
            page2_ui.SetActive(false);
            page3_ui.SetActive(false);

            //Values for calibration
            tmv.SetPaperWhite(400f);
            tmv.SetMaxNits(5000f);
            tmv.SetMinNits(0f);

            //Reset logo brightness value
            page1_ui_settings.Reset();
        }

        public void Page2()
        {
            currentPage = 2;

            page1.SetActive(false);
            page2.SetActive(true);
            page3.SetActive(false);

            page1_ui.SetActive(false);
            page2_ui.SetActive(true);
            page3_ui.SetActive(false);

            //Values for calibration
            tmv.SetPaperWhite(400f);
            tmv.SetMaxNits(page1_ui_settings.value);
            tmv.SetMinNits(0f);
            

            //Reset logo brightness value
            page2_ui_settings.Reset();
        }

        public void Page3()
        {
            currentPage = 3;

            page1.SetActive(false);
            page2.SetActive(false);
            page3.SetActive(true);

            page1_ui.SetActive(false);
            page2_ui.SetActive(false);
            page3_ui.SetActive(true);

            //Values for calibration
            tmv.SetPaperWhite(400f);
            tmv.SetMaxNits(page1_ui_settings.value);
            tmv.SetMinNits(page2_ui_settings.value);
            page3_ui_settings.logoMaterial.SetFloat("_Value", 1f);

            //Reset logo brightness value
            page3_ui_settings.Reset();
        }

        public void EndTest()
        {
            page1.SetActive(false);
            page2.SetActive(false);
            page3.SetActive(false);

            page1_ui.SetActive(false);
            page2_ui.SetActive(false);
            page3_ui.SetActive(false);

            //Apply values after calibration
            menu_max_settings.value = page1_ui_settings.value; menu_max_settings.SetValue();
            menu_min_settings.value = page2_ui_settings.value; menu_min_settings.SetValue();
            menu_ui_settings.value = page3_ui_settings.value; menu_ui_settings.SetValue();

            menuUI.SetActive(true);
            calibrationUI.SetActive(false);
        }
    }
}