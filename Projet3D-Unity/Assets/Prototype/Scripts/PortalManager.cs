using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    private GameObject portal;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitPlayerInstatiation());
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (player.GetComponent<PlayerManager>().GetHasKey())
            {
                portal.SetActive(true);
            }
            else
            {
                portal.SetActive(false);
            }
        }
    }

    IEnumerator WaitPlayerInstatiation()
    {
        yield return new WaitUntil(() => GameObject.Find("Player0") != null);
        if (gameObject.name == "PortalPlayer0")
        {
            player = GameObject.Find("Player0");
        }
        if (gameObject.name == "PortalPlayer1")
        {
            player = GameObject.Find("Player1");
        }
    }
}
