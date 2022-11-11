using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public enum Abilities
{
    red = 0,
    blue,
    green,
    yellow,
    NULL = -1

}
public class InventoryManager : MonoBehaviour
{
    int[] startingAmount = new int[4];

    [SerializeField]
    int[] Amount = new int[4];
    public GameObject[] InventorySlots = new GameObject[4];
    private Text[] SlotTexts = new Text[4];
    

    private void Awake()
    {
        //Player prefs loaded here. 
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Amount.Length; i++)
        {
            startingAmount[i] = Amount[i];
        }
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            SlotTexts[i] = InventorySlots[i].GetComponentInChildren<Text>();
        }
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            SetTextbox((Abilities)i);
        }
    }
    public void AddToInventorySlot(Abilities type, int amount)
    {
        Amount[(int)type] += amount; 
        SetTextbox(type);
    }
    public int GetInventorySlot(Abilities type)
    {
        return Amount[(int)type];
    }

    public bool CheckInventory(Abilities ability, int amount = 0)
    {
        if (Amount[(int)ability]-amount <= 0)
        {
            return false;
        }
        return true;
    }

    public void ResetInventory()
    {
        for (int i = 0; i < startingAmount.Length; i++)
        {
            Amount[i] = startingAmount[i];
        }
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            SetTextbox((Abilities)i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Alpha1))
            AddToInventorySlot(Abilities.red, -1);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            AddToInventorySlot(Abilities.blue, -1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            AddToInventorySlot(Abilities.green, -1);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            AddToInventorySlot(Abilities.yellow, -1);*/
    }
    private void OnDestroy()
    {
        //Player Prefs Saved here
    }
    private void SetTextbox(Abilities ability)
    {
        SlotTexts[(int)ability].text = Amount[(int)ability].ToString();
    }

    
    


}
