using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juice : MonoBehaviour
{
    public float amplitude = 0.5f;  // Set the amplitude of the up and down movement
    public float speed = 2.0f;      // Set the speed of the up and down movement

    private bool hasCollided = false;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // add stamina if collected
        if (hasCollided)
        {
            addStamina();
        }
        else
        {
            // Perform the up-and-down movement
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

    void addStamina()
    {
        // Instantiate the UI image at the Juice's position
        //GameObject uiImage = Instantiate(juiceUIPrefab, transform.position, Quaternion.identity);

        // Destroy the Juice object
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasCollided)
        {
            hasCollided = true;
            Debug.Log("drinking juice!");
        }
    }
}
