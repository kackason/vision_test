using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameController gc;
    [SerializeField] EndlessGameController egc;

    public void Restart()
    {
        if (gc != null)
        {
            StartCoroutine(RestartGame());
        }
        if (egc != null)
        {
            StartCoroutine(RestartEndlessGame());
        }
    }

    public void Pause()
    {
        if (gc != null)
        {
            StartCoroutine(PauseGame());
        }
        if (egc != null)
        {
            StartCoroutine(PauseEndlessGame());
        }
    }

    public void Quit()
    {
        if (gc != null)
        {
            StartCoroutine(QuitGame());
        }
        if (egc != null)
        {
            StartCoroutine(QuitEndlessGame());
        }
    }
    
    public void Resume()
    {
        if (gc != null)
        {
            StartCoroutine(ResumeGame());
        }
        if (egc != null)
        {
            StartCoroutine(ResumeEndlessGame());
        }
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(0.25f);
        gc.StartGame();
    }

    IEnumerator RestartEndlessGame()
    {
        yield return new WaitForSeconds(0.5f);
        egc.StartGame();
    }

    IEnumerator PauseGame()
    {
        yield return new WaitForSeconds(0.25f);
        gc.PauseGame();
    }

    IEnumerator PauseEndlessGame()
    {
        yield return new WaitForSeconds(0.25f);
        egc.PauseGame();
    }

    IEnumerator QuitGame()
    {
        yield return new WaitForSeconds(0.25f);
        gc.QuitGame();
    }

    IEnumerator QuitEndlessGame()
    {
        yield return new WaitForSeconds(0.25f);
        egc.QuitGame();
    }

    IEnumerator ResumeGame()
    {
        yield return new WaitForSeconds(0.25f);
        gc.ResumeGame();
    }

    IEnumerator ResumeEndlessGame()
    {
        yield return new WaitForSeconds(0.25f);
        egc.ResumeGame();
    }
}
