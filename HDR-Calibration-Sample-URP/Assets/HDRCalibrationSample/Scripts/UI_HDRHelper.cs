using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HDRHelper : MonoBehaviour
{
    private static bool isHDRActivePrevious = false;
    public static bool isHDRActiveChanged = false;

    private static bool isHDRAvailablePrevious = false;
    public static bool isHDRAvailableChanged = false;

    #if UNITY_EDITOR
    public static bool editorHDRJustBeingTurnedOff = false;
    #endif

    public const float alphaDisabled = 0.2f;

    public UI_ErrorPopup errorPopup;

    void Start()
    {
        //Show error popup if HDR is not available
        if(!IsHDRAvaiable())
        {
            errorPopup.gameObject.SetActive(true);
        }
    }

    public static bool DoesSystemSupportsHDR()
    {
        return SystemInfo.hdrDisplaySupportFlags.HasFlag(HDRDisplaySupportFlags.Supported);
    }

    public static bool IsHDRAvaiable()
    {
        HDROutputSettings d = HDROutputSettings.main;
        return DoesSystemSupportsHDR() && d.available;
    }

    public static bool IsHDRActive()
    {
        HDROutputSettings d = HDROutputSettings.main;
        return d.active;
    }

    public static HDROutputSettings GetCurrentHDRDisplay()
    {
        return HDROutputSettings.main;
    }

    void Update()
    {
        //Checking if there is any changes in HDR state, so that the UIs can update accordingly
        if(isHDRActivePrevious != IsHDRActive())
        {
            isHDRActiveChanged = true;

            //No need to show error popup if HDR is active
            if(IsHDRActive())
            {
                errorPopup.gameObject.SetActive(false);
            }
        }
        else
        {
            isHDRActiveChanged = false;
        }

        if(isHDRAvailablePrevious != IsHDRAvaiable())
        {
            isHDRAvailableChanged = true;

            //Show error popup again if HDR changed to not available
            #if UNITY_EDITOR
            if(!IsHDRAvaiable() && !editorHDRJustBeingTurnedOff)
            {
                errorPopup.gameObject.SetActive(true);
            }
            #else
            if(!IsHDRAvaiable())
            {
                errorPopup.gameObject.SetActive(true);
            }
            #endif

        }
        else
        {
            isHDRAvailableChanged = false;
        }

        isHDRActivePrevious = IsHDRActive();
        isHDRAvailablePrevious = IsHDRAvaiable();
    }
}
