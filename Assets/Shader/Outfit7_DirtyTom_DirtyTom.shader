Shader "Outfit7/DirtyTom/DirtyTom" {
	Properties {
		_MainTex ("Diffuse", 2D) = "white" {}
		_MainTexCloth ("Cloth", 2D) = "white" {}
		_MainTexClothAlpha ("ClothAlpha", 2D) = "white" {}
		_DirtMask ("_DirtMask", 2D) = "black" {}
		_DirtColor ("_DirtColor", Vector) = (0.188,0.114,0.0588,1)
		_LightAmount ("LightAmount", Float) = 1
		_CustomTex ("CustomTexture", 2D) = "black" {}
		_CustomColor ("CustomColor", Vector) = (0.99218,0.85937,0.09375,1)
		_CustomCubeMap ("CustomCubeMap", Cube) = "white" {}
		_CustomModeType ("_CustomModeType", Float) = 0
		[Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull", Float) = 2
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