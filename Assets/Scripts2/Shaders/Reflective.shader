Shader "Example/WorldRefl Normalmap" {

    Properties{
      _Color("Color", Color) = (1, 1, 1, 1)
      _ReflectColor("Reflection Color", Color) = (1, 1, 1, 1)
      _MainTex("Texture", 2D) = "white" {}
      _BumpMap("Normalmap", 2D) = "bump" {}
      _Cube("Reflection Cubemap", CUBE) = "" {}
      
    }
        SubShader{

          Tags { "RenderType" = "Opaque" }
          
          CGPROGRAM
          #pragma surface surf Lambert

          struct Input {
              float2 uv_MainTex;
              float3 worldRefl;
              INTERNAL_DATA
          };
          sampler2D _MainTex;
          sampler2D _BumpMap;
          samplerCUBE _Cube;
          fixed4 _Color;
          fixed4 _ReflectColor;

          void surf(Input IN, inout SurfaceOutput o) {
              
              o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * _Color;
              o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
              o.Emission = texCUBE(_Cube, WorldReflectionVector(IN, o.Normal)).rgb  * _ReflectColor;
          }
          ENDCG
    }
        Fallback "Diffuse"
}
