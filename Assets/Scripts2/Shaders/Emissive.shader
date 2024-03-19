Shader "Custom/Emissive" {
    Properties{
      _MainTex("Texture", 2D) = "white" {}
      _Color("Color", Color) = (1, 1, 1, 1)
      _BumpMap("Bumpmap", 2D) = "bump" {}
      _EmissionMap("Base (RGB)", 2D) = "white" {}
      _ColorMap("Color Emission", Color) = (1, 1, 1, 1)
      _Emission("Emission", float) = 0

    }
        SubShader{
          Tags { "RenderType" = "Opaque" }
          CGPROGRAM
          #pragma surface surf Lambert

          struct Input {
            float2 uv_MainTex;
          };

          sampler2D _MainTex;
          sampler2D _BumpMap;
          sampler2D _EmissionMap;
          fixed4 _Color;
          fixed4 _ColorMap;
          float _Emission;
          half _Metallic;

          void surf(Input IN, inout SurfaceOutput o) {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            fixed4 e = tex2D(_EmissionMap, IN.uv_MainTex) * _ColorMap;

            o.Albedo = c.rgb ;
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
            o.Emission = e.rgb * tex2D(_EmissionMap, IN.uv_MainTex).a * _Emission;
          }
          ENDCG
      }
          Fallback "Diffuse"
}