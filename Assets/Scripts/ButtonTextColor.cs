using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//http://www.buehling.org/2015/11/unity-text-und-grafik-eines-buttons-bei-mouseover-einfarben/
public class ButtonTextColor : MonoBehaviour 
{
	private Graphic 	target = null;
	private Button 		button = null;

	void Start () 
	{
		string n=gameObject.name+"/"+GetType().Name;
		if (GetComponent<Button>()!=null)
		{
			Debug.LogWarning("Note: "+n+": Suspicious setup found, check if correct: Add GUICopyButtonTint to the child of the button (i.e. Text), not the button itself!");
		}

		target = GetComponent<Graphic>();
		button = GetComponentInParent<Button>();
		if (button==null)
		{
			Debug.LogWarning("Note: "+n+": Object has not parent button. Component will have no effect and is disabled now.");
			enabled=false;
		}
		else if (target==null)
		{
			Debug.LogWarning("Note: "+GetType().Name+": Object has not target graphic. Component will have no effect and is disabled now.");
			enabled=false;
		}
	}

	void Update () 
	{
		try{
			if ((button==null) || (target==null)) return;
			target.color=button.targetGraphic.canvasRenderer.GetColor(); 
		}catch{}
	}
}
