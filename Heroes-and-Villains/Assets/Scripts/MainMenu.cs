using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	public Canvas quitMenu;
	public Button startText;
	public Button exsitText;
	private AudioSource source;
	// Use this for initialization
	void Start () {
		quitMenu = quitMenu.GetComponent<Canvas> ();
		startText = startText.GetComponent<Button> ();
		exsitText = exsitText.GetComponent<Button> ();
		quitMenu.enabled = false;
	}
	
	public void ExsitPress()
	{
		quitMenu.enabled = true;
		startText.enabled = false;
		exsitText.enabled = false;
	}

	public void NoPress()
	{
		quitMenu.enabled = false;
		startText.enabled = true;
		exsitText.enabled = true;
	}

	public void StartLevel()
	{
		Application.LoadLevel (1);
	}

	public void ExsitGame()
	{
		Application.Quit();
	}
}

