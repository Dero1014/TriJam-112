using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public static ScoreTracker ScrTracker;

    private void Awake()
    {
        ScrTracker = this;
    }

    public float Score;

    BasketScript[] _baskets;


    void Start()
    {
        _baskets = FindObjectsOfType<BasketScript>();
    }

    public void UpdateScore(int scr)
    {
        Score += scr;
        print("Your score " + Score);
    }
}
