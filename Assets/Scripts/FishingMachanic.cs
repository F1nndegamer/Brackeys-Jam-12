using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FishingMachanic : MonoBehaviour
{
    [Header("Fish")]
    [SerializeField] private float timer;
    [SerializeField] private float timerMulti;
    [SerializeField] private float fishSpeed;
    [SerializeField] private UnityEngine.UI.Slider fish;
    private RectTransform sliderHandleRect;

    [Header("Fishrod")]
    [SerializeField] private float fishrodSpeed;
    [SerializeField] private Scrollbar fishrode;
    private RectTransform scrollbarHandleRect;

    public bool isFishing;
    // Start is called before the first frame update
    void Start()
    {
        sliderHandleRect = fish.handleRect;
        scrollbarHandleRect = fishrode.handleRect;
    }

    // Update is called once per frame
    void Update()
    {
        if (AreHandlesTouching(sliderHandleRect, scrollbarHandleRect))
        {
            Debug.Log("aaaa");
        }
        BaseFishingMach();
        FishMachanic();
        FishrodMachanic();
    }
    void BaseFishingMach()
    {
        if (Input.GetMouseButtonDown(0) && !isFishing)
        {
            isFishing = true;
        }
        else if (Input.GetMouseButtonDown(0) && isFishing)
        {
            isFishing = false;
        }
    }
    void FishMachanic()
    {
        if (!isFishing) return;
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = Random.value * timerMulti;
            fishSpeed = -fishSpeed;
        }
        fish.value += fishSpeed;
    }
    void FishrodMachanic()
    {
        if(!isFishing) return;
        if (Input.GetKey(KeyCode.Space) && fishrode.value < 1)
        {
            fishrode.value += fishrodSpeed;
        }
        else
        {
            fishrode.value += -fishrodSpeed * 1.5f;
        }
    }
    bool AreHandlesTouching(RectTransform rect1, RectTransform rect2)
    {
        Rect rect1Bounds = RectTransformToScreenSpace(rect1);
        Rect rect2Bounds = RectTransformToScreenSpace(rect2);

        return rect1Bounds.Overlaps(rect2Bounds);
    }
    Rect RectTransformToScreenSpace(RectTransform rectTransform)
    {
        Vector3[] worldCorners = new Vector3[4];
        rectTransform.GetWorldCorners(worldCorners);

        return new Rect(worldCorners[0].x, worldCorners[0].y,
                        worldCorners[2].x - worldCorners[0].x,
                        worldCorners[2].y - worldCorners[0].y);
    }
}
