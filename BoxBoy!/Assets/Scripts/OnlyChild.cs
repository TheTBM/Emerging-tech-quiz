using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyChild : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject sacrifice = GameObject.FindGameObjectWithTag("Cube");

        if (sacrifice != this.gameObject)
            Destroy(sacrifice);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
