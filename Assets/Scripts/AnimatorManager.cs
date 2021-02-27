using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Animator anim;
    int horizontal;
    int vertical;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");//get the id from animator
        vertical = Animator.StringToHash("Vertical");
    }
    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement)
    {
        //Animation snapping
        float snapperHorizontal;
        float snappedVertical;

        #region Snapped Horizontal
        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            snapperHorizontal = 0.5f;
        }
        else if(horizontalMovement > 0.55f)
        {
            snapperHorizontal = 1.0f;
        }
        else if(horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            snapperHorizontal = -0.5f;
        }
        else if(horizontalMovement < -0.55f)
        {
            snapperHorizontal = -1f;
        }
        else
        {
            snapperHorizontal = 0f;
        }
        #endregion
        #region Snapped vertical
        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            snappedVertical = 0.5f;
        }
        else if (verticalMovement > 0.55f)
        {
            snappedVertical = 1.0f;
        }
        else if (verticalMovement < 0 && verticalMovement > -0.55f)
        {
            snappedVertical = -0.5f;
        }
        else if (verticalMovement < -0.55f)
        {
            snappedVertical = -1f;
        }
        else
        {
            snappedVertical = 0f;
        }
        #endregion

        anim.SetFloat(horizontal, snapperHorizontal, 0.1f, Time.deltaTime);
        anim.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);

        //anim.SetFloat(horizontal, snapperHorizontal);
        //anim.SetFloat(vertical, snappedVertical);
    }
}
