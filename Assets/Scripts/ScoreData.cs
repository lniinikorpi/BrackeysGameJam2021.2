using System;

[Serializable]
public class ScoreData
{
    public int highScore;

    public ScoreData(int value)
    {
        highScore = value;
    }
}
