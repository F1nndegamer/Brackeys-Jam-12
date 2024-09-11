using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform shore;
    private Player playerscript;
    private void Start()
    {
        player = Player.Instance.transform;
        playerscript = player.gameObject.GetComponent<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(shore.position.x - player.transform.position.x) <= 10f)
        {
            playerscript.isNearShop = true;
            print("true");
        }
        else
        {
            print("false");
            playerscript.isNearShop = false;
        }
    }
}
