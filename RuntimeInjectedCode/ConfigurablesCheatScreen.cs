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
                switch (c.GetConfigurableType())
                {
                    case ConfigurableTypes.Int:
                        UIFieldEditors.IntEditor(c.Key, c.GetIntValue(), ((IntConfigurable)c).SetValue);
                        break;
                    case ConfigurableTypes.Float:
                        UIFieldEditors.FloatEditor(c.Key, c.GetFloatValue(), ((FloatConfigurable)c).SetValue);
                        break;
                    case ConfigurableTypes.Boolean:
                        UIFieldEditors.BooleanEditor(c.Key, c.GetBooleanValue(), ((BooleanConfigurable)c).SetValue);
                        break;
                    case ConfigurableTypes.String:
                        UIFieldEditors.StringEditor(c.Key, c.GetStringValue(), ((StringConfigurable)c).SetValue);
                        break;
                    default:
                        GUILayout.Label(c.Key + " = " + c.PrintValue());
                        break;
                }
            }
            
        }
    }
}