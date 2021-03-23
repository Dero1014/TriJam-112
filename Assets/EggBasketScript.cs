using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBasketScript : MonoBehaviour
{
    public int MaxEggsInBasket = 0;
    [HideInInspector]
    public int SpawnEggTime = 0;
    [HideInInspector]
    public bool EggSpawning = false;
    [HideInInspector]
    public int CurrentEggsInBasket = 0;

    public Transform[] EggsInBasket;


    

    private void Start()
    {
        EggsInBasket = GetComponentsInChildren<Transform>();


        for (int i = 1; i < EggsInBasket.Length; i++)
        {
            EggsInBasket[i].gameObject.SetActive(false);
        }
    }

    public IEnumerator SpawnTime(float time)
    {
        EggSpawning = true;

        yield return new WaitForSeconds(time);

        SpawnEgg();

        EggSpawning = false;
        Spawner.Spwnr.ActivateEggSpawns();
        
    }

    public void SpawnEgg()
    {
        CurrentEggsInBasket++;

        if (CurrentEggsInBasket <= MaxEggsInBasket)
        {
            //spawn egg in basket
            EggsInBasket[CurrentEggsInBasket].gameObject.SetActive(true);
        }
        else
        {
            //drop egg and lose points
            ScoreTracker.ScrTracker.UpdateScore(-1);
        }

    }
    
    public void TakeEggsFromBasket(ref int inv)
    {
        CurrentEggsInBasket--;
        EggsInBasket[CurrentEggsInBasket+1].gameObject.SetActive(false);
        inv++;
    }

}
