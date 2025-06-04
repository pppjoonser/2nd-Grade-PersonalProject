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
                float4 positionOS   : POSITION;  //��ġ
            };

            struct Varyings
            {
                float4 positionCS  : SV_POSITION;
            };


            Varyings vert(Attributes IN)
            {
                 Varyings OUT;

                OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
                // TransformObjectToHClip �Լ��� ���ؽ� �������� ������Ʈ ��������
                // ������ Ŭ�� �������� ��ȯ�մϴ�. (-1, -1) ~ (1 ~1)�� ����ü ������ǥ ��ȯ


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
