using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LOAD_SCENE : MonoBehaviour
{
    private Rigidbody2D STATIC_BODY;
    public string SCENE;
    // Start is called before the first frame update
    void Start()
    {
        STATIC_BODY = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SCENE);
        }

    }
}
