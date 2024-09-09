using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class OceanGenerator : MonoBehaviour
{
    private const int NUM_OF_THREADS = 8;

    [SerializeField] private ComputeShader _compute;
    [SerializeField] private OceanSimulationParams _params;
    [SerializeField] private GameObject _player;
    [SerializeField] private Camera _simCam;

    private OceanTileState _state = new OceanTileState();

    private OceanTile _playerTile;
    private Dictionary<Vector2Int, OceanTile> _tiles;

    private OceanTile GenerateTile(Vector2Int position)
    {
        GameObject tile = Instantiate(_params.tilePrefab, new Vector3(position.x * _params.tilePrefab.transform.localScale.x, position.y * _params.tilePrefab.transform.localScale.y, 0f), Quaternion.identity);
        return new OceanTile(tile);
    }

    private void AddTile(Vector2Int position)
    {
        if (_tiles.ContainsKey(position))
        {
            _tiles[position].IsActive = true;
        }
        else
        {
            _tiles.Add(position, GenerateTile(position));
        }
    }

    private void InitilizeTexture(ref RenderTexture tex)
    {
        tex = new RenderTexture(_params.resolution.x, _params.resolution.y, 1, UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_SNorm);
        tex.enableRandomWrite = true;
        tex.Create();
    }

    private void InitilizeSimulation()
    {
        _compute.SetTexture(_compute.FindKernel("CSMain"), "pastWave", _state.pastWave);
        _compute.SetTexture(_compute.FindKernel("CSMain"), "currentWave", _state.currentWave);
        _compute.SetTexture(_compute.FindKernel("CSMain"), "nextWave", _state.nextWave);

        _compute.SetInts("resolution", new int[2] { _params.resolution.x, _params.resolution.y });
        _compute.SetFloat("elasticity", _params.elasticity);
        _compute.SetBool("useReflectiveBoundaryCondition", _params.useReflectiveBoundaryCondition);
        _compute.SetBool("loop", _params.loop);
        _compute.SetTexture(_compute.FindKernel("CSMain"), "obstaclesTex", _params.obstaclesTex);
    }

    public void Blits()
    {
        Graphics.CopyTexture(_state.currentWave, _state.pastWave);
        Graphics.CopyTexture(_state.nextWave, _state.currentWave);
    }

    public void Step()
    {
        _compute.Dispatch(_compute.FindKernel("CSMain"), _params.resolution.x / NUM_OF_THREADS, _params.resolution.y / NUM_OF_THREADS, 1);
    }

    private void Awake()
    {
        InitilizeTexture(ref _state.currentWave);
        InitilizeTexture(ref _state.pastWave);
        InitilizeTexture(ref _state.nextWave);

        _params.obstaclesTex.enableRandomWrite = true;
        Debug.Assert(_params.obstaclesTex.width == _params.resolution.x && _params.obstaclesTex.height == _params.resolution.y);
        _params.material.SetTexture("_SimulationTex", _state.currentWave);

        InitilizeSimulation();

        _tiles = new Dictionary<Vector2Int, OceanTile>();

        AddTile(new Vector2Int(0, 0));

        _playerTile = _tiles[Vector2Int.zero];
        _playerTile.PlayerOnTile = true;
    }
    private void Update()
    {
        Vector2Int playerPos = new Vector2Int(Mathf.RoundToInt(_player.transform.position.x / _params.tilePrefab.transform.localScale.x), Mathf.RoundToInt(_player.transform.position.y / _params.tilePrefab.transform.localScale.y));

        if (_tiles.ContainsKey(playerPos))
        {
            _simCam.transform.position = new Vector3(playerPos.x * _params.tilePrefab.transform.localScale.x, playerPos.y * _params.tilePrefab.transform.localScale.y, _simCam.transform.position.z);
            _playerTile.PlayerOnTile = false;
            _playerTile = _tiles[playerPos];
            _playerTile.PlayerOnTile = true;
        }

        foreach (KeyValuePair<Vector2Int, OceanTile> pair in _tiles)
        {
            pair.Value.IsActive = false;
        }

        if (Mathf.Abs(_player.transform.position.x - playerPos.x * _params.tilePrefab.transform.localScale.x) <= _params.tilePrefab.transform.localScale.x)
        {
            AddTile(new Vector2Int(playerPos.x, playerPos.y));
        }
        if (Mathf.Abs(_player.transform.position.x - (playerPos.x + 1) * _params.tilePrefab.transform.localScale.x) <= _params.tilePrefab.transform.localScale.x) {
            AddTile(new Vector2Int(playerPos.x + 1, playerPos.y));
        }
        if (Mathf.Abs(_player.transform.position.x- (playerPos.x - 1) * _params.tilePrefab.transform.localScale.x) <= _params.tilePrefab.transform.localScale.x)
        {
            AddTile(new Vector2Int(playerPos.x - 1, playerPos.y));
        }

        Blits();
    }

    private void LateUpdate()
    {
        Step();
    }
}
