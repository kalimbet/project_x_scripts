using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeavyAI : StateMachineBehaviour
{
    public float speed = 0.7f;
    public float attackRange = 0.24f;
    float speedRand;
    EnemyLightMotionLogic enemyLogic;
    Transform player;
    Rigidbody2D rb;
    EnemyLookAtPlayer enemyLookAtPlayer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        enemyLookAtPlayer = animator.GetComponent<EnemyLookAtPlayer>();
        enemyLogic = animator.GetComponent<EnemyLightMotionLogic>();
        speedRand = Random.Range(speed - 0.2f, speed + 0.2f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyLookAtPlayer.LookAtPlayer();
        //Vector2 target = new Vector2(player.position.x, rb.position.y);
        //Vector2 newPosition = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        //rb.MovePosition(newPosition);
        if (player.position.x > rb.position.x)
        {
            rb.velocity = new Vector2(speedRand, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speedRand, rb.velocity.y);
        }

        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("Attack_1");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack_1");
    }
}
