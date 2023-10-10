using System;
using UnityEngine;

public class GuardController : MonoBehaviour
{
    public StateMachine stateMachine;
    public StateMachine.State patrol, chase, attack, runAway, enraged;

    public GameObject enemy;

    public float GuardFOV = 89f; //degrees
    private float cosGuardFOVover2InRAD;

    public float closeEnoughAttackCutoff = 2f; //if d(G,E) <= 5m) => closeEnough to attack!
    public float closeEnoughSenseCutoff = 15f; //if d(G,E) <= 5m) => closeEnough to chase!

    public float strenght = 90; //[0, 100]
    public float health = 100; //[0, 100]
    public float speed = 2;//2m per sec
    public Transform[] waypoints;
    public int nextWayPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new StateMachine();

        //Use the Factory Pattern
        //StateMachine.State patrolState = new StateMachine.State();
        patrol = stateMachine.CreateState("Patrol"); //This is the factory pattern.
        patrol.onEnter = delegate { Debug.Log("Patrol.onEnter"); };
        patrol.onExit = delegate { Debug.Log("Patrol.onExit"); };
        patrol.onFrame = PatrolOnFrame;

        chase = stateMachine.CreateState("Chase"); //This is the factory pattern.
        chase.onEnter = delegate { Debug.Log("Chase.onEnter"); };
        chase.onExit = delegate { Debug.Log("Chase.onExit"); };
        chase.onFrame = ChaseOnFrame;

        attack = stateMachine.CreateState("Attack"); //This is the factory pattern.
        attack.onEnter = delegate { Debug.Log("Attack.onEnter"); };
        attack.onExit = delegate { Debug.Log("Attack.onExit"); };
        attack.onFrame = AttackOnFrame;

        runAway = stateMachine.CreateState("RunAway"); //This is the factory pattern.
        runAway.onEnter = delegate { Debug.Log("RunAway.onEnter"); };
        runAway.onExit = delegate { Debug.Log("RunAway.onExit"); };
        runAway.onFrame = RunAwayOnFrame;

