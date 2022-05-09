using System;
using UnityEngine;

namespace RuntimeInjectedCode
{
    public class CamCheatScreen : MonoBehaviour, ICheatScreen
    {
        private bool _doFog = false;
        private bool _wobbleFov = false;

        public string GetName()
        {
            return "Cam";
        }

        public void DrawUI()
        {
            if (GUILayout.Button("Fog"))
            {
                _doFog = !_doFog;
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
                Camera.main.orthographic = !Camera.main.orthographic;
                Camera.main.orthographicSize = 5.0f;
            }
            
            if (GUILayout.Button("Wobble FOV"))
            {
                _wobbleFov = !_wobbleFov;
            }
        }

        public void Update()
        {
            if (_wobbleFov)
            {
                StanleyController.Instance.FieldOfViewAdditiveModifier = (float)Math.Sin(Time.time) * 10;
            }

            // Sometimes fog gets reset, so we repeatedly check and reenable it
            if (_doFog)
            {
                RenderSettings.fog = true;
                RenderSettings.fogDensity = 10.0f;
                RenderSettings.fogEndDistance = 10.0f;
                RenderSettings.fogStartDistance = 1.0f;
                RenderSettings.fogColor = Color.black;
                RenderSettings.fogMode = FogMode.Exponential;
            }
        }
    }
}