using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushSpell : PullSpell
{
    // Start is called before the first frame update
    protected override IEnumerator PullEnemyToPoint(Transform Enemy)
    {
        Enemy.GetComponent<BaseEnemyController>().isStunned = true;
        Enemy.transform.rotation = Quaternion.LookRotation(Enemy.transform.position - transform.position);

        float time = Time.time;
        while (Time.time - time < 0.5)
        {
            Enemy.transform.position += Enemy.transform.forward * 0.05f;
            yield return null;
        }
        Enemy.GetComponent<BaseEnemyController>().isStunned = false;
        Enemy.GetComponent<BaseEnemyController>().TakeDamage(Damage,Type);
        Execute(Enemy.gameObject);
    }
}
