using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager gameManager;

    //PowerUps in the game
    [HideInInspector]public int powerUpsCount;

    //Panels
    [Header("Panels")]
    public GameObject goodEndPanel; //win
    public GameObject badEndPanel;  //dies


    private void Awake()
    {
        //Singleton
        gameManager = this;

        //Disable panel on begining
        goodEndPanel.SetActive(false);
        badEndPanel.SetActive(false);


        //Restart TimeScale
        Time.timeScale = 1;

    }

    private IEnumerator Start()
    {
        //the start will start after 0.05f
        yield return new WaitForSeconds(0.05f);

        //Get all powerUps
        powerUpsCount = GameObject.FindGameObjectsWithTag("PowerUp").Length;
    }


    /// <summary>
    /// Lose method Panel
    /// </summary>
    public void LoseGame()
    {
        //Active the Panel
        badEndPanel.SetActive(true);
        badEndPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "YOU LOSE - POINTS: " + (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().powerUps * 100).ToString("0000");

        //Set time to 0
        Time.timeScale = 0;

    }

    /// <summary>
    /// Win Game Panel
    /// </summary>
    public void WinGame()
    {
        //Active the Panel
        goodEndPanel.SetActive(true);
        goodEndPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "YOU WIN - POINTS: " + (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().powerUps * 100).ToString("0000");

        //Set time to 0
        Time.timeScale = 0;

    }

    /// <summary>
    /// Change the Scene to the sceneName
    /// </summary>
    /// <param name="sceneName"></param>
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
