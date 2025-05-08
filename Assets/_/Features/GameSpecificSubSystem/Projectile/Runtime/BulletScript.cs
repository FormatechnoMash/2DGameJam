using UnityEngine;


namespace Projectile.Runtime
{
    public class BulletScript : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        void Start()
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

        public void Launch(bool isFacingRight)
        {
            Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;
            _rb.linearVelocity = direction * _speed ;
            if (isFacingRight)
            {
                
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                _rb.linearVelocity =direction * _speed *1.3f;
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            if (_particleSystem != null && !_particleSystem.isPlaying)
            {
                _particleSystem.Play();
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            
            if (collider.gameObject.layer == LayerMask.NameToLayer("Ennemy") )
            {
                TurretHp controller = collider.gameObject.GetComponent<TurretHp>();
                controller.TurretDeath();
                gameObject.SetActive(false);
                
            }

            if (collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                gameObject.SetActive(false);
            }

        }

        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _speed;
        [SerializeField] private float _maxTimeAlive;
        private float _countTime;
    }
}