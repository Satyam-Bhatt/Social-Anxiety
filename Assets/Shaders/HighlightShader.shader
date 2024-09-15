Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Noise ("Noise Texture", 2D) = "white" {}
        _Strength("Strength", Float) = 0.01
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            ZWrite Off
            ZTest LEqual
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR0;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 colorr : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _Noise;
            float _Strength;

            float StepTypeFunction()
            {
                float a = _Time.y * 4;
                float b = fmod(a,4);
                float c = floor(b)/4;
                return c;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.colorr = v.color;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float2 uvWithOffset = i.uv + StepTypeFunction();

                float4 n = tex2D(_Noise, uvWithOffset);
                n = n * _Strength;
                float2 newUV = i.uv + n.rgb;

                float4 col = tex2D(_MainTex, newUV);
                
                float2 output = (i.uv * 2) - 1;
                float len = length(output) - 0.8;
                len = saturate(len);               
                len = len;
                float3 len3 = float3(1,1,0) * len + sin(_Time.y * 5) * 0.25 + 0.25 ;
                return float4(col.rgb * i.colorr.rgb + len3,col.a);
            }
            ENDCG
        }
    }
}
