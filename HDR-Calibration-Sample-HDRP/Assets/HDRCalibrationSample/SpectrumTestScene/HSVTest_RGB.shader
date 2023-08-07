Shader "Unlit/HSVTest_RGB"
{
    Properties
    {
        _Saturation ("Saturation", Range (0, 1)) = 1
        _MinValue ("Min Value", Range (0, 10)) = 0
        _MaxValue ("Max Value", Range (0, 10)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float _Hue;
            float _Saturation;
            float _MinValue;
            float _MaxValue;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            //https://docs.unity3d.com/Packages/com.unity.shadergraph@16.0/manual/Colorspace-Conversion-Node.html
            //HSV > RGB
            void Unity_ColorspaceConversion_HSV_RGB_float(float3 In, out float3 Out)
            {
                float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
                float3 P = abs(frac(In.xxx + K.xyz) * 6.0 - K.www);
                Out = In.z * lerp(K.xxx, saturate(P - K.xxx), In.y);
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 col = 1;
                float value = lerp(_MinValue,_MaxValue,i.uv.y);
                Unity_ColorspaceConversion_HSV_RGB_float(float3(i.uv.x,_Saturation,value), col.rgb);

                return col;
            }
            ENDCG
        }
    }
}
