using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScoresData
{
    public MaxHeap HighScores;
    public int LastScore;

    public HighScoresData()
    {
        HighScores = new MaxHeap(20);
    }
}
