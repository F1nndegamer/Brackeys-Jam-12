using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormSelf : MonoBehaviour
{
    public StormManager manager;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            manager.Colliding = true;
        }
        if (collision.gameObject.CompareTag("Line"))
        {
            Destroy(gameObject);
        }
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            manager.Colliding = false;
        }

    }
}
