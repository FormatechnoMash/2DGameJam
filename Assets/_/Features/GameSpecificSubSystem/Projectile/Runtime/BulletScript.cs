using UnityEngine;


namespace Projectile.Runtime
{
    public class BulletScript : MonoBehaviour
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

        public void Launch(bool isFacingRight)
        {
            Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;
            _rb.linearVelocity = direction * _speed ;
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