using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_SetBrightness : MonoBehaviour
{
    public float minValue = 0f;
    public float maxValue = 1000f;
    public float defaultValue = 500f;
    public float increment = 1f;
    public bool isInteger = false;

    public TextMeshProUGUI label;

    [HideInInspector] public float value = 0f;
    private bool isContinuous = false;
    private float direction = 1;

    public Button increaseButton;
    public Button decreaseButton;
    public UI_HighlightOnHover highlightOnHover;

    private const float interval = 0.2f;
    private float timer = 0f;
    private const float keyHoldTimeAcceleration = 10f;
    private float keyHoldTime = 0f;

    public UI_TonemappingValues tonemappingValues;
    public enum TonemappingValueTypes
    {
        PaperWhite,
        MinNits,
        MaxNits,
        Material
    }
    public TonemappingValueTypes tonemappingValue = TonemappingValueTypes.PaperWhite;
    public Material logoMaterial;
    public bool useDetectedAsDefault = true;

    void Start()
    {
        Reset();
        UpdateButtonStatus();
    }

    void Update()
    {
        if(UI_HDRHelper.isHDRActiveChanged)
        {
            UpdateButtonStatus();
        }

        if(!UI_HDRHelper.IsHDRActive()) return;

        if(isContinuous)
        {
            if(timer <= 0)
            {
                value += increment * direction * (1f+keyHoldTime*keyHoldTimeAcceleration);
                SetValue();

                timer = interval;
            }
            timer -= Time.deltaTime;
            keyHoldTime += Time.deltaTime;
        }
    }

    private void UpdateButtonStatus()
    {
        if(UI_HDRHelper.IsHDRActive())
        {
            UseDetectedAsDefault();
            increaseButton.interactable = true;
            decreaseButton.interactable = true;
            if(highlightOnHover != null) highlightOnHover.EnableUI();
        }
        else //disable buttons if HDR is not active
        {
            increaseButton.interactable = false;
            decreaseButton.interactable = false;
            if(highlightOnHover != null) highlightOnHover.DisableUI();
        }
    }

    public void PointerUp()
    {
        isContinuous = false;
        timer = interval;
        keyHoldTime = 0f;
    }

    public void Reset()
    {
        PointerUp();
        if(useDetectedAsDefault) UseDetectedAsDefault();
        value = defaultValue;
        SetValue();
    }

    public void UIButtonTap(float dir)
    {
        InputControl(dir, true);
    }

    public void UIButtonPointerDown(float dir)
    {
        InputControl(dir, false);
    }

    public void InputControl(float dir, bool isTap)
    {
        direction = dir;

        if(isTap)
        {
            isContinuous = false;
            keyHoldTime = 0f;
            timer = interval;
            value += increment * direction;
            SetValue();
        }
        else
        {
            isContinuous = true;
            keyHoldTime = 0f;
            timer = interval;
        }
    }

    public void SetValue()
    {
        value = Mathf.Clamp(value, minValue, maxValue);

        if(value == minValue)
        {
            decreaseButton.interactable = false;
        }
        else
        {
            decreaseButton.interactable = true;
        }

        if(value == maxValue)
        {
            increaseButton.interactable = false;
        }
        else
        {
            increaseButton.interactable = true;
        }

        if (isInteger)
        {
            label.text = value.ToString("F0");
        }
        else
        {
            label.text = value.ToString("F2");
        }

        switch(tonemappingValue)
        {
            case TonemappingValueTypes.PaperWhite:
                tonemappingValues.SetPaperWhite(value);
                break;
            case TonemappingValueTypes.MinNits:
                tonemappingValues.SetMinNits(value);
                break;
            case TonemappingValueTypes.MaxNits:
                tonemappingValues.SetMaxNits(value);
                break;
            case TonemappingValueTypes.Material:
                logoMaterial.SetFloat("_Value", value/tonemappingValues.GetPaperWhite());
                break;
            default:
                break;
        }
    }

    public void UseDetectedAsDefault()
    {
        var display = UI_HDRHelper.GetCurrentHDRDisplay();
        if(UI_HDRHelper.IsHDRAvaiable())
        {
            switch(tonemappingValue)
            {
                case TonemappingValueTypes.PaperWhite:
                    if(display.paperWhiteNits > 0f) defaultValue = display.paperWhiteNits;
                    break;
                case TonemappingValueTypes.MinNits:
                    if(display.minToneMapLuminance > 0f) defaultValue = display.minToneMapLuminance;
                    break;
                case TonemappingValueTypes.MaxNits:
                    if(display.maxToneMapLuminance > 0f) defaultValue = display.maxToneMapLuminance;
                    break;
                default:
                    break;
            }
        }
    }
}
