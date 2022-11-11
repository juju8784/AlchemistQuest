using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using UnityEngine;

public abstract class BaseSpell : MonoBehaviour
{

    [SerializeField] public Abilities Type;
    public float Damage = 5;
    protected GameObject Next = null;
    [SerializeField] public List<GameObject> Effects = new List<GameObject>();
    public float Duration;
    [SerializeField] protected bool IsEnemySpell;
    protected GameObject source;
    protected virtual void Start()
    {
        
    }
    // Update is called once per frame
    protected abstract void Update();
    protected virtual void Execute(GameObject origin, Vector3 OffSet = new Vector3())
    {
        
        if (Effects.Count > 0)
        {
            GameObject ob = Instantiate(Effects[0], origin.transform.position + OffSet, transform.rotation);
            
            BaseSpell spell = ob.GetComponent<BaseSpell>();
            spell.Effects = new List<GameObject>(Effects);
            spell.Effects.RemoveAt(0);
            spell.source = origin;

        }
    }
    protected abstract void OnTriggerEnter(Collider other);
    public  void AddEffect(GameObject effect)
    {
       /* GameObject ob = gameObject;
        while(ob != null)
        {
            
            if (ob.GetComponent<BaseSpell>().Next == null)
            {
                    ob.GetComponent<BaseSpell>().Next = effect;
                    break;
            }
            else
                ob = ob.GetComponent<BaseSpell>().Next;*/

        }
    public void Awake()
    {
        Destroy(gameObject, 30);
    }

    protected void DealDamageToEnemy(BaseEnemyController Enemy)
    {
        Enemy.TakeDamage(Damage,Type);
    }


}
    
        
        


    

