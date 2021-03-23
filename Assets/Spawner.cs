using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Spwnr;

    private void Awake()
    {
        Spwnr = this;
    }

    public float MaxTimeSpawn;
    public float MinTimeSpawn;

    EggBasketScript[] _eggBaskets;

    void Start()
    {
        _eggBaskets = FindObjectsOfType<EggBasketScript>();
        ActivateEggSpawns();
    }

    public void ActivateEggSpawns(int x = 0)
    {
        if (x == 0)
        {
            for (int i = 0; i < _eggBaskets.Length; i++)
            {
                if (!_eggBaskets[i].EggSpawning)
                {
                    float randTime = Random.Range(MinTimeSpawn, MaxTimeSpawn);

                    StartCoroutine(_eggBaskets[i].SpawnTime(randTime));
                }
            }
        }
        
    }


}
