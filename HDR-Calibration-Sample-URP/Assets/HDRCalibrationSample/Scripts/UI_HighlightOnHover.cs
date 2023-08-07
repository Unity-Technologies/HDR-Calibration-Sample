using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace HDRCalibrationSample
{
    public class UI_HighlightOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private TextMeshProUGUI[] labels;
        public bool isEnabled = true;
        public GameObject dot;
        private CanvasGroup canvasGroup;
        public EventTrigger eventTrigger;

        void Start()
        {
            labels = GetComponentsInChildren<TextMeshProUGUI>();
            canvasGroup = GetComponent<CanvasGroup>();

            LeaveUI();

            if(isEnabled)
            {
                EnableUI();
            }
            else
            {
                DisableUI();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            EnterUI();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            LeaveUI();
        }

        public void EnterUI()
        {
            if(!isEnabled) return;

            foreach (TextMeshProUGUI label in labels)
            {
                label.fontStyle = FontStyles.Bold;
            }

            if(dot != null) dot.SetActive(true);
        }

        public void LeaveUI()
        {
            if(!isEnabled) return;

            foreach (TextMeshProUGUI label in labels)
            {
                label.fontStyle = FontStyles.Normal;
            }

            if(dot != null) dot.SetActive(false);
        }

        public void EnableUI()
        {
            isEnabled = true;
            if(canvasGroup != null) canvasGroup.alpha = 1f;
            if(eventTrigger != null) eventTrigger.enabled = true;
        }

        public void DisableUI()
        {
            LeaveUI();
            isEnabled = false;
            if(canvasGroup != null) canvasGroup.alpha = UI_HDRHelper.alphaDisabled;
            if(eventTrigger != null) eventTrigger.enabled = false;
        }
    }
}