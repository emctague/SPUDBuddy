using UnityEngine;

namespace RuntimeInjectedCode
{
    public class EmptyCheatScreen : MonoBehaviour, ICheatScreen
    {
        public string Name => "X";
        public void DrawUI()
        {
            
        }
    }
}