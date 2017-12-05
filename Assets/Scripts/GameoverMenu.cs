using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameoverMenu : PauseMenu {

	public Text victoryLabel;

	public void SetResult(bool victory)
	{
		victoryLabel.text = victory ? "ESCAPE!" : "GAME OVER";
	}

}
