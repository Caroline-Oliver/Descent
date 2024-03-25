using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/GameState")]
public class GameState : ScriptableObject
{
    int currentLevel = 0;

    public int GetLevel() {
        return currentLevel;
    }

    public void NextLevel() {
        currentLevel++;
    }
}
