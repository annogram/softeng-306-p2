using UnityEngine;
using System.Collections;
using Managers;
using UnityEngine.EventSystems;

/* Script which handlesFacebook posting. To be attatched to a button, and used by invoking 
 * the ShareScoreOnFB method */
public class FacebookShare : MonoBehaviour {
	// App’s unique identifier.
	public string AppID = "1399925576886522";

	// The link attached to this post.
	public string Link = "https://facebook.com";

	// The URL of a picture attached to this post. The picture must be at least 200px by 200px.
	public string Picture = "http://i65.tinypic.com/35hiigw.jpg";

	// The name of the link attachment.
	public string Name = "PINSCAPE";

	// The caption of the link (appears beneath the link name).
	public string Caption = "SuicideSquad";

	// The description of the link (appears beneath the link caption).
	public string Description = "My total score on Pinscape is 100! Can you beat it?";

	public void ShareScoreOnFB(){
		string score = GameController.Instance.GetTotalTokens ().ToString ();
		Description = "My High Score on Pinscape is " + score + "! Can you beat it?";
		Application.OpenURL("https://www.facebook.com/dialog/feed?"+ "app_id="+AppID+ "&link="+
			Link+ "&picture="+Picture+ "&name="+ReplaceSpace(Name)+ "&caption="+
			ReplaceSpace(Caption)+ "&description="+ReplaceSpace(Description)+
			"&redirect_uri=https://facebook.com/");

		EventSystem.current.SetSelectedGameObject(null);
	}

	string ReplaceSpace (string val) {
		return val.Replace(" ", "%20");
	}
}