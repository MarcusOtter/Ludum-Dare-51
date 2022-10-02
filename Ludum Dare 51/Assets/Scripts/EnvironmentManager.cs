using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private Transform _pacmanLevel;
    private MeshRenderer _mesh;
    private Material _floorMaterial;
    [SerializeField] private Material _pacmanMaterial;

    private void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
        _floorMaterial = _mesh.material;
    }

    public void StartPacMan()
    {
        _pacmanLevel.gameObject.SetActive(true);
        _mesh.material = _pacmanMaterial;
    }
    public void EndPacMan()
    {
        _pacmanLevel.gameObject.SetActive(false);
        _mesh.material = _floorMaterial;
    }
}
