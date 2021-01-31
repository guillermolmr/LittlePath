using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyPlayerSRC : MonoBehaviour
{

  public float Health_ = 1.0f;
  public float Max_Health_ = 100.0f;

  [SerializeField]
  private float Speed_ = 0.0f;
  [SerializeField]
  private float Sprint_ = 0.0f;

  private Vector3 Velocity_ = new Vector3();
  private Vector3 ScalePatch_ = new Vector3();

  private Transform MyTransform_;
  private Animator MyAnimator_;

  private bool ImMoving_ = false;
  private int MyDirection_ = 0;

  private float blink = 0;
  private SpriteRenderer sr;

  void Start()
  {
    MyTransform_ = GetComponent<Transform>();
    MyAnimator_ = GetComponent<Animator>();
    ScalePatch_.x = 1.0f;
    ScalePatch_.y = 1.0f;
    ScalePatch_.z = 1.0f;
    Health_ = Max_Health_;
    sr = GetComponent<SpriteRenderer>();
  }

  private void Update()
  {
    if (Velocity_.x >= 0.7f)
    {
      MyDirection_ = 2;
      ScalePatch_.x = 1.0f;
    }
    else if (Velocity_.x <= -0.7f)
    {
      MyDirection_ = 3;
      ScalePatch_.x = 1.0f;
    }
    else if (Velocity_.y >= 0.7f)
    {
      MyDirection_ = 1;
      ScalePatch_.x = -1.0f;
    }
    else if (Velocity_.y <= -0.7f)
    {
      MyDirection_ = 0;
      ScalePatch_.x = 1.0f;
    }

    MyAnimator_.SetBool("IsMoving", ImMoving_);
    MyAnimator_.SetInteger("Direction", MyDirection_);
    MyTransform_.localScale = ScalePatch_;

    if (blink > 0)
    {
      sr.enabled = !sr.enabled;
      blink -= Time.deltaTime;
    }
    else
    {
      sr.enabled = true;
    }
  }

  void FixedUpdate()
  {
    Velocity_.x = Input.GetAxis("Horizontal");
    Velocity_.y = Input.GetAxis("Vertical");
    Velocity_.Normalize();
    MyTransform_.position += Velocity_ * Speed_ * Time.deltaTime;
    MyTransform_.position += Velocity_ * (Sprint_ * Input.GetAxis("Fire3")) * Time.deltaTime;
    MyTransform_.rotation = Quaternion.identity;
    if (Velocity_.x == 0 && Velocity_.y == 0 && Velocity_.z == 0)
    {
      ImMoving_ = false;
    }
    else
    {
      ImMoving_ = true;
    }

    //print(Velocity_);


  }
  public void DamagePlayer(float damage)
  {
    blink = 1.0f;
    Health_ -= damage;
    if (Health_ <= 0)
    {
      Health_ = 0;
      SceneManager.LoadScene(0);
    }
    else
    {

    }

    GameObject.FindGameObjectWithTag("Health").GetComponent<PlayerHeathHeartSRC>().updateScale();
  }
}



/*
     0 == Down
     1 == Up
     2 == Right
     3 == Left
*/