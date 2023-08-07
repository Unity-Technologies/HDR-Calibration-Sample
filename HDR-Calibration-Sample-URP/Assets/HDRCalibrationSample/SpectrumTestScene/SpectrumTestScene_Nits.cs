using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace HDRCalibrationSample
{
    [ExecuteInEditMode]
    public class SpectrumTestScene_Nits : MonoBehaviour
    {
        private Tonemapping tonemapping;
        private bool success = false;

        void Init()
        {
            if(tonemapping == null || !success)
            {
                success = GetComponent<Volume>().profile.TryGet<Tonemapping>(out tonemapping);
            }
        }

        void Update()
        {
            Init();

            Shader.SetGlobalFloat("_PaperWhite", tonemapping.paperWhite.value);
        }
    }
}