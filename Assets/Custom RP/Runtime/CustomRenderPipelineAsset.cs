/*************************************************************************************
 *
 * 文 件 名:   CustomRenderPipelineAsset.cs
 * 描    述: 
 * 
 * 创 建 者：  洪金敏 
 * 创建时间：  2023-01-16 18:30:49
*************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//通过Rendering/Custom Render Pipeline菜单创建 Asset文件
[CreateAssetMenu(menuName = "Rendering/Custom Render Pipeline")]
public class CustomRenderPipelineAsset : RenderPipelineAsset
{
    protected override RenderPipeline CreatePipeline()
    {
        //实际渲染对象是 CustomRenderPipeline的实例
        //Asset是对 实际渲染函数的包装
        return new CustomRenderPipeline();
    }
}