using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    float newX;

    // Update is called once per frame
    void Update()
    {
        newX = player.transform.position.x;
        gameObject.transform.position = new Vector3(newX, gameObject.transform.position.y, gameObject.transform.position.z);
    }
}
