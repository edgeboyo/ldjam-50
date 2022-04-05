using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;
    
    public LevelGen levelgen;
    public int levelSize;
    public int level;
    
    void Awake()
    {
        level = 0;
        
        if(instance == null){
            instance = this;
        } else if (instance != this){
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        InitGame();
    }

    void InitGame()
    {
        levelgen.makeLevel(levelSize);    }

    public void nextLevel()
    {
        level++;
        levelgen.generate(levelSize);
    }
}