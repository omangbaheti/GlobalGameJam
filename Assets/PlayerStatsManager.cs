using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStatsManager : MonoBehaviour
{
    public float staminaRatio => Stamina / maxStamina;
    [HideInInspector] public float Stamina;
    [HideInInspector] public int Food;

    [SerializeField] private float maxStamina;
    [SerializeField] private int maxFood;
    [Range(0f,1f)]
    [SerializeField] private float rateOfStaminaLoss;

    private PlayerInput playerInput;
    private InputSystem inputSystem;
    private RectTransform playerSplitScreen;
    private SplitScreenCanvasManager playerUI;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerSplitScreen = playerInput.playerIndex switch
        {
            0 => CanvasManager.instance.player1Screen,
            1 => CanvasManager.instance.player2Screen,
            _ => playerSplitScreen
        };
        playerUI = playerSplitScreen.GetComponent<SplitScreenCanvasManager>();
        inputSystem = GetComponent<InputSystem>();
        Stamina = maxStamina;
        Food = maxFood;
    }

    private void Update()
    {
        playerUI.SetStaminaBar(staminaRatio);
        if (inputSystem.sprint)
        {
            Debug.Log("stamina drained  ");
            RemoveStamina();
        }
    }
    

    public void AddStamina(float add)
    {
        Stamina += add* maxStamina;
        Stamina = Mathf.Clamp(Stamina, 0, maxStamina);
    }

    public void RemoveStamina()
    {
        Stamina -= rateOfStaminaLoss * maxStamina * Time.deltaTime;
    }

    public void AddFood()
    {
        Food += 1;
        Food = Mathf.Clamp(Food, 0, maxFood);
        playerUI.SetFood(Food);
    }

    public void RemoveFood()
    {
        Food -= 1;
        Food = Mathf.Clamp(Food, 0, maxFood);  
        playerUI.SetFood(Food);
    }
}
