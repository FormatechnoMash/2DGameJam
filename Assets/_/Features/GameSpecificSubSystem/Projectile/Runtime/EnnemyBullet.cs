using UnityEngine;
namespace Projectile.Runtime
{
    public class EnnemyBullet : MonoBehaviour
    {
      
        // Update is called once per frame
        void FixedUpdate()
        {
            
            _countTime += Time.deltaTime;
            if (_countTime > _maxTimeAlive)
            {
                _countTime = 0;
                gameObject.SetActive(false);
            }
            
        }

        public void Launch(Vector3 targetposition)
        {
            Vector2 direction= ((Vector2)(targetposition - transform.position)).normalized;
            _rb.linearVelocity = direction * _speed;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Player") || collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                
                gameObject.SetActive(false);
               
            }

        }

        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _speed;
        [SerializeField] private float _maxTimeAlive;
        private float _countTime;
        [SerializeField] private GameObject _canon;
        
    }
}

