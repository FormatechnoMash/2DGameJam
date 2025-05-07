using System.Collections.Generic;
using UnityEngine;

namespace Projectile.Runtime
{
    public class ObjectPool : MonoBehaviour
    {
        #region public
        
        public static ObjectPool instance;
        
        #endregion
        
        #region Unity API

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        void Start()
        {
            for (int i = 0; i < _amountToPool; i++)
            {
                GameObject obj = Instantiate(_projectilePrefab);
                obj.SetActive(false);
                _pooledObjects.Add(obj);
            }
        }
        
        #endregion
        
        #region Utils

        public GameObject GetPooledObject()
        {
            for (int i = 0; i < _pooledObjects.Count; i++)
            {
                if (!_pooledObjects[i].activeInHierarchy)
                {
                    return _pooledObjects[i];
                }
            }

            return null;
        }
        
        #endregion
        
        #region private
        
        private List<GameObject> _pooledObjects= new List<GameObject>();
        private int _amountToPool = 30;
        [SerializeField] private GameObject _projectilePrefab;

        #endregion
    }
}
