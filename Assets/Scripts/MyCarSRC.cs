using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCarSRC : MonoBehaviour
{

  private SpriteRenderer MySP;
  private Transform MyTransform_;

  public Sprite Normal_;
  public Sprite Clicked_;

  private Vector3 New_Position_ = new Vector3();
  private Camera MyCam_;
  [SerializeField]
  private float CoolDownStun_ = 1.0f;
  private float CoolDown_ = 0;

  void Start()
  {
    MySP = GetComponent<SpriteRenderer>();
    MyTransform_ = GetComponent<Transform>();
    Cursor.visible = false;
    MyCam_ = Camera.main;
  }


  void Update()
  {
    if (CoolDown_ > 0)
    {
      CoolDown_ -= Time.deltaTime;
      if (CoolDown_ < 0)
        CoolDown_ = 0;
    }
    New_Position_ = MyCam_.ScreenToWorldPoint(Input.mousePosition);
    New_Position_.z = 0.0f;
    MyTransform_.position = New_Position_;
    if (Input.GetAxis("Fire1") > 0.0f)
    {
      if (CoolDown_<=0)
      {
        MySP.sprite = Clicked_;
        RaycastHit2D hit= Physics2D.CircleCast(MyTransform_.position, 0.5f, Vector2.zero);
        if (hit.collider!=null)
        {
          if(hit.collider.CompareTag("Enemy"))
          {
            hit.collider.SendMessage("Stun");
            CoolDown_ = CoolDownStun_;
          }
          
        }
      }
      
    }
    else
    {
      MySP.sprite = Normal_;
    }
  }
}
