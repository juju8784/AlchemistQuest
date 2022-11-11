using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PoolSpell : BaseSpell
{
    // Start is called before the first frame update

    Dictionary<GameObject, bool> Affected = new Dictionary<GameObject, bool>();
    public float Interval;
    private float distanceOffset;
    public GameObject[] hitEffect;
    protected override void Start()
    {
        base.Start();
        //transform.position += offset;
        distanceOffset = GetComponent<Collider>().bounds.extents.y;
        for (int i = 0; i < hitEffect.Length; i++)
        {
            Instantiate(hitEffect[i], gameObject.transform.position, Quaternion.identity);
        }
        Destroy(gameObject, Duration);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
      {
            if (!Affected.ContainsKey(other.gameObject))
            {
                Affected.Add(other.gameObject, true);
                Execute(other.gameObject);
                
            }
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (Affected.ContainsKey(other.gameObject))
                    Affected.Remove(other.gameObject);
        }
    }

    float time;
    protected override void Update()
    {
        if (GameManager.instance.paused == false)
        {
            if (!Physics.Raycast(transform.position, -Vector3.up, distanceOffset))
            {
                transform.position += new Vector3(0, -2 * Time.deltaTime, 0);
            }

            if (Time.time - time > Interval)
            {
                Duration -= Interval;
                time = Time.time;
                foreach (var item in Affected)
                {
                    item.Key.GetComponent<BaseEnemyController>().TakeDamage(Damage,Type);
                }
            }
            if(Duration < 0)
            {
                Destroy(gameObject);
            }

        }


    }
}
