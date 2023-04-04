using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.SceneManagement;

namespace RuntimeInjectedCode
{
    public class SceneViewCheatScreen : MonoBehaviour, ICheatScreen
    {
        public string Name => "Scene";

        private Transform _inspecting;

        private Component _inspectingComponent;

        private bool _includeGlobals = false;

        public void DrawUI()
        {
            if (_inspectingComponent != null)
            {
                if (GUILayout.Button("<--- Back (Inspecting: " + _inspectingComponent.GetType().Name + " on " + _inspecting.name + ")"))
                {
                    _inspectingComponent = null;
                    return;
                }

                if (GUILayout.Button("! DELETE THIS COMPONENT !"))
                {
                    Destroy(_inspectingComponent);
                    _inspectingComponent = null;
                    return;
                }
                
                var getEnabledMethod = _inspectingComponent.GetType().GetMethod("get_enabled");
                var setEnabledMethod = _inspectingComponent.GetType().GetMethod("set_enabled");
                if (getEnabledMethod != null && setEnabledMethod != null)
                {
                    bool enabled = (bool) getEnabledMethod.Invoke(_inspectingComponent, new object[] { });
                    if (GUILayout.Button("Enabled: " + enabled))
                    {
                        setEnabledMethod.Invoke(_inspectingComponent, new object[] { !enabled });
                    }
                } else if (_inspectingComponent is MonoBehaviour)
                {
                    var icMB = (MonoBehaviour)_inspectingComponent;
                    if (GUILayout.Button("Enabled: " + icMB.enabled))
                    {
                        icMB.enabled = !icMB.enabled;
                    }
                }
                
                GUILayout.Space(10.0f);

                
                foreach (var mi in _inspectingComponent.GetType().GetMethods())
                {
                    if (mi.GetParameters().Length != 0 || mi.Name.StartsWith("get_")) continue;
                    if (GUILayout.Button("Call: " + mi.Name))
                    {
                        mi.Invoke(_inspectingComponent, new object[] { });
                    }
                }
                
            } else if (_inspecting != null)
            {
                if (GUILayout.Button("<--- Back (Inspecting: " + _inspecting.name + ")"))
                {
                    _inspecting = _inspecting.parent;
                    return;
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
                
                GUILayout.Space(10.0f);

                foreach (var c in _inspecting.GetComponents(typeof(Component)))
                {
                    if (GUILayout.Button("Component: " + c.GetType().Name))
                    {
                        _inspectingComponent = c;
                    }
                }
                
                GUILayout.Space(10.0f);

                foreach (Transform child in _inspecting)
                {
                    if (GUILayout.Button("Child: " + child.name))
                    {
                        _inspecting = child;
                    }
                }
            }
            else
            {
                if (GUILayout.Button("Include Globals: " + _includeGlobals)) _includeGlobals = !_includeGlobals;
                
                GUILayout.Space(10.0f);

                List<GameObject> rootGameObjects = new List<GameObject>();
                
                if (_includeGlobals) {
                    Transform[] allTransforms = Resources.FindObjectsOfTypeAll<Transform>();
                    for( int i = 0; i < allTransforms.Length; i++ )
                    {
                        Transform root = allTransforms[i].root;
                        if( root.hideFlags == HideFlags.None && !rootGameObjects.Contains( root.gameObject ) )
                        {
                            rootGameObjects.Add( root.gameObject );
                        }
                    }
                }
                else
                {
                    rootGameObjects.AddRange(SceneManager.GetActiveScene().GetRootGameObjects());
                }

                
                foreach (var rootGameObject in rootGameObjects)
                {
                    if (GUILayout.Button(rootGameObject.name))
                    {
                        _inspecting = rootGameObject.transform;
                    }
                }
            }
        }

    }
}