using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float damage;
    public float lifeTime;

    //Make this greater than 1 for an increase in size
    public float explosionSizeMultiplier;

    public GameObject hitEffect;

    private float timer;
    private bool dealtDamage;
    private Vector3 totalSizeChange;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        dealtDamage = false;
        totalSizeChange = transform.localScale * explosionSizeMultiplier - transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hitEffect)
            Instantiate(hitEffect, transform.position, transform.rotation);
        if (other.gameObject.CompareTag("Player") && !dealtDamage && GameManager.instance.playerManager.dead == false)
        {
            GameManager.instance.playerManager.TakeDamage(damage);
            dealtDamage = true;
        }

        BossGasAttack gasAttack = other.GetComponent<BossGasAttack>();
        if (gasAttack != null)
        {
            gasAttack.Explode();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.paused == false && GameManager.instance.playerManager.dead == false)
        {
            timer += Time.deltaTime;
            transform.localScale = transform.localScale + totalSizeChange * Time.deltaTime / lifeTime;
            if (timer >= lifeTime)
                Destroy(gameObject);
        }
    }
}
