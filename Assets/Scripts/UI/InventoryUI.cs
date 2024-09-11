using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;
    [SerializeField] private Transform fishContent;
    [SerializeField] private GameObject inventoryFish;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Player.Instance.OnInventoryOpened += Player_OnInventoryOpened;
        Player.Instance.OnInvetoryClosed += Player_OnInventoryClosed;
        Hide();
    }
    private void Player_OnInventoryClosed(object sender, System.EventArgs e)
    {
        Hide();
    }
    private void Player_OnInventoryOpened(object sender, System.EventArgs e)
    {
        Show();
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    public void AddInventory(FishSO fish)
    {
        GameObject item = Instantiate(inventoryFish, fishContent);
        var itemName = item.transform.GetChild(0).GetComponent<TMP_Text>();

        itemName.text = fish.fishName;
    }
    public void RemoveInventory(FishSO fish)
    {
        //remove but idk how remove
    }
}
