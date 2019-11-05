using System;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UnityEngine.Rendering.Universal.Internal
{
    /// <summary>
    /// End XR rendering
    ///
    /// This pass disables XR rendering. Pair this pass with the BeginXRRenderPass.
    /// If this pass is issued without a matching BeginXRRenderPass it will lead to
    /// undefined rendering results.
    /// </summary>
    internal class EndXRRenderPass : ScriptableRenderPass
    {
        public EndXRRenderPass(RenderPassEvent evt)
        {
            renderPassEvent = evt;
        }

        /// <inheritdoc/>
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            Camera camera = renderingData.cameraData.camera;
            context.StopMultiEye(camera);

            bool isLastPass = eyeIndex == renderingData.cameraData.numberOfXRPasses - 1;
            context.StereoEndRender(camera, eyeIndex, isLastPass);
            ++eyeIndex;
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            eyeIndex = 0;
        }
    }
}
