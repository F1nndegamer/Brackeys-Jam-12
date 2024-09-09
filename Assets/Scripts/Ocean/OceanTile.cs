using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanTileState
{
    public RenderTexture pastWave;
    public RenderTexture currentWave;
    public RenderTexture nextWave;
}

[System.Serializable]
public class OceanTile
{
    private GameObject _instance;

    public bool IsActive { get => _instance.activeSelf; set => _instance.SetActive(value); }
    
    public Vector3 Position { get => _instance.transform.position; set => _instance.transform.position = value; }
    
    public bool PlayerOnTile { get; set; }

    public OceanTile(GameObject instance)
    {
        _instance = instance;
        PlayerOnTile = false;
    }
}
