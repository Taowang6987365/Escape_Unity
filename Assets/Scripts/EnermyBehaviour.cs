using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnermyBehaviour : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float turnRate;
    private Vector3 destination;
    private NavMeshAgent agent;
    private Animator anim;
    private float distance;
    public GameObject[] patrolWP;
    private List<Vector3> wayPoints = new List<Vector3>();
    private int index;
    private float attackRange;
    private float detectRange;
    public static bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        attackRange = 2.0f;
        detectRange = 10.0f;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        destination = agent.destination;
        LoadPath(patrolWP);
    }

    private void Update()
    {
        EnermyAnim();
        if (distance > 10f)
        {
            anim.SetBool("isWalking", true);
            EnermyPatrol();
        }
    }
    private void FixedUpdate()
    {
        if (distance <= 2f)
        {
            EnermyLook();
        }
    }

    public void LoadPath(GameObject[] go)
    {
        index = 0;
        wayPoints.Clear();
        for (int i = 0; i < go.Length; i++)
        {
            wayPoints.Add(go[i].transform.position);
        }
    }
    public void EnermyPatrol()
    {
        if (index == patrolWP.Length)
        {
            LoadPath(patrolWP);
        }
        destination = wayPoints[index];
        agent.destination = destination;
        if (Vector3.Distance(this.transform.position, destination) <= 1.0f)
        {
            index++;
        }
    }

    public void EnermyAnim()
    {
        distance = Vector3.Distance(this.transform.position, target.position);
        isAttacking = anim.GetBool("Attack");
        if (distance > attackRange && distance < detectRange)
        {
            agent.isStopped = false;
            anim.SetBool("isWalking", true);
            anim.SetBool("Attack", false);
            destination = target.position;
            agent.destination = destination;
        }
        else if (distance <= attackRange)
        {
            agent.isStopped = true;
            anim.SetBool("Attack", true);
        }
    }
    public void EnermyLook()
    {
        Vector3 targetDelta = target.position - transform.position;
        float angleToTarget = Vector3.Angle(transform.forward, targetDelta);
        Vector3 turnAxis = Vector3.Cross(transform.forward, targetDelta);
        transform.RotateAround(transform.position, turnAxis, Time.deltaTime* turnRate * angleToTarget);
    }

}
