using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class GameManager : Singleton<GameManager>
{
    public UnityEvent GameStarted;
    public UnityEvent GameOver;
    public float SecondsLeft => secondsLeft;
    
    
    [SerializeField] public int gameTime;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private TerritoryCalculator territoryCalculator;

    private float secondsLeft;
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        territoryCalculator = TerritoryCalculator.instance;
        secondsLeft = gameTime * 60;
        GameStarted?.Invoke();
    }

    
    void Update()
    {
        secondsLeft -= Time.deltaTime;
    }
}
