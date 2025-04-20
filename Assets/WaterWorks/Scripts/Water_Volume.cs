﻿using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Water_Volume : ScriptableRendererFeature
{
    class CustomRenderPass : ScriptableRenderPass
    {
        public RTHandle source;
        private Material _material;

        private RTHandle tempRenderTarget;
        private RTHandle tempRenderTarget2;

        public CustomRenderPass(Material mat)
        {
            _material = mat;
        }

        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            var descriptor = renderingData.cameraData.cameraTargetDescriptor;
            descriptor.depthBufferBits = 0;

            RenderingUtils.ReAllocateIfNeeded(ref tempRenderTarget, descriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, name: "_TemporaryColorTexture");
            RenderingUtils.ReAllocateIfNeeded(ref tempRenderTarget2, descriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, name: "_TemporaryDepthTexture");

            source = renderingData.cameraData.renderer.cameraColorTargetHandle;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (renderingData.cameraData.cameraType != CameraType.Reflection)
            {
                CommandBuffer cmd = CommandBufferPool.Get("Water Volume Pass");

                // Blit to temp, apply material, then blit back
                Blit(cmd, source, tempRenderTarget, _material);
                Blit(cmd, tempRenderTarget, source);

                context.ExecuteCommandBuffer(cmd);
                CommandBufferPool.Release(cmd);
            }
        }

        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            // Nothing to cleanup for now
        }
    }

    [System.Serializable]
    public class _Settings
    {
        public Material material = null;
        public RenderPassEvent renderPass = RenderPassEvent.AfterRenderingSkybox;
    }

    public _Settings settings = new _Settings();

    CustomRenderPass m_ScriptablePass;

    public override void Create()
    {
        if (settings.material == null)
        {
            settings.material = (Material)Resources.Load("Water_Volume");
        }

        m_ScriptablePass = new CustomRenderPass(settings.material);
        m_ScriptablePass.renderPassEvent = settings.renderPass;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(m_ScriptablePass);
    }
}