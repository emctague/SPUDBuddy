using System;
using UnityEngine;

namespace RuntimeInjectedCode
{
    public static class RuntimeInjector
    {
        /// <summary>
        /// This is loaded and run by a properly patched Unity engine DLL automatically.
        /// It will automatically load the InjectedComponent (a cheat screen manager MonoBehaviour)
        /// and keep it alive until the game quits.
        /// </summary>
        public static void CreateBootstrapper()
        {
            _load = new GameObject("InjectedCheatScreen");
            _load.AddComponent<InjectedComponent>();
            UnityEngine.Object.DontDestroyOnLoad(_load);
        }
        
        private static GameObject _load;
    }
}