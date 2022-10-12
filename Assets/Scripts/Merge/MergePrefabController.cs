using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MergePrefabController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _evoLevelObjectsList = new List<GameObject>();
    private GameObject _currentRaycastObject;
    private Vector3 _currentPrefabPosition;
    private Vector3 _newtPrefabPosition;
    [SerializeField] LayerMask _layer;
    public int _prefabLevel;
    private Camera _camera;
    public GameObject _parentTile;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnMouseDown()
    {
        _currentPrefabPosition = transform.position;
        gameObject.layer = 0;
    }

    private void OnMouseDrag()
    {
        transform.position = new Vector3(_camera.ScreenToWorldPoint(Input.mousePosition).x, _camera.ScreenToWorldPoint(Input.mousePosition).y, transform.position.z);
    }

    private void OnMouseUp()
    {
        RaycastHit _hit;
        if (RayHit(out _hit))
        {
            Debug.Log("hit the: " + _hit.collider);
            _newtPrefabPosition = new Vector3(_hit.collider.transform.position.x, _hit.collider.transform.position.y, -0.01f);
            _currentRaycastObject = _hit.collider.gameObject;
            if (_currentRaycastObject.layer == 7)//tile
            {
                transform.position = _newtPrefabPosition;
                gameObject.layer = 8;
                _parentTile.layer = 7;
                MergeSpawnManager.Instance._listOfAvailableTiles.Add(_parentTile);
                MergeSpawnManager.Instance._listOfAvailableTiles.Remove(_currentRaycastObject);
                _parentTile = _currentRaycastObject;
                _currentRaycastObject.layer = 0;
            }
            else if (_currentRaycastObject.layer == 8)//turret
            {
                MergePrefabController currenController = _currentRaycastObject.GetComponent<MergePrefabController>();
                if (currenController._prefabLevel == _prefabLevel)
                {
                    currenController._prefabLevel++;
                    if (_evoLevelObjectsList.Count <= currenController._prefabLevel)
                    {
                        Debug.Log("IT`S MAXIMUM LEVEL");
                        currenController._prefabLevel--;
                        OnNullResult();
                        return;
                    }
                    currenController.ActivateNextLevelSkin();
                    MergeSpawnManager.Instance._listOfAvailableTiles.Add(_parentTile);
                    _parentTile.layer = 7;
                    Destroy(gameObject);
                }
                else
                {
                    OnNullResult();
                }
            }
        }
        else
        {
            OnNullResult();
        }
    }

    public void ActivateNextLevelSkin()
    {   
        
        for (int i = 0; i < _evoLevelObjectsList.Count; i++)
        {
            if(i == _prefabLevel)
            {
                _evoLevelObjectsList[i].SetActive(true);
            }
            else
            {
                _evoLevelObjectsList[i].SetActive(false);
            }
        }
    }

    private bool RayHit(out RaycastHit hit)
    {
        Vector2 mousePosition = Input.mousePosition;
        Ray ray = _camera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layer))
        {
            return true;
        }
        return false;
    }

    private void OnNullResult()
    {
        transform.position = _currentPrefabPosition;
        gameObject.layer = 8;//turret
    }
}
