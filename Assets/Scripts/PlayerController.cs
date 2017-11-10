using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour {

    public float speed; // if the variable is public, it appears in the editor
	public Text countText;
	public Text winText;

    private Rigidbody rb;
	private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
		count = 0;
		winText.text = "";
		setCountText();
    }

    // Update is called before rendering a frame
    void Update ()
	{
		switch (SystemInfo.deviceType) {
		case DeviceType.Desktop:
			// Exit condition for Desktop devices
			if (Input.GetKey ("escape"))
				Application.Quit ();
			break;
		case DeviceType.Handheld:
			// if (XRSettings.isDeviceActive)
			if (XRDevice.isPresent) {
			} else {
				// Exit condition for mobile devices
				if (Input.GetKeyDown (KeyCode.Escape))
					Application.Quit ();    
			}
			break;
		}
	}

    // Fixed Update is called before perform any physics calculations
    void FixedUpdate()
    {
		float moveHorizontal, moveVertical;

		switch (SystemInfo.deviceType) 
		{
			case DeviceType.Desktop:
				moveHorizontal = Input.GetAxis ("Horizontal");
				moveVertical = Input.GetAxis ("Vertical");
			break;
			case DeviceType.Handheld:
//				if (XRSettings.isDeviceActive)
				if (XRDevice.isPresent)
				{
					moveHorizontal = Input.GetAxis ("Mouse X");
					moveVertical = Input.GetAxis ("Mouse Y");
				}
				else
				{
					moveHorizontal = Input.acceleration.x;
					moveVertical = Input.acceleration.y;
				}
			break;
			default:
				moveHorizontal = 0;
				moveVertical = 0;
			break;
		}

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed * 2);
    }

    void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			other.gameObject.SetActive (false);
			count++;
			setCountText();
		}
    }

	void setCountText()
	{
		countText.text = "Count: " + count.ToString();
		if (count >= 12)
			winText.text = "You win!";	
	}
}


