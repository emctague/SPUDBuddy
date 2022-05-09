using UnityEngine;

namespace RuntimeInjectedCode
{
    public class EmptyCheatScreen : MonoBehaviour, ICheatScreen
    {
        public string GetName()
        {
            return "X";
        }

        public void DrawUI()
        {
            
        }
    }
}