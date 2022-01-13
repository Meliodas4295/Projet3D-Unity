using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject keyPrefab;
    [SerializeField]
    private GameObject playerPrefab;
    private GameObject key;
    private List<GameObject> players = new List<GameObject>();
    private float numberOfPlayers = 2;
    private List<Vector3> playersFirstPosition = new List<Vector3>();
    private List<Quaternion> playersFirstRotation = new List<Quaternion>();
    public bool hasKeyPresent = false;
    // Start is called before the first frame update
    void Start()
    {
        InstantiateFirstRotationPlayers();
        InstatiateFirstPositionPlayers();
        InstantiateKey();
        InstantiatePlayer();
    }

    private void InstantiateFirstRotationPlayers()
    {
        playersFirstRotation.Add(Quaternion.Euler(0, 0, 0));
        playersFirstRotation.Add(Quaternion.Euler(0, 0, 0));
    }

    private void InstatiateFirstPositionPlayers()
    {
        playersFirstPosition.Add(new Vector3(0, 1, 0));
        playersFirstPosition.Add(new Vector3(10, 1, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasKeyPresent) {

            StartCoroutine(RespawnKey());
        }   
    }

    void InstantiateKey()
    {
        key = Instantiate(keyPrefab, keyPrefab.transform.position, keyPrefab.transform.rotation);
        key.name = "Key";
        hasKeyPresent = true;

    }
    void InstantiatePlayer()
    {
        for(int i = 0; i < numberOfPlayers; i++)
        {
            GameObject player = Instantiate(playerPrefab, playersFirstPosition[i], playersFirstRotation[i]);
            PlayerManager playerManager = player.GetComponent<PlayerManager>();
            playerManager.id = i;
            player.name = "Player" + i;
            SplitScreenCamera(player, playerManager);
        }
    }
    public void SplitScreenCamera(GameObject player, PlayerManager playerManager)
    {
        if (playerManager.id == 1)
        {
            player.GetComponentInChildren<Camera>().rect = new Rect(0, 0, 0.5f, 1);
        }
        else
        {
            player.GetComponentInChildren<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1);
        }
    }

    IEnumerator RespawnKey()
    {
        hasKeyPresent = true;
        yield return new WaitForSeconds(4f);
        key = Instantiate(keyPrefab, keyPrefab.transform.position, keyPrefab.transform.rotation);
    }
}
