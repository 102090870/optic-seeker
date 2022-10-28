using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
	
	public GameObject Cutscene_Camera;
	public GameObject Cutscene_Enemy;
	public GameObject Cutscene_Player;
	
    void Start()
    {
        StartCoroutine (Cutscene ());
    }
	
	IEnumerator Cutscene() {
		Cutscene_Enemy.SetActive (true);
		Cutscene_Player.SetActive (true);
		yield return new WaitForSeconds (19);
	}

   
}
