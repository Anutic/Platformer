using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hero : Entity
{
    #region Fields
    [SerializeField] private float speed = 3f; //скорость перемещения 
    private Vector3 dir;

   private void Start()
   {
       dir = transform.right;
       lives = 3;
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
    private bool isFacingRight = true;
    public static Hero Instance { get; set; }

    private States State
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }
    private void Awake()
    {
        lives = 3;
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
			}
         if (isGrounded && Input.GetButtonDown("Jump"))
                Jump();
         if (Input.GetButtonDown("Fire1"))//левая кнопка мыши :()
             Attack();
     }
    private void Run()
{
    if (isGrounded) State = States.run;
    dir = transform.right * Input.GetAxis("Horizontal");
    transform.position = 
        Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
    // проверяем направление движения и поворачиваем спрайт
        if ((dir.x > 0f && !isFacingRight) || (dir.x < 0f && isFacingRight))
        {
            Flip();
        }
}
private void Flip()
{
    // меняем направление спрайта
    isFacingRight = !isFacingRight;

    // поворачиваем спрайт
    Vector3 scale = transform.localScale;
    scale.x *= -1;
    transform.localScale = scale;
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
          HealthSystem.health -= 1;
          if(lives<1) {
            Die();
          }
      }
      public void PleaseWork()
      {
          lives = GetHeal();
      }
      private void Attack()
      {
          if (isGrounded && isRecharged)
          {
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
          State = States.attack03;
          isAttacking = true;
          yield return new WaitForSeconds(0.6f);
          isAttacking = false;
          State = States.idle;
          OnAttack();
      }
      private IEnumerator AttackCoolDown()
      {
          yield return new WaitForSeconds(0.5f);
          isRecharged = true;
      }
}