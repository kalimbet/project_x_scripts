using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public float attackDamage = 20;

    public Vector3 attackOffSet;
    public float attackRange = 1f;
    public LayerMask attackMask;

    public void EnemyAttack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffSet.x;
        pos += transform.up * attackOffSet.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            //Debug.Log("Damage: " + attackDamage); // Hey Witek, I've moved this part over to KnightHealth.
                                                    // This is done because the damage reduction is calculated on player's end.
            colInfo.GetComponent<KnightHealth>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffSet.x;
        pos += transform.up * attackOffSet.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
