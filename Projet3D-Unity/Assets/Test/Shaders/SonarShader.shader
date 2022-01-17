// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Echolocation" {
    Properties {
        _MainTex ("Texture", 2D) = "black" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Center ("CenterX", vector) = (0, 0, 0)
        _Radius ("Radius", float) = 0
        _Transparency("Transparency", Range(0.0,0.5)) = 0.25
    }
    SubShader
    {
        Tags { "Queue"="Transparent"  "RenderType"="Transparent" }
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass {
       
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"


            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float3 _Center;
            float _Radius;
            float _Transparency;
 
            struct v2f {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };
 
            v2f vert(appdata_base v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }
 
            fixed4 frag(v2f i) : COLOR {
                //fixed4 col = tex2D(_MainTex, i.worldPos);
                //col.a = _Transparency;

                float dist = distance(_Center, i.worldPos);

                float val = 1 - step(dist, _Radius - 0.1) * 0.5;
                val = step(_Radius - 1.5, dist) * step(dist, _Radius) * val;
                return fixed4(val * _Color.r, val * _Color.g,val * _Color.b, 1.0);
            }
 
            ENDCG
        }
    }
    FallBack "Diffuse"
}
