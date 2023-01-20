// 和c一样 重复include 会造成代码重复 所以加个判断的宏
#ifndef CUSTOM_UNLIT_PASS_INCLUDED
#define CUSTOM_UNLIT_PASS_INCLUDED

#include "../ShaderLibrary/Common.hlsl"

// 不是所有平台都支持 常量缓冲区
// cbuffer UnityPerMaterial
// {
//     float4 _BaseColor;
// }

CBUFFER_START(UnityPerMaterial) 
    float4 _BaseColor;
CBUFFER_END


float4 UnlitPassVertex(float3 positionOS:POSITION):SV_POSITION
{
    // https://zhuanlan.zhihu.com/p/261097735
    // 模型空间 -> 世界空间
    float3 positionWS = TransformObjectToWorld(positionOS.xyz);
    //世界空间 -> 视图空间 -> 投影空间
    return TransformWorldToHClip(float4(positionWS, 1));
}

float4 UnlitPassFragment():SV_TARGET
{
    return _BaseColor;
}

#endif
