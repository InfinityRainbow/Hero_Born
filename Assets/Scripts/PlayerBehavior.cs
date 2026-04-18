using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
   
    public float MoveSpeed = 10f;
    public float RotateSpeed = 75f;
    private float _vInput;
    private float _hInput;
    private Rigidbody _rb;
    public float JumpVelocity = 5f;
    private bool _isJumping;
    //make sure once jump
    public float DistanceToGround = 0.1f;
    public LayerMask GroundLayer;
    private CapsuleCollider _col;
    //shoot
    public GameObject Bullet;
    public Transform muzzleTransform;
    public float BulletSpeed = 50f;
    private bool _isShooting;
    //be hitted
    private GameBehavior _gameManager;




    //委托相关以及触发事件
    public delegate void JumpingEvent();
    public event JumpingEvent playerJump;


    public HitFlash hitFlash;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
        muzzleTransform = transform.Find("Gun_P/GunMuzzle");
        if (muzzleTransform == null)
        {
            Debug.Log("no muzzle");
        }
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameBehavior>();
    }



    // Update is called once per frame
    void Update()
    {
        _vInput = Input.GetAxis("Vertical") * MoveSpeed;
        _hInput = Input.GetAxis("Horizontal") * RotateSpeed;
        _isJumping |= Input.GetKeyDown(KeyCode.Space);
        _isShooting |= Input.GetKeyDown(KeyCode.J);
        //this.transform.Translate(Vector3.forward * _vInput * Time.deltaTime);
        //this.transform.Rotate(Vector3.up * _hInput * Time.deltaTime);
    }


    private void FixedUpdate()
    {
        
        Vector3 rotation = Vector3.up * _hInput;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        _rb.MovePosition(this.transform.position + this.transform.forward * _vInput * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * angleRot);

        if(IsGrounded()&&_isJumping)
        {
            _rb.AddForce(Vector3.up * JumpVelocity, ForceMode.Impulse);

            //委托与触发事件测试
            playerJump();
        }
        _isJumping = false;
        
        //射击优化
        if (_isShooting)
        {
            // 使用玩家的前向向量，确保子弹生成在玩家的正前方
            //Vector3 bulletSpawnPosition = transform.position + transform.forward * 1.5f;
            Vector3 bulletSpawnPosition = muzzleTransform.position;
            GameObject newBullet = Instantiate(Bullet, bulletSpawnPosition, this.transform.rotation);

            Rigidbody BulletRB = newBullet.GetComponent<Rigidbody>();
            BulletRB.velocity = this.transform.forward * BulletSpeed;
        }
        _isShooting = false;
        
        
        //if (_isShooting)
        //{
        //    GameObject newBullet = Instantiate(Bullet, this.transform.position + 
        //        new Vector3(0, 0, 1), this.transform.rotation) as GameObject;

        //    Rigidbody BulletRB = newBullet.GetComponent<Rigidbody>();
        //    BulletRB.velocity = this.transform.forward * BulletSpeed;
        //}
        //_isShooting = false;

    }



    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);

        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, DistanceToGround, GroundLayer, QueryTriggerInteraction.Ignore);
        return grounded;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name=="Enemy"||collision.gameObject.name=="EnemyBullet(Clone)")
        {
            
            _gameManager.HP -= 1;
            hitFlash.TakeDamage();
        }
    }
}
