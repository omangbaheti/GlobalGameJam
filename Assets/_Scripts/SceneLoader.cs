using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameSceneOmang");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
