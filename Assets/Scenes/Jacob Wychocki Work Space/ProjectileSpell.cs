using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileSpell : BaseSpell
{
    [SerializeField]
    protected float BulletSpeed;
    [SerializeField]
    public GameObject[] hitEffect;
    // Start is called before the first frame update
    protected override void Start()
    {
        Destroy(gameObject, Duration);
        base.Start();
        distanceOffset = 2f;

    }

    // Update is called once per frame
    private float distanceOffset;
    protected override void Update()
    {
        if (GameManager.instance.paused == false && GameManager.instance.playerManager.dead == false)
        {
            RaycastHit hit;
            Ray DownRay = new Ray(transform.position, -Vector3.up);
            if (Physics.Raycast(DownRay, out hit))
            {
                if (hit.distance != distanceOffset)
                {
                    float hover = distanceOffset - hit.distance;

                    if(Mathf.Abs(hover) <= 0.32f)
                        transform.position += new Vector3(0, hover, 0);
                }
        }

                transform.position += transform.forward * BulletSpeed * Time.deltaTime;


            // ------------------------ Failed attempt at making it a throwing projectile ----------------------------------

            //Ray mouse = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Plane p = new Plane(Vector3.up, transform.position);
            //p.Raycast(mouse, out float hit);
            //Vector3 hitPoint = mouse.GetPoint(hit);
            //gameObject.GetComponent<Rigidbody>().velocity = CalculateVelocity(hitPoint, transform.position, BulletSpeed);
        }

    }


    // got from https://www.youtube.com/watch?v=03GHtGyEHas
    Vector3 CalculateVelocity(Vector3 target, Vector3 player, float time)
    {
        Vector3 distance = target - player;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }
    protected override void OnTriggerEnter(Collider other)
    {
        
        if(IsEnemySpell)
        {

        }
        else if (other.gameObject.CompareTag("Enemy") && other.gameObject != source)
        {
            for (int i = 0; i < hitEffect.Length; i++)
            {
                Instantiate(hitEffect[i], transform.position, hitEffect[i].transform.rotation);
            }
            other.GetComponent<BaseEnemyController>().TakeDamage(Damage,Type);
            Execute(other.gameObject);
            Destroy(gameObject);
        }
       
        if (other.CompareTag("Terrain"))
        {
            Destroy(gameObject);
        }
    }
    
}
