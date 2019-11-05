using System;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UnityEngine.Rendering.Universal.Internal
{

    /// <summary>
    /// This pass sets up XR rendering. Pair this pass with the EndXRRenderPass.  If this
    /// pass is issued without a matching EndXRRenderPass
    /// it will lead to undefined rendering results.
    /// </summary>
    internal class BeginXRRenderPass : ScriptableRenderPass
    {
       public BeginXRRenderPass(RenderPassEvent evt)
        {
            renderPassEvent = evt;
        }

        /// <inheritdoc/>
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            Camera camera = renderingData.cameraData.camera;
            context.SetupCameraProperties(camera, renderingData.cameraData.isStereoEnabled, eyeIndex);
            context.StartMultiEye(camera, eyeIndex);
            ++eyeIndex;
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            eyeIndex = 0;
        }
    }
}
