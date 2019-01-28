using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryScreen : MonoBehaviour {

    private Text moneyText;
    private Text armorText;
    private Player player;
    private PlayerData playerInventory;

    private Button[] inventorySlots;


	// Use this for initialization
	void Start () {
        moneyText = GameObject.Find("MoneyText").GetComponent<Text>();
        armorText = GameObject.Find("ArmorText").GetComponent<Text>();
        player = this.GetComponentInParent<Player>();
        playerInventory = player.GetComponent<PlayerData>();

        inventorySlots = GetComponentsInChildren<Button>();
	}
	
	// Update is called once per frame
	void Update () {
        TextFields();
        for(int i = 0; i < playerInventory.InventoryList.Count; i++)
        {
            inventorySlots[i].GetComponentInChildren<Text>().text = playerInventory.InventoryList[i].name;
        }
	}

    private void TextFields()
    {
        moneyText.text = playerInventory.Currency.ToString();

        if (playerInventory.Armor == null)
        {
            armorText.text = "None";
        }
    }

    //public void AddItem(GameObject item)
    //{
    //    for(int i = 0; i < inventorySlots.Length; i++)
    //    {
    //        if(inventorySlots[i].GetComponent<Text>().text != null)
    //        {
    //            inventorySlots[i].GetComponent<Text>().text = item.name;
    //        }
    //    }
    //}
}
