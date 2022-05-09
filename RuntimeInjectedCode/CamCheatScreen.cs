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
            if (GUILayout.Button("No Fog"))
            {
                _noFog = !_noFog;
            }

            if (GUILayout.Button("Invert Cull"))
            {
                GL.invertCulling = !GL.invertCulling;
            }

            if (GUILayout.Button("Wireframe"))
            {
                GL.wireframe = !GL.wireframe;
            }

            if (GUILayout.Button("Orthographic"))
            {
                var cam = StanleyController.Instance.cam;
                cam.orthographic = !cam.orthographic;
                cam.orthographicSize = 5.0f;
            }
            
            if (GUILayout.Button("Wobble FOV"))
            {
                _wobbleFov = !_wobbleFov;
                if (!_wobbleFov)
                {
                    StanleyController.Instance.FieldOfViewAdditiveModifier = 0;
                }
            }
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