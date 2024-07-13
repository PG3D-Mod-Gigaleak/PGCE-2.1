// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "iPhone/LightMap (linux friendly)" {
Properties {
    _Color ("Main Color", Color) = (1,1,1,1)
    _texBase ("Base (RGB)", 2D) = "white" {}
    _texLightmap ("Lightmap (RGB)", 2D) = "black" {}
}

SubShader {
    LOD 200
    Tags { "RenderType" = "Opaque" }
CGPROGRAM
#pragma surface surf Lambert nodynlightmap
struct Input {
  float2 uv_texBase;
  float2 uv2_texLightmap;
};
sampler2D _texBase;
sampler2D _texLightmap;
fixed4 _Color;
void surf (Input IN, inout SurfaceOutput o)
{
  o.Albedo = tex2D (_texBase, IN.uv_texBase).rgb * _Color;
  half4 lm = tex2D (_texLightmap, IN.uv2_texLightmap);
  o.Emission = lm.rgb*o.Albedo.rgb;
  o.Alpha = lm.a * _Color.a;
}
ENDCG
}
FallBack "Legacy Shaders/Lightmapped/VertexLit"
}
