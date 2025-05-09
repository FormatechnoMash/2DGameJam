using Projectile.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CharacterController.Runtime
{
    public class CharacterController : MonoBehaviour
    {
        #region public

        public Image healthBar;
        public Sprite[] healthSprites;

        
        #endregion
         
         
         
        #region Unity APi4
        void Awake()
        {
           // _animator = GetComponent<Animator>();
            _renderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            if(_rigidbody2D == null) throw new MissingComponentException("Rigidbody2D not found");
            _jetPackForce = 0.01f;
            _jetPackSpeed = 3;
            _maxUpwardSpeed = 8f;
            _maxJetPackTime = 5;
            _maxPistolCharge = 7;
            _healthPoints = 4; 
            _jetpackSlider.maxValue = _maxJetPackTime;
            _pistolSlider.maxValue = _maxPistolCharge;
            _deathCanvas.SetActive(false);
            retryButton.onClick.AddListener(TryAgain);
            quitButton.onClick.AddListener(Quit);
            
        }

        // Update is called once per frame
        void Update()
        {
           
            if(IsAlive==true)
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
                else
                {
                    _animationJettpackLeft.SetActive(false);
                    _animationJettpackRight.SetActive(false);
                    _audioSourceSound.Stop();
                }
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    
                    if (_actualPistolCharge <= _maxPistolCharge-1.5f && _lastTimeShot==0)
                    {
                       ShootPistol(); 
                    }
                    
                }
                if (_lastTimeShot <= 0)
                {
                    _lastTimeShot = 0;
                    _actualPistolCharge -= Time.deltaTime*4;
                }
                 if (_lastTimeShot > 0)
                 {
                     _lastTimeShot -= Time.deltaTime;
                }

                 if (_actualPistolCharge <= 0)
                 {
                     _actualPistolCharge = 0;
                 }
                 _jetpackSlider.value = _actualJetPackTime;
                 _pistolSlider.value = _actualPistolCharge;
                 if (transform.position.y <= -10)
                 {
                     DecreaseHealthpoint();
                 }
                }
            else
            {
                EndGame();
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


        public void EndGame()
        {
            _deathCanvas.SetActive(true);
            Time.timeScale = 0f;
        }

        public void TryAgain()
        {
            Time.timeScale = 1f; 
            IsAlive = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Quit()
        {
            Application.Quit();
        }
        public void Left()
        {
            // Déplace à gauche
            _rigidbody2D.linearVelocity = new Vector2(-Mathf.Abs(_speed), _rigidbody2D.linearVelocity.y);
            _renderer.flipX = true;

           
            _isFacingRight = false;
            //transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }                                     

        public void Right()
        {
            // Déplace à droite
            _rigidbody2D.linearVelocity = new Vector2(Mathf.Abs(_speed), _rigidbody2D.linearVelocity.y);
            _renderer.flipX = false;


            _isFacingRight = true;
            //transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Rotation normale
            

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

                if (_isFacingRight == false)
                {
                    _animationJettpackRight.SetActive(true);
                    _animationJettpackLeft.SetActive(false);
                }
                if (_isFacingRight == true)
                {
                    _animationJettpackLeft.SetActive(true);
                    _animationJettpackRight.SetActive(false);
                }
                _audioSourceSound.Play();
                
            }
            if (_actualJetPackTime > _maxJetPackTime)
            {
                _actualJetPackTime = _maxJetPackTime;
            }
            
        }

       
        public void ShootPistol()
        {
           /*⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠸⣶⣦⡄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⢀⣀⣀⣀⡀⢀⠀⢹⣿⣿⣆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠙⠻⣿⣿⣷⣄⠨⣿⣿⣿⡌⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⠘⣿⣿⣿⣷⣿⣿⣿⣿⣿⣶⣦⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⣠⣴⣾⣿⣮⣝⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠈⠉⠙⠻⢿⣿⣿⣿⣿⣿⣿⠟⣹⣿⡿⢿⣿⣿⣬⣶⣶⡶⠦⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⣀⣢⣙⣻⢿⣿⣿⣿⠎⢸⣿⠕⢹⣿⣿⡿⣛⣥⣀⣀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠈⠉⠛⠿⡏⣿⡏⠿⢄⣜⣡⠞⠛⡽⣸⡿⣟⡋⠉⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠰⠾⠿⣿⠁⠀⡄⠀⠀⠰⠾⠿⠛⠓⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⠠⢐⢉⢷⣀⠛⠠⠐⠐⠠⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⣀⣠⣴⣶⣿⣧⣾⠡⠼⠎⢎⣋⡄⠆⠀⠱⡄⢉⠃⣦⡤⡀⠀⠀⠀⠀
            ⠀⠀⠐⠙⠻⢿⣿⣿⣿⣿⣿⣿⣄⡀⠀⢩⠀⢀⠠⠂⢀⡌⠀⣿⡇⠟⠀⠀⢄⠀
            ⠀⣴⣇⠀⡇⠀⠸⣿⣿⣿⣿⣽⣟⣲⡤⠀⣀⣠⣴⡾⠟⠀⠀⠟⠀⠀⠀⠀⡰⡀
            ⣼⣿⠋⢀⣇⢸⡄⢻⣟⠻⣿⣿⣿⣿⣿⣿⠿⡿⠟⢁⠀⠀⠀⠀⠀⢰⠀⣠⠀⠰
            ⢸⣿⡣⣜⣿⣼⣿⣄⠻⡄⡀⠉⠛⠿⠿⠛⣉⡤⠖⣡⣶⠁⠀⠀⠀⣾⣶⣿⠐⡀
            ⣾⡇⠈⠛⠛⠿⣿⣿⣦⠁⠘⢷⣶⣶⡶⠟⢋⣠⣾⡿⠃⠀⠀⠀⠰⠛⠉⠉⠀⠀*/
           
            GameObject bullet = ObjectPool.instance.GetPooledObject();
            if (bullet != null)
            {
                if (_isFacingRight == true)
                {
                    bullet.transform.position = _muzzleRight.transform.position;
                    bullet.SetActive(true);
                }
                else if (_isFacingRight == false)
                {
                    bullet.transform.position = _muzzleLeft.transform.position;
                    bullet.SetActive(true);
                }
                BulletScript bulletScript = bullet.GetComponent<BulletScript>();
                if (bulletScript != null)
                {
                    bulletScript.Launch(_isFacingRight);
                    _audioSourceBlasterSound.PlayOneShot(_blasterSound);
                }
                
                _actualPistolCharge += 1.5f;
                _lastTimeShot = 0.2f;

            }
        }

        public void DecreaseHealthpoint()
        {
            _healthPoints--;
            UpdateHealthPoints();
            if (transform.position.y <= -10)
            {
                _healthPoints = 0;
            }
            if (_healthPoints <= 0)
            {
                IsAlive = false;
            }
        }

        public void IncreaseHealthpoint()
        {
            if (_healthPoints < 4 && _healthPoints!=0)
            {
                _healthPoints++;
                UpdateHealthPoints();
            }
            
        }

        public void UpdateHealthPoints()
        {
            if (_healthPoints == 4)
            {
                healthBar.sprite = healthSprites[0];
            }
            else if (_healthPoints == 3)
            {
                healthBar.sprite = healthSprites[1];
                
            }
            else if (_healthPoints == 2)
            {
                healthBar.sprite = healthSprites[2];
            }
            else if (_healthPoints == 1)
            {
                healthBar.sprite = healthSprites[3];
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
        [FormerlySerializedAs("_muzzle")] [SerializeField] private GameObject _muzzleRight;
        [SerializeField] private GameObject _muzzleLeft;
        private bool _isFacingRight = true;
        [SerializeField]private float _maxPistolCharge;
        private float _actualPistolCharge;
        private float _pistolChargeDecreasing;
        private float _lastTimeShot;
        private int _healthPoints;
        [SerializeField] private Slider _jetpackSlider;
        [SerializeField] private Slider _pistolSlider;
        private bool IsAlive = true;
        [SerializeField] private GameObject _deathCanvas;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button quitButton;
        [FormerlySerializedAs("_animationJettpack")] [SerializeField] private GameObject _animationJettpackLeft;
        [SerializeField] private GameObject _animationJettpackRight;
        [SerializeField] AudioSource _audioSourceBlasterSound;
        [SerializeField] private AudioSource _audioSourceSound;
        //[SerializeField] private AudioClip _jettpackSound;
        [SerializeField] private AudioClip _blasterSound;

        #endregion
    }
}
