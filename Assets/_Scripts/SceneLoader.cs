using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGameScene()
    {
        AudioManager.instance.PlaySoundWait("menu-select-orback", 15f, 0.1f);
        SceneManager.LoadScene("GameSceneOmang_duplicate");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
        AudioManager.instance.PlaySoundImmediate("menu-startgame", 15f);
    }
}
