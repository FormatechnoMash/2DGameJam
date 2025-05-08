using UnityEngine;

namespace Projectile.Runtime
{
    public class TurretHp : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        _turretHp = 5;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void TurretDeath()
        {
            _turretHp--;
            if (_turretHp == 0)
            {
                Destroy(this.gameObject);
            }
        }

        #region private
        
        private int _turretHp;

        #endregion
    }
}
