using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    private void OnCollisionEnter(Collision trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            var controller = trigger.transform.GetComponent<DogController>();
            controller.OnCollisionWithPoo();
            AudioManager.instance.PlaySound("squish1" ,10f);
            Destroy(gameObject);
        }
    }
}
