using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class TurretBehavior : MonoBehaviour
{

    AudioSource turret;
    public AudioClip turretFire;
    public AudioClip turretDetecting;

    public float Range;

    public Transform Target;

    bool Detected = false;

    Vector3 Direction;

    public GameObject AlarmLight;

    public GameObject Gun;

    public GameObject Bullet;

    public Transform ShootPoint;

    public float FireRate;

    public float Force;

    float nextTimeToFire = 0;

    public GameManager gm;
    public GameObject playerChar;
    private void Awake()
    {
        gm = GameManager.Instance;
    
    }
    IEnumerator Start()
    {
        enabled = false;

        while(gm.CurrentState != GameManager.GMState.GAME)
        {
            yield return null;
        }

        playerChar = gm.CurrentPlayer;
        Target = playerChar.transform;
        enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetPos = Target.position;
   
        Direction = targetPos - (Vector2)transform.position;
        
        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, Direction, Range);

        if (rayInfo)
        {
            if (rayInfo.collider.gameObject.tag == "Player")
            {
                if (Detected == false)
                {
                    AlarmLight.GetComponent<SpriteRenderer>().color = Color.red;
                    turret.PlayOneShot(turretDetecting);
                    Detected = true;
                    Debug.Log("Being detected");
                }
            }

            else
            {
                if (Detected == true)
                {
                    Detected = false;
                    AlarmLight.GetComponent<SpriteRenderer>().color = Color.green;
                }
            }

            if (Detected)
            {
               
                Gun.transform.up = Direction;
                if (Time.time > nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1 / FireRate;
                    turret.PlayOneShot(turretFire);
                    shoot();
                }
           
               
            }
        }

        

       

       
    }

    void shoot()
    {
        GameObject BulletIns = Instantiate(Bullet, ShootPoint.position, Quaternion.identity);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(Direction * Force);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, Range);
    }

}
