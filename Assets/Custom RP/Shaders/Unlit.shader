Shader "Custom RP/Unlit"
{
    Properties {
        _BaseColor("Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        Pass
        {
            //内置管线使用 CGPROGRAM 
            //URP 使用 HLSLPROGRAM
            HLSLPROGRAM
            #pragma vertex UnlitPassVertex
			#pragma fragment UnlitPassFragment
            //把所有hlsl代码都放在这个文件中
            #include "UnlitPass.hlsl"
            ENDHLSL
        }
    }
}
