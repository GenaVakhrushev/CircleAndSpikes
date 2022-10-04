using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameStateManager : Singleton<GameStateManager>
{
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject LosePanel;

    public static UnityEvent OnGameEnded = new UnityEvent();

    public static void Win()
    {
        Instance.WinPanel.SetActive(true);

        OnGameEnded?.Invoke();
    }

    public static void Lose()
    {
        Instance.LosePanel.SetActive(true);

        Circle.Instance.Dead();

        OnGameEnded?.Invoke();
    }

    public static void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
