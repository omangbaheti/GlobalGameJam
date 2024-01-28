using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : Singleton<CanvasManager>
{
    public RectTransform player1Screen;
    public RectTransform player2Screen;
    public RectTransform timer;
    public RectTransform player1Percentage;
    public RectTransform player2Percentage;

    private Slider player1Slider;
    private Slider player2Slider;
    
    void Start()
    {
        player1Slider = player1Percentage.GetComponentInChildren<Slider>();
        player2Slider = player2Percentage.GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        player1Slider.value = TerritoryCalculator.instance.Player1Percent;
        player2Slider.value = TerritoryCalculator.instance.Player2Percent;
    }
}
