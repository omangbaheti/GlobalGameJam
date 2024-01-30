using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juice : MonoBehaviour
{
    public float amplitude = 0.5f;  // Set the amplitude of the up and down movement
    public float speed = 2.0f;
    [Range(0, 1f)] public float stamina = 0.5f;// Set the speed of the up and down movement
    private bool hasCollided = false;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // add stamina if collected
        if (!hasCollided)
        {
            MoveUpDown();
        }
    }

    void MoveUpDown()
    {
        // Calculate the new Y position based on a sine wave
        float newY = startPos.y + amplitude * Mathf.Sin(speed * Time.time);

        // Update the object's position
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasCollided)
        {
            hasCollided = true;
            var statsManager = other.GetComponent<PlayerStatsManager>();
            statsManager.AddStamina(stamina);
            Debug.Log("drinking juice!");
            AudioManager.instance.PlaySound("powerup1", 15f);
            AudioManager.instance.PlaySound("dog_drink", 15f);
            Destroy(gameObject);
        }
    }
}
