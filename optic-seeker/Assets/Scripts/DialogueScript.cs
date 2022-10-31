using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    private bool animationCheck1;
    private bool animationCheck2;
    private bool animationCheck3;
    private bool animationCheck4;
    public TMP_Text dialogueText;
    public Animation anim;
    public Camera playerCam;
    public Camera cutSceneCam;
    public Animation cutSceneAnim;
    public GameObject playerCutscene;
    public GameObject EnemyCutscene;
    public SonarFx cameraobject;
    //public GameObject fadeCanvas;
    ////public GDTSecondEffect fadeScript;
    //public GDTFadeEffect fadeScript;
    void Awake()
    {
        animationCheck1 = false;
        animationCheck2 = false;
        animationCheck3 = false;
        animationCheck4 = false;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if (cutSceneAnim.IsPlaying("Cutscene") == false)
        //{
        //    fadeCanvas.SetActive(true);
        //}
        playAnimation();
    }
   
    public void playAnimation()
    {

        if (cutSceneAnim.IsPlaying("Cutscene") == false && animationCheck4 == false)
        {
            playerCam.enabled = true;
            cutSceneCam.enabled = false;
            Destroy(playerCutscene);
            Destroy(EnemyCutscene);
            cameraobject.enabled = true;

            if (animationCheck1 == false)
            {
                dialogueText.text = "Where am I? How did I get here?";

                anim.Play("DialogueAnimation");
                animationCheck1 = true;
            }

            if (anim.IsPlaying("DialogueAnimation") == false && animationCheck2 == false)
            {
                dialogueText.text = "He took my eyes! I need to use echolocation to get them back";
                anim.Play("DialogueAnimation");
                animationCheck2 = true;
            }

            if (anim.IsPlaying("DialogueAnimation") == false && animationCheck3 == false)
            {
                dialogueText.text = "If I move, I should see my surroundings, just like a bat";
                anim.Play("DialogueAnimation");
                animationCheck3 = true;
            }

            if (anim.IsPlaying("DialogueAnimation") == false && animationCheck4 == false)
            {
                dialogueText.text = " Now I need to get out of here! where is the keys to this door?";
                anim.Play("DialogueAnimation");
                animationCheck4 = true;
            }
        }
     
    }
}
