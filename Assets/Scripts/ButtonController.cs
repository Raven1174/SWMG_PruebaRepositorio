using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonController : MonoBehaviour
{

    /// <summary>
    /// Each Button loads its scene
    /// </summary>
    public void OnBtnPlay()
    {
        SceneManager.LoadScene("LVL1");
    }
    public void OnBtnCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void OnBtnBack()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void OnBtnExit()
    {
        Application.Quit();
    }
    public void OnBtnNextlvlv2()
    {
        SceneManager.UnloadSceneAsync("LVL1");
        SceneManager.LoadSceneAsync("LVL2", LoadSceneMode.Additive);
        GetComponent<GameManager>().goodEndPanel.SetActive(false);
        GetComponent<GameManager>().badEndPanel.SetActive(false);

    }
    public void OnBtnNextlvlv3()
    {
        SceneManager.UnloadSceneAsync("LVL2");

        SceneManager.LoadSceneAsync("LVL3", LoadSceneMode.Additive);
        GetComponent<GameManager>().goodEndPanel.SetActive(false);
        GetComponent<GameManager>().badEndPanel.SetActive(false);

    }
}
