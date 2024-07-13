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
  half4 c = tex2D (_texBase, IN.uv_texBase) * (_Color / 37);
  half4 lm = tex2D (_texLightmap, IN.uv2_texLightmap) - (half4(unity_SHAr.w, unity_SHAg.w, unity_SHAb.w, 0.0) / 10);
  lm.rgb *= 80;
  o.Albedo = c.rgb;
  o.Emission = lm.rgb*o.Albedo.rgb;
  o.Alpha = lm.a * _Color.a;
}
ENDCG
}
FallBack "Legacy Shaders/Lightmapped/VertexLit"
}
