// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Skybox/6 Sided Time Of Day" {
Properties {
    _Tint ("Tint Color", Color) = (.5, .5, .5, .5)
    [Gamma] _Exposure ("Exposure", Range(0, 8)) = 1.0
    _Rotation ("Rotation", Range(0, 360)) = 0
    [NoScaleOffset] _FrontTex ("Front [+Z]   (HDR)", 2D) = "grey" {}
    [NoScaleOffset] _BackTex ("Back [-Z]   (HDR)", 2D) = "grey" {}
    [NoScaleOffset] _LeftTex ("Left [+X]   (HDR)", 2D) = "grey" {}
    [NoScaleOffset] _RightTex ("Right [-X]   (HDR)", 2D) = "grey" {}
    [NoScaleOffset] _UpTex ("Up [+Y]   (HDR)", 2D) = "grey" {}
    [NoScaleOffset] _DownTex ("Down [-Y]   (HDR)", 2D) = "grey" {}
    [NoScaleOffset] _FrontTexEvening ("Front Evening [+Z]   (HDR)", 2D) = "grey" {}
    [NoScaleOffset] _BackTexEvening ("Back Evening [-Z]   (HDR)", 2D) = "grey" {}
    [NoScaleOffset] _LeftTexEvening ("Left Evening [+X]   (HDR)", 2D) = "grey" {}
    [NoScaleOffset] _RightTexEvening ("Right Evening [-X]   (HDR)", 2D) = "grey" {}
    [NoScaleOffset] _UpTexEvening ("Up Evening [+Y]   (HDR)", 2D) = "grey" {}
    [NoScaleOffset] _DownTexEvening ("Down Evening [-Y]   (HDR)", 2D) = "grey" {}
    [NoScaleOffset] _FrontTexNight ("Front Night [+Z]   (HDR)", 2D) = "grey" {}
    [NoScaleOffset] _BackTexNight ("Back Night [-Z]   (HDR)", 2D) = "grey" {}
    [NoScaleOffset] _LeftTexNight ("Left Night [+X]   (HDR)", 2D) = "grey" {}
    [NoScaleOffset] _RightTexNight ("Right Night [-X]   (HDR)", 2D) = "grey" {}
    [NoScaleOffset] _UpTexNight ("Up Night [+Y]   (HDR)", 2D) = "grey" {}
    [NoScaleOffset] _DownTexNight ("Down Night [-Y]   (HDR)", 2D) = "grey" {}
    _TimeOfDay ("Time Of Day", Range(0, 4)) = 0
}

