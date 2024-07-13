Shader "Water+/Mobile" {
	Properties {
		_MainTex ("Main texture", 2D) = "bump" {}
		_DUDVFoamMap ("DUDV(RG) Foam(B)", 2D) = "white" {}
		_Cube ("Cubemap", Cube) = "" {}
		_WaterMap ("Depth (R), Foam (G), Transparency(B) Refr strength(A)", 2D) = "white" {}
		_SecondaryRefractionTex ("Refraction texture", 2D) = "bump" {}
		_AnisoMap ("AnisoDir(RGB), AnisoLookup(A)", 2D) = "bump" {}
		_Reflectivity ("Reflectivity", Range(0, 1)) = 0.3
		_Refractivity ("Refractivity", Range(1, 5)) = 1
		_WaterAttenuation ("Water attenuation", Range(0, 2)) = 1
		_ShallowWaterTint ("Shallow water tint", Vector) = (0,0.26,0.39,1)
		_DeepWaterTint ("Deep water tint", Vector) = (0,0.26,0.39,1)
		_Shininess ("Shininess", Range(0.05, 20)) = 1
		_Gloss ("Gloss", Range(0, 20)) = 10
		_Fresnel0 ("fresnel0", Float) = 0.1
		_EdgeFoamStrength ("Edge foam strength", Range(0, 3)) = 1
		_normalStrength ("Normal strength", Range(0.01, 5)) = 0.5
		_refractionsWetness ("Refractions wetness", Range(0, 1)) = 0.8
		_Opaqueness ("Opaqueness", Range(0, 1)) = 0.9
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Water+/Mobile Opaque"
}