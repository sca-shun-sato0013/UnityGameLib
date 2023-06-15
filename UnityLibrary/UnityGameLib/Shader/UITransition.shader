// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "UnityGameLib/Unlit/UITransition"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _DisolveTex("DisolveTex (RGB)", 2D) = "white" {}
        _Threshold("Threshold", Range(0,1)) = 0
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                // make fog work
                #pragma multi_compile_fog

                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    UNITY_FOG_COORDS(1)
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                sampler2D _DisolveTex;
                float4 _MainTex_ST;
                float _Threshold;

                UNITY_INSTANCING_BUFFER_START(Props)
                UNITY_INSTANCING_BUFFER_END(Props)

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    UNITY_TRANSFER_FOG(o,o.vertex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col = tex2D(_MainTex, i.uv);

                fixed4 m = tex2D(_DisolveTex,i.uv);
                half g = m.r * 0.299 + m.g * 0.587 + m.b * 0.114;

                // しきい値以下なら表示しない
                if (g < _Threshold) discard;

                return col;
            }
            ENDCG
        }
        }
}
