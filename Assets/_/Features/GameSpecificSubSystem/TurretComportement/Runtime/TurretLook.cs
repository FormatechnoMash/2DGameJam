using Projectile.Runtime;
using UnityEngine;
using UnityEngine.Serialization;

namespace TurretComportement.Runtime
{
    public class TurretLook : MonoBehaviour
    {
        #region public
        
        
        #endregion
        
        
        #region Unity API
        void Start()
        {
        
        }

        
        void Update()
        {
            if (_target != null)
            {
                _countDown+=Time.deltaTime;
                LookAt();
               
            }
        }
        #endregion

        #region Utils

        public void LookAt()
        {
            float sqrDistance = (transform.position - _target.transform.position).sqrMagnitude;

            if (sqrDistance <= _range * _range)
            {
                Vector3 direction = _target.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
                if (_countDown >= _timeBetweenShots)
                {
                    _countDown = 0;
                    Shoot();
                }
            }
        }

        public void Shoot()
        {
            GameObject bullet = TurretObjectPool.instance.GetPooledObject();
            if (bullet != null)
            {
                
                    bullet.transform.position = _muzzle.transform.position;
                    bullet.SetActive(true);
                    
                EnnemyBullet bulletScript = bullet.GetComponent<EnnemyBullet>();
                if (bulletScript != null)
                {
                    bulletScript.Launch(_target.transform.position);
                }
                

            }
        }
        
        #endregion
        
        #region private

        
        [SerializeField] private float _range;
        private float _countDown;
        [SerializeField]private float _timeBetweenShots;
        [SerializeField]private GameObject _muzzle;
        [SerializeField] private GameObject _target;
        #endregion
    }
}
