using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IDataPersistance
{
    bool isDead = false;
    bool isActive = true;
    private bool isFacingRight = true;
    private bool isWalking = false;
    Rigidbody2D rb;
    public float moveSpeed = 1f;
    public float jumpForce = 200f;
    Animator anim;

    bool isGrounded;
    bool canJump;
    bool jumpInput;
    float moveInput;
    public Transform groundCheck;
    public float groundCheckRadius = 1f;
    public float objCheckRadius = 1f;
    public LayerMask groundLayer;
    public LayerMask interactableLayer;

    Interactable interactable = null;

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.instance.playerSet += SetPlayerActive;
        EventSystem.instance.death += Death;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();    
    }

    

    

    void SetPlayerActive(object sender, bool active)
    {
        moveInput = 0;
        isActive = active;
    }


    // Update is called once per frame
    void Update()
    {
        if(isDead) 
            return;

        OtherInput();

        if (isActive)
            GetInput();

        
        CheckMoveDir();
        Animate();
        CheckEnviroment();
        CheckIfCanJump();
        UpdateUI();
    }

    private void UpdateUI()
    {
        bool uiActive = false;
        if(interactable != null)
        {
            uiActive = true;
            EventSystem.instance.ChangeInteractText(interactable.type.ToString() + " the " + interactable.name);
        }
        EventSystem.instance.ToggleInteractText(uiActive);
    }

    private void OtherInput()
    {
        if(Input.GetKeyDown(KeyCode.I))
            EventSystem.instance.ToggleInventory(isActive);

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadSceneAsync(0);
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Death()
    {
        isDead = true;
        anim.Play("Death");
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void GetInput()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.F)) 
        {
            if(interactable != null) 
                interactable.Interact(this.transform);
        }
    }

    void Jump()
    {
        if(canJump) 
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void CheckIfCanJump()
    {
        if(isGrounded) 
        {
            canJump = true; 
        }
        else
        {
            canJump = false;
        }
    }

    private void CheckEnviroment()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        List<Collider2D> collider2Ds = Physics2D.OverlapCircleAll(transform.position, objCheckRadius, interactableLayer).ToList<Collider2D>();

        interactable = FindClosestObj(collider2Ds);
    }

    private Interactable FindClosestObj(List<Collider2D> colliders)
    {
        Interactable returnObj = null;



        if (colliders != null && colliders.Count!= 0) 
        {
            float minDistance = float.MaxValue;

            foreach (Collider2D c in colliders)
            {
                float dist = Vector2.Distance(transform.position, c.transform.position);
                if ( dist < minDistance)
                {
                    returnObj = c.transform.GetComponent<Interactable>();
                }
            }
        }

        return returnObj;
    }

    private void CheckMoveDir()
    {
        if (isFacingRight && moveInput < 0)
        {
            Flip();   
        }
        else if(!isFacingRight && moveInput > 0)
        {
            Flip();
        }

        if(moveInput == 0)
            isWalking = false;
        else
            isWalking = true;


    }

    

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        transform.Rotate(0, 180, 0);
    }
    

    private void Animate()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }
    

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawWireSphere(transform.position, objCheckRadius);
    }

    
    public void LoadData(ref GameData data)
    {

        ScaneTrigger[] entryExits = FindObjectsOfType<ScaneTrigger>();

        Vector3 position = Vector3.zero;

        Debug.Log(data.didWalk);
        if (data.didWalk == true)
        {
            
            foreach (ScaneTrigger trigger in entryExits)
            {
                if(trigger.isEnterance != data.isEnterence)
                {
                    transform.position = trigger.transform.position;
                    break;
                }
            }


            data.didWalk = false;
        }
        else
        {
            Debug.Log(data.litBonfireLocations[data.lastBonfire]);
            transform.position = data.litBonfireLocations[data.lastBonfire];
        }
        
        

        

        

    }

    public void SaveData(ref GameData data)
    {
        if (isDead)
        {
            data.playerHealth = 10;
        }
    }
}
