using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform PatroRoute;
    public List<Transform> Locations;
    private int _locationindex = 0;
    private NavMeshAgent _agent;
    public Transform Player;


    public HitFlash hitFlash;
    private bool _isShooting;
    public GameObject Bullet;
    public Transform muzzleTransform;
    public float BulletSpeed = 50f;
    //private MeshRenderer meshRenderer;
    //private Material originalMaterial;
    //public Material flashMaterial; // 在 Inspector 中指定一个白色/红色材质
    //public float flashDuration = 0.8f;

    private int _lives = 3;
    public int EnemyLives
    {
        get { return _lives; }

        private set
        {
            _lives = value;
            if(_lives<=0)
            {
                
                Destroy(this.gameObject);
                Debug.Log("Enemy dead.");
            }
        }
    }


    // Start is called before the first frame update
    
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        InitializePatrolRoute();
        Player = GameObject.Find("Player").transform;
        _isShooting = false;
        muzzleTransform = transform.Find("Cannon_E/CannonMuzzle");
        //meshRenderer = GameObject.Find("Enemy").GetComponent<MeshRenderer>();
        //originalMaterial = meshRenderer.material; // 保存原始材质

    }



    // Update is called once per frame
    void Update()
    {
        if(_agent.remainingDistance<0.2f&& !_agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
        
    }

    //敌人射击实现
    private void FixedUpdate()
    {

        if (_isShooting)
        {
            Vector3 bulletSpawnPosition = muzzleTransform.position;
            var heading = (Player.position - bulletSpawnPosition).normalized;
            Quaternion bulletRotation = Quaternion.LookRotation(heading);
            GameObject newBullet = Instantiate(Bullet, bulletSpawnPosition, bulletRotation)as GameObject;

            Rigidbody BulletRB = newBullet.GetComponent<Rigidbody>();

            BulletRB.velocity = heading * BulletSpeed;

        }
        _isShooting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            _agent.destination = Player.position;
            Debug.Log("Player detected - attack!!");
            //敌人射击
            _isShooting = true;
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player out of range,rusume patrol");
        }
    }
    void InitializePatrolRoute()
    {
        foreach (Transform child in PatroRoute)
        {
            Locations.Add(child);
        }
    }




    void MoveToNextPatrolLocation()
    {
        if (Locations.Count == 0)
            return;

        _agent.destination = Locations[_locationindex].position;
        _locationindex = (_locationindex + 1) % Locations.Count;
    }


    //受击特效,敌人变红并微小抖动
    //private void HitByRed()
    //{
    //    Material _currentColor = EnemyMat;

    //}
    //IEnumerator FlashRoutine()
    //{
    //    // 切换到闪烁材质
    //    meshRenderer.material = flashMaterial;

    //    // 等待短暂时间
    //    yield return new WaitForSeconds(flashDuration);

    //    // 恢复原始材质
    //  meshRenderer.material = originalMaterial;
    //}
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name=="Bullet(Clone)")
        {
            EnemyLives -= 1;
            hitFlash.TakeDamage();
            Debug.Log("Critical hit!!");
           
        }
    }
}
