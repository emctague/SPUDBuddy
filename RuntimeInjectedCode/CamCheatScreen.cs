using System;
using UnityEngine;

namespace RuntimeInjectedCode
{
    public class CamCheatScreen : MonoBehaviour, ICheatScreen
    {
        private bool _noFog;
        private bool _wobbleFov;
        public string Name => "Cam";

        public void DrawUI()
        {
            var cam = StanleyController.Instance.cam;
            
            UIFieldEditors.BooleanEditor("Occlusion Culling", cam.useOcclusionCulling, f => cam.useOcclusionCulling = f);
            UIFieldEditors.FloatEditor("Far Plane", cam.farClipPlane, f => cam.farClipPlane = f);
            UIFieldEditors.BooleanEditor("No Fog", _noFog, f => _noFog = f);
            UIFieldEditors.BooleanEditor("Invert Culling", GL.invertCulling, f => GL.invertCulling = f);
            UIFieldEditors.BooleanEditor("Wireframe", GL.wireframe, f => GL.wireframe = f);
            UIFieldEditors.BooleanEditor("Orthographic", cam.orthographic, f =>
            {
                cam.orthographic = f;
                cam.orthographicSize = 5.0f;
            });
            
            UIFieldEditors.BooleanEditor("Wobble FOV", _wobbleFov, f =>
            {
                _wobbleFov = f;
                if (!_wobbleFov)
                {
                    StanleyController.Instance.FieldOfViewAdditiveModifier = 0;
                }
            });
        }

        public void Update()
        {
            if (_wobbleFov)
            {
                StanleyController.Instance.FieldOfViewAdditiveModifier = (float)Math.Sin(Time.time) * 10;
            }

            // Sometimes fog gets reset, so we repeatedly check and re-disable it
            if (_noFog)
            {
                RenderSettings.fog = false;
            }
        }
    }
}