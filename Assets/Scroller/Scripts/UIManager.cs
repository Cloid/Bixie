using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Slider healthUI;
	public Image playerImage;
	public Text playerName;
	public Text livesText;
	public Text displayMessage;


	public GameObject heartContainer;
    private float fillValue;
    private float oldValue;
	public GameObject enemyUI;
	public Slider enemySlider;
	public Text enemyName;
	public Image enemyImage;

	public float enemyUITime = 4f;

	private float enemyTimer;
	private Player player;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player>();
		// healthUI.maxValue = player.maxHealth;
		// healthUI.value = healthUI.maxValue;
		// playerName.text = player.playerName;
		// playerImage.sprite = player.playerImage;
		UpdateLives();

	}
	
	// Update is called once per frame
	void Update () {

		enemyTimer += Time.deltaTime;
		if(enemyTimer >= enemyUITime)
		{
			enemyUI.SetActive(false);
			enemyTimer = 0;
		}

		// fillValue = (float)GameController.Health;
        // fillValue = fillValue / GameController.MaxHealth;
        // print(Time.deltaTime);
		UpdateHealth();

	}

	public void UpdateHealth()
	{
		// healthUI.value = amount;
		fillValue = (float)player.curHealth() / player.maxHealth;
		
		// print("player:"+player+"cur:"+fillValue+", total:"+player.maxHealth);
		// print(Time.deltaTime);
		
		oldValue = heartContainer.GetComponent<Image>().fillAmount;
		
        heartContainer.GetComponent<Image>().fillAmount = Mathf.Lerp(oldValue,fillValue, Time.deltaTime);
		// heartContainer.GetComponent<Image>().fillAmount = fillValue;
		// print(heartContainer.GetComponent<Image>().fillAmount);
	}

	public void UpdateEnemyUI(int maxHealth, int currentHealth, string name, Sprite image)
	{
		enemySlider.maxValue = maxHealth;
		enemySlider.value = currentHealth;
		// enemyName.text = name;
		// enemyImage.sprite = image;
		enemyTimer = 0;
		enemyUI.SetActive(true);
	}

	public void UpdateLives()
	{
		livesText.text = "X " + FindObjectOfType<GameManager>().lives.ToString();
	}

	public void UpdateDisplayMessage(string message)
	{
		displayMessage.text = message;
		
	}
}
