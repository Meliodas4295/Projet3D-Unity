using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject weaponPrefab;
    private Rigidbody weaponRigidBody;
    private Image spell;
    [SerializeField]
    private GameObject player;
    private float timerBeforeNewWeapon = 1f;
    private Animator animator;

    public GameObject GetPlayer()
    {
        return player;
    }

    // Start is called before the first frame update
    private void Start()
    {
        spell = GetComponentInParent<PlayerManager>().GetSpell();
        animator = GetComponentInParent<PlayerManager>().GetAnimator();
    }
    // Update is called once per frame
    void Update()
    {
        if (!GetComponentInParent<PlayerManager>().GetIsTouching())
        {

            WeaponInstantiation();
        }
    }

    private void WeaponInstantiation()
    {
        if (!GetComponentInParent<PlayerManager>().GetIsWinner() && !GetComponentInParent<PlayerManager>().GetIsGameOver() && GetComponentInParent<PlayerManager>().GetCountDown())
        {
            timerBeforeNewWeapon += Time.deltaTime;
            if (timerBeforeNewWeapon > 1f)
            {
                spell.color = new Color(spell.color.r, spell.color.g, spell.color.b, 1f);
                if (Input.GetKeyDown(InputTouch()))
                {                   
                    StartCoroutine(Attack());
                    spell.color = new Color(spell.color.r, spell.color.g, spell.color.b, 0.25f);
                    GameObject weaponInstantiate = Instantiate(weaponPrefab, transform.position, Quaternion.Euler(90, player.transform.rotation.eulerAngles.y, 0));
                    weaponInstantiate.GetComponent<WeaponManager>().idPlayer = player.GetComponent<PlayerManager>().GetId();
                    weaponRigidBody = weaponInstantiate.GetComponent<Rigidbody>();
                    weaponRigidBody.AddForce(transform.forward * 1000f);
                    timerBeforeNewWeapon = 0;                    

                }
            }
        }

    }

    KeyCode InputTouch()
    {
        if(GetComponentInParent<PlayerManager>().GetId() == 0)
        {
            return KeyCode.Keypad1;
        }
        else
        {
            return KeyCode.A;
        }
    }
    private IEnumerator Attack()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("AttackLayer"), 1);
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(2);

        animator.SetLayerWeight(animator.GetLayerIndex("AttackLayer"), 0);
    }
}
