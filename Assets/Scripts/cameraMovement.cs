using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{

    public Transform MyTarget_;

    public float MySquareOffSet = 0.0f;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float PercentageDistanceMove_ = 0.0f;

    private Transform MyTransform_;

    private Vector3 MyVelocity_ = new Vector3();

    private float MyDistance_ = 0.0f;

    void Start()
    {
        MyTransform_ = GetComponent<Transform>();
    }

    void Update()
    {
        MyDistance_ = (MyTransform_.position - MyTarget_.position).magnitude;
        //print(MyDistance_);
        
        if ((MyDistance_ > MySquareOffSet) && Time.timeScale>0) {
            MyVelocity_ = (MyTarget_.position - MyTransform_.position) * PercentageDistanceMove_;
            MyVelocity_.z = 0.0f;
            //print(MyVelocity_);
            MyTransform_.position += MyVelocity_ * Time.deltaTime;
        }
    }

}
