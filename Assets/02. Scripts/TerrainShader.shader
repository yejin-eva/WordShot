Shader "Custom/TerrainShader"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex1("Albedo (RGB)", 2D) = "white" {}
        _MainTex2("Albedo (RGB)", 2D) = "white" {}
        _MainTex3("Albedo (RGB)", 2D) = "white" {}
        _MainTex4("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 200

            CGPROGRAM
            // Physically based Standard lighting model, and enable shadows on all light types
            #pragma surface surf Standard fullforwardshadows

            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0

            sampler2D _MainTex1;
            sampler2D _MainTex2;
            sampler2D _MainTex3;
            sampler2D _MainTex4;

            struct Input
            {
                float2 uv_MainTex1;
                float2 uv_MainTex2;
                float2 uv_MainTex3;
                float2 uv_MainTex4;
                float4 color:COLOR;
            };

            half _Glossiness;
            half _Metallic;
            fixed4 _Color;

            // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
            // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
            // #pragma instancing_options assumeuniformscaling
            UNITY_INSTANCING_BUFFER_START(Props)
                // put more per-instance properties here
            UNITY_INSTANCING_BUFFER_END(Props)

            void surf(Input IN, inout SurfaceOutputStandard o)
            {
                // Albedo comes from a texture tinted by color
                fixed4 c1 = tex2D(_MainTex1, IN.uv_MainTex1) * _Color;
                fixed4 c2 = tex2D(_MainTex2, IN.uv_MainTex2) * _Color;
                fixed4 c3 = tex2D(_MainTex3, IN.uv_MainTex3) * _Color;
                fixed4 c4 = tex2D(_MainTex4, IN.uv_MainTex4) * _Color;
                // Mixing textures depending on color
                o.Albedo = lerp(c1.rgb, c2.rgb, IN.color.r);
                o.Albedo = lerp(o.Albedo, c3.rgb, IN.color.g);
                o.Albedo = lerp(o.Albedo, c4.rgb, IN.color.b);
                // Metallic and smoothness come from slider variables
                o.Metallic = _Metallic;
                o.Smoothness = _Glossiness;
                o.Alpha = c1.a;
            }
            ENDCG
        }
            FallBack "Diffuse"
}
