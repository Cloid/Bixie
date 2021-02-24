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
        text.text = mainSlider.value.ToString();
	}
}
