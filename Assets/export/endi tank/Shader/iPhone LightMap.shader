Shader "ETB/iPhone Lightmap" {
Properties {
 _MainTex ("Texture (RGB)", 2D) = "" {}
 _LightMap ("Lightmap", 2D) = "" {}
}
SubShader { 
 Pass {
  BindChannels {
   Bind "vertex", Vertex
   Bind "texcoord", TexCoord0
   Bind "texcoord1", TexCoord1
   Bind "texcoord", TexCoord2
   Bind "texcoord1", TexCoord3
  }
  SetTexture [_MainTex] { combine texture, texture alpha }
  SetTexture [_LightMap] { combine previous * texture }
 }
}
SubShader { 
 Pass {
  BindChannels {
   Bind "vertex", Vertex
   Bind "texcoord", TexCoord0
   Bind "texcoord1", TexCoord1
  }
  Blend SrcAlpha OneMinusSrcAlpha
  SetTexture [_MainTex] { combine texture, texture alpha }
  SetTexture [_LightMap] { combine previous * texture }
 }
}
}