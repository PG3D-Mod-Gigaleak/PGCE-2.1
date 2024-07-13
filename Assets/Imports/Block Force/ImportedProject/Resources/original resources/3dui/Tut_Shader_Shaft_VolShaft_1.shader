Shader "Tut/Shader/Shaft/VolShaft_1" {
	Properties {
		FracTex ("Fractral Tex for shaft", 2D) = "white" {}
		BaseC ("Base Color", Vector) = (1,1,1,1)
		exL ("Extrusion", Range(0, 12)) = 10
		kP ("Factor of Power", Float) = 1
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
}