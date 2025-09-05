using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
            Debug.LogError("PlayerCam: No player assigned.");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, -10);
    }
}
