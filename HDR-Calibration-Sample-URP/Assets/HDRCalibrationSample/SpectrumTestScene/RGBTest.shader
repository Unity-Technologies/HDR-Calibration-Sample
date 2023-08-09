Shader "Unlit/RGBTest"
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

            float Range(float x, float offset)
            {
                float v = frac(x + offset);
                v = 2*(v - 0.5);
                v = abs(v);
                v = saturate(2*(v-0.5));
                return v;
            }

            float4 frag (v2f i) : SV_Target
            {
                float value = lerp(_MinValue,_MaxValue,i.uv.y);
                float4 col = 0;
                
                float spacing = 1.0/3.0;
                float x = i.uv.x;

                col.r = Range(x,0*spacing);
                col.g = Range(x,1*spacing);
                col.b = Range(x,2*spacing);

                col.rgb *= value;
                col.a = 1;

                return col;
            }
            ENDCG
        }
    }
}
