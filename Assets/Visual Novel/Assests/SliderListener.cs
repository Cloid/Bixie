using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderListener : MonoBehaviour
{
   public Slider mainSlider;
   public Text text;
	
	public void Start()
	{
		//Adds a listener to the main slider and invokes a method when the value changes.
		mainSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
	}
	
	// Invoked when the value of the slider changes.
	public void ValueChangeCheck()
	{
		if(Mathf.Ceil(mainSlider.value*100) % 10 != 0){
			text.text = Mathf.Floor(mainSlider.value*100).ToString();
		} else{
			text.text = Mathf.Ceil(mainSlider.value*100).ToString();
		}

		/*
		if((mainSlider.value *100)>=50){
			text.text = Mathf.Floor((mainSlider.value *100)).ToString();
		} else {
			text.text = Mathf.Ceil((mainSlider.value *100)).ToString();
		}*/
        
	}
}
