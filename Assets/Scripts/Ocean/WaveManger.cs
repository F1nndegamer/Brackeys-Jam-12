using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManger : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private ComputeShader _waveCompute;

    [Header("Wave States")]
    [SerializeField] private RenderTexture _pastWave;
    [SerializeField] private RenderTexture _currentWave;
    [SerializeField] private RenderTexture _nextWave;

    [SerializeField] private Vector2Int _resolution = new Vector2Int(100, 100);

    [SerializeField] private RenderTexture _obstaclesTex;

    [SerializeField] private Material _mat;

    // How long for wave to decipation
    [Range(0, 1)]
    [SerializeField] private float elasticity = 0.98f;

    [SerializeField] private bool useReflectiveBoundaryCondition;

    private void InitilizeTexture(ref RenderTexture tex)
    {
        tex = new RenderTexture(_resolution.x, _resolution.y, 1, UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_SNorm);
        tex.enableRandomWrite = true;
        tex.Create();
    }

    private void Start()
    {
        InitilizeTexture(ref _currentWave);
        InitilizeTexture(ref _pastWave);
        InitilizeTexture(ref _nextWave);
        
        _obstaclesTex.enableRandomWrite = true;
        Debug.Assert(_obstaclesTex.width == _resolution.x && _obstaclesTex.height == _resolution.y);
        _mat.SetTexture("_SimulationTex", _currentWave);
    }

    private void Update()
    {
        Graphics.CopyTexture(_currentWave, _pastWave);
        Graphics.CopyTexture(_nextWave, _currentWave);

        _waveCompute.SetTexture(_waveCompute.FindKernel("CSMain"), "pastWave", _pastWave);
        _waveCompute.SetTexture(_waveCompute.FindKernel("CSMain"), "currentWave", _currentWave);
        _waveCompute.SetTexture(_waveCompute.FindKernel("CSMain"), "nextWave", _nextWave);
        _waveCompute.SetInts("resolution", new int[2] { _resolution.x, _resolution.y });
        _waveCompute.SetFloat("elasticity", elasticity);
        _waveCompute.SetBool("useReflectiveBoundaryCondition", useReflectiveBoundaryCondition);
        _waveCompute.SetTexture(_waveCompute.FindKernel("CSMain"), "obstaclesTex", _obstaclesTex);
        
        _waveCompute.Dispatch(_waveCompute.FindKernel("CSMain"), _resolution.x / 8, _resolution.y / 8, 1);
    }
}
