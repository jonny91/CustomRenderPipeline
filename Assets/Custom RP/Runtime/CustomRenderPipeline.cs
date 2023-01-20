/*************************************************************************************
 *
 * 文 件 名:   CustomRenderPipeline.cs
 * 描    述: 
 * 
 * 创 建 者：  洪金敏 
 * 创建时间：  2023-01-16 18:41:21
*************************************************************************************/

using UnityEngine;
using UnityEngine.Rendering;

public class CustomRenderPipeline : RenderPipeline
{
    private CameraRenderer renderer = new CameraRenderer();

    public CustomRenderPipeline()
    {
        GraphicsSettings.useScriptableRenderPipelineBatching = true;
    }
    
    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        //可以针对不同的Camera设置不同的渲染方式，实例中只是简化成了同一个。项目中这里可以扩展出更多Render类型
        foreach (var camera in cameras)
        {
            renderer.Render(context, camera);
        }
    }
}