using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MergeSpawnManager : Singleton<MergeSpawnManager>
{
    [SerializeField] private float _cooldownTime;
    [SerializeField] private Image _cooldownImage;
    [SerializeField] private GameObject _prefabToSpawn;
    private float _currentCooldownTime;
    private bool _cooldown = false;
    public List<GameObject> _listOfAvailableTiles = new List<GameObject>();
    private GameObject _tileToSpawnOn;
    private void Start()
    {
        MergeTile[] merges = FindObjectsOfType<MergeTile>();
        foreach (MergeTile merge in merges)
        {
            _listOfAvailableTiles.Add(merge.gameObject);
        }
    }
    private void Update()
    {
        if (_cooldown && _listOfAvailableTiles.Count > 0)
        {
            _currentCooldownTime += Time.deltaTime;
            float _cooldownValueNormal = _currentCooldownTime /_cooldownTime;
            _cooldownImage.fillAmount = _cooldownValueNormal;
            if (_currentCooldownTime>=_cooldownTime)
            {
                _cooldown = false;
                _currentCooldownTime = 0;
                _cooldownImage.fillAmount = _currentCooldownTime;
            }
        }
        else
        {
            if (_listOfAvailableTiles.Count > 0)
            {
                _tileToSpawnOn = _listOfAvailableTiles[Random.Range(0, _listOfAvailableTiles.Count)];
                UnityEngine.GameObject _currentPrefab = Instantiate(_prefabToSpawn, new Vector3(_tileToSpawnOn.transform.position.x, _tileToSpawnOn.transform.position.y,-0.01f), Quaternion.identity);
                _listOfAvailableTiles.Remove(_tileToSpawnOn);
                _tileToSpawnOn.gameObject.layer = 0;
                _cooldown = true;
                _currentPrefab.GetComponent<MergePrefabController>()._parentTile = _tileToSpawnOn;
            }
        }
    }
}
