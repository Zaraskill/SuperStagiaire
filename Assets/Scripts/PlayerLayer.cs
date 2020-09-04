using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayer : MonoBehaviour
{
    public GameObject player;
    public Transform upSide;
    public Transform downSide;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y > upSide.position.y)
        {
            player.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        else if (player.transform.position.y > downSide.position.y)
        {
            player.GetComponent<SpriteRenderer>().sortingOrder = 4;
        }
        else
        {
            player.GetComponent<SpriteRenderer>().sortingOrder = 7;
        }
    }
}
