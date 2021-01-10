using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ENEMY_PEICZONTKA : MonoBehaviour
{
    public float DISTANCE;
    public GameObject PLAYER;
    public float SPEED;
    public Rigidbody2D PHYSICS_BODY;
    public GameObject EXPLODE;
    // Start is called before the first frame update
    void Start()
    {
        PHYSICS_BODY = GetComponent<Rigidbody2D>();
        PLAYER = GameObject.Find("PLAYER");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (Vector2.Distance(PHYSICS_BODY.position, PLAYER.GetComponent<Rigidbody2D>().position) < DISTANCE)
        {
            Vector2 DIR = PHYSICS_BODY.position - PLAYER.GetComponent<Rigidbody2D>().position;
            PHYSICS_BODY.AddForceAtPosition(-DIR.normalized * SPEED, PLAYER.GetComponent<Rigidbody2D>().position);
        }
        if(PHYSICS_BODY.IsTouching(PLAYER.GetComponent<Collider2D>()))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1,0,0,0.2F);
        Gizmos.DrawWireSphere(transform.position, DISTANCE);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
            if (other.gameObject.tag == "PUKE")
            {
            GameObject OBJ;
            OBJ = Instantiate(EXPLODE, transform.position, transform.rotation);
            Destroy(OBJ, 15F);
            Destroy(gameObject);
            Destroy(other.gameObject, 0.5f);
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
