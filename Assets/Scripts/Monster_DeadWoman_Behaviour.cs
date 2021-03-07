using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster_DeadWoman_Behaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private Vector3 destination;
    public Transform target;
    [SerializeField] private float distance;
    [SerializeField] private float ScreamTime = 3.5f;
    [SerializeField] private Transform startPos;
    private float attackRange;
    private float detectRange;
    

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        destination = agent.destination;
        attackRange = 2f;
        detectRange = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        DeadWoman_Behaviour();
    }

    private void DeadWoman_Behaviour()
    {
        distance = Vector3.Distance(gameObject.transform.position, target.position);
        if (!agent.isStopped)
        {
            AgentToDes(target.position);
        }
        if (anim.GetBool("isScream"))
        {
            agent.isStopped = true;
            ScreamTime -= Time.deltaTime;
            if (ScreamTime <= 0)
            {
                anim.SetBool("isScream", false);
                agent.isStopped = false;
                ScreamTime = 3.5f;
            }
        }
        else
        {
            ScreamTime = 3.5f;
            if (distance > attackRange && distance <= detectRange)
            {
                ChasePlayer();
            }
            else if (distance > detectRange)
            {
                ReturnPos();
            }
            else if (distance <= attackRange)
            {
                EnermyAttack();
            }
        }
    }

    private void AgentToDes(Vector3 targetPos)
    {
        destination = targetPos;
        agent.destination = destination;
    }

    private void ChasePlayer()
    {
        agent.speed = 3.5f;
        agent.isStopped = false;
        anim.SetBool("isWalking", false);
        anim.SetBool("isSitting", false);
        anim.SetBool("isCraw", true);
        anim.SetBool("isStandUp", false);
    }

    private void EnermyAttack()
    {
        agent.isStopped = true;
        anim.SetBool("isWalking", false);
        anim.SetBool("isCraw", false);
        anim.SetBool("isStandUp", true);
        if (anim.GetBool("isStandUp"))
        {
            anim.SetBool("isScream", true);
        }
    }

    private void ReturnPos()
    {
        agent.isStopped = false;
        AgentToDes(startPos.position);
        if(anim.GetBool("isSitting"))
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
             anim.SetBool("isWalking", true);
        }
        if(Vector3.Distance(this.transform.position,startPos.position) <= 1f)
        {
            anim.SetBool("isWaling", false);
            anim.SetBool("isSitting", true);
            agent.speed = 0.5f;
            agent.isStopped = true;
            transform.LookAt(startPos);
        }
    }
}
