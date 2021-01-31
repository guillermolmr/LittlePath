using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSRC : MonoBehaviour
{

    //public int Score_;
    public float Health_Recovery = 0.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<MyPlayerSRC>().Health_ += Health_Recovery;
            if (collision.gameObject.GetComponent<MyPlayerSRC>().Health_ > collision.gameObject.GetComponent<MyPlayerSRC>().Max_Health_)
            {
                collision.gameObject.GetComponent<MyPlayerSRC>().Health_ = collision.gameObject.GetComponent<MyPlayerSRC>().Max_Health_;
            }
            Destroy(this.gameObject, 0.0f);
            GameObject.FindGameObjectWithTag("Health").GetComponent<PlayerHeathHeartSRC>().updateScale();
        }
    }
}
