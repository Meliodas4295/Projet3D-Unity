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
    private List<Vector3> keyPositions = new List<Vector3>();
    // Start is called before the first frame update

    public List<Vector3> GetPlayersFirstPosition()
    {
        return playersFirstPosition;
    }
    void Start()
    {
        InstantiatePositionKey();
        InstantiateFirstRotationPlayers();
        InstatiateFirstPositionPlayers();
        InstantiateKey();
        InstantiatePlayer();
    }

    private void InstantiatePositionKey()
    {
        keyPositions.Add(new Vector3(24, 1, 26));
        keyPositions.Add(new Vector3(-12, 3, -25));
        keyPositions.Add(new Vector3(-8, 1, -6));
    }

    private void InstantiateFirstRotationPlayers()
    {
        playersFirstRotation.Add(Quaternion.Euler(0, 0, 0));
        playersFirstRotation.Add(Quaternion.Euler(0, 0, 0));
    }

    private void InstatiateFirstPositionPlayers()
    {
        playersFirstPosition.Add(new Vector3(-6, 0f, 14f));
        playersFirstPosition.Add(new Vector3(29, 0f, 8f));
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
        int rand = Random.Range(0, 3);
        key = Instantiate(keyPrefab, keyPositions[rand], keyPrefab.transform.rotation);
        key.name = "Key";
        hasKeyPresent = true;

    }
    void InstantiatePlayer()
    {
        for(int i = 0; i < numberOfPlayers; i++)
        {
            GameObject player = Instantiate(playerPrefab, playersFirstPosition[i], playersFirstRotation[i]);
            PlayerManager playerManager = player.GetComponent<PlayerManager>();
            playerManager.SetId(i);
            player.name = "Player" + i;
            SplitScreenCamera(player, playerManager);
        }
    }
    public void SplitScreenCamera(GameObject player, PlayerManager playerManager)
    {
        if (playerManager.GetId() == 0)
        {
            player.GetComponentInChildren<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1);
        }
        else
        {
            player.GetComponentInChildren<Camera>().rect = new Rect(0, 0, 0.5f, 1);
        }
    }

    IEnumerator RespawnKey()
    {
        int rand = Random.Range(0, 3);
        hasKeyPresent = true;
        yield return new WaitForSeconds(4f);
        key = Instantiate(keyPrefab, keyPositions[rand], keyPrefab.transform.rotation);
    }
}
