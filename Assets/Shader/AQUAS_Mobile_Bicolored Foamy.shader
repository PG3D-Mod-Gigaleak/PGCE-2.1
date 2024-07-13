Shader "AQUAS/Mobile/Bicolored Foamy" {
	Properties {
		[NoScaleOffset] _NormalTexture ("Normal Texture", 2D) = "white" {}
		_NormalTiling ("Normal Tiling", Float) = 1
		_DeepWaterColor ("Deep Water Color", Vector) = (0.07843138,0.3921569,0.7843137,1)
		_ShallowWaterColor ("Shallow Water Color", Vector) = (0.4411765,0.9537525,1,1)
		_DepthTransparency ("Depth Transparency", Float) = 1.5
		_ShoreFade ("Shore Fade", Float) = 0.3
		_ShoreTransparency ("Shore Transparency", Float) = 0.04
		_ShallowDeepBlend ("Shallow-Deep-Blend", Float) = 3.6
		_Fade ("Shallow-Deep-Fade", Float) = 3
		[HideInInspector] _ReflectionTex ("Reflection Tex", 2D) = "white" {}
		[MaterialToggle] _UseReflections ("Enable Reflections", Float) = 0.5429346
		_Reflectionintensity ("Reflection intensity", Range(0, 1)) = 0.5
		_Distortion ("Distortion", Range(0, 2)) = 0.3
		_Specular ("Specular", Float) = 1
		_SpecularColor ("Specular Color", Vector) = (0.5,0.5,0.5,1)
		_Gloss ("Gloss", Float) = 0.8
		_LightWrapping ("Light Wrapping", Float) = 1.5
		_Refraction ("Refraction", Range(0, 1)) = 0.5
		_WaveSpeed ("Wave Speed", Float) = 40
		[NoScaleOffset] _FoamTexture ("Foam Texture", 2D) = "white" {}
		_FoamTiling ("Foam Tiling", Float) = 3
		_FoamBlend ("Foam Blend", Float) = 0.15
		_FoamVisibility ("Foam Visibility", Range(0, 1)) = 0.3
		_FoamIntensity ("Foam Intensity", Float) = 5
		_FoamContrast ("Foam Contrast", Range(0, 0.5)) = 0.25
		_FoamColor ("Foam Color", Vector) = (0.5,0.5,0.5,1)
		_FoamSpeed ("Foam Speed", Float) = 120
		[HideInInspector] _Cutoff ("Alpha cutoff", Range(0, 1)) = 0.5
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	Fallback "Diffuse"
}