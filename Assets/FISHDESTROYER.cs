using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FISHDESTROYER : MonoBehaviour
{
    public List<GameObject> Fish;
    public void Go()
    {
            foreach (GameObject obj in Fish) obj.SetActive(false);
            Debug.Log("destory");
        

    }
}
