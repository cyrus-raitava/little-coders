﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DialogueScripts;

public class ConferenceScenarioExecutor : AScenarioExecutor
{
	public Dialogue dialogue2;

	public Sprite Jayne;

    public override void execute()
    {

        ProjectManager.Instance.PauseProject();

		var dialogue1 = new Dialogue
		{
			Sentences = new Sentence[]{
				new Sentence(){
					icon = Jayne,
					Title = "Jayne Hustleson",
				    sentenceLine = "Hey there, I've got a ticket to the DivDevs Diversity Conference tonight, and was wondering if you wanted to go along instead of me?",
				    sentenceChoices = new string[]{
					    "Sure thing!",
						"Sorry I am sick that day"
				    },
				    sentenceChoiceActions = new UnityAction[]{
					    YesChoice,
						NoChoice
				    },
				}
			}	
		};
        
		DialogueManager.Instance.StartDialogue(dialogue1);
		
    }
    
	public void YesChoice(){
		StartCoroutine(WaitThenQueueDialogue());
		DialogueManager.Instance.QueueDialogue(dialogue2);

		var dialogue3 = new Dialogue
		{
			Sentences = new Sentence[]{
				new Sentence(){
					icon = Jayne,
                    Title = "Jayne Hustleson",
                    sentenceLine = "Thanks for going along to the conference! I really appreciate it, hope it went well. Make sure to get your workers working, and get rid of bugs in your code, by TAPPING THEM QUICKLY in projects!",
                    sentenceChoices = new string[]{
                        "Finish Conference"
                    },
                    sentenceChoiceActions = new UnityAction[]{
                        Finish
                    }
                }
            }
        };
		DialogueManager.Instance.QueueDialogue(dialogue3);
	}

	public void Finish() {
		Debug.Log("UNFADING NOW");
		GameManager.Instance.Unfade();
		ProjectManager.Instance.ResumeProject();
	}

	public IEnumerator WaitThenQueueDialogue(){
		Debug.Log("FADING NOW");
		GameManager.Instance.fadeToBlack();
		yield return new WaitForSeconds(2);
	}

	public void NoChoice(){
		ProjectManager.Instance.ResumeProject();
	}

}
