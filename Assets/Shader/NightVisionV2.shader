Shader "Hidden/Aubergine/NightVisionV2" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_NoiseTex ("Noise Map", 2D) = "white" {}
		_NoiseAmount ("Noise Amount", Float) = 0.9
		_LumThreshold ("LumThreshold", Float) = 0.2
		_BrightenFactor ("BrightenFactor", Float) = 2
		_VisionColor ("Vision Color", Vector) = (0.1,0.95,0.2,1)
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
}