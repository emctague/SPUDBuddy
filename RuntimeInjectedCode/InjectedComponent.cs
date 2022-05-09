using System.Collections.Generic;
using UnityEngine;

namespace RuntimeInjectedCode
{
    /// <summary>
    /// The InjectedComponent is responsible for drawing one or more CheatScreens in a menu across the screen.
    /// </summary>
    public class InjectedComponent : MonoBehaviour
    {
        private Vector2 _scrollPos;
        private int _screen;

        private readonly List<ICheatScreen> _cheatScreens = new List<ICheatScreen>();

        public void Start()
        {
            _cheatScreens.Add(gameObject.AddComponent<EmptyCheatScreen>());
            _cheatScreens.Add(gameObject.AddComponent<CamCheatScreen>());
            _cheatScreens.Add(gameObject.AddComponent<PlayerCheatScreen>());
            _cheatScreens.Add(gameObject.AddComponent<MapCheatScreen>());
            _cheatScreens.Add(gameObject.AddComponent<ConfigurablesCheatScreen>());
        }

        public void OnGUI()
        {
            GUILayout.BeginArea(new Rect((Screen.width / 3) * 2, 0, Screen.width / 3, Screen.height));
            // Screen Selection
            GUILayout.BeginHorizontal("box");
            for (int i = 0; i < _cheatScreens.Count; i++)
            {
                if (GUILayout.Button(_cheatScreens[i].Name))
                {
                    _screen = i;
                    _scrollPos = Vector2.zero;
                }
            }
            GUILayout.EndHorizontal();

            // Screen zero hides everything
            if (_screen != 0)
            {
                // Draw the current screen in a scroll view
                GUILayout.BeginVertical("box");
                GUILayout.Label(_cheatScreens[_screen].Name);

                _scrollPos = GUILayout.BeginScrollView(_scrollPos, false, false, GUILayout.ExpandHeight(true),
                    GUILayout.ExpandWidth(true));
                _cheatScreens[_screen].DrawUI();

                GUILayout.EndScrollView();
                GUILayout.EndVertical();
            }

            GUILayout.EndArea();
        }

    }
}