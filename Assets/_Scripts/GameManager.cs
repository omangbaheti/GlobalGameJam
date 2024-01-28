using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
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
    private TextMeshProUGUI timer;
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        territoryCalculator = TerritoryCalculator.instance;
        secondsLeft = gameTime * 60;
        GameStarted?.Invoke();
        timer = CanvasManager.instance.timer.GetComponent<TextMeshProUGUI>();
        StartCoroutine(Timer());
    }

    
    void Update()
    {
        //secondsLeft -= Time.deltaTime;
    }

    IEnumerator Timer()
    {
        while (secondsLeft >= 0)
        {
            yield return new WaitForSeconds(1f);
            secondsLeft -= 1;
            timer.text = secondsLeft.ToString(Mathf.FloorToInt(secondsLeft / 60) + ":" + Mathf.FloorToInt(secondsLeft % 60));
        }
        SceneManager.LoadScene(2);

    }

}
