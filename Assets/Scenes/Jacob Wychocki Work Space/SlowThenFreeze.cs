using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowThenFreeze : BaseSpell
{
    List<GameObject> Affected = new List<GameObject>();
    Dictionary<GameObject, int> Aff = new Dictionary<GameObject, int>();
    public float SlowsBeforeStun;
    public float Interval = .25f;
    public GameObject Stun;
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!Aff.ContainsKey(other.gameObject) && !Affected.Contains(other.gameObject))
            {
                Affected.Add(other.gameObject);
                Aff.Add(other.gameObject, 0);
                Execute(other.gameObject);
            }
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            RemoveSlowStacks(other.gameObject);
            Affected.Remove(other.gameObject);
            Aff.Remove(other.gameObject);
            
        }
    }

    float time;
    protected override void Update()
    {
        //Debug.Log(Affected.Count + " " + Aff.Count);
        if (Time.time - time > Interval)
        {
            Duration -= Interval;
            time = Time.time;

            List<GameObject> keys = new List<GameObject>(Aff.Keys);
           
            foreach (GameObject item in keys)
            {
                BaseEnemyController enem = item.GetComponent<BaseEnemyController>();
                if (Aff[item] >= SlowsBeforeStun)
                    Instantiate(Stun, transform.position, transform.rotation);
                if (enem.AddSlowStack() && Aff[item] != -1)
                {
                    Aff[item] += 1;
                    DealDamageToEnemy(enem);
                    
                }
                
            }
            
        }
        if (Duration <= 0)
        {
            Destroy(gameObject);
        }
    }
    protected void RemoveSlowStacks(GameObject enemy)
    {
        
        BaseEnemyController enem = enemy.GetComponent<BaseEnemyController>();
        if (Aff.ContainsKey(enemy))
        {
            for (int i = 0; i < Aff[enemy]; i++)
            {
                enem.RemoveSlowStack();
            }
            
           

            Aff[enemy] = -1;
        }
       
        
    }
    private void OnDestroy()
    {
        List<GameObject> keys = new List<GameObject>(Aff.Keys);
        foreach (GameObject item in keys)
        {
            RemoveSlowStacks(item);
        }
    }

    // Start is called before the first frame update

}
