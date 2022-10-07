
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Smash(){
        anim.SetBool("smash", true);
        StartCoroutine(BreakCo());
    }

    private IEnumerator BreakCo(){
        yield return new WaitForSeconds(.3f);
        this.gameObject.SetActive(false);
    }
}
