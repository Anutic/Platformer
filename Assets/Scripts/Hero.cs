using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hero : Entity
{
    #region Fields
    [SerializeField] private float speed = 3f; //скорость перемещения 
  // [SerializeField] private int lives = 6;
	private Vector3 dir;

   private void Start()
   {
       dir = transform.right;
       lives = 6;
   }

   [SerializeField] private float jumpForce = 15f;
   private bool isGrounded = false;

   public bool isAttacking = false;
   public bool isRecharged = true;

   public Transform attackPos;
   public float attackRange;
   public LayerMask enemy;
   #endregion
   
   
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    public static Hero Instance { get; set; }

    private States State
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }
    private void Awake()
    {
        lives = 5;
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        isRecharged = true;//на начало игры мы перезаряжены 
    }
    private void FixedUpdate()
    {
        CheckGround();
    }
    
     private void Update()//отслеживание нажатий
     {
         if (isGrounded) State = States.idle;
         if (Input.GetButton("Horizontal"))
			{
				Run();
				Debug.Log("Бежим ");
			}
         if (isGrounded && Input.GetButtonDown("Jump"))
                Jump();
         if (Input.GetButtonDown("Fire1"))//левая кнопка мыши :()
             Attack();

     }

private void Run()
{
    if (isGrounded) State = States.run;
	Debug.Log("Меняем позицию ");

    dir = transform.right * Input.GetAxis("Horizontal");
    transform.position = 
        Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
    				Debug.Log("Поворачиваемся ");

sprite.flipX = dir.x < 0.0f;
				Debug.Log("Повернулись ");

}

      private void Jump()
      {
          rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
      }

      private void CheckGround()
      {
          Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
          isGrounded = collider.Length > 1;
          if (!isGrounded) State = States.jump;

      }

     // public override v
      public enum States
      {
          idle,
          run,
          jump,
          attack03
      }
      
      public override void GetDamage()
      {
          lives -= 1;
          Debug.Log(lives);
      }
      //
      private void Attack()
      {
          if(isGrounded && isRecharged)
          {
              State = States.attack03;
              isAttacking = true;
              isRecharged = false;


              StartCoroutine(AttackAnimation());
              StartCoroutine(AttackCoolDown());
          }
      }

      private void OnAttack()
      {
          Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
          for (int i = 0; i < colliders.Length; i++)
          {
              colliders[i].GetComponent<Entity>().GetDamage();
          }
      }
      
      private void OnDrawGizmosSelected()
      {
          Gizmos.color = Color.red;
          Gizmos.DrawWireSphere(attackPos.position, attackRange);
      }
      private IEnumerator AttackAnimation()
      {
          yield return new WaitForSeconds(0.4f);
          isAttacking = false;
      }

      private IEnumerator AttackCoolDown()
      {
          yield return new WaitForSeconds(0.5f);
          isRecharged = true;
      }
}
/*public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f; // скорость движения
    [SerializeField] private int lives = 5; // скорость движения
    [SerializeField] private float jumpForce = 15f; // сила прыжка
    private bool isGrounded = false;

    public static Hero Instance { get; set; }

    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    public bool isAttacking = false;
    public bool isRecharged = true;

    public Transform attackpos;
    public float attackRange;
    public LayerMask enemy;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Instance = this;
        isRecharged = true;
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 6;
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = 3;
            }
        }

        if (isGrounded && !isAttacking) State = States.idle;

        if (!isAttacking && Input.GetButton("Horizontal"))
            Run();
        if (!isAttacking && isGrounded && Input.GetButtonDown("Jump"))
            Jump();
        if (Input.GetButtonDown("Fire1"))
            Attack();
    }

    private void Run()
    {
        if (isGrounded) State = States.run;

        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackpos.position, attackRange);
    }
    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;

        if (!isGrounded) State = States.jump;
    }

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }
    private void Attack()
    {
        if (isGrounded && isRecharged)
        {
            State = States.attack;
            isAttacking = true;
            isRecharged = false;

            StartCoroutine(AttackAnimation());
            StartCoroutine(AttackCoolDown());
        }
    }
    private void OnAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackpos.position, attackRange, enemy);

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<Entity>().GetDamage();
        }
    }
    public void GetDamage()
    {
        lives -= 1;
        Debug.Log(lives);
    }
    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
    }
    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        isRecharged = true;
    }
}

public enum States
{
    idle,
    run,
    jump,
    attack
}*/