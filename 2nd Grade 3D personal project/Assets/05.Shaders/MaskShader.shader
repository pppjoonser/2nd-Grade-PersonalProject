Shader "Unlit/MaskShader"
{
    Properties
    {
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        ZWrite Off

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            CBUFFER_START(UnityPerMaterial)
            float4 _BaseMap_ST;
            float _Alpha;
            CBUFFER_END


            struct Attributes
            {
                float4 positionOS   : POSITION;  //위치
            };

            struct Varyings
            {
                float4 positionCS  : SV_POSITION;
            };


            Varyings vert(Attributes IN)
            {
                 Varyings OUT;

                OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
                // TransformObjectToHClip 함수는 버텍스 포지션을 오브젝트 공간에서
                // 균일한 클립 공간으로 변환합니다. (-1, -1) ~ (1 ~1)의 절두체 공간좌표 전환


                return OUT;

            }

            half4 frag (Varyings i) : SV_Target
            {                
                return (half4)0;
            }
            ENDHLSL

        }
       
    }
}
