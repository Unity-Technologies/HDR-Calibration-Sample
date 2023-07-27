using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UI_TonemappingValues : MonoBehaviour
{
    [HideInInspector] public UnityEngine.Rendering.Universal.Tonemapping urp_tonemapping;
    private bool urp_success = false;

    void Awake()
    {
        if(urp_tonemapping == null)
        {
            urp_success = GetComponent<Volume>().profile.TryGet<UnityEngine.Rendering.Universal.Tonemapping>(out urp_tonemapping);
        }
    }

    public float GetPaperWhite()
    {
        return urp_tonemapping.paperWhite.value;
    }

    public float GetMaxNits()
    {
        return urp_tonemapping.maxNits.value;
    }

    public float GetMinNits()
    {
        return urp_tonemapping.minNits.value;
    }

    public void SetPaperWhite(float value)
    {
        urp_tonemapping.paperWhite.value = value;
    }

    public void SetMaxNits(float value)
    {
        urp_tonemapping.maxNits.value = value;
    }

    public void SetMinNits(float value)
    {
        urp_tonemapping.minNits.value = value;
    }
}
