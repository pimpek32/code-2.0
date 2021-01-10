using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class END_CUTSCENE : MonoBehaviour
{
    private VideoPlayer VIDEO_PLAYER;
    // Start is called before the first frame update
    void Start()
    {
        VIDEO_PLAYER = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if((VIDEO_PLAYER.frame) > 0 && (VIDEO_PLAYER.isPlaying == false))
        {
            Destroy(gameObject);
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            Destroy(gameObject);
        }
    }
}
