using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    private GameObject player0;
    private GameObject player1;
    [SerializeField]
    private GameObject portal0;
    [SerializeField]
    private GameObject portal1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitPlayerInstatiation());
    }

    // Update is called once per frame
    void Update()
    {
        if (player0 != null && player1 != null)
        {       
            if (player0.GetComponent<PlayerManager>().GetHasKey())
            {
                portal0.SetActive(true);  
            }
            else
            {
                portal0.SetActive(false);
            }
            if (player1.GetComponent<PlayerManager>().GetHasKey())
            {
                portal1.SetActive(true);

            }
            else
            {
                portal1.SetActive(false);
            }
        }
    }

    IEnumerator WaitPlayerInstatiation()
    {
        yield return new WaitUntil(() => GameObject.Find("Player0") != null);
        player0 = GameObject.Find("Player0");
        player1 = GameObject.Find("Player1");
    }
}
