using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnManager;
    [SerializeField]
    private GameObject uiMenu;
    [SerializeField]
    private GameObject uIPlayer;
    [SerializeField]
    private GameObject uiGameOver;
    [SerializeField]
    private GameObject uiOptions;
    [SerializeField]
    private GameObject uiWinner;
    [SerializeField]
    private GameObject uiEndGame;
    [SerializeField]
    private GameObject camera;
    [SerializeField]
    private GameObject directionalLight;
    [SerializeField]
    private GameObject timerBeginning;
    [SerializeField]
    private GameObject uiCredits;

    [SerializeField]
    private Material skyboxMaterial;


    private GameObject player0;
    private GameObject player1;

    private GameObject gameOverPlayer0;
    private GameObject gameOverPlayer1;

    private GameObject winnerPlayer0;
    private GameObject winnerPlayer1;

    private bool respawn = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InstatiatePlayers());
        spawnManager.SetActive(false);
        uiMenu.SetActive(true);
        uIPlayer.SetActive(false);
        camera.SetActive(true);
        gameOverPlayer0 = uiGameOver.transform.Find("GameOverPlayer0").gameObject;
        gameOverPlayer1 = uiGameOver.transform.Find("GameOverPlayer1").gameObject;
        winnerPlayer0 = uiWinner.transform.Find("WinnerPlayer0").gameObject;
        winnerPlayer1 = uiWinner.transform.Find("WinnerPlayer1").gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if (player0 != null && player1 != null)
        {
            if (player0.GetComponent<PlayerManager>().GetIsWinner())
            {
                RenderSettings.skybox = skyboxMaterial;
                directionalLight.SetActive(true);
                winnerPlayer0.SetActive(true);
                gameOverPlayer1.SetActive(true);
                uIPlayer.SetActive(false);
                uiEndGame.SetActive(true);
                player1.GetComponent<PlayerManager>().SetIsGameOver(true);

                if (respawn)
                {
                    player0.GetComponent<PlayerManager>().GetAnimator().SetBool("isWinning", true);
                    player1.GetComponent<PlayerManager>().GetAnimator().SetBool("isLosing", true);
                    RotationAroundPlayer(player0);
                    RotationAroundPlayer(player1);
                    respawn = false;
                }
                player0.transform.GetChild(5).gameObject.transform.Rotate(0, 0.3f, 0);
                player1.transform.GetChild(5).gameObject.transform.Rotate(0, 0.3f, 0);
            }
            if (player1.GetComponent<PlayerManager>().GetIsWinner())
            {
                RenderSettings.skybox = skyboxMaterial;
                directionalLight.SetActive(true);
                winnerPlayer1.SetActive(true);
                gameOverPlayer0.SetActive(true);
                uIPlayer.SetActive(false);
                uiEndGame.SetActive(true);
                player0.GetComponent<PlayerManager>().SetIsGameOver(true);
                if (respawn)
                {
                    player1.GetComponent<PlayerManager>().GetAnimator().SetBool("isWinning", true);
                    player0.GetComponent<PlayerManager>().GetAnimator().SetBool("isLosing", true);
                    RotationAroundPlayer(player0);
                    RotationAroundPlayer(player1);
                    respawn = false;
                }
                player0.transform.GetChild(5).gameObject.transform.Rotate(0, 0.3f, 0);
                player1.transform.GetChild(5).gameObject.transform.Rotate(0, 0.3f, 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    private void RotationAroundPlayer(GameObject player)
    {
        player.transform.position = spawnManager.GetComponent<SpawnManager>().GetPlayersFirstPosition()[player.GetComponent<PlayerManager>().GetId()];
        player.transform.GetChild(5).gameObject.transform.GetChild(0).gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z - 2);
        player.transform.GetChild(5).gameObject.transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(20, 0, 0);
    }

    public void PlayButton()
    {
        spawnManager.SetActive(true);
        uiMenu.SetActive(false);
        uIPlayer.SetActive(true);
        camera.SetActive(false);
        timerBeginning.SetActive(true);
        //RenderSettings.skybox = null;
        directionalLight.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void OptionsButton()
    {
        uiMenu.SetActive(false);
        uiOptions.SetActive(true);

    }
    public void QuitOptionsButton()
    {
        uiMenu.SetActive(true);
        uiOptions.SetActive(false);
    }

    public void CreditsButton()
    {
        uiMenu.SetActive(false);
        uiCredits.SetActive(true);
    }
    public void QuitCreditsButton()
    {
        uiMenu.SetActive(true);
        uiCredits.SetActive(false);
    }

    public void QuitEndGameButton()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    IEnumerator InstatiatePlayers()
    {
        yield return new WaitUntil(() => GameObject.Find("Player0") && GameObject.Find("Player1"));
        player0 = GameObject.Find("Player0");
        player1 = GameObject.Find("Player1");
        Debug.Log(player0.GetComponent<PlayerManager>().GetAnimator());

    }

    //IEnumerator timer()
    //{
    //    while (timerBeforeBeginningOfPlay > 0)
    //    {
    //        timerBeforeBeginningOfPlay--;
    //        yield return new WaitForSeconds(1f);
    //        Debug.Log(count);
    //        count.text = timerBeforeBeginningOfPlay.ToString();
    //    }
    //}

}
