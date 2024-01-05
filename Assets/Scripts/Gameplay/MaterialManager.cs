using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoSingleton<MaterialManager> {
    [SerializeField] private BaseMaterial _materialPrefab;
    [SerializeField] private Transform _materialSpawnPoint;

    private BaseMaterial _currentMaterial;

    public void CreateNewMaterial () {
        if (GameplayManager.Instance.GameOver)
            return;
            
        _currentMaterial = Instantiate<BaseMaterial> (_materialPrefab, _materialSpawnPoint.position, _materialSpawnPoint.rotation, _materialSpawnPoint);
        int count = Random.Range (10, 25);
        _currentMaterial.Init (count);
    }

}
