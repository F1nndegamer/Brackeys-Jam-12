using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;
    [SerializeField] private Transform fishContent;
    [SerializeField] private GameObject inventoryFish;
    [SerializeField] private TMP_Text fishsNumber;
    private Dictionary<FishSO, TMP_Text> inventoryFishList = new Dictionary<FishSO, TMP_Text>();
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
    public void UpdateUI(FishSO fish)
    {
        AddInventory(fish);
        FishsNumber();
    }
    public void AddInventory(FishSO fish)
    {
        if (!FishingMachanic.basket.ContainsKey(fish))
        {
            FishingMachanic.basket.Add(fish, 1);
            var item = Instantiate(inventoryFish, fishContent);
            var itemIcon = item.transform.GetChild(0).GetComponent<Image>();
            var itemName = item.transform.GetChild(1).GetComponent<TMP_Text>();
            var itemQuantity = item.transform.GetChild(2).GetComponent<TMP_Text>();

            item.name = fish.name;
            itemIcon.sprite = fish.icon;
            itemName.text = fish.fishName;
            itemQuantity.text = FishingMachanic.basket[fish].ToString();
            inventoryFishList.Add(fish, itemQuantity);
        }
        else
        {
            FishingMachanic.basket[fish]++;
            var itemQuantity = inventoryFishList[fish].GetComponent<TMP_Text>();
            itemQuantity.text = FishingMachanic.basket[fish].ToString();
        }
    }
    public void FishsNumber()
    {
        fishsNumber.text = FishingMachanic.basket.Sum(x => x.Value).ToString();
    }
    public void RemoveInventory(FishSO fish)
    {
        //remove but idk how remove
    }
}
