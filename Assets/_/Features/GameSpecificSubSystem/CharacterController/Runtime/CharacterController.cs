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
        }

        // Update is called once per frame
        void Update()
        {
           ;
                
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
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                _compteurJump = 0;
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
        // public Vector3 GetMousePosition()
        // {
        //     Vector3 mousePosition = Input.mousePosition;
        //     Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //     mouseWorldPosition.z = 0;
        //     return ;
        // }
        #endregion
        
        
        #region private
        private Animator _animator;
        private SpriteRenderer _renderer;
        private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _speed=0;
        [SerializeField] private float _jumpForce=0;
        private int _compteurJump = 0;
      

        #endregion
    }
}
