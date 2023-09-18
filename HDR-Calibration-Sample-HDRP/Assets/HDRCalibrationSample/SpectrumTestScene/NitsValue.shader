Shader "Unlit/NitsValue"
{
    Properties
    {
        [IntRange] _Count ("Count", Range (0, 20)) = 10
        _MinValue ("Min Value", float) = 0
        _MaxValue ("Max Value", float) = 5000
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

            int _Count;
            float _MinValue;
            float _MaxValue;
            float _PaperWhite;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float minVal = _MinValue / 400;
                float maxVal = _MaxValue / 400;

                float x = i.uv.x;
                float discrete = floor(x * _Count) / _Count;
                discrete = lerp(minVal,maxVal,discrete);

                return discrete;
            }
            ENDCG
        }
    }
}
