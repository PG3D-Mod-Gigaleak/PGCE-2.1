Shader "Custom/Rainbow-Animated" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Emission ("Emission (Lightmapper)", Range(0, 1)) = 1.0
        _Speed ("Speed", Range(0, 10)) = 1.0
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        fixed _Emission;
        float _Speed;

        fixed3 hsv2rgb (fixed3 c)
        {
            fixed4 K = fixed4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
            fixed3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
            return c.z * lerp(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
        }

        struct Input {
            float2 uv_MainTex;
            float2 uv_Illum;
        };

        void surf (Input IN, inout SurfaceOutput o) {
            fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
            float t = _Time.y * _Speed;
            fixed3 hsv = fixed3(t, 1.0, 1.0); // Rainbow effect in HSV color space
            fixed3 rgb = hsv2rgb(hsv);
            fixed4 c = fixed4(tex.rgb, tex.a);
            o.Albedo = c.rgb;
            o.Emission = tex.a * rgb;
    #if defined (UNITY_PASS_META)
            o.Emission *= _Emission.rrr;
    #endif
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}