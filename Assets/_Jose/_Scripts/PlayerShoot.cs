using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This allows the player to shoot
public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPosition;
    public float bulletSpeed;
    public float fireRate;

    private float timer;

    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.paused == false && GameManager.instance.playerManager.dead == false)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane p = new Plane(Vector3.up, transform.position);
            if (p.Raycast(mouseRay, out float hitDist))
            {
                Vector3 hitPoint = mouseRay.GetPoint(hitDist);
                transform.LookAt(hitPoint);
            }

            timer += Time.deltaTime;

            if (Input.GetMouseButtonDown(0) && timer >= 1 / fireRate) // && GameManager.instance.Queue.TryToCast())
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        List<Abilities> Queue = GameManager.instance.Queue.GetQueue();
        int[] Amounts = GameManager.instance.Queue.GetAmountInEachSlot();
       
        for (int i = 0; i < 4; i++)
        {
            GameManager.instance.Inventory.AddToInventorySlot((Abilities)i, Amounts[i] * -1);
        }
        
        GameObject bull =  DecideAbility.CreateAbility(Queue, spawnPosition.position, transform.rotation);
        GameManager.instance.Queue.ClearQueue();
        
        timer = 0;
    }
}
