using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public GameObject ctrlScrn;
	public GameObject singleCtrlScrn;
	public GameObject twoCtrlScrn;


	public void LoadSinglePlayer(){
		SceneManager.LoadScene ("Level1");
	}
	public void LoadTwoPlayer(){
		SceneManager.LoadScene ("Level2");
	}

	public void OpenCtrlScrn(){
		ctrlScrn.SetActive (true);
	}

	public void QuitGame(){
		Application.Quit ();
	}


	public void CloseCtrlScrn(){
		ctrlScrn.SetActive (false);
	}

	public void FirstCtrlScrn(){
		twoCtrlScrn.SetActive (false);
		singleCtrlScrn.SetActive (true);
	}

	public void SecondCtrlScrn(){
		twoCtrlScrn.SetActive (true);
		singleCtrlScrn.SetActive (false);
	}

}
