Shader "Custom/VertexBlend_Water" {
	Properties {
		_Color ("Color", Vector) = (0.5,0.5,0.5,1)
		_MainTex ("MainTex", 2D) = "white" {}
		_Normalmap ("Normalmap", 2D) = "bump" {}
		_Spec ("Spec", Float) = 0.7
		_Gloss ("Gloss", Float) = 8
		_Tessellation ("Tessellation", Float) = 5
		_Displacement ("Displacement", Range(0, 0.1)) = 0.1
		_WaterColor ("Water Color", Vector) = (0.5,0.5,0.5,1)
		_Normalwater ("Normal water", 2D) = "bump" {}
		_Mask ("Mask", 2D) = "white" {}
		_WaterSpeed ("Water Speed", Float) = 1
		_Cube ("Cube", Cube) = "_Skybox" {}
		_CubeStrength ("Cube Strength", Float) = 0.6
		_Height ("Height", Range(0, 0.1)) = 0.02
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
	Fallback "Tessellation/Bumped Specular (displacement)"
	//CustomEditor "ShaderForgeMaterialInspector"
}