using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int currentHealth = 5;
    Transform Player;
    [SerializeField] float followDistance;
    NavMeshAgent agent;
    AudioSource audio;

    void Start()
    {
        Player = FindFirstObjectByType<PlayerMovement>().transform;
        agent = GetComponent<NavMeshAgent>();
        audio = GetComponent<AudioSource>();
        
        //turn off ragdoll by default
        foreach (Rigidbody ragdollBone in GetComponentsInChildren<Rigidbody>())
        {
            ragdollBone.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Player.position) < followDistance)
        {
            Follow();
        }

        else
        {
            Idle();
        }
    }
    void Follow()
    {
        agent.destination = Player.position;
    }
    void Idle()
    {
        agent.destination = transform.position;

        agent.SetDestination(transform.position);
    }

    void Wander()
    {

    }
    public void Damage(int damageAmount)
    {
        //subtract damage amount when Damage function is called
        currentHealth -= damageAmount;

        //Check if health has fallen below zero
        if (currentHealth <= 0)
        {
            //if health has fallen below zero, deactivate it 
            ragdollTrigger();
        }
    }

    public void ragdollTrigger()
    {        
        foreach (Rigidbody ragdollBone in GetComponentsInChildren<Rigidbody>())
        {
            ragdollBone.isKinematic = false;
        }

        GetComponent<NavMeshAgent>().isStopped = true;
        GetComponent<Collider>().enabled = false;
        GetComponent<AudioSource>().Stop();
    }
}
