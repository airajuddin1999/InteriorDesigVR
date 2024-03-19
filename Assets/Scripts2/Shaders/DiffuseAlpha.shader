Shader "Custom/DiffuseAlpha" {
    Properties{
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Texture (RGBA)", 2D) = "white" {}
        _Cutoff("Alpha cutoff", Range(0,1)) = 0.5

    }
        SubShader{
            Tags { "Queue" = "transparent" "IgnoreProjector" = "True" "RenderType" = "TransparentCutout" }
            LOD 200
            Blend SrcAlpha OneMinusSrcAlpha

            //Cull off

            CGPROGRAM
            #pragma surface surf Lambert fullforwardshadows keepalpha
            #pragma target 3.0

            sampler2D _MainTex;

            struct Input {
                float2 uv_MainTex;
            };


            fixed4 _Color;
            float  _Cutoff;

            void surf(Input IN, inout SurfaceOutput o) {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
                o.Albedo = c.rgb * _Color;
                o.Alpha = c.a;
                clip(o.Alpha - _Cutoff);
            }
            ENDCG
        }
            Fallback "Legacy Shaders/Transparent/Cutout/VertexLit"

}