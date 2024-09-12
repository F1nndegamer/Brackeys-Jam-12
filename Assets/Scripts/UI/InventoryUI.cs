using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;
    public static Dictionary<FishSO, int> basket = new Dictionary<FishSO, int>();
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
    private Dictionary<FishSO, TMP_Text> inventoryFishList = new Dictionary<FishSO, TMP_Text>();
    private List<Button> inventoryButtons = new List<Button>();
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
    private void AddInventory(FishSO fish)
    {
        if (!basket.ContainsKey(fish))
        {
            basket.Add(fish, 1);
            var item = Instantiate(inventoryFish, fishContent);
            var itemIcon = item.transform.GetChild(0).GetComponent<Image>();
            var itemName = item.transform.GetChild(1).GetComponent<TMP_Text>();
            var itemQuantity = item.transform.GetChild(2).GetComponent<TMP_Text>();

            item.name = fish.name;
            itemIcon.sprite = fish.icon;
            itemName.text = fish.fishName;
            itemQuantity.text = basket[fish].ToString();
            inventoryFishList.Add(fish, itemQuantity);
            item.GetComponent<Button>().onClick.AddListener(() => ShowInformation(item, fish));
        }
        else
        {
            basket[fish]++;
            var itemQuantity = inventoryFishList[fish].GetComponent<TMP_Text>();
            itemQuantity.text = basket[fish].ToString();
        }
    }
    private void ShowInformation(GameObject x, FishSO fish)
    {
        TextSelectedItemName.text = x.name;
        ImageSelectedItem.sprite = fish.icon;
        TextSelectedItemDescription.text = fish.price.ToString();
    }
    
    private void ShowInformation(Button x, string des)
    {
        TextSelectedItemName.text = x.gameObject.name;
        ImageSelectedItem.sprite = x.image.sprite;
        TextSelectedItemDescription.text = des;
    }
    private void FishsNumber()
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
    public void UpdateUI(FishSO fish)
    {
        AddInventory(fish);
        FishsNumber();
    }
    public void RemoveInventory(FishSO fish)
    {
        //remove but idk how remove
    }
}
