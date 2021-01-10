using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TURRET_MK4 : MonoBehaviour
{
    public float DISTANCE;
    public GameObject PLAYER;
    public float SPEED;
    public Rigidbody2D PHYSICS_BODY;
    public GameObject EXPLODE;
    private float COOLDOWN = 1F;
    private float CDOWN;
    public GameObject PROJECTILE;
    private Transform PROJECTILE_BEGIN;
    private int HEALTH = 3;
    // Start is called before the first frame update
    void Start()
    {
        PHYSICS_BODY = GetComponent<Rigidbody2D>();
        PLAYER = GameObject.Find("PLAYER");
        CDOWN = COOLDOWN;
        PROJECTILE_BEGIN = transform.Find("PROJECTILE_BEGIN");
    }

    // Update is called once per frame
    void Update()
    {
        COOLDOWN -= Time.deltaTime;
        if (HEALTH <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        //if (Vector2.Distance(PHYSICS_BODY.position, PLAYER.GetComponent<Rigidbody2D>().position) < DISTANCE && COOLDOWN < 0F)
        //{
          //  COOLDOWN = CDOWN;
            
        //}
        if (PHYSICS_BODY.IsTouching(PLAYER.GetComponent<Collider2D>()))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.2F);
        Gizmos.DrawWireSphere(transform.position, DISTANCE);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "PUKE")
        {
            HEALTH--;
            for (int i = 0; i < 2; i++)
            {
                Vector2 DIR = PHYSICS_BODY.position - PLAYER.GetComponent<Rigidbody2D>().position;
                GameObject CUM;
                CUM = Instantiate(PROJECTILE, PROJECTILE_BEGIN.position, Quaternion.identity);
                CUM.GetComponent<Rigidbody2D>().AddForceAtPosition(-DIR.normalized + new Vector2(3, 2F) * SPEED, PLAYER.GetComponent<Rigidbody2D>().position);
            }
            GameObject OBJ;
            OBJ = Instantiate(EXPLODE, other.transform.position, other.transform.rotation);
            Destroy(OBJ, 15F);
            Destroy(other.gameObject);
        }

    }
    private void OnDrawGizmosSelected()
    {
        if (Vector2.Distance(PHYSICS_BODY.position, PLAYER.GetComponent<Rigidbody2D>().position) < DISTANCE)
        {

            Gizmos.color = new Color(1, 0, 0, 0.6F);
            Gizmos.DrawWireSphere(transform.position, DISTANCE);
        }
        else
        {

            Gizmos.color = new Color(1, 1, 0, 0.6F);
            Gizmos.DrawWireSphere(transform.position, DISTANCE);
        }
    }
}
