using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingMinigameUI : MonoBehaviour
{
    public static FishingMinigameUI Instance;
    [SerializeField] private GameObject bar;
    [SerializeField] private Animator animator;
    [SerializeField] private Vector3 offset;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        bar.SetActive(false);
    }
    private void Update()
    {
        transform.position = Player.Instance.transform.position + offset;
    }
    public void Flash()
    {
        animator.SetTrigger("Flash");
    }
    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
    }
    public void FadeIn()
    {
        animator.SetTrigger("FadeIn");
    }
}
