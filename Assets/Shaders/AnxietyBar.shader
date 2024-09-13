Shader "Unlit/AnxietyBar"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Anxiety ("_Anxiety", Range(0, 1)) = 1
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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Anxiety;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float ifcheck = i.uv.x < _Anxiety;
                if (ifcheck == 0) {
				    discard;
			    }

                fixed4 col = tex2D(_MainTex, float2(1 - _Anxiety, i.uv.y));
                return col;
            }
            ENDCG
        }
    }
}
