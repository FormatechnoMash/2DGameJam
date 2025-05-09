using UnityEngine;

namespace Projectile.Runtime
{
    public class EnnemyBullet : MonoBehaviour
    {
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _particleSystem = GetComponentInChildren<ParticleSystem>();
        }

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
            Vector2 direction = ((Vector2)(targetposition - transform.position)).normalized;
            _rb.linearVelocity = direction * _speed;

            bool isRight = targetposition.x > transform.position.x;

            
            transform.localScale = new Vector3(
                isRight ? Mathf.Abs(transform.localScale.x) : -Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );

          
            if (_particleSystem != null)
            {
                _particleSystem.transform.localRotation = Quaternion.Euler(0, isRight ? 0 : 180, 0);
                if (!_particleSystem.isPlaying)
                {
                    _particleSystem.Play();
                }
            }
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
        [SerializeField] private ParticleSystem _particleSystem;
        
    }
}

