using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueWINScript : MonoBehaviour
{
    private bool animationCheck1;
    private bool animationCheck2;
    public TMP_Text dialogueText;
    public Animation anim;

    void Awake()
    {  
        animationCheck1 = false;
        animationCheck2 = false;        
    }

    void Start()
    {
    }

    void Update()
    {
        playAnimation();
    }
   
    public void playAnimation()
    {
        if (animationCheck1 == false)
        {
            dialogueText.text = "Where am I? How did I get here?";
            anim.Play("DialogueAnimation");
            animationCheck1 = true;
        }

        if (anim.IsPlaying("DialogueAnimation") == false && animationCheck2 == false)
        {
            dialogueText.text = "I need to get out of here! where is the keys to this door?";
            anim.Play("DialogueAnimation");
            animationCheck2 = true;
        }
    }
}