        enraged = stateMachine.CreateState("Enraged"); //This is the factory pattern.
        enraged.onEnter = delegate { Debug.Log("Enraged.onEnter"); };
        enraged.onExit = delegate { Debug.Log("Enraged.onExit"); };
        enraged.onFrame = EnragedOnFrame;
    }

    void PatrolOnFrame() {
        Debug.Log("Patrol.OnFrame");
        DoPatrol();

        if (Utils.SenseEnemy(this.transform.position, enemy.transform.position, this.transform.forward, cosGuardFOVover2InRAD, closeEnoughSenseCutoff)) 
            stateMachine.ChangeState(chase);        

        if (ThreatenedAndWeakerThanEnemy()) 
            stateMachine.ChangeState(patrol);        
    }

    private void DoPatrol() {
        if (Vector3.Distance(this.transform.position, waypoints[nextWayPointIndex].position) < float.Epsilon) 
            nextWayPointIndex = (nextWayPointIndex + 1) % waypoints.Length; //% to get a circular movement        

        Vector3 target = waypoints[nextWayPointIndex].position;
        Vector3 movement = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
        this.transform.position = movement;
    }

    private bool ThreatenedAndWeakerThanEnemy() {
        return Threatened() && WeakerThanEnemy() ? true : false;       
    }

    private bool Threatened() {
        return Utils.EnemyCloseEnough(this.transform.position, enemy.transform.position, closeEnoughAttackCutoff);
    }

    private bool WeakerThanEnemy() {
        PlayerController enemyController = enemy.GetComponent<PlayerController>();
        return strenght < enemyController.strenght ? true : false;
    }

    void ChaseOnFrame() {
        Debug.Log("Chase.OnFrame");
        Chase();

        if (WithingRangeAndStrongerThanEnemy()) 
            stateMachine.ChangeState(attack);

        if (WeakerThanEnemy()) 
            stateMachine.ChangeState(patrol);        
    }

    private bool WithingRangeAndStrongerThanEnemy() {
        return WithinRange() && !WeakerThanEnemy() ? true : false;
    }

    private bool WithinRange() {
        return Utils.EnemyCloseEnough(this.transform.position, enemy.transform.position, closeEnoughAttackCutoff);
    }

    private void Chase() {
        Vector3 enemyHeading = (enemy.transform.position - this.transform.position);
        float enemyDistance = enemyHeading.magnitude;
        enemyHeading.Normalize();

        Vector3 movement = enemyHeading * speed * Time.deltaTime; //m/s * s/frame = m/frame
        Vector3.ClampMagnitude(movement, enemyDistance);
        this.transform.position += movement;
    }

    void AttackOnFrame() {
        Debug.Log("Attack.OnFrame");

        if (ThreatenedAndWeakerThanEnemy()) 
            stateMachine.ChangeState(runAway);        
        else 
            Combat();        

        if (this.health <= 50) 
            stateMachine.ChangeState(enraged);
        
        if (!Utils.SenseEnemy(this.transform.position, enemy.transform.position, this.transform.forward, cosGuardFOVover2InRAD, closeEnoughSenseCutoff)) 
            stateMachine.ChangeState(patrol);
        
        if (WeakerThanEnemy()) 
            stateMachine.ChangeState(patrol);        
    }

    void EnragedOnFrame() {
        //DEFAULT actions during Die state
        print("Enraged...");
        this.GetComponent<MeshRenderer>().material.color = Color.yellow;

        if (!Utils.SenseEnemy(this.transform.position, enemy.transform.position, this.transform.forward, cosGuardFOVover2InRAD, closeEnoughSenseCutoff))
            stateMachine.ChangeState(patrol);

        if (WeakerThanEnemy())
            stateMachine.ChangeState(patrol);
    }

    void Combat() {
        if (this.health > 0) {
            PlayerController enemyController = enemy.GetComponent<PlayerController>();
            this.health -= enemyController.damage;
        }
    }

    void RunAwayOnFrame() {
        Debug.Log("RunAway.OnFrame");
        Runaway();

        //CHECK TRANSITION CONDITIONS
        if (Safe()) 
            stateMachine.ChangeState(patrol);        
    }

    private void Runaway() {
        Vector3 enemyHeading = (enemy.transform.position - this.transform.position);
        float enemyDistance = enemyHeading.magnitude;
        enemyHeading.Normalize();

        Vector3 movement = enemyHeading * speed * Time.deltaTime; //m/s * s/frame = m/frame
        Vector3.ClampMagnitude(movement, enemyDistance);
        this.transform.position -= movement;
    }


    private bool Safe() {
        return !Threatened();
    }

    // Update is called once per frame
    void Update() {
        stateMachine.Update();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < waypoints.Length; i++) {
            int i1 = (i + 1) % waypoints.Length;
            Gizmos.DrawLine(waypoints[i].transform.position, waypoints[i1].transform.position);
        }

        Gizmos.color = Color.cyan;
        Vector3[] pointsArray = new Vector3[20];
        float dAlpha = GuardFOV / pointsArray.Length;
        Vector3 fwdInWorldSpace = this.transform.TransformDirection(this.transform.forward);

        for (int i = 0; i < pointsArray.Length / 2; i++) {
            float dAlphaPlus = dAlpha * i * Mathf.Deg2Rad;
            float dAlphaMinus = -dAlphaPlus;

            pointsArray[2 * i] += this.transform.position; //P0
            Vector3 v;

            if (i % 2 == 0) {
                Vector3 target = new Vector3(Mathf.Cos(dAlphaPlus), 0, Mathf.Sin(dAlphaPlus));
                v = Vector3.RotateTowards(fwdInWorldSpace, target, dAlphaPlus, 10);                
            }
            else {
                Vector3 target = new Vector3(Mathf.Cos(dAlphaMinus), 0, Mathf.Sin(dAlphaMinus));
                v = Vector3.RotateTowards(fwdInWorldSpace, target, dAlphaMinus, 10);
            }

            pointsArray[2 * i + 1] += this.transform.position + v * 10;
        }
        ReadOnlySpan<Vector3> points = new ReadOnlySpan<Vector3>(pointsArray);
        Gizmos.DrawLineList(points);
    }
}