using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace HDRCalibrationSample
{
    public class UI_InputMenu : MonoBehaviour
    {
        public InputActionAsset actions;
        public InputActionMap actionMap_MenuPanel;

        public UI_DisplayInfo displayInfo;
        public UI_ShowHide showHide;
        public UI_ErrorPopup errorPopup;
        public UI_SampleImagesNavigation sampleImagesNavigation;

        //Quit popup and button
        public GameObject quitButton;
        public UI_QuitPopup quitPopup;

        //Menu items
        public UI_HDRToggle hdrToggle; // #0
        public UI_SetBrightness brightness_ui_settings; // #1
        public UI_SetBrightness brightness_min_settings; // #2
        public UI_SetBrightness brightness_max_settings; // #3
        public UI_FullScreenCalibration fullScreenCalibration; // #4
        // Reset  #5
        private int currentSelection = -1;
        private int selectionCount = 6;

        //Menu items hover
        public UI_HighlightOnHover[] menuItem_hovers;

        void Init()
        {
            actionMap_MenuPanel = actions.FindActionMap("MenuPanel");
            actionMap_MenuPanel.Enable();
        }

        void Start()
        {
            //Hide the quit button if not Desktop platforms
            quitPopup.quitButtonActive = SystemInfo.deviceType == DeviceType.Desktop;
            quitButton.SetActive(quitPopup.quitButtonActive);

            //Display Info
            actionMap_MenuPanel.FindAction("DisplayInfo").performed += DisplayInfo;

            //Show Hide UI
            actionMap_MenuPanel.FindAction("ShowUI").performed += ShowHideUI;

            //Back button
            actionMap_MenuPanel.FindAction("Back").performed += BackButton;

            //Sample Images Navigation
            actionMap_MenuPanel.FindAction("SampleImages").performed += SampleImagesNavigation;

            //Menu Select
            actionMap_MenuPanel.FindAction("MenuSelect").performed += MenuSelect;

            //Menu Confirm
            actionMap_MenuPanel.FindAction("Confirm").performed += MenuConfirm;

            //Brightness Adjust
            actionMap_MenuPanel.FindAction("BrightnessIncrease").performed += BrightnessIncrease;
            actionMap_MenuPanel.FindAction("BrightnessIncrease").canceled += StopBrightnessAdjust;
            actionMap_MenuPanel.FindAction("BrightnessDecrease").performed += BrightnessDecrease;
            actionMap_MenuPanel.FindAction("BrightnessDecrease").canceled += StopBrightnessAdjust;
        }

        void OnEnable()
        {
            Init();
        }

        void OnDisable()
        {
            actionMap_MenuPanel.Disable();
        }

        private void DisplayInfo(InputAction.CallbackContext context)
        {
            if(!showHide.menu.activeSelf) return;
            
            displayInfo.TogglePopup();
        }

        private void ShowHideUI(InputAction.CallbackContext context)
        {
            showHide.ToggleMenu();
            displayInfo.ClosePopup();
        }

        private void BackButton(InputAction.CallbackContext context)
        {
            if(quitPopup.gameObject.activeSelf)
            {
                //Cancel quit
                quitPopup.Cancel();
            }
            else if(errorPopup.gameObject.activeSelf)
            {
                //CloseEditorWarning
                errorPopup.gameObject.SetActive(false);
            }
            else if(displayInfo.gameObject.activeSelf)
            {
                //Close DisplayInfo
                displayInfo.ClosePopup();
            }
            else
            {
                //Quit
                quitPopup.Quit();
            }
        }

        private void SampleImagesNavigation(InputAction.CallbackContext context)
        {
            float direction = context.ReadValue<float>();
            sampleImagesNavigation.NextOrPrevImage(direction);
        }

        public void MenuSelect(InputAction.CallbackContext context)
        {
            if(!showHide.menu.activeSelf) return;

            float direction = context.ReadValue<float>();
            currentSelection += 1 * (int)Mathf.Sign(direction);

            //When menu items are disabled because of HDR state, skip them
            selectionCount = 0;
            for(int i = 0; i < menuItem_hovers.Length; i++)
            {
                if(menuItem_hovers[i].isEnabled)
                {
                    selectionCount++;
                }
            }
            if(selectionCount == 0) selectionCount = -1;

            //Loop the menu items
            if(currentSelection >= selectionCount)
            {
                currentSelection = 0;
            }
            else if(currentSelection < 0)
            {
                currentSelection = selectionCount-1;
            }

            //Highlight selection
            for(int i = 0; i < menuItem_hovers.Length; i++)
            {
                if(i == currentSelection)
                {
                    menuItem_hovers[i].EnterUI();
                }
                else
                {
                    menuItem_hovers[i].LeaveUI();
                }
            }
        }

        public void MenuConfirm(InputAction.CallbackContext context)
        {
            if(quitPopup.gameObject.activeSelf)
            {
                quitPopup.DoQuit();
                return;
            }

            if(!showHide.menu.activeSelf) return;

            switch(currentSelection)
            {
                case 0: //HDR Toggle
                    if(hdrToggle.toggle.interactable)
                    {
                        hdrToggle.ToggleHDR();
                    }
                    break;
                case 4: //Full Screen Calibration
                    fullScreenCalibration.Page1();
                    break;
                case 5: //Reset
                    brightness_ui_settings.Reset();
                    brightness_min_settings.Reset();
                    brightness_max_settings.Reset();
                    break;
                default:
                    break;
            }
        }

        private void BrightnessIncrease(InputAction.CallbackContext context)
        {
            if(!showHide.menu.activeSelf) return;
            bool isTap = context.interaction is TapInteraction;
            ControlDir(1f, isTap);
        }

        private void BrightnessDecrease(InputAction.CallbackContext context)
        {
            if(!showHide.menu.activeSelf) return;
            bool isTap = context.interaction is TapInteraction;
            ControlDir(-1f, isTap);
        }

        private void ControlDir(float direction, bool isTap)
        {
            switch(currentSelection)
            {
                case 1:
                    brightness_ui_settings.InputControl(direction,isTap);
                    break;
                case 2:
                    brightness_min_settings.InputControl(direction,isTap);
                    break;
                case 3:
                    brightness_max_settings.InputControl(direction,isTap);
                    break;
            }
        }

        private void StopBrightnessAdjust(InputAction.CallbackContext context)
        {
            brightness_ui_settings.PointerUp();
            brightness_min_settings.PointerUp();
            brightness_max_settings.PointerUp();
        }
    }
}