using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlitchImageEffect : ScriptableRendererFeature
{
    public enum GlitchType
    {
        Type1 = 0,
        Type2 = 1,
        Type3 = 2,
        Type4 = 3
    }

    [System.Serializable]
    public class GlitchImageEffectSettings
    {
        public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingTransparents;

        public GlitchType type = GlitchType.Type1;

        [Range(0, 1)]
        public float blend = 1;

        [Header("Parameters of Type1")]
        [Range(0, 10)]
        public float frequency = 1;

        [Range(0, 500)]
        public float interference = 130;

        [Range(0, 5)]
        public float noise = 0.15f;

        [Range(0, 20)]
        public float scanLine = 1;

        [Range(0, 1)]
        public float colored = 0.25f;

        [Header("Parameters of Type3")]
        [Range(0, 30)]
        public float intensityType3 = 10;

        [Header("Parameters of Type4")]
        [Range(100, 500)]
        public float lines = 240;

        [Range(1, 6)]
        public float scanSpeed = 2;

        [Range(0.1f, 0.9f)]
        public float linesThreshold = 0.7f;

        [Range(0, 0.8f)]
        public float exposure = 0.3f;

        public Texture2D noiseTex = null;

        public Material material = null;
    }

    public GlitchImageEffectSettings settings = new GlitchImageEffectSettings();

    class CustomRenderPass : ScriptableRenderPass
    {
        public GlitchImageEffectSettings settings;
        
        string profilerTag;

        private int sourceRTId_copy;
        private RenderTargetIdentifier sourceRT_copy;

        private RenderTargetIdentifier source { get; set; }

        public void Setup(RenderTargetIdentifier source) {
            this.source = source;
        }

        public CustomRenderPass(string profilerTag)
        {
            this.profilerTag = profilerTag;
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            var width = cameraTextureDescriptor.width;
            var height = cameraTextureDescriptor.height;

            sourceRTId_copy = Shader.PropertyToID("_SourceRT");
            cmd.GetTemporaryRT(sourceRTId_copy, width, height, 0, FilterMode.Bilinear, cameraTextureDescriptor.colorFormat);
            sourceRT_copy = new RenderTargetIdentifier(sourceRTId_copy);
            ConfigureTarget(sourceRT_copy);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get(profilerTag);

            RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
            opaqueDesc.depthBufferBits = 0;

            cmd.Blit(source, sourceRT_copy);

            cmd.SetGlobalFloat("_Blend", settings.blend);
            cmd.SetGlobalFloat("_Frequency", settings.frequency);
            cmd.SetGlobalFloat("_Interference", settings.interference);
            cmd.SetGlobalFloat("_Noise", settings.noise);
            cmd.SetGlobalFloat("_ScanLine", settings.scanLine);
            cmd.SetGlobalFloat("_Colored", settings.colored);
            cmd.SetGlobalTexture("_NoiseTex", settings.noiseTex);
            cmd.SetGlobalFloat("_IntensityType3", settings.intensityType3);
            cmd.SetGlobalFloat("_Lines", settings.lines);
            cmd.SetGlobalFloat("_ScanSpeed", settings.scanSpeed);
            cmd.SetGlobalFloat("_LinesThreshold", settings.linesThreshold);
            cmd.SetGlobalFloat("_Exposure", settings.exposure);
            cmd.Blit(sourceRT_copy, source, settings.material, (int)settings.type);

            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();

            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
        }
    }

    CustomRenderPass scriptablePass;

    public override void Create()
    {
        scriptablePass = new CustomRenderPass("GlitchImageEffect");
        scriptablePass.settings = settings;
        scriptablePass.renderPassEvent = settings.renderPassEvent;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        var src = renderer.cameraColorTarget;
        scriptablePass.Setup(src);
        renderer.EnqueuePass(scriptablePass);
    }
}


