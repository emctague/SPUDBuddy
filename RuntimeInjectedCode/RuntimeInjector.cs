using System;
using UnityEngine;
using UnityPatcher;

namespace RuntimeInjectedCode
{
    public class RuntimeInjector
    {
        public static void CreateBootstrapper()
        {
            _load = new GameObject("NonDestructiveBootstrapper");
            _load.AddComponent<InjectedComponent>();
            UnityEngine.Object.DontDestroyOnLoad(_load);
        }
        
        private static GameObject _load = null;
    }
}