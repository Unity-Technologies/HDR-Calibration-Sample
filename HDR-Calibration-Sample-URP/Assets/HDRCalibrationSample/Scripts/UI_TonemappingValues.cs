using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace HDRCalibrationSample
{
    public class UI_TonemappingValues : MonoBehaviour
    {
        [HideInInspector] public UnityEngine.Rendering.Universal.Tonemapping tonemapping;
        private bool success = false;

        void Awake()
        {
            if(tonemapping == null)
            {
                success = GetComponent<Volume>().profile.TryGet<UnityEngine.Rendering.Universal.Tonemapping>(out tonemapping);
            }
        }

        public float GetPaperWhite()
        {
            return tonemapping.paperWhite.value;
        }

        public float GetMaxNits()
        {
            return tonemapping.maxNits.value;
        }

        public float GetMinNits()
        {
            return tonemapping.minNits.value;
        }

        public void SetPaperWhite(float value)
        {
            tonemapping.paperWhite.value = value;
        }

        public void SetMaxNits(float value)
        {
            tonemapping.maxNits.value = value;
        }

        public void SetMinNits(float value)
        {
            tonemapping.minNits.value = value;
        }
    }
}