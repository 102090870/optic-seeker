using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    //public Animation startingDialogue;
    private bool animationCheck1;
    private bool animationCheck2;
    public TMP_Text dialogueText;
    //public Animator anim;
    public Animation anim;
    //private Animation mainAnim;

    void Awake()
    {
        //mainAnim = anim;
        //anim = gameObject.GetComponent<Animator>();
        animationCheck1 = false;
        animationCheck2 = false;
        
    }

    // Start is called before the first frame update
    void Start()
    {

        //startingDialogue = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        playAnimation();
        //if (startingDialogue.isPlaying)
        //{
        //    return;
        //}

        //if (animationCheck == false)
        //{
        //    startingDialogue.Play();
        //}
        //if (animationCheck1 == false)
        //{
        //    playAnimation();
        //}
        //animationCheck1 = true;
        //if (animationCheck1 == false)
        //{
        //    dialogueText.text = "Where am I? How did I get here?";

        //    //anim.Play("Base Layer.DialogueAnimation");
        //    anim.Play("DialogueAnimation");
        //    //anim.PlayQueued("DialogueAnimation", QueueMode.PlayNow);
        //    animationCheck1 = true;
        //}

        //if (anim.IsPlaying("DialogueAnimation") == false && animationCheck2 == false)
        //{
        //    dialogueText.text = "Where am I? How did I get here?";
        //    anim.Play("DialogueAnimation");
        //    Debug.Log("ANIMATION STOPPED PLAYING");
        //    animationCheck2 = true;
        //}
    }
   
    public void playAnimation()
    {
        //GameObject prefab = Instantiate(dialogueText);
        //prefab.GetComponentInChildren<TextMesh>().text = "I need to get out of here! where is the keys to this door?";
        if (animationCheck1 == false)
        {
            dialogueText.text = "Where am I? How did I get here?";
           
            //anim.Play("Base Layer.DialogueAnimation");
            anim.Play("DialogueAnimation");
            //anim.PlayQueued("DialogueAnimation", QueueMode.PlayNow);
            animationCheck1 = true;
        }

        if (anim.IsPlaying("DialogueAnimation") == false && animationCheck2 == false)
            {
            dialogueText.text = "I need to get out of here! where is the keys to this door?";
            anim.Play("DialogueAnimation");
            Debug.Log("ANIMATION STOPPED PLAYING");
            animationCheck2 = true;
        }
        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("DialogueAnimation") == true)
        //{
        //    Debug.Log("YES ANIMATION STATE");
        //}
        //if (animationCheck1 == true)
        //{
        //    dialogueText.text = "I need to get out of here! where is the keys to this door?";

        //    anim.PlayQueued("DialogueAnimation", QueueMode.CompleteOthers);
        //    anim.Play();
        //    animationCheck2 = true;
        //}
    }
}
