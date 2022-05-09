using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RuntimeInjectedCode
{
    public class SceneViewCheatScreen : MonoBehaviour, ICheatScreen
    {
        public string Name => "Scene";

        private Transform _inspecting;
        
        public void DrawUI()
        {
            if (_inspecting == null)
            {
                foreach (var rootGameObject in SceneManager.GetActiveScene().GetRootGameObjects())
                {
                    if (GUILayout.Button(rootGameObject.name))
                    {
                        _inspecting = rootGameObject.transform;
                    }
                }
            }
            else
            {
                if (GUILayout.Button("<--- Back (Inspecting: " + _inspecting.name + ")"))
                {
                    _inspecting = _inspecting.parent;
                }

                if (GUILayout.Button("! DELETE THIS OBJECT !"))
                {
                    var prevInsp = _inspecting;
                    _inspecting = _inspecting.parent;
                    Destroy(prevInsp.gameObject);
                }

                if (GUILayout.Button("Enabled: " + _inspecting.gameObject.activeSelf))
                {
                    _inspecting.gameObject.SetActive(!_inspecting.gameObject.activeSelf);
                }

                foreach (var c in _inspecting.GetComponents(typeof(Component)))
                {
                    GUILayout.Label("Component: " + c.GetType().Name);
                }

                foreach (Transform child in _inspecting)
                {
                    if (GUILayout.Button("Child: " + child.name))
                    {
                        _inspecting = child;
                    }
                }
            }
        }

    }
}