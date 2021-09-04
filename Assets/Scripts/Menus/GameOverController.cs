using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public GameObject gameOverScreen;
    private Animator animator;

    private void Awake()
    {
        animator = gameOverScreen.GetComponent<Animator>();
        GameEvents.instance.onCarStop += ShowGameOver;
    }

    private void ShowGameOver()
    {
        animator.SetTrigger("GameOver");
    }

    private void OnDestroy()
    {
        GameEvents.instance.onCarStop += ShowGameOver;
    }
}
