Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Noise ("Noise", 2D) = "white" {}
        _Strength("Strength", Float) = 0.01
        _Strength_Noise("Strength Noise", Float) = 0.01
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
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float StepTypeFunction()
            {
                float a = _Time.y * 4;
                float b = fmod(a,4);
                float c = floor(b)/4;
                return c;
            }

            #define PI 3.1415926535897932384626433832795

            sampler2D _MainTex;
            sampler2D _Noise;
            float4 _MainTex_ST;
            float _Strength;
            float _Strength_Noise;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float2 uvWithOffset = i.uv + StepTypeFunction();

                float4 n = tex2D(_Noise, uvWithOffset);
                n = n *  _Strength_Noise;
                float2 newUV = i.uv + n.rgb;

                float2 centeredUV = i.uv * 2 - 1;
                float dist = 1 - length(centeredUV);
                dist = dist + 0.5;
                //return float4(dist.xxx,1);

                float vary_Cos = (-cos(_Time.y/1.5 + PI/2)) * _Strength;
                float vary_Sin = (-sin(_Time.y/1.5 + PI/2)) * _Strength;
                float vary_Sin2 = (sin(_Time.y/1.5 + PI/2)) * _Strength;
                
                float4 col = tex2D(_MainTex,newUV);

                float4 col_right = tex2D(_MainTex, float2(newUV.x + vary_Sin2,newUV.y + vary_Sin2));
                float4 col_left = tex2D(_MainTex, float2(newUV.x - vary_Cos,newUV.y - vary_Sin));
                float4 col_down = tex2D(_MainTex, float2(newUV.x + vary_Cos,newUV.y + vary_Sin));

                float4 finalCol = float4(col_right.r,col_left.g,col_down.b,col.a);

                float4 lerpCol = lerp(finalCol,col,saturate(dist));

                //float4 testShift = tex2D(col.r, i.uv);

                return lerpCol;
            }
            ENDCG
        }
    }
}
