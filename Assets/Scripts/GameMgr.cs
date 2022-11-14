using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    public void SetProcessSpeed(float value)
    {
        Time.timeScale = value;
    }

    public void RestartFactor()
    {
        SceneManager.LoadScene("Game");
    }
}
