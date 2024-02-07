using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticData {
    public static int Level { get; private set; } = 1;
    public static float Difficulty { get; private set; } = 1;
    public static bool SkipTutorial { get; private set; } = false;

    public static void SetPreGameData(int level, float difficulty, bool skipTutorial) {
        Level = level;
        Difficulty = difficulty;
        SkipTutorial = skipTutorial;
    }
}
