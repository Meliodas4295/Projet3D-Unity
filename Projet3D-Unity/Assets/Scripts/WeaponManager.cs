using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private Rigidbody weaponRigidBody;
    private GameObject player;
    public int idPlayer;
    // Start is called before the first frame update
    void Start()
    {
        weaponRigidBody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player" + idPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("ok");
        weaponRigidBody.velocity = Vector3.zero;
        StartCoroutine(DeleteCollider(collision));
    }

    IEnumerator DeleteCollider(Collision collision)
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<CapsuleCollider>().enabled = false;
        Vector3 directionOfShoot = (transform.position - player.transform.position).normalized;
        transform.position += directionOfShoot * 0.1f;
    }
}
