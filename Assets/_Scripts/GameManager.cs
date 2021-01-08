using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty
{
    Easy = 0,
    Mediem,
    Hard
}

public class GameManager : MonoBehaviour
{
    [SerializeField] int m_Errors = 0;
    [SerializeField] int m_Score;
    [SerializeField] Difficulty m_Difficulty;

    public static GameManager Instance;

    public delegate void Scoring();

    public event Scoring OnScorePoint;
    public event Scoring OnErrorPoint;

    public void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    public void Reset()
    {
        
    }

    public void ScorePoint()
    {
        m_Score++;
        OnScorePoint();
    }

    public void ErrorPoint()
    {
        m_Errors++;
        OnErrorPoint();
    }
}
