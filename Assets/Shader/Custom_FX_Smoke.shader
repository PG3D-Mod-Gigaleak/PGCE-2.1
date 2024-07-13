Shader "Custom/FX/Smoke" {
	Properties {
		_Color ("Color", Vector) = (0.5,0.5,0.5,1)
		_MainTex ("MainTex", 2D) = "white" {}
		_Emission ("Emission", Float) = 2
		_Speed ("Speed", Float) = 1
		_NoiseTiling ("Noise Tiling", Float) = 0.5
		_NoiseIntensity ("Noise Intensity", Float) = 1
		[MaterialToggle] _UseSoftParticles ("Use Soft Particles", Float) = 0
		_SoftParticles ("Soft Particles", Range(0, 1)) = 0
		[HideInInspector] _Cutoff ("Alpha cutoff", Range(0, 1)) = 0.5
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Transparent/Diffuse"
	//CustomEditor "ShaderForgeMaterialInspector"
}