using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationStateMachine : MonoBehaviour
{
    private string currentAnimation;
    private bool currentCanBeOverwritten = true;
    private Coroutine currentCoroutine = null;

    // -----------------------------------------------------------------------------------
    public void playAnimation(string newAnimation, bool canBeOverwritten, bool willAlwaysOverwrite, GenEnemy enemy)
    {   
        if (enemy.Animator == null) {
            return;
        }

        // stop the same animation from interrupting itself
        if (currentAnimation == newAnimation) return;

        // either play or wait to play
        if (willAlwaysOverwrite)
        {
            // Stop any ongoing coroutine and play the new animation immediately
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
                currentCoroutine = null;
            }
            PlayAndSet(enemy.Animator, canBeOverwritten, newAnimation);
        }
        else
        {
            if (!currentCanBeOverwritten)   // If override is not allowed, wait for this animation to finish
            {
                StartCoroutine(WaitForAnimationToEnd(enemy.Animator, canBeOverwritten, currentAnimation, newAnimation));
            }
            else
            {
                PlayAndSet(enemy.Animator, canBeOverwritten, newAnimation);
            }
        }
    }

    // -----------------------------------------------------------------------------------
    private IEnumerator WaitForAnimationToEnd(Animator animator, bool canBeOverwritten, string currentAnimation, string newAnimation)
    {
        // Wait until the animation completes
        while (animator.GetCurrentAnimatorStateInfo(0).IsName(currentAnimation) &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;  // Wait until the next frame
        }

        PlayAndSet(animator, canBeOverwritten, newAnimation);
        currentCoroutine = null;
    }

    // -----------------------------------------------------------------------------------
    private void PlayAndSet(Animator animator, bool canBeOverwritten, string newAnimation)
    {
        animator.Play(newAnimation);
        currentAnimation = newAnimation;
        currentCanBeOverwritten = canBeOverwritten;
    }
}
