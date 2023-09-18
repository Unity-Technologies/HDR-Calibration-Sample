using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HDRCalibrationSample
{
    public class UI_HDRToggle : MonoBehaviour
    {
        [HideInInspector] public Toggle toggle;
        public UI_HighlightOnHover highlightOnHover;
        public UI_HighlightOnHover[] otherMenuItems;

        void Start()
        {
            toggle = GetComponent<Toggle>();
            UpdateToggleOnOff();
        }

        void Update()
        {
            if(UI_HDRHelper.isHDRAvailableChanged || UI_HDRHelper.isHDRActiveChanged)
            {
                UpdateToggleOnOff();
            }
        }

        private void UpdateToggleOnOff()
        {
            //Update checkbox based on status
            if(UI_HDRHelper.IsHDRActive())
            {
                toggle.isOn = true;
                foreach (UI_HighlightOnHover item in otherMenuItems)
                {
                    item.EnableUI();
                }
            }
            else
            {
                toggle.isOn = false;
                foreach (UI_HighlightOnHover item in otherMenuItems)
                {
                    item.DisableUI();
                }
            }

            //Grey out toggle depends on Editor or Player settings
            bool shouldGreyOutToggle = false;
            
            // Check if HDR is available and switchable
            if( !UI_HDRHelper.IsHDRAvaiable() || !SystemInfo.hdrDisplaySupportFlags.HasFlag(HDRDisplaySupportFlags.RuntimeSwitchable) )
            {
                shouldGreyOutToggle = true;
            }

            //Do the actual grey out
            if(shouldGreyOutToggle)
            {
                toggle.interactable = false;
                highlightOnHover.DisableUI();
            }
            else
            {
                toggle.interactable = true;
                highlightOnHover.EnableUI();
            }
        }

        public void ToggleHDR()
        {
            //If HDR is not switchable, do nothing
            if( !SystemInfo.hdrDisplaySupportFlags.HasFlag(HDRDisplaySupportFlags.RuntimeSwitchable) ) return;

            //Determine if HDR is on or off
            bool hdrIsOn = UI_HDRHelper.GetCurrentHDRDisplay().active;
            hdrIsOn = hdrIsOn?false:true;
            toggle.isOn = hdrIsOn;

            //Turn on/off HDR
            if(UI_HDRHelper.IsHDRAvaiable())
            {
                if(hdrIsOn)
                {
                    UI_HDRHelper.GetCurrentHDRDisplay().RequestHDRModeChange(true);
                }
                else
                {
                    UI_HDRHelper.GetCurrentHDRDisplay().RequestHDRModeChange(false);
                }
            }
        }
    }
}