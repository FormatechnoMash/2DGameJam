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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ennemy"))
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