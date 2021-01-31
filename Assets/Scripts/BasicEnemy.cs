using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
  //[HideInInspector]


  [SerializeField]
  private float Speed_ = 0.0f;


  private Vector3 Velocity_ = new Vector3();

  private Transform MyTransform_;
  private bool targetPoint1_=false;
  [SerializeField]
  private Transform point1_;
  [SerializeField]
  private Transform point2_;
  [SerializeField]
  private float RadiusTarget_;

  void Start()
  {
    MyTransform_ = GetComponent<Transform>();
  }

  void FixedUpdate()
  {
    if (targetPoint1_)
    {
      if(Vector2.Distance(MyTransform_.position,point1_.position)> RadiusTarget_)
      {
        Velocity_ = point1_.position - MyTransform_.position;
      }
      else
      {
        targetPoint1_ = false;
      }
    }
    else
    {
      if (Vector2.Distance(MyTransform_.position, point2_.position) > RadiusTarget_)
      {
        Velocity_ = point2_.position - MyTransform_.position;
      }
      else
      {
        targetPoint1_ = true;
      }
    }
    //Velocity_.x = Input.GetAxis("Horizontal");
    //Velocity_.y = Input.GetAxis("Vertical");
    Velocity_.Normalize();
    MyTransform_.position += Velocity_ * Speed_ * Time.deltaTime;
    MyTransform_.rotation = Quaternion.identity;
    //print(Velocity_ );
  }
}
