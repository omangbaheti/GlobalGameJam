using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class food : MonoBehaviour
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
        // Check if the UI image should appear
        if (hasCollided)
        {
            ShowUIImage();
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

    void ShowUIImage()
    {
        // Find the Canvas in the scene
        Canvas canvas = FindObjectOfType<Canvas>();

        // Check the number of existing food UI images on the canvas
        int existingFoodCount = canvas.transform.childCount;

        // Spawn UI image only if the max food number is not reached
        if (existingFoodCount < 4)
        {
            // Instantiate the UI image on the Canvas
            GameObject uiImage = Instantiate(foodUIPrefab, canvas.transform);

            // Set the position of the UI image based on the number of existing food UI images
            float distanceBetweenFood = 100f; // Adjust the distance as needed
            RectTransform uiImageRect = uiImage.GetComponent<RectTransform>();
            uiImageRect.anchoredPosition = new Vector2(existingFoodCount * distanceBetweenFood, 0f);
        }
        // Destroy the Juice object
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasCollided)
        {
            hasCollided = true;
            Debug.Log("eating food!");
        }
    }
}
