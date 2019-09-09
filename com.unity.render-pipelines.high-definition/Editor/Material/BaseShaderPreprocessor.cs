using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace UnityEditor.Rendering.HighDefinition
{
    public abstract class BaseShaderPreprocessor
    {
        // Common keyword list
        protected ShaderKeyword m_Transparent;
        protected ShaderKeyword m_AlphaTestOn;
        protected ShaderKeyword m_DebugDisplay;
        protected ShaderKeyword m_TileLighting;
        protected ShaderKeyword m_ClusterLighting;
        protected ShaderKeyword m_LodFadeCrossFade;
        protected ShaderKeyword m_DecalsOFF;
        protected ShaderKeyword m_Decals3RT;
        protected ShaderKeyword m_Decals4RT;
        protected ShaderKeyword m_LightLayers;
        protected ShaderKeyword m_ShadowLow;
        protected ShaderKeyword m_ShadowMedium;
        protected ShaderKeyword m_ShadowHigh;
        protected ShaderKeyword m_WriteNormalBuffer;
        protected ShaderKeyword m_WriteMSAADepth;
        protected ShaderKeyword m_SubsurfaceScattering;

        protected Dictionary<HDShadowFilteringQuality, ShaderKeyword> m_ShadowVariants;

        public virtual int Priority => 0;

        public BaseShaderPreprocessor()
        {
            // NOTE: All these keyword should be automatically stripped so there's no need to handle them ourselves.
            // LIGHTMAP_ON, DIRLIGHTMAP_COMBINED, DYNAMICLIGHTMAP_ON, LIGHTMAP_SHADOW_MIXING, SHADOWS_SHADOWMASK
            // FOG_LINEAR, FOG_EXP, FOG_EXP2
            // STEREO_INSTANCING_ON, STEREO_MULTIVIEW_ON, STEREO_CUBEMAP_RENDER_ON, UNITY_SINGLE_PASS_STEREO
            // INSTANCING_ON
            m_Transparent = new ShaderKeyword("_SURFACE_TYPE_TRANSPARENT");
            m_AlphaTestOn = new ShaderKeyword("_ALPHATEST_ON");
            m_DebugDisplay = new ShaderKeyword("DEBUG_DISPLAY");
            m_TileLighting = new ShaderKeyword("USE_FPTL_LIGHTLIST");
            m_ClusterLighting = new ShaderKeyword("USE_CLUSTERED_LIGHTLIST");
            m_LodFadeCrossFade = new ShaderKeyword("LOD_FADE_CROSSFADE");
            m_DecalsOFF = new ShaderKeyword("DECALS_OFF");
            m_Decals3RT = new ShaderKeyword("DECALS_3RT");
            m_Decals4RT = new ShaderKeyword("DECALS_4RT");
            m_LightLayers = new ShaderKeyword("LIGHT_LAYERS");
            m_ShadowLow = new ShaderKeyword("SHADOW_LOW");
            m_ShadowMedium = new ShaderKeyword("SHADOW_MEDIUM");
            m_ShadowHigh = new ShaderKeyword("SHADOW_HIGH");
            m_WriteNormalBuffer = new ShaderKeyword("WRITE_NORMAL_BUFFER");
            m_WriteMSAADepth = new ShaderKeyword("WRITE_MSAA_DEPTH");
            m_SubsurfaceScattering = new ShaderKeyword("OUTPUT_SPLIT_LIGHTING");

            m_ShadowVariants = new Dictionary<HDShadowFilteringQuality, ShaderKeyword>
            {
                {HDShadowFilteringQuality.Low, m_ShadowLow},
                {HDShadowFilteringQuality.Medium, m_ShadowMedium},
                {HDShadowFilteringQuality.High, m_ShadowHigh},
            };
        }

		// Prepare any per-shader data needed for variant stripping. For example pass name checks etc.
        public abstract void PrepareShaderStripping(Shader shader, ShaderSnippetData snippet);

        // Check if this shader snippet (specific pass/program type/shader) should be stripped as a whole.
        public abstract bool ShouldStripShader(HDRenderPipelineAsset hdrpAsset, Shader shader, ShaderSnippetData snippet);

        // Check if this specific variant should be stripped.
        public abstract bool ShouldStripVariant(HDRenderPipelineAsset hdrpAsset, ShaderCompilerData inputData);
    }
}
