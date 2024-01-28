using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float amplitude = 0.5f;  // Set the amplitude of the up and down movement
    public float speed = 2.0f;      // Set the speed of the up and down movement

    public GameObject foodUIPrefab;  // Reference to the UI image prefab
    private bool hasCollided = false;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if(!hasCollided)
            MoveUpDown();
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
            var statsManager = other.GetComponent<PlayerStatsManager>();
            statsManager.AddFood();
            Debug.Log("eating food!");
            AudioManager.instance.PlaySound("dog_eat", 10f);
            Destroy(gameObject);
        }
    }
}
