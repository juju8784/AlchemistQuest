using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    Abilities type = 0;
    [SerializeField]
    int Amount = 0;

    //[SerializeField] GameObject textPrefab = null;
    //private Canvas canvas;
    //private GameObject temp;
    //private bool inRange;

    //void Start()
    //{
    //    canvas = FindObjectOfType<Canvas>();
    //    temp = Instantiate(textPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    //    temp.transform.SetParent(canvas.transform);
    //    temp.SetActive(false);
    //    inRange = false;
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            GameManager.instance.Inventory.AddToInventorySlot(type, Amount);
            Destroy(gameObject);
            //temp.SetActive(true);
            //inRange = true;
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        temp.SetActive(false);
    //        inRange = false;
    //    }
    //}

    //void Update()
    //{
    //    if (GameManager.instance.paused == false)
    //    {
    //        if(inRange == true)
    //        {
    //            temp.SetActive(true);
    //        }
    //        temp.transform.position = Camera.main.WorldToScreenPoint(transform.position + (new Vector3(0, 5, 0)));
    //        if (Input.GetKey("e") && inRange == true)
    //        {
    //            gameObject.SetActive(false);
    //            GameManager.instance.Inventory.AddToInventorySlot(type, Amount);
    //            temp.SetActive(false);
    //            Destroy(gameObject);
    //        }
    //    }
    //    else
    //    {
    //        temp.SetActive(false);
    //    }
    //}
}
