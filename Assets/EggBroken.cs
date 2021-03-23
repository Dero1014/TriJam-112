using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBroken : MonoBehaviour
{
    [HideInInspector]
    public bool CanBrake = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (CanBrake && collision.gameObject.tag != "Basket")
        {
            Destroy(gameObject); //change to split egg and particles
            ScoreTracker.ScrTracker.UpdateScore(-2);
        }        
    }
}
