Shader "Custom/VertexBlend" {
	Properties {
		_Color ("Color", Vector) = (0.5,0.5,0.5,1)
		_MainTex ("MainTex", 2D) = "white" {}
		_Normalmap ("Normalmap", 2D) = "bump" {}
		_Spec ("Spec", Float) = 1
		_Gloss ("Gloss", Float) = 8
		_Height ("Height", 2D) = "white" {}
		_BlendTex ("BlendTex", 2D) = "white" {}
		_BlendNormal ("BlendNormal", 2D) = "bump" {}
		_Parallax ("Parallax", Range(0, 0.1)) = 0
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
	Fallback "Bumped Specular"
	//CustomEditor "ShaderForgeMaterialInspector"
}