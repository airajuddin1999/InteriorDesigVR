Shader "Custom/Vertex Color Splat Surf Shader" {
     Properties {
     	 _ColorA ("Color A", Color) = (1,1,1,1)
     	 _Splat1 ("Base A (RGB)", 2D) = "white" {}
     	 _IntensityA ("Intensity A", Range(0.5, 1.5)) = 1

         _ColorC ("Color C", Color) = (1,1,1,1)
         _Splat3 ("Base C (RGB)", 2D) = "white" {}
         _IntensityC ("Intensity C", Range(0.5, 1.5)) = 1



     }
     SubShader {
         Tags { "RenderType"="Transparent" }
         LOD 200
         
         CGPROGRAM
         #pragma surface surf BlinnPhong
         
         sampler2D _Splat1;

         sampler2D _Splat3;

         fixed4 _ColorA;

		 fixed4 _ColorC;

		 float _IntensityA;

		 float _IntensityC;
		 
	


         struct Input {
             float2 uv_Splat1;

             float2 uv_Splat3;
             float4 color: Color; // Vertex color
         };
 
         void surf (Input IN, inout SurfaceOutput o) {

             half4 splat1 = tex2D (_Splat1, IN.uv_Splat1) * _ColorA * _IntensityA;

             half4 splat3 = tex2D (_Splat3, IN.uv_Splat3) * _ColorC * _IntensityC;

             fixed3 albedo = lerp(splat3.rgb, splat1.rgb, IN.color.b);
             
             o.Albedo = albedo;

         }
         ENDCG
     } 
     FallBack "Diffuse"
 }