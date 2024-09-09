using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OceanSimulationParams", menuName = "Ocean Simulation/Params")]
public class OceanSimulationParams : ScriptableObject
{
    [Header("Simulation Settings")]
    public Vector2Int resolution = new Vector2Int(256, 256);
    public float elasticity = 0.995f;
    public bool useReflectiveBoundaryCondition = false;
    public bool loop = true;
    public RenderTexture obstaclesTex;
    public GameObject tilePrefab;
    public Material material;
}
