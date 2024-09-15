Shader "Unlit/StartPoint"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color1 ("Color 1", Color) = (1,1,1,1)
        _Color2 ("Color 2", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { 
            "RenderType"="Transparent" 
            "Queue"="Transparent" 
        }

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
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color1;
            float4 _Color2;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                i.uv = i.uv * 2 - 1;

                float dist = 1 - length(i.uv);
                float someCos = cos(dist * 3.1415926 * 5 - _Time.y * 3) * 0.5 + 0.5;
                float4 colWeNeed = lerp(_Color1, _Color2 * 1.5, someCos);

                float clamp_ = saturate(dist);
                if (clamp_ > 0) clamp_ += 0.2;
                return float4(colWeNeed.rgb,clamp_);
            }
            ENDCG
        }
    }
}
