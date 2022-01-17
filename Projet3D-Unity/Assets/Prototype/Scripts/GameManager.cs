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
    // Start is called before the first frame update
    void Start()
    {
        spawnManager.SetActive(false);
        uiMenu.SetActive(true);
        uIPlayer.SetActive(false);
        uiGameOver.SetActive(false);
        camera.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void PlayButton()
    {
        spawnManager.SetActive(true);
        uiMenu.SetActive(false);
        uIPlayer.SetActive(true);
        camera.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

}
