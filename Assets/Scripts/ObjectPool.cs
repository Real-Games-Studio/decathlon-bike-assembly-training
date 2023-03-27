using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> _poolPrefabs = new List<GameObject>();
    [SerializeField] private int _poolSize = 1000;
    private List<GameObject> _poolList = new List<GameObject>();
    private Transform _spawnObjectsParent;


    protected virtual void Awake()
    {
        CreateParent();
        InstantiateObjects();
           
    }

    private void CreateParent()
    {
        if (_poolPrefabs.Count <= 0)
        {
            return;
        }

        _spawnObjectsParent = new GameObject(_poolPrefabs[0].name + "s").transform;
    }

    
    void InstantiateObjects()
    {
        if (_poolPrefabs.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < _poolSize; i++)
        {
            int randomIndex = Random.Range(0, _poolPrefabs.Count);
            GameObject temporaryObject = Instantiate(_poolPrefabs[randomIndex], transform.position, Quaternion.identity);
            temporaryObject.name = temporaryObject.name + i;
            temporaryObject.transform.parent = _spawnObjectsParent;           
            //temporaryObject.SetActive(false);           
            _poolList.Add(temporaryObject);
            
        }
    }

    
    public GameObject GetObject()
    {
        for (int i = 0; i < _poolList.Count; i++) 
        {            
            if (_poolList[i].activeSelf == false)
            {
                return _poolList[i];
                
            }
        }

        return null; 

    }
}
