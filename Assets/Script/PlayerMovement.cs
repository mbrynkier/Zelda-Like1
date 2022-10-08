using System.Collections;
using UnityEngine;

public enum PlayerState{
    walk,
    attack,
    interact
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;
    public bool interactiveInRange;

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();        
        myRigidBody = GetComponent<Rigidbody2D>();

        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);

        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {        
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");   

        if(Input.GetButtonDown("Attack") && currentState != PlayerState.attack && interactiveInRange == false){
            Debug.Log("attacking");
            StartCoroutine(AttackCo());
        }else if(currentState == PlayerState.walk){
            UpdateAnimationAndMove();
        }

        
    }

    private IEnumerator AttackCo(){
        animator.SetBool("attacking",true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking",false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;
    }

    void UpdateAnimationAndMove()
    {
        if(change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }else{
            animator.SetBool("moving", false);            
        }
    }

    void MoveCharacter()
    {        
        myRigidBody.MovePosition(
            transform.position + change.normalized * speed * Time.deltaTime
        );
    }  

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("interactive")){            
            interactiveInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("interactive")){            
            interactiveInRange = false;            
        }
    }
}
