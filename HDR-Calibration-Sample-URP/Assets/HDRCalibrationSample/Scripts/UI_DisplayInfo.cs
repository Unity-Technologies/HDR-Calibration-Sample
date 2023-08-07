using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace HDRCalibrationSample
{
    public class UI_DisplayInfo : MonoBehaviour
    {
        public TextMeshProUGUI label_key;
        public TextMeshProUGUI label_value;

        private const string green = "<color=#7cf56b>";
        private const string red = "<color=#db4b4b>";

        private VerticalLayoutGroup group;
        private HorizontalLayoutGroup group_content;

        public void Start()
        {
            this.gameObject.SetActive(false);
            GetComponent<CanvasGroup>().alpha = 1f;
        }

        public void ShowPopup()
        {
            this.gameObject.SetActive(true);
        }

        public void ClosePopup()
        {
            this.gameObject.SetActive(false);
        }

        public void TogglePopup() //For input control
        {
            if(this.gameObject.activeSelf)
            {
                ClosePopup();
            }
            else
            {
                ShowPopup();
            }
        }

        public void Update()
        {
            UpdateInfo();
        }

        public void UpdateInfo()
        {

            string key = "";
            string value = "";

            //System header
            key = "<b>"+"System";
            key += "</b>" + "\n";
            value += "\n";

            //Platform
            key += "Platform" + "\n";
            value += Application.platform.ToString() + "\n";

            //Graphics API
            key += "Graphics API" + "\n";
            value += SystemInfo.graphicsDeviceType.ToString() + "\n";

            //System - HDR Support flags
            key += "HDR Display Support Flags" + "\n";
            value += SystemInfo.hdrDisplaySupportFlags + "\n";

            //For each display
            HDROutputSettings[] displays = HDROutputSettings.displays;
            for(int i=0; i<displays.Length; i++)
            {
                //New line
                key += "\n";
                value += "\n";

                HDROutputSettings d = displays[i];

                //Display Header
                key += "<b>"+"Connected Display";
                if(HDROutputSettings.main == d)
                {
                    key += " (main)";
                }
                key += "</b>" + "\n";
                value += "\n";

                //Active
                key += "HDR Output Active" + "\n";
                value += d.active? green : red;
                value += d.active + "</color>" + "\n";           

                //Available
                key += "HDR Output Available" + "\n";
                value += d.available? green : red;
                value += d.available + "</color>" + "\n";

                //space
                key += "\n";
                value += "\n";  

                if(d.available)
                {
                    //Display Color Gamut
                    key += "Display Color Gamut" + "\n";
                    value += d.displayColorGamut + "\n";

                    //PlayerSettings bit depth
                    #if UNITY_EDITOR
                    key += "PlayerSettings Bit Depth" + "\n";
                    value += UnityEditor.PlayerSettings.hdrBitDepth + "\n";
                    #endif

                    //Format
                    key += "Graphics Format" + "\n";
                    value += d.graphicsFormat + "\n";

                    //Automatic HDR Tonemapping
                    key += "Automatic HDR Tonemapping" + "\n";
                    value += d.automaticHDRTonemapping + "\n";

                    //space
                    key += "\n";
                    value += "\n";

                    //paperWhiteNits
                    key += "PaperWhiteNits" + "\n";
                    value += d.paperWhiteNits + "\n";

                    //minToneMapLuminance
                    key += "MinToneMapLuminance" + "\n";
                    value += d.minToneMapLuminance + "\n";

                    //maxToneMapLuminance
                    key += "MaxToneMapLuminance" + "\n";
                    value += d.maxToneMapLuminance + "\n";

                    //maxFullFrameToneMapLuminance
                    key += "MaxFullFrameToneMapLuminance" + "\n";
                    value += d.maxFullFrameToneMapLuminance + "\n";
                }
            }

            //Apply to labels
            label_key.text = key;
            label_value.text = value;

            //Make sure popup is resized to fit content
            if(group_content == null) group_content = GetComponentInChildren<HorizontalLayoutGroup>();
            group_content.CalculateLayoutInputHorizontal();
            group_content.CalculateLayoutInputVertical();
            group_content.SetLayoutHorizontal();
            group_content.SetLayoutVertical();
            LayoutRebuilder.ForceRebuildLayoutImmediate(group_content.GetComponent<RectTransform>());

            if(group == null) group = GetComponent<VerticalLayoutGroup>();
            group.CalculateLayoutInputHorizontal();
            group.CalculateLayoutInputVertical();
            group.SetLayoutHorizontal();
            group.SetLayoutVertical();
            LayoutRebuilder.ForceRebuildLayoutImmediate(group.GetComponent<RectTransform>());
        }
    }
}