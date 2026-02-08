Shader "Gemini/GlintShader"
{
    Properties
    {
        _MainTex ("Base Map", 2D) = "white" {}
        _BaseColor ("Base Color", Color) = (1,1,1,1)
        _GlintColor ("Glint Color", Color) = (1,1,1,1)
        _GlintSpeed ("Glint Speed", Range(0.1, 5.0)) = 1.0
        _GlintWidth ("Glint Width", Range(0.01, 0.5)) = 0.1
        _GlintIntensity ("Glint Intensity", Range(0, 10)) = 2.0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }
        LOD 100

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float3 normalOS : NORMAL;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _BaseColor;
                float4 _GlintColor;
                float _GlintSpeed;
                float _GlintWidth;
                float _GlintIntensity;
            CBUFFER_END

            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = TRANSFORM_TEX(input.uv, _MainTex);
                output.worldNormal = TransformObjectToWorldNormal(input.normalOS);
                return output;
            }

            float4 frag(Varyings input) : SV_Target
            {
                float4 baseMap = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv);
                float4 finalColor = baseMap * _BaseColor;

                // Glint Logic: Diagonal scrolling line
                // We use screen-space or world-space inspired coordinates for the glint
                float glintPos = frac(_Time.y * _GlintSpeed);
                float lineCoord = (input.uv.x + input.uv.y) * 0.5;
                
                float glintLine = smoothstep(glintPos - _GlintWidth, glintPos, lineCoord) - 
                                 smoothstep(glintPos, glintPos + _GlintWidth, lineCoord);
                
                glintLine = saturate(glintLine);

                // Add glint to final color
                finalColor.rgb += _GlintColor.rgb * glintLine * _GlintIntensity;

                return finalColor;
            }
            ENDHLSL
        }
    }
}
