using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QueueManagment : MonoBehaviour
{
    [SerializeField]
    private GameObject[] QueueSlots = new GameObject[2];
    Image[] SlotImages = new Image[2];
    List<Abilities> Queue = new List<Abilities>(2);
    Color baseCol;
    [SerializeField]
    GameObject TimerBar = null;
    Image TimerBarImage;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < QueueSlots.Length; i++)
        {
            SlotImages[i] = QueueSlots[i].GetComponent<Image>();
        }
        baseCol = SlotImages[0].color;
        TimerBarImage = TimerBar.GetComponent<Image>();
        QueueTime -= 1;
        
    }

    public void ClearQueue()
    {
        QueueDelay = 0;
        Queue.Clear();
        for (int i = 0; i < SlotImages.Length; i++)
        {
            SlotImages[i].color = baseCol;
        }
    }
    void UpdateQueueUI()
    {
        for (int i = 0; i < Queue.Count; i++)
        {
                if (Queue[i] == Abilities.red)
                    SlotImages[i].color = Color.red;
                if (Queue[i] == Abilities.blue)
                    SlotImages[i].color = Color.blue;
                if (Queue[i] == Abilities.green)
                    SlotImages[i].color = Color.green;
                if (Queue[i] == Abilities.yellow)
                    SlotImages[i].color = Color.yellow;
        }
    }



    // Update is called once per frame
    private float QueueDelay = 0;
    [SerializeField]
    private float QueueTime;
    void Update()
    {
        if (GameManager.instance.paused == false && GameManager.instance.playerManager.dead == false)
        {
            if (QueueDelay > 0)
            {
                QueueDelay -= Time.deltaTime;
                TimerBarImage.fillAmount = QueueDelay / QueueTime;


            }
            else
            {

                ClearQueue();
                TimerBarImage.fillAmount = 1;
            }

            if (Queue.Count < 2)
            {


                if (Input.GetKeyDown(KeyCode.Alpha1) && GameManager.instance.Inventory.CheckInventory(Abilities.red, EligbleToClick(Abilities.red)))
                {
                    Queue.Add(Abilities.red);
                    UpdateQueueUI();
                    QueueDelay = 5;
                }

                else if (Input.GetKeyDown(KeyCode.Alpha2) && GameManager.instance.Inventory.CheckInventory(Abilities.blue, EligbleToClick(Abilities.blue)))
                {
                    Queue.Add(Abilities.blue);
                    UpdateQueueUI();
                    QueueDelay = 5;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3) && GameManager.instance.Inventory.CheckInventory(Abilities.green, EligbleToClick(Abilities.green)))
                {
                    Queue.Add(Abilities.green);
                    UpdateQueueUI();
                    QueueDelay = 5;

                }
                else if (Input.GetKeyDown(KeyCode.Alpha4) && GameManager.instance.Inventory.CheckInventory(Abilities.yellow, EligbleToClick(Abilities.yellow) ))
                {
                    Queue.Add(Abilities.yellow);
                    UpdateQueueUI();
                    QueueDelay = 5;
                }

            }
        }
    }
    public List<Abilities> GetQueue()
    {
        return Queue;
    }

    public bool TryToCast()
    {
        if ((Queue.Count > 0 && Input.GetMouseButton(0)) || (QueueDelay <= 0 && SlotImages[0].color != baseCol))
        {
            return true;
        } 
        else 
            return false;
    }
    public int [] GetAmountInEachSlot()
    {
        int [] Amounts = new int[4];
        for (int i = 0; i < Queue.Count; i++)
        {
            Amounts[(int)Queue[i]] += 1;
        }
        return Amounts;
    }

    int EligbleToClick(Abilities type)
   {
        int[] check = GetAmountInEachSlot();
        return check[(int)type];
    }
}

