using UnityEngine;

namespace CharacterController.Runtime
{
    public class CharacterController : MonoBehaviour
    {
         
        #region Unity APi4
        void Awake()
        {
           // _animator = GetComponent<Animator>();
           // _renderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            if(_rigidbody2D == null) throw new MissingComponentException("Rigidbody2D not found");
            _jetPackForce = 0.01f;
            _jetPackSpeed = 3;
            _maxUpwardSpeed = 8f;
            _maxJetPackTime = 5;

        }

        // Update is called once per frame
        void Update()
        {
           
                
            if (Input.GetKey(KeyCode.A))
            {
                Left();
            } 
            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                StopRunning();
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Right();
            }


            if (Input.GetKeyDown(KeyCode.Space)&& _compteurJump ==0 )
            {
                jump();
            }

            if (Input.GetKey(KeyCode.Space) && _compteurJump ==1)
                
            {
                JetPack();
            }
            
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                _compteurJump = 0;
                if (_actualJetPackTime > 0)
                {
                    _actualJetPackTime-=Time.deltaTime*2;
                }

                if (_actualJetPackTime < 0)
                {
                    _actualJetPackTime=0;
                }
                
            }
        }
        

        #endregion


        #region utils

        public void Left()
        {
            _rigidbody2D.linearVelocity = new Vector2(-_speed, _rigidbody2D.linearVelocity.y);
            //_renderer.flipX = true;
            //_animator.SetBool("IsRunning", true);
            
        }
        public void Right()
        {
            _rigidbody2D.linearVelocity = new Vector2(_speed, _rigidbody2D.linearVelocity.y);
            //_renderer.flipX = false;
            //_animator.SetBool("IsRunning", true);
            
        }
        public void StopRunning()
        {
            //_animator.SetBool("IsRunning", false);
            _rigidbody2D.linearVelocity = new Vector2(0, _rigidbody2D.linearVelocity.y);
        }

        public void jump()
        {
            _compteurJump++;
            _rigidbody2D.AddForce(new Vector2( 0f, _jumpForce), ForceMode2D.Impulse);
            
            //_animator.SetTrigger("Roll");
        }

        public void JetPack()
        {
            if (_actualJetPackTime < _maxJetPackTime)
            {
                _actualJetPackTime += Time.deltaTime;
                
                float fallSpeed = Mathf.Clamp(-_rigidbody2D.linearVelocity.y, 0f, 5f);
                float dynamicBoost = Mathf.Lerp(0f, _jetPackForce, fallSpeed / 5f);
                
                _rigidbody2D.AddForce(new Vector2(0f, (dynamicBoost + _jetPackForce) * _jetPackSpeed), ForceMode2D.Impulse);
                
                if (_rigidbody2D.linearVelocity.y > _maxUpwardSpeed)
                {
                    _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, _maxUpwardSpeed);
                }
            }
            if (_actualJetPackTime > _maxJetPackTime)
            {
                _actualJetPackTime = _maxJetPackTime;
            }
            
        }
        #endregion
        
        
        #region private
        private Animator _animator;
        private SpriteRenderer _renderer;
        private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _speed=0;
        [SerializeField] private float _jumpForce=0;
        private float _actualJetPackTime=0;
        [SerializeField] private float _maxJetPackTime;
        [SerializeField] private float _jetPackForce;
        [SerializeField] private float _jetPackSpeed;
        private int _compteurJump = 0;
        [SerializeField] private float _maxUpwardSpeed;



        #endregion
    }
}
