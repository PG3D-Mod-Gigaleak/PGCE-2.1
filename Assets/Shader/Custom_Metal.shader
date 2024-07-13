Shader "Custom/Metal" {
	Properties {
		_MainTex ("MainTex", 2D) = "white" {}
		_Cubemap ("Cubemap", Cube) = "_Skybox" {}
		_BumpMap ("BumpMap", 2D) = "bump" {}
		_Specular ("Specular", Float) = 0.8
		_Gloss ("Gloss", Float) = 8
		_Fresnel ("Fresnel", Range(0.1, 5)) = 0.1
		_CubeBlur ("Cube Blur", Range(0, 7)) = 0
		_CubeStrength ("Cube Strength", Float) = 1.5
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
	Fallback "Diffuse"
	//CustomEditor "ShaderForgeMaterialInspector"
}