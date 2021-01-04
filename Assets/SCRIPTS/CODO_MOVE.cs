using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CODO_MOVE : MonoBehaviour
{
    private Rigidbody2D RIGID_BODY;
    public float PLAYER_SPEED = 5F;
    public SpriteRenderer PLAYER_SPRITE;
    public float PLAYER_ACCELERATION = 5F;
    public float PLAYER_JUMPFORCE = 5F;
    public float PLAYER_PUKERECOIL = 5F;
    public float BULLET_FORCE = 250F;
    public float nie_uzyje_tej_zmiennej_po_prostu_mowie_wam_ze_bola_mnie_oczy_po_2_takich_zmiennych_czemu_wy_tak_robicie_kodolowo = 0F;
    public float INPUT_BIAS = 0.05F;
    public LayerMask LAYER_MASK;
    float LAST_INPUT = 0F;
    public GameObject BULLET;
    public Transform CAM_MOVE;
    public int FRAMES = 0;

    int CURFRAME = 0;
    // Start is called before the first frame update
    void Start()
    {
        RIGID_BODY = GetComponent<Rigidbody2D>();
        PLAYER_SPRITE = GetComponent<SpriteRenderer>();
        CAM_MOVE = transform.Find("CAM_MOVE");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SHOOT();
        }

        if(PLAYER_SPRITE.flipX)
            CAM_MOVE.localPosition = new Vector3(-3, 0, 0);
        else
            CAM_MOVE.localPosition = new Vector3(3, 0, 0);
    }
    private bool IS_GROUNDED()
    {
        /*  return Physics2D.Raycast(RIGID_BODY.position, Vector2.down, 0.7F, LAYER_MASK) ||
              Physics2D.Raycast(RIGID_BODY.position - new Vector2(0.4F, 0F), Vector2.down, 0.7F, LAYER_MASK) ||
              Physics2D.Raycast(RIGID_BODY.position - new Vector2(-0.4F, 0F), Vector2.down, 0.7F, LAYER_MASK);*/
        return Physics2D.OverlapCircle(RIGID_BODY.position, 0.6F, LAYER_MASK);
    } //prawdofa³sz wziêty za to czy gracz dotyka ziemi
    private float CLAMPED_INPUT()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > INPUT_BIAS)
            return Input.GetAxis("Horizontal");
        else
            return 0;
    } //input wziety po obliczeniu dead zone
    private void FixedUpdate()
    {
        
        //FRAMES++;
        
        if(IS_GROUNDED())
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > INPUT_BIAS)
            {
                LAST_INPUT = Input.GetAxis("Horizontal");
                RIGID_BODY.AddForce(new Vector2(Input.GetAxis("Horizontal") * PLAYER_ACCELERATION, 0));
            }
            CURFRAME ++;
            if (CURFRAME > 4)
            {
                RIGID_BODY.velocity = new Vector2(Mathf.Clamp(RIGID_BODY.velocity.x, -PLAYER_SPEED, PLAYER_SPEED), RIGID_BODY.velocity.y);
                CURFRAME = 0;
            }
        }


        if (Input.GetAxis("Horizontal") > 0.1F)
            PLAYER_SPRITE.flipX = false;

        else if (Input.GetAxis("Horizontal") < -0.1F)
            PLAYER_SPRITE.flipX = true;
        if (Input.GetButtonDown("Jump") || Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0.01F && IS_GROUNDED())
        {
            RIGID_BODY.AddForce(Vector2.up * PLAYER_JUMPFORCE);
            GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        }

    }
    private void SHOOT()
    {
        RIGID_BODY.AddForce(Vector2.right * PLAYER_PUKERECOIL * (PLAYER_SPRITE.flipX ? 1 : -1));
        GameObject obj;
        obj = Instantiate(BULLET, transform.position, transform.rotation);
        obj.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        obj.GetComponent<Rigidbody2D>().AddForce(Vector2.left * BULLET_FORCE * (PLAYER_SPRITE.flipX ? 1 : -1));
        Destroy(obj, 5F);
    } // strzelanko :)
}
