using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Variables")]
    public float interval;
    private float shootTimer;
    public  float maxReloadTime;
    private float reloadTimer;
    public int maxBulletNumber;
    public int currentbulletNumber;
    private bool isReload;
    private Vector2 mousePosition;
    private Vector2 direction;
    [Header("Components")]
    public GameObject bulletPrefab;
    public GameObject shellPrefab;
    private Transform muzzle;
    private Transform shellPosition;
    private Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        maxBulletNumber = 5;
        animator =GetComponent<Animator>();
        muzzle = transform.Find("Muzzle");
        shellPosition = transform.Find("Shell");

    }

    private void OnEnable()
    {
        currentbulletNumber = maxBulletNumber;
    }
    private void Update()
    {
        
        Direction(); //Control the rotation direction of the gun
    }

    private void FixedUpdate()
    {
        if (!Reload())
        Shoot(); //Cooldown shooting system to avoid rapid, continuous firing like a spray.
    }

    private void Direction() 
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - new Vector2(transform.position.x, transform.position.y)).normalized;
        transform.right = direction;
        if (mousePosition.x < transform.position.x)
            transform.localScale = new Vector3(transform.localScale.x, -Mathf.Abs(transform.localScale.y), 1);
        if (mousePosition.x > transform.position.x)
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), 1);

    }
    private void Shoot()
    {
        if(shootTimer != 0)
        {
            shootTimer -= Time.deltaTime;
            if(shootTimer <= 0)
                shootTimer = 0;
        }
        if(Input.GetMouseButton(0))
        {
            if(shootTimer == 0)
            {
                Fire();
                shootTimer = interval;
            }
        }
    }
    private void Fire()
    {
        currentbulletNumber--;
        animator.SetTrigger("Shoot");
        GameObject bullet = GameManager.instance.poolManager.Get(0);
        bullet.GetComponent<Bullet>().SetSpeed(direction);//Call the flight function of the bullet
        bullet.transform.position= muzzle.position;
        GameObject shell=GameManager.instance.poolManager.Get(1);
        shell.transform.position = shellPosition.position;
    }

    private bool Reload()
    {
        if(currentbulletNumber <= 0)
        {
            isReload = true;
            reloadTimer -= Time.deltaTime;
            Debug.Log("IsReload");
        }
        if(reloadTimer <= 0)
        {
            reloadTimer= maxReloadTime;
            isReload = false;
            currentbulletNumber = maxBulletNumber;
        }

        return isReload;
    }
}
