using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketScript : MonoBehaviour
{
    public int EggsInBasket = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Egg")
        {
            EggsInBasket++;
            Destroy(collision.gameObject);
        }
    }
}
