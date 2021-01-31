using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Children_;

    private bool Pause_ = false;

    void Start()
    {
        foreach (GameObject objet in Children_) {
            objet.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel")) {
            Pause_ = !Pause_;
            foreach (GameObject objet in Children_)
            {
                objet.SetActive(Pause_);
            }
            if (Pause_)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}
