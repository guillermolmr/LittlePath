using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerSRC : MonoBehaviour
{
    //[HideInInspector]
    public float Health_ = 0.0f;
    public float Max_Health_ = 0.0f;

    [SerializeField]
    private float Speed_ = 0.0f;
    [SerializeField]
    private float Sprint_ = 0.0f;

    private Vector3 Velocity_ = new Vector3();

    private Transform MyTransform_;

    void Start()
    {
        MyTransform_ = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        Velocity_.x = Input.GetAxis("Horizontal");
        Velocity_.y = Input.GetAxis("Vertical");
        Velocity_.Normalize();
        MyTransform_.position += Velocity_ * Speed_ * Time.deltaTime;
        MyTransform_.position += Velocity_ * (Sprint_ * Input.GetAxis("Fire3")) * Time.deltaTime;
        MyTransform_.rotation = Quaternion.identity;
        //print(Velocity_ );
    }
}
