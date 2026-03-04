using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private float delayTime;
    void Start()
    {
        Time.timeScale = 1;
    }

    public void OnClickGame()
    {
        StartCoroutine(StartGame());
    }

    public void OnClickEndlessGame()
    {
        StartCoroutine(StartEndlessGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene("GameScene");
    }

    IEnumerator StartEndlessGame()
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene("EndlessGameScene");
    }

}
