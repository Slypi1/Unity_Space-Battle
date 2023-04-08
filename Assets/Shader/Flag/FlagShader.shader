Shader "Custom/FlagShader" {
    Properties {
        _Amplitude ("Amplitude", Range(0, 1)) = 0.1
        _Frequency ("Frequency", Range(0, 10)) = 1
        _Speed ("Speed", Range(0, 10)) = 1
        _Offset ("Offset", Range(0, 1)) = 0.5
        _Color ("Color", Color) = (1, 1, 1, 1)
        _MainTex ("Texture", 2D) = "white" {}
        _ScrollOffset ("Scroll Offset", Range(0, 1)) = 0
    }

    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Opaque"}
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Amplitude;
            float _Frequency;
            float _Speed;
            float _Offset;
            float4 _Color;
            float _ScrollOffset;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;               
                float time = _Time.y * _Speed;
                float wave = sin(v.vertex.x * _Frequency + time + _Offset) * _Amplitude;
                o.vertex.y += wave;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                float2 uv = i.uv;
                uv.x += _ScrollOffset;
                fixed4 col = tex2D(_MainTex, uv) * _Color;
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}