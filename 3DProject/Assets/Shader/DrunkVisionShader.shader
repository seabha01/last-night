/*
 * DrunkVisionShader.shader
 * 3D Project
 * Luba Grynyshin, Hannah Seabert, Caroline Henning, Thomas Mallick, David Ross
 * 25-Apr-2019
 * 
 * Custom Shader used to translate Mesh's to create double vision effect. 
 */

Shader "Custom/DrunkVisionShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Transparency ("Transparency", Range(-3,1)) = 0.0
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Distance("Distance", Range(-1,1)) = 0.0 //This distace from the original shape
        _ShakeAmount("Shake", Range(0,20)) = 10 //Higher the number, the less shake
        _Ang ("Angle", float) = 0
        
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent" }
        
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha
        #pragma vertex vert
            
        #include "UnityCG.cginc" //has many helpful functions
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };
        
        struct vertexInput
        {
            float4 position : POSITION;
        };
        
        struct vertexOutput
        {
            float4 position : SV_POSITION;
        };
       
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _Transparency;
        float _Distance;
        float _ShakeAmount;
        float _Ang;
        
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void vert (inout appdata_full v) {
              v.vertex.z += sin(_Ang) * _Distance;
              if (_Distance < 0)
              {
                v.vertex.z -= cos(_Time[1]) * _ShakeAmount;
              } else 
              {
                v.vertex.z += cos(_Time[1]) * _ShakeAmount;
              }
              v.vertex.x += cos(_Ang) * _Distance;
              if (_Distance < 0)
              {
                v.vertex.x -= cos(_Time[1]) * _ShakeAmount;
              } else 
              {
                v.vertex.x += cos(_Time[1]) * _ShakeAmount;
              }
              v.vertex.y += cos(_Time[1]) * _ShakeAmount;
              
        }
        
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = _Transparency;
        }
        ENDCG
    }
    FallBack "Diffuse"
}