using UnityEngine;

namespace RuntimeInjectedCode
{
    public class UIFieldEditors
    {
        public delegate void Setter<T>(T newVal);
        
        public static void BooleanEditor(string label, bool inValue, Setter<bool> setter)
        {
            if (GUILayout.Button(label + " = " + inValue)) setter(!inValue);
        }

        public static void IntEditor(string label, int inValue, Setter<int> setter)
        {
            GUILayout.BeginHorizontal(GUIStyle.none);
            GUILayout.Label(label + " = " + inValue);
            if (GUILayout.Button("+")) setter(inValue + 1);
            if (GUILayout.Button("-")) setter(inValue - 1);
            GUILayout.EndHorizontal();
        }
        
        public static void FloatEditor(string label, float inValue, Setter<float> setter)
        {
            GUILayout.BeginHorizontal(GUIStyle.none);
            GUILayout.Label(label + " = " + inValue);
            if (GUILayout.Button("+")) setter(inValue + 1);
            if (GUILayout.Button("-")) setter(inValue - 1);
            GUILayout.EndHorizontal();
        }

        public static void StringEditor(string label, string inValue, Setter<string> setter)
        {
            GUILayout.BeginHorizontal(GUIStyle.none);
            GUILayout.Label(label);
            string newValue = GUILayout.TextField(inValue);
            if (newValue != inValue) setter(newValue);
            GUILayout.EndHorizontal();
        }
    }
}