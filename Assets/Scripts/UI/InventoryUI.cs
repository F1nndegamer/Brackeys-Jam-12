using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;
    public Dictionary<FishSO, FishInfo> basket = new Dictionary<FishSO, FishInfo>();
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
    private System.Random rand = new System.Random();

    private List<GameObject> fishItemList = new List<GameObject>();
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
    private void AddFishToInventory(FishSO fish, float normalizedDistance)
    {
        if (!basket.ContainsKey(fish))
        {
            basket.Add(fish, new FishInfo());
            basket[fish].AddFish(NormalDistributionCalculation(fish.meanDistance, fish.maxPrice, fish.minPrice, normalizedDistance)); //Calculate scaled price 

            var item = Instantiate(inventoryFish, fishContent);
            var itemIcon = item.transform.GetChild(0).GetComponent<Image>();
            var itemName = item.transform.GetChild(1).GetComponent<TMP_Text>();
            var itemQuantity = item.transform.GetChild(2).GetComponent<TMP_Text>();
            item.name = fish.name;
            itemIcon.sprite = fish.icon;
            itemName.text = fish.fishName;
            itemQuantity.text = basket[fish].count.ToString();

            fishItemList.Add(item);
            fishQuantityList.Add(fish, itemQuantity);

            item.GetComponent<Button>().onClick.AddListener(() => ShowFishInformation(item, fish));
        }
        else
        {
            basket[fish].AddFish(NormalDistributionCalculation(fish.meanDistance, fish.maxPrice, fish.minPrice, normalizedDistance)); //Calculate scaled price 
            var itemQuantity = fishQuantityList[fish].GetComponent<TMP_Text>();
            itemQuantity.text = basket[fish].count.ToString();   
        }
    }
    private void ShowFishInformation(GameObject x, FishSO fish)
    {
        TextSelectedItemName.text = fish.name;
        ImageSelectedItem.sprite = fish.icon;
        TextSelectedItemDescription.text = $"Price Range: {fish.minPrice} - {fish.maxPrice}\nSize: {fish.length} cm - {fish.weight} kg\nAverage Distance From Shore: {fish.meanDistance}\n{fish.description} ";
    }
    
    private void ShowItemInformation(Button x, string des)
    {
        TextSelectedItemName.text = x.gameObject.name;
        ImageSelectedItem.sprite = x.image.sprite;
        TextSelectedItemDescription.text = des;
    }
    private void UpdateFishQuantity()
    {
        fishsNumber.text = basket.Sum(x => x.Value.count).ToString();
    }
    private int NormalDistributionCalculation(float meanDistance, int max, int min, float distance, float stdDevDistance = 10f)
    {
        int result = 0;

        double u1 = 1.0 - rand.NextDouble();
        double u2 = 1.0 - rand.NextDouble();

        double randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log((float)u1)) * Mathf.Sin(2.0f * Mathf.PI * (float)u2);

        double adjustedDistance = meanDistance + stdDevDistance * randStdNormal;

        result = Mathf.Clamp((int)(max - Mathf.Abs(distance - (float)adjustedDistance)), min, max);
        return result;
    }
    #region Item
    public void FishrodeItem(bool isBought, string des = null, int level = 1)
    {
        fishrodeItem.gameObject.GetComponent<Image>().enabled = true;
        fishrodeItem.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = ROMAN_NUMERALS[level];
        fishrodeItem.onClick.AddListener(() => ShowItemInformation(fishrodeItem, des));  
    }
    public void RangeFidnerItem(bool isBought, string des = null, int level = 1)
    {
        rangeFinderItem.gameObject.GetComponent<Image>().enabled = true;
        rangeFinderItem.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = ROMAN_NUMERALS[level];
        rangeFinderItem.onClick.AddListener(() => ShowItemInformation(rangeFinderItem, des));  
    }
    public void PropellerItem(bool isBought, string des = null, int level = 1)
    {
        propellerItem.gameObject.GetComponent<Image>().enabled = true;
        propellerItem.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = ROMAN_NUMERALS[level];
        propellerItem.onClick.AddListener(() => ShowItemInformation(propellerItem, des));  
    } 
    public void MultiPurposeMeterItem(bool isBought, string des = null, int level = 1)
    {
        multiPurposeMeterItem.gameObject.GetComponent<Image>().enabled = true;
        multiPurposeMeterItem.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = ROMAN_NUMERALS[level];
        multiPurposeMeterItem.onClick.AddListener(() => ShowItemInformation(multiPurposeMeterItem, des));  
    }
    #endregion
    public void AddFish(FishSO fish, float normalizedDistance)
    {
        AddFishToInventory(fish, normalizedDistance);
        UpdateFishQuantity();
    }
    public void RemoveAllFish()
    {
        basket = basket.ToDictionary(p => p.Key, p => new FishInfo());

        fishsNumber.text = "0"; // Reset fish number
        foreach (GameObject fishItem in fishItemList)
        {
            var fishQuantityText = fishItem.transform.GetChild(2).GetComponent<TMP_Text>();
            fishQuantityText.text = "0";
        }
    }
    public void SellAllFish()
    {
        int totalFishSold = basket.Sum(x => x.Value.count);
        int totalEarnings = basket.Sum(x => x.Value.scaledPriceList.Sum() * x.Value.count);

        Player.Instance.UpdateMoney(totalEarnings);

        RemoveAllFish();
    }
}