SubShader {
    Tags { "Queue"="Background" "RenderType"="Background" "PreviewType"="Skybox" }
    Cull Off ZWrite Off

    CGINCLUDE
    #include "UnityCG.cginc"

    half4 _Tint;
    half _Exposure;
    float _Rotation;
    float _TimeOfDay;

    float3 RotateAroundYInDegrees (float3 vertex, float degrees)
    {
        float alpha = degrees * UNITY_PI / 180.0;
        float sina, cosa;
        sincos(alpha, sina, cosa);
        float2x2 m = float2x2(cosa, -sina, sina, cosa);
        return float3(mul(m, vertex.xz), vertex.y).xzy;
    }

    struct appdata_t {
        float4 vertex : POSITION;
        float2 texcoord : TEXCOORD0;
        UNITY_VERTEX_INPUT_INSTANCE_ID
    };
    struct v2f {
        float4 vertex : SV_POSITION;
        float2 texcoord : TEXCOORD0;
        UNITY_VERTEX_OUTPUT_STEREO
    };
    v2f vert (appdata_t v)
    {
        v2f o;
        UNITY_SETUP_INSTANCE_ID(v);
        UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
        float3 rotated = RotateAroundYInDegrees(v.vertex, _Rotation);
        o.vertex = UnityObjectToClipPos(rotated);
        o.texcoord = v.texcoord;
        return o;
    }
    half4 skybox_frag (v2f i, sampler2D smp, half4 smpDecode, sampler2D smp_e, half4 smpDecode_e, sampler2D smp_n, half4 smpDecode_n)
    {
        half4 tex = tex2D (smp, i.texcoord);
        if (_TimeOfDay <= 1)
        {
            tex = lerp(tex, tex2D(smp_e, i.texcoord), _TimeOfDay);
        }
        else if (_TimeOfDay <= 2)
        {
            tex = lerp(tex2D(smp_e, i.texcoord), tex2D(smp_n, i.texcoord), _TimeOfDay - 1);
        }
        else if (_TimeOfDay <= 3)
        {
            tex = lerp(tex2D(smp_n, i.texcoord), tex2D(smp_e, i.texcoord), _TimeOfDay - 2);
        }
        else if (_TimeOfDay <= 4)
        {
            tex = lerp(tex2D(smp_e, i.texcoord), tex, _TimeOfDay - 3);
        }
        half3 c = DecodeHDR (tex, smpDecode);
        c = c * _Tint.rgb * unity_ColorSpaceDouble.rgb;
        c *= _Exposure;
        return half4(c, 1);
    }
    ENDCG

    Pass {
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #pragma target 2.0
        sampler2D _FrontTex;
        half4 _FrontTex_HDR;
        sampler2D _FrontTexEvening;
        half4 _FrontTexEvening_HDR;
        sampler2D _FrontTexNight;
        half4 _FrontTexNight_HDR;
        half4 frag (v2f i) : SV_Target
        {
            return skybox_frag(i,_FrontTex, _FrontTex_HDR, _FrontTexEvening, _FrontTexEvening_HDR, _FrontTexNight, _FrontTexNight_HDR);
        }
        ENDCG
    }
    Pass{
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #pragma target 2.0
        sampler2D _BackTex;
        half4 _BackTex_HDR;
        sampler2D _BackTexEvening;
        half4 _BackTexEvening_HDR;
        sampler2D _BackTexNight;
        half4 _BackTexNight_HDR;
        half4 frag (v2f i) : SV_Target
        { 
            return skybox_frag(i,_BackTex, _BackTex_HDR, _BackTexEvening, _BackTexEvening_HDR, _BackTexNight, _BackTexNight_HDR);
        }
        ENDCG
    }
    Pass{
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #pragma target 2.0
        sampler2D _LeftTex;
        half4 _LeftTex_HDR;
        sampler2D _LeftTexEvening;
        half4 _LeftTexEvening_HDR;
        sampler2D _LeftTexNight;
        half4 _LeftTexNight_HDR;
        half4 frag (v2f i) : SV_Target
        { 
            return skybox_frag(i,_LeftTex, _LeftTex_HDR, _LeftTexEvening, _LeftTexEvening_HDR, _LeftTexNight, _LeftTexNight_HDR);
        }
        ENDCG
    }
    Pass{
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #pragma target 2.0
        sampler2D _RightTex;
        half4 _RightTex_HDR;
        sampler2D _RightTexEvening;
        half4 _RightTexEvening_HDR;
        sampler2D _RightTexNight;
        half4 _RightTexNight_HDR;
        half4 frag (v2f i) : SV_Target
        { 
            return skybox_frag(i,_RightTex, _RightTex_HDR, _RightTexEvening, _RightTexEvening_HDR, _RightTexNight, _RightTexNight_HDR);
        }
        ENDCG
    }
    Pass{
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #pragma target 2.0
        sampler2D _UpTex;
        half4 _UpTex_HDR;
        sampler2D _UpTexEvening;
        half4 _UpTexEvening_HDR;
        sampler2D _UpTexNight;
        half4 _UpTexNight_HDR;
        half4 frag (v2f i) : SV_Target
        { 
            return skybox_frag(i,_UpTex, _UpTex_HDR, _UpTexEvening, _UpTexEvening_HDR, _UpTexNight, _UpTexNight_HDR);
        }
        ENDCG
    }
    Pass{
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #pragma target 2.0
        sampler2D _DownTex;
        half4 _DownTex_HDR;
        sampler2D _DownTexEvening;
        half4 _DownTexEvening_HDR;
        sampler2D _DownTexNight;
        half4 _DownTexNight_HDR;
        half4 frag (v2f i) : SV_Target
        { 
            return skybox_frag(i,_DownTex, _DownTex_HDR, _DownTexEvening, _DownTexEvening_HDR, _DownTexNight, _DownTexNight_HDR);
        }
        ENDCG
    }
}
}