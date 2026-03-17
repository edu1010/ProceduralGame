using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyAI : MonoBehaviour
{
    //states
    enum EFly
    {
        Move, 
        Attack
    }
    //fsm
    FSM<EFly> brain;

    //player
    public Transform Player;

    //enemy characteristics
    private Rigidbody2D _rigidbody;
    public float speed = 3.0f;
    [SerializeField]
    Vector2 direction = new Vector2(1, 0.25f);

    //detectCollision
    [SerializeField]
    LayerMask Ground;
    public Transform UpPoint;
    public Transform LateralPoint;
    public Transform DownPoint;
    Collider2D upHit, lateralHit, downHit;

    //attack characteristics
    float attackDistance = 10.0f;
    float timerToShoot;

    //delegates
    public delegate void FireDelegate();
    public static FireDelegate Fire;

    //detection edges and walls variables
    public float radiusDetectWalls = 0.25f;

    void Start()
    {
        InitSFM();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        brain.Update();
    }

    void InitSFM()
    {
        brain = new FSM<EFly>(EFly.Move);

        brain.SetOnEnter(EFly.Attack, () => timerToShoot = 0.0f);

        brain.SetOnStay(EFly.Move, MoveUpdate);
        brain.SetOnStay(EFly.Attack, AttackUpdate);
    }

    void MoveUpdate()
    {
        _rigidbody.linearVelocity = direction * speed;

        if (DetectCollision())
        {
            ChangeDirection();
        }
        if (IsPlayerCloseByDistance(attackDistance))
        {
            brain.ChangeState(EFly.Attack);
        }
    }

    void AttackUpdate()
    {
        Vector2 dirAttack = Player.position - transform.position;
        dirAttack = dirAttack.normalized;

        _rigidbody.linearVelocity = dirAttack * speed;

        timerToShoot += Time.deltaTime;

        if (timerToShoot >= 1.5f)
        {
            Fire?.Invoke();
            timerToShoot = 0.0f;
        }
        
        if (!IsPlayerCloseByDistance(attackDistance))
        {
            brain.ChangeState(EFly.Move);
        }
    }

    bool DetectCollision()
    {
        upHit = Physics2D.OverlapCircle(UpPoint.position, radiusDetectWalls, Ground);
        lateralHit = Physics2D.OverlapCircle(LateralPoint.position, radiusDetectWalls, Ground);
        downHit = Physics2D.OverlapCircle(DownPoint.position, radiusDetectWalls, Ground);

        if (upHit || lateralHit || downHit)
        {
            return true;
        }
        else
        {
            return false;
        }        
    }

    void ChangeDirection()
    {
        if (lateralHit)
        {
            transform.Rotate(0, 180, 0);
            direction.x = -direction.x;
        }
        
        if (upHit)
        {
            if(direction.y > 0)
            {
                direction.y = -direction.y;
            }
        }

        if (downHit)
        {
            if(direction.y < 0)
            {
                direction.y = -direction.y;
            }   
        }
    }

    private bool IsPlayerCloseByDistance(float distance)
    {
        float d = Vector2.Distance(transform.position, Player.position);
        return d < distance;
    }
}
