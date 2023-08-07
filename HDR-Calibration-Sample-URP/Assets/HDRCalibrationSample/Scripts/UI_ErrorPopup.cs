using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDRCalibrationSample
{
    public class UI_ErrorPopup : MonoBehaviour
    {
        public GameObject msg_system;
        private UI_ErrorPopup_Text msg_system_text;

        public GameObject msg_available;
        private UI_ErrorPopup_Text msg_available_text;

        public GameObject msg_osmain;
        public GameObject msg_gameview;

        public GameObject msg_playerSettings;
        private UI_ErrorPopup_Text msg_playerSettings_text;

        public GameObject msg_graphicsAPI;
        private UI_ErrorPopup_Text msg_graphicsAPI_text;

        void Awake()
        {
            GetComponent<CanvasGroup>().alpha = 1f;
            gameObject.SetActive(false);

            msg_available_text = msg_available.GetComponent<UI_ErrorPopup_Text>();
            msg_playerSettings_text = msg_playerSettings.GetComponent<UI_ErrorPopup_Text>();
            msg_graphicsAPI_text = msg_graphicsAPI.GetComponent<UI_ErrorPopup_Text>();
            msg_system_text = msg_system.GetComponent<UI_ErrorPopup_Text>();

            //These messages are not needed in player
            #if !UNITY_EDITOR
            msg_osmain.SetActive(false);
            msg_playerSettings.SetActive(false);
            msg_graphicsAPI.SetActive(false);
            #endif
        }

        void Update()
        {
            //Warning message if system is not HDR capable
            msg_system_text.SetStatus(UI_HDRHelper.DoesSystemSupportsHDR());

            //Warning message if HDR display is not available
            msg_available_text.SetStatus(UI_HDRHelper.IsHDRAvaiable());

            #if UNITY_EDITOR

            //Warning message if player settings does not have HDR output enabled
            msg_playerSettings_text.SetStatus(UnityEditor.PlayerSettings.useHDRDisplay);

            //Warning message if editor is using DX11
            bool isDX11 = SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Direct3D11;
            msg_graphicsAPI_text.SetStatus(!isDX11);

            #endif
        }
    }
}
