using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderListener : MonoBehaviour
{
   public Slider musicSlider;
   public Slider sfxSlider;
   public Text musicNum;
   public Text sfxNum;
   public AudioOptions AudOptions;
	
	public void Awake()
	{
		//Adds a listener to the main slider and invokes a method when the value changes.
        AudOptions = GameObject.Find("AudioController").GetComponent<AudioOptions>();
		musicSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
		sfxSlider.onValueChanged.AddListener (delegate {ValueChangeCheck2 ();});
	}

	private void Start() {
		if(AudOptions == null){
			AudOptions = GameObject.Find("AudioController").GetComponent<AudioOptions>();
		}
		musicSlider.value = AudOptions.MusicVolume;
		sfxSlider.value = AudOptions.SFXVolume;
		ValueChangeCheck();
		ValueChangeCheck2();
	}

	private void Update() {
		if(AudOptions == null){
			AudOptions = GameObject.Find("AudioController").GetComponent<AudioOptions>();
		}
	}
	
	// Invoked when the value of the slider changes.
	public void ValueChangeCheck()
	{
		if(Mathf.Ceil(musicSlider.value*100) % 10 != 0){
			musicNum.text = Mathf.Floor(musicSlider.value*100).ToString();
		} else{
			musicNum.text = Mathf.Ceil(musicSlider.value*100).ToString();
		}

		/*
		if((musicSlider.value *100)>=50){
			text.text = Mathf.Floor((musicSlider.value *100)).ToString();
		} else {
			text.text = Mathf.Ceil((musicSlider.value *100)).ToString();
		}*/

		
        
	}

	public void ValueChangeCheck2()
	{
		if(Mathf.Ceil(sfxSlider.value*100) % 10 != 0){
			sfxNum.text = Mathf.Floor(sfxSlider.value*100).ToString();
		} else{
			sfxNum.text = Mathf.Ceil(sfxSlider.value*100).ToString();
		}

		/*
		if((musicSlider.value *100)>=50){
			text.text = Mathf.Floor((musicSlider.value *100)).ToString();
		} else {
			text.text = Mathf.Ceil((musicSlider.value *100)).ToString();
		}*/

		// Play a test sound (funky frog)
        FMODUnity.RuntimeManager.PlayOneShot("event:/Sounds/Go");
        
	}
}
