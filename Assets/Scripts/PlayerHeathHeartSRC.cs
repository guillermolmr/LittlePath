using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeathHeartSRC : MonoBehaviour
{

    private MyPlayerSRC MyPlyaerHealth_ = null;

    private Transform MyTransform_;

    private float NewScale = 0.0f;
    private Vector3 NewScaleVector = new Vector3();

    void Start()
    {
        MyPlyaerHealth_ = GameObject.FindGameObjectWithTag("Player").GetComponent<MyPlayerSRC>();
        MyTransform_ = GetComponent<Transform>();
        updateScale();
    }

    public void updateScale()
    {
        NewScale = MyPlyaerHealth_.Health_ / MyPlyaerHealth_.Max_Health_;
        NewScaleVector.x = NewScale;
        NewScaleVector.y = NewScale;
        NewScaleVector.z = 0.0f;
        MyTransform_.localScale = NewScaleVector;
    }
}
