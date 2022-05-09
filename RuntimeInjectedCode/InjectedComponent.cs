using System;
using System.Collections.Generic;
using RuntimeInjectedCode;
using UnityEngine;

namespace UnityPatcher
{
    public class InjectedComponent : MonoBehaviour
    {
        private Vector2 _scrollPos;
        private int _screen;

        private List<ICheatScreen> _cheatScreens = new List<ICheatScreen>();

        public void Start()
        {
            _cheatScreens.Add(gameObject.AddComponent<EmptyCheatScreen>());
            _cheatScreens.Add(gameObject.AddComponent<CamCheatScreen>());
            _cheatScreens.Add(gameObject.AddComponent<PlayerCheatScreen>());
            _cheatScreens.Add(gameObject.AddComponent<MapCheatScreen>());
        }

        public void OnGUI()
        {
            GUILayout.BeginHorizontal("box");
            for (int i = 0; i < _cheatScreens.Count; i++)
                if (GUILayout.Button(_cheatScreens[i].GetName())) _screen = i;
            GUILayout.EndHorizontal();

            if (_screen == 0) return;

            GUILayout.BeginVertical("box");
            GUILayout.Label(_cheatScreens[_screen].GetName());

            _scrollPos = GUILayout.BeginScrollView(_scrollPos, false, true, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
            _cheatScreens[_screen].DrawUI();
            
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
        }

    }
}