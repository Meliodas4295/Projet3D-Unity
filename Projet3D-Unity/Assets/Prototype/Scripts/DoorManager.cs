using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private GameObject player0;
    private GameObject player1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InstantiatePlayer());
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "PortailPrincipal0")
        {
            if (player0 != null)
            {
                if (player0.GetComponent<PlayerManager>().GetHasKey())
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    gameObject.transform.GetChild(1).gameObject.SetActive(false);
                }
                else
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                }
            }
        }
        if (gameObject.name == "PortailPrincipal1")
        {
            if (player1 != null)
            {
                if (player1.GetComponent<PlayerManager>().GetHasKey())
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    gameObject.transform.GetChild(1).gameObject.SetActive(false);
                }
                else
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                }
            }
        }
    }

    IEnumerator InstantiatePlayer()
    {
        yield return new WaitUntil(() => GameObject.Find("Player0") != null);
        player0 = GameObject.Find("Player0");
        player1 = GameObject.Find("Player1");
    }
}
