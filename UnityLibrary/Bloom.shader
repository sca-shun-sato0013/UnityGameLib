Shader "Unlit/Bloom"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Threshold ("Threshold",Range(0,1)) = 0
        _Strength ("Strength",Range(1,64)) = 1
        _Blur ("Blur",Range(0,1)) = 0
    }
    SubShader
    {        
        Tags { "RenderType"="Opaque" }

        CGINCLUDE
        #include "UnityCG.cginc"
        sampler2D _MainTex;
        sampler2D _Tmp;
        float4 _MainTex_ST;
        float _Threshold;
        float _Strength;
        float _Blur;
        

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                float bright = (col.r + col.g + col.b) / 3;
                float tmp = step(_Threshold, bright);
                
                return col * tmp * _Strength;
            }


            fixed4 fragBlur(v2f i) : SV_Target
            {
                float u = 1 / _ScreenParams.x;
                float v = 1 / _ScreenParams.y;

                fixed4 result;

                for (float x = 0; x < _Blur; x++) {

                    float xx = i.uv.x + (x - _Blur / 2) * u;
                    
                    for (float y = 0; y < _Blur; y++) {

                        float yy = i.uv.y + (y - _Blur / 2) * v;
                        float4 smp = tex2D(_MainTex, float2(xx, yy));
                        result += smp;       
                    }
                }

                result /= _Blur * _Blur;
                return tex2D(_MainTex,i.uv) + result;
            }

        ENDCG

            Pass
            {
                CGPROGRAM
                #pragma vertex vert  
                #pragma fragment frag
                ENDCG
            }

            // Pass 1 blur & ‡¬  
            Pass
            {
                Blend One One
                CGPROGRAM
                #pragma vertex vert  
                #pragma fragment fragBlur  
                ENDCG
            }
    }
}
