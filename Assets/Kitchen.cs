using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kitchen : MonoBehaviour
{
    public bool cooking = false;
    public float cookTimer = 10.0f;

    public Item FoodItem;

    private Text cookTime;
	
	private AudioSource foodReadySound;
	private AudioSource OrderRecievedSound;
	private AudioSource CookingSound;
    // Start is called before the first frame update
    void Start()
    {
        cookTime = transform.Find("FoodCounter").Find("Canvas").Find("FoodCount").GetComponent<Text>();
        cookTime.gameObject.SetActive(false);
		
		//for sounds
		foodReadySound = GetComponents<AudioSource>()[0];
		OrderRecievedSound = GetComponents<AudioSource>()[1];
		CookingSound = GetComponents<AudioSource>()[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (cooking == true){
            cookTimer = cookTimer - Time.deltaTime;

            if(cookTime.gameObject.activeSelf == false){
                cookTime.gameObject.SetActive(true);
            }

            cookTime.text = Math.Round(cookTimer).ToString();


           
            if (cookTimer <= 0){
                cooking = false;
                cookTime.gameObject.SetActive(false);
                Item newFood = (Item) Instantiate(FoodItem, transform.position, transform.rotation);
                newFood.gameObject.transform.position = new Vector3(27, 7, 1);
                cookTimer = 10.0f;
                //FOOD READY SOUND GOES HERE
				foodReadySound.Play();
            }
        }
    }

    void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == "Menu"){
            if (cooking == false){
            Destroy(col.gameObject);
            cooking = true;
            //ORDER RECIEVED SOUND GOES HEREs
			OrderRecievedSound.Play();
			CookingSound.Play();
            }
            else{
                return;
            }
            
        }
    }
}
