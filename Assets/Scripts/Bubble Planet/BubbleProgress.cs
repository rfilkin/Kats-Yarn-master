using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BubbleProgress : MonoBehaviour {

	[SerializeField] UnityEvent onComplete;
	[SerializeField] List<BoxLock> lockers = new List<BoxLock>();

	void Start(){
		print(Statics.currentScene);
		foreach(BoxLock part in lockers){
			part.InitPuzzleManager(this);
		}
	}

	//-------------------------------------------------------------------------------
	//returns the number of completed parts of the puzzle
	//-------------------------------------------------------------------------------

	public int LockedBoxCount(){
		int count = 0;
		foreach (BoxLock part in lockers){
			if (part.GetComplete())
				++count;
		}
		return count;
	}

	//-------------------------------------------------------------------------------
	//checks if all of the parts of this puzzle are complete
	//-------------------------------------------------------------------------------
	public void CheckSolution(){
		foreach(BoxLock part in lockers){
			if (!part.GetComplete())
				return;
		}
		onComplete.Invoke();
	}
}
