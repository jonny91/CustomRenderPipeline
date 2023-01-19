/*************************************************************************************
 *
 * 文 件 名:   CameraRenderer.cs
 * 描    述: 
 * 
 * 创 建 者：  洪金敏 
 * 创建时间：  2023-01-16 18:47:18
*************************************************************************************/

using UnityEngine;
using UnityEngine.Rendering;

public partial class CameraRenderer
{
    ScriptableRenderContext context;

    Camera camera;
    private const string bufferName = "Render Camera";
    static ShaderTagId unlitShaderTagId = new ShaderTagId("SRPDefaultUnlit");

    /// <summary>
    /// 被剔除的物体
    /// </summary>
    private CullingResults cullingResults;

    private CommandBuffer buffer = new CommandBuffer()
    {
        name = bufferName,
    };

    /// <summary>
    /// 最终渲染方式
    /// </summary>
    /// <param name="context"></param>
    /// <param name="camera"></param>
    public void Render(ScriptableRenderContext context, Camera camera)
    {
        this.context = context;
        this.camera = camera;

        PrepareBuffer();
        PrepareForSceneWindow();
        if (!Cull())
        {
            return;
        }

        Setup();
        DrawVisibleGeometry();
        DrawUnsupportedShaders();
        DrawGizmo();
        //需要显卡配合 绘制一次 一次draw call
        Submit();
    }

    private void Setup()
    {
        //读取摄像机上设置的属性矩阵 unity_MatrixVP  摄像机位置 角度 正交/透视参数 
        context.SetupCameraProperties(camera);
        //清理render target 。。屏幕 、 rt --> framebuffer
        var clearFlags = camera.clearFlags;
        buffer.ClearRenderTarget(
            clearFlags <= CameraClearFlags.Depth,
            clearFlags == CameraClearFlags.Color,
            clearFlags == CameraClearFlags.Color ? camera.backgroundColor.linear: Color.clear);
        // buffer.ClearRenderTarget(
        //     true,
        //     true,
        //     Color.clear);
        buffer.BeginSample(SampleName);
        ExecuteBuffer();
    }

    private void ExecuteBuffer()
    {
        //把buffer中的命令 放到context中 用于后续commit到GPU
        context.ExecuteCommandBuffer(buffer);
        buffer.Clear();
    }

    /// <summary>
    /// the commands that we issue to the context are buffered.
    /// We have to submit the queued work for execution
    /// </summary>
    private void Submit()
    {
        buffer.EndSample(SampleName);
        ExecuteBuffer();
        context.Submit();
    }

    private void DrawVisibleGeometry()
    {
        //确定是否应用正交排序或基于距离的排序
        var sortingSetting = new SortingSettings(camera);
        //指出允许使用哪种着色器通道
        var drawingSettings = new DrawingSettings(unlitShaderTagId, sortingSetting);
        //指出允许哪些渲染队列
        var filteringSettings = new FilteringSettings(RenderQueueRange.opaque);
        
        context.DrawRenderers(
            cullingResults, ref drawingSettings, ref filteringSettings
        );

        context.DrawSkybox(camera);

        sortingSetting.criteria = SortingCriteria.CommonTransparent;
        drawingSettings.sortingSettings = sortingSetting;
        filteringSettings.renderQueueRange = RenderQueueRange.transparent;
        
        context.DrawRenderers(
            cullingResults, ref drawingSettings, ref filteringSettings
        );
    }

    private bool Cull()
    {
        ScriptableCullingParameters p;
        if (camera.TryGetCullingParameters(out p))
        {
            //有内容可以显示
            cullingResults = context.Cull(ref p);
            return true;
        }

        return false;
    }
}