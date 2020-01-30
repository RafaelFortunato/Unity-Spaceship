using UnityEngine;

public class SaveGame
{
    private static readonly string HIGHSCORE = "highscore";
    public static void SetHighscore(int newHighscore)
    {
        PlayerPrefs.SetInt(HIGHSCORE, newHighscore);
    }

    public static int GetHighscore()
    {
        return PlayerPrefs.GetInt(HIGHSCORE, 0);
    }
}
