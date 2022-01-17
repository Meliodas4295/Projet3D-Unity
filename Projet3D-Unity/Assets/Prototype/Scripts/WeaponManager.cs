using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private Rigidbody weaponRigidBody;
    private GameObject player;
    public int idPlayer;
    private Vector3 directionOfShoot;
    private float speedRotation = 10f;
    private bool isTouching = false;

    // Start is called before the first frame update
    void Start()
    {
        weaponRigidBody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player" + idPlayer);
        StartCoroutine(TimeBeforeDestruction());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTouching)
        {
            transform.Rotate(new Vector3(0, 0, speedRotation));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        weaponRigidBody.velocity = Vector3.zero;
        isTouching = true;
        gameObject.transform.parent = collision.gameObject.transform;
        StartCoroutine(DeleteCollider(collision));
    }

    IEnumerator DeleteCollider(Collision collision)
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<MeshCollider>().enabled = false;
        directionOfShoot = (transform.position - player.transform.position).normalized;
        transform.position += directionOfShoot * 0.1f;
    }
    IEnumerator TimeBeforeDestruction()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}


//if (playerTouching != null)
//{
//    if (rotationInX != playerTouching.transform.rotation.eulerAngles.x || rotationInZ != playerTouching.transform.rotation.eulerAngles.z)
//    {
//        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x - playerTouching.transform.rotation.eulerAngles.x - playerTouching.transform.rotation.eulerAngles.z, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
//        rotationInX = playerTouching.transform.rotation.eulerAngles.x;
//        rotationInZ = playerTouching.transform.rotation.eulerAngles.z;
//    }
//}
//if (isTouching)
//{
//    transform.SetParent(playerTouching.transform.parent, false);
//transform.position += playerTouching.transform.position - lastPlayerPosition;
//lastPlayerPosition = playerTouching.transform.position;
//Debug.Log(transform.rotation.x);
//transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + playerTouching.transform.rotation.eulerAngles.x + playerTouching.transform.rotation.eulerAngles.z,transform.rotation.eulerAngles.y ,transform.rotation.eulerAngles.z);
//}

//isTouching = true;
//playerTouching = collision.gameObject;
//rotationInX = playerTouching.transform.rotation.eulerAngles.x;
//rotationInZ = playerTouching.transform.rotation.eulerAngles.z;
//lastPlayerPosition = playerTouching.transform.position;

//private bool isTouching = false;
//private Vector3 lastPlayerPosition;
//private GameObject playerTouching;
//private float rotationInX;
//private float rotationInZ;