using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Three_Projectiles : ProjectileSpell
{
    public float spacing = 10;
    // This Script is just to spawn in 3 objects for the effects just worry about the effects for the Projectile

    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        GameObject ob = gameObject;
        Execute(gameObject);
        Execute(gameObject);
        Execute(gameObject);
        Destroy(gameObject);
        
    }
    int ProjectileNumber = 0;
    GameObject First = null;
    protected override void Execute(GameObject origin, Vector3 OffSet = default)
    {
        
        if (Effects.Count > 0)
        {
            ProjectileNumber++;

            GameObject ob = null;
            //ob = Instantiate(Effects[0], origin.transform.position, transform.rotation);

            if (ProjectileNumber == 1)
            {
                
                First = Instantiate(Effects[0], origin.transform.position, transform.rotation);
                ob = First;

            }
            else
            {
                
                ob = Instantiate(Effects[0], First.transform);
                ob.transform.localPosition = OffSet;
                if (ProjectileNumber == 2)
                    ob.transform.Rotate(new Vector3( 0, -15, 0));
                if(ProjectileNumber == 3)
                   ob.transform.Rotate(new Vector3(0, 15, 0));
                ob.transform.parent = null;
            }
            
            //ob.transform.position += OffSet;
            BaseSpell spell = ob.GetComponent<BaseSpell>();
            spell.Effects = new List<GameObject>(Effects);
            spell.Effects.RemoveAt(0);
            
            

        }
    }

        
    

}
