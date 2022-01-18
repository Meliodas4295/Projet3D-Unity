using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private GameObject camera;
    [SerializeField]
    private GameObject directionalLight;


    private GameObject player0;
    private GameObject player1;

    private bool Reposition = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InstatiatePlayers());
        spawnManager.SetActive(false);
        uiMenu.SetActive(true);
        uIPlayer.SetActive(false);
        uiGameOver.SetActive(false);
        camera.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if(player0 != null && player1 != null)
        {
            if (player0.GetComponent<PlayerManager>().GetIsWinner())
            {
                player1.GetComponent<PlayerManager>().SetIsGameOver(true);
                if (Reposition)
                {
                    player0.transform.position = new Vector3(-6, 0f, 14f);
                    player0.transform.GetChild(5).gameObject.transform.GetChild(0).gameObject.transform.position = new Vector3(player0.transform.position.x, player0.transform.position.y + 2, player0.transform.position.z - 2);
                    player0.transform.GetChild(5).gameObject.transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(20, 0, 0);
                    Reposition = false;
                }
                player0.transform.GetChild(5).gameObject.transform.Rotate(0, 0.3f, 0);
            }
            if (player1.GetComponent<PlayerManager>().GetIsWinner())
            {
                player0.GetComponent<PlayerManager>().SetIsGameOver(true);
            }
        }
    }

    public void PlayButton()
    {
        spawnManager.SetActive(true);
        uiMenu.SetActive(false);
        uIPlayer.SetActive(true);
        camera.SetActive(false);
        //RenderSettings.skybox = null;
        //directionalLight.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    IEnumerator InstatiatePlayers()
    {
        yield return new WaitUntil(() => GameObject.Find("Player0") && GameObject.Find("Player1"));
        player0 = GameObject.Find("Player0");
        player1 = GameObject.Find("Player1");
    }

}
