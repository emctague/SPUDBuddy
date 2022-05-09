using Bundlelizer;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RuntimeInjectedCode
{
    /// <summary>
    /// Allows the manipulation of Configurable objects.
    /// </summary>
    public class ConfigurablesCheatScreen : MonoBehaviour, ICheatScreen
    {
        public string Name => "Conf";

        private Configurable[] _configurables;

        public void Start()
        {
            _configurables = Resources.FindObjectsOfTypeAll<Configurable>();
        }
        public void DrawUI()
        {
            if (GUILayout.Button("Refresh List"))
            {
                _configurables = Resources.FindObjectsOfTypeAll<Configurable>();
            }
            
            foreach (var c in _configurables)
            {
                if (c.GetConfigurableType() == ConfigurableTypes.Boolean)
                {
                    if (GUILayout.Button(c.Key + " = " + c.PrintValue()))
                    {
                        ((BooleanConfigurable)c).SetValue(!c.GetBooleanValue());
                    }
                }
                else if (c.GetConfigurableType() == ConfigurableTypes.Int)
                {
                    GUILayout.BeginHorizontal(GUIStyle.none);
                    GUILayout.Label(c.Key + " = " + c.PrintValue());
                    if (GUILayout.Button("+"))
                    {
                        ((IntConfigurable)c).SetValue(c.GetIntValue() + 1);
                    }
                    if (GUILayout.Button("-"))
                    {
                        ((IntConfigurable)c).SetValue(c.GetIntValue() - 1);
                    }
                    GUILayout.EndHorizontal();
                }
                else
                {
                    GUILayout.Label(c.Key + " = " + c.PrintValue());
                }
                
                
            }
            
        }
    }
}