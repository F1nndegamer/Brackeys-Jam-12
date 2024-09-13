using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;
    public Dictionary<FishSO, int> basket = new Dictionary<FishSO, int>();
    [SerializeField] private Transform fishContent;
    [SerializeField] private GameObject inventoryFish;
    [SerializeField] private TMP_Text fishsNumber;
    [SerializeField] private TMP_Text TextSelectedItemName;
    [SerializeField] private TMP_Text TextSelectedItemDescription;
    [SerializeField] private Image ImageSelectedItem;
    [SerializeField] private Button fishrodeItem;
    [SerializeField] private Button rangeFinderItem;
    [SerializeField] private Button propellerItem;
    [SerializeField] private Button multiPurposeMeterItem;
    private readonly Dictionary<int, string> ROMAN_NUMERALS = new Dictionary<int, string>() { { 1, "I" }, { 2, "II" }, { 3, "III" }, { 4, "IV" }, { 5, "V" } };
    private Dictionary<FishSO, TMP_Text> fishQuantityList = new Dictionary<FishSO, TMP_Text>();

    private List<GameObject> fishItemList = new List<GameObject>();
    private List<int> distances = new List<int>();
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Player.Instance.OnInventoryOpened += Player_OnInventoryOpened;
        Player.Instance.OnInvetoryClosed += Player_OnInventoryClosed;
        Hide();
        fishrodeItem.gameObject.GetComponent<Image>().enabled = false;
        rangeFinderItem.gameObject.GetComponent<Image>().enabled = false;
        propellerItem.gameObject.GetComponent<Image>().enabled = false;
        multiPurposeMeterItem.gameObject.GetComponent<Image>().enabled = false;
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
    private void AddInventory(FishSO fish, float x = 0)
    {
        if (!basket.ContainsKey(fish))
        {
            basket.Add(fish, 1);
            var item = Instantiate(inventoryFish, fishContent);
            var itemIcon = item.transform.GetChild(0).GetComponent<Image>();
            var itemName = item.transform.GetChild(1).GetComponent<TMP_Text>();
            var itemQuantity = item.transform.GetChild(2).GetComponent<TMP_Text>();
            fishItemList.Add(item);
            distances.Add(((int)UnityEngine.Random.Range(Mathf.Log(x, 2) * 5, Mathf.Log(x, 2) * 10)));
            fishQuantityList.Add(fish, itemQuantity);
            item.name = fish.name;
            itemIcon.sprite = fish.icon;
            itemName.text = fish.fishName;
            itemQuantity.text = basket[fish].ToString();
            item.GetComponent<Button>().onClick.AddListener(() => ShowInformation(item, fish));
        }
        else
        {
            basket[fish]++;
            var itemQuantity = fishQuantityList[fish].GetComponent<TMP_Text>();
            itemQuantity.text = basket[fish].ToString();   
        }
    }
    private void ShowInformation(GameObject x, FishSO fish)
    {
        TextSelectedItemName.text = fish.name;
        ImageSelectedItem.sprite = fish.icon;
        TextSelectedItemDescription.text = fish.price.ToString() + "~" + (fish.price + 100);
    }
    
    private void ShowInformation(Button x, string des)
    {
        TextSelectedItemName.text = x.gameObject.name;
        ImageSelectedItem.sprite = x.image.sprite;
        TextSelectedItemDescription.text = des;
    }
    private void UpdateFishQuantity()
    {
        fishsNumber.text = basket.Sum(x => x.Value).ToString();
    }
    public void FishrodeItem(bool isBought, string des = null, int level = 1)
    {
        fishrodeItem.gameObject.GetComponent<Image>().enabled = true;
        fishrodeItem.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = ROMAN_NUMERALS[level];
        fishrodeItem.onClick.AddListener(() => ShowInformation(fishrodeItem, des));  
    }
    public void RangeFidnerItem(bool isBought, string des = null, int level = 1)
    {
        rangeFinderItem.gameObject.GetComponent<Image>().enabled = true;
        rangeFinderItem.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = ROMAN_NUMERALS[level];
        rangeFinderItem.onClick.AddListener(() => ShowInformation(rangeFinderItem, des));  
    }
    public void PropellerItem(bool isBought, string des = null, int level = 1)
    {
        propellerItem.gameObject.GetComponent<Image>().enabled = true;
        propellerItem.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = ROMAN_NUMERALS[level];
        propellerItem.onClick.AddListener(() => ShowInformation(propellerItem, des));  
    } 
    public void MultiPurposeMeterItem(bool isBought, string des = null, int level = 1)
    {
        multiPurposeMeterItem.gameObject.GetComponent<Image>().enabled = true;
        multiPurposeMeterItem.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = ROMAN_NUMERALS[level];
        multiPurposeMeterItem.onClick.AddListener(() => ShowInformation(multiPurposeMeterItem, des));  
    }
    public void UpdateUI(FishSO fish, float x = 0)
    {
        AddInventory(fish, x);
        UpdateFishQuantity();
    }
    public void RemoveAllFish()
    {
        basket = basket.ToDictionary(p => p.Key, p => 0);
        distances.Clear();

        fishsNumber.text = "0"; // Reset fish number
        foreach (GameObject fishItem in fishItemList)
        {
            var fishQuantityText = fishItem.transform.GetChild(2).GetComponent<TMP_Text>();
            fishQuantityText.text = "0";
        }
    }
    public void SellAllFish()
    {
        int totalFishSold = basket.Sum(x => x.Value);
        int totalEarnings = basket.Sum(x => x.Key.price * x.Value);
        totalEarnings += distances.Sum();

        Player.Instance.UpdateMoney(totalEarnings);

        Debug.Log($"Sold {totalFishSold} fish for {totalEarnings} currency!");

        RemoveAllFish();
    }
}
