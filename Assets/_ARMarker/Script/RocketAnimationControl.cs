using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RocketAnimationControl : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

  /*  public void PlayForward()
    {
        anim.speed = 1.0f;
        anim.Play("rocketlaunch", 0, 0f);

    }
    public void PlayBackward()
    {
        anim.speed = -1.0f;
        anim.Play("rocketlaunch", 0, 1f);

    }*/

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("Press space");
            anim.SetTrigger("Launch");
        }
            /*PlayForward();
        if (Input.GetKeyDown(KeyCode.DownArrow)) PlayBackward();*/

    }

}
