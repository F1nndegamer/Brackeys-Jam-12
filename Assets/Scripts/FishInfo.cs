using System.Collections.Generic;

[System.Serializable]
public class FishInfo
{
    public int count; // Number of this type of fish caught
    public List<int> scaledPriceList; // List of distances from the shore for each fish caught

    public FishInfo()
    {
        count = 0;
        scaledPriceList = new List<int>();
    }
    public void AddFish(int scaledPrice)
    {
        count++;
        this.scaledPriceList.Add(scaledPrice);
    }
}
