Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float4 col = tex2D(_MainTex,i.uv);

                float4 col_right = tex2D(_MainTex, float2(i.uv.x,i.uv.y-0.04));
                float4 col_left = tex2D(_MainTex, float2(i.uv.x,i.uv.y-0.05));
                float4 col_down = tex2D(_MainTex, float2(i.uv.x,i.uv.y-0.03));

                //float4 testShift = tex2D(col.r, i.uv);

                return float4(col_right.r,col_left.g,col_down.b,1);
            }
            ENDCG
        }
    }
}
