using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }

    public void Animation(int index)
    {
        switch (index)
        {
            case 0:
                anim.SetBool("isLeft", true);
                break;
            case 1:
                anim.SetBool("isUp", true);
                break;
            case 2:
                anim.SetBool("isRight", true);
                break;
            case 3:
                anim.SetBool("isLeft", false);
                break;
            case 4:
                anim.SetBool("isUp", false);
                break;
            case 5:
                anim.SetBool("isRight", false);
                break;
        }
    }
}
