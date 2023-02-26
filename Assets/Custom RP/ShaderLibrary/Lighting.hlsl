#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

//这里可以不添加include 因为在litPass中统一include了
//这里加了只是为了ide代码提示 不添加比较好
#include "./Surface.hlsl"

float3 GetLighting(Surface surface)
{
    return surface.normal.y * surface.color;
}

#endif