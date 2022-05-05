using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Attack", false);
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Attack", true);
        }
    }
}
