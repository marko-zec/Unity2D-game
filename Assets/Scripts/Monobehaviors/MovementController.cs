using UnityEngine;

[RequireComponent(typeof(Character))]
public class MovementController : MonoBehaviour
{
    public float movementSpeed = 2.0f;

    [HideInInspector]
    public Vector2 movement = new Vector2();
    [HideInInspector]
    //public Animator animator;
    Weapon2 weapon;

    Rigidbody2D rb2D;
    Character character;

    void Start()
    {
        //animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        weapon = GetComponent<Weapon2>();
    }

    void Update()
    {
        //UpdateState();
        if (Input.GetMouseButtonDown(0))
        {
            int rand = Random.Range(1, 4);
            SoundManager.PlaySound("pistol" + rand.ToString());
            weapon.Fire();  
        }
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    public void MoveCharacter()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        //move player
        rb2D.velocity = movement * movementSpeed;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //rotate player woth mouse
        Vector2 aimDirection = mousePosition - rb2D.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb2D.rotation = aimAngle;
    }
    /*
    void UpdateState()
    {
        if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
        {
            animator.SetBool("isWalking", false);
        } 
        else
        {
            animator.SetBool("isWalking", true);
        }

        animator.SetFloat("xDir", movement.x);
        animator.SetFloat("yDir", movement.y);
    }
    */
}