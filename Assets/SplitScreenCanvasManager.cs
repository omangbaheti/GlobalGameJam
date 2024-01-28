using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplitScreenCanvasManager : MonoBehaviour
{
    [SerializeField] private Slider staminaBar;
    [SerializeField] private HorizontalLayoutGroup foodBar;
    [SerializeField] private GameObject foodLogo;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStaminaBar(float ratio)
    {
        staminaBar.value = ratio;
    }

    public void SetFood(int foodNumber)
    {
        foreach (RectTransform foodItem in foodBar.transform)
        {
            Destroy(foodItem.gameObject);
        }

        for (int i = 0; i < foodNumber; i++)
        {
            Instantiate(foodLogo, foodBar.transform);
        }
    }
}
