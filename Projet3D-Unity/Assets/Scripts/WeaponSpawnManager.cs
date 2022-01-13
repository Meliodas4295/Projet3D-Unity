using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject weaponPrefab;
    private Rigidbody weaponRigidBody;
    [SerializeField]
    private GameObject player;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameObject weaponInstantiate = Instantiate(weaponPrefab, transform.position, Quaternion.Euler(90, player.transform.rotation.eulerAngles.y, 0));
            weaponInstantiate.GetComponent<WeaponManager>().idPlayer = player.GetComponent<PlayerManager>().id;
            weaponRigidBody = weaponInstantiate.GetComponent<Rigidbody>();
            weaponRigidBody.AddForce(transform.forward * 50f, ForceMode.Impulse);
        }
    }
}
