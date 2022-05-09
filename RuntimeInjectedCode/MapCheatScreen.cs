using UnityEngine;

namespace RuntimeInjectedCode
{
    public class MapCheatScreen : MonoBehaviour, ICheatScreen
    {
        private static readonly string[] MapNames = {
            "Settings_UD_MASTER",
            "apartment_ending_UD_MASTER",
            "babygame_UD_MASTER",
            "boss-return-1_UD_MASTER",
            "boss-return-2_UD_MASTER",
            "boss-return-3_UD_MASTER",
            "bucket_confusion_ending_UD_MASTER",
            "bucket_quiz_UD_MASTER",
            "buttonworld_UD_MASTER",
            "Firewatch_UD_MASTER",
            "freedom_UD_MASTER",
            "incorrect_UD_MASTER",
            "LoadingScene_UD_MASTER",
            "map1_UD_MASTER",
            "map2_UD_MASTER",
            "map_death_UD_MASTER",
            "map_one_UD_MASTER",
            "map_two_UD_MASTER",
            "map_UD_MASTER",
            "MemoryzonePartOne_UD_MASTER",
            "MemoryzonePartThree_UD_MASTER",
            "MemoryzonePartTwo_UD_MASTER",
            "Menu_UD_MASTER",
            "NewContentPartTwo_UD_MASTER",
            "NewContentPartOne_UD_MASTER",
            "playtest_UD_MASTER",
            "redstairs_UD_MASTER",
            "RocketLeague_UD_MASTER",
            "tape_UD_MASTER",
            "thefirstmap_UD_MASTER",
            "theonlymap_UD_MASTER",
            "zending_UD_MASTER",
            "eight_UD_MASTER",
        };
        
        public string GetName()
        {
            return "Map";
        }

        public void DrawUI()
        {
            foreach (var m in MapNames)
            {
                if (GUILayout.Button(m))
                {
                    Singleton<GameMaster>.Instance.ChangeLevel(m);
                }
            }
        }
    }
}