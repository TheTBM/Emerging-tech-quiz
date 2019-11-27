using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;

public class Player : MonoBehaviour
{

    SerialPort ardIn = new SerialPort("COM4", 115200);

    public float jumpForce = 200.0f;
    public bool onGround = true;

    Rigidbody rb;
    float timer;

    public GameObject box;
    GameObject b;

    bool dead;

    public Text text;

    bool buttonDown;
    bool prevButtonDown;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        dead = false;
        text.text = "";

        ardIn.Open();

        ardIn.ReadTimeout = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ardIn.IsOpen)
        {
            try
            {
                string input = ardIn.ReadLine();
                print(input);

                string[] valueStrings = input.Split(',');

                if (valueStrings[5] == "1")
                {
                    buttonDown = true;
                }

                if (valueStrings[5] == "0")
                {
                    buttonDown = false;
                }

                if (!dead)
                {
                    if (getButton())
                    {
                        timer += Time.deltaTime;
                    }

                    if (getButtonUp() && rb.velocity.y == 0.0f)
                    {
                        if (timer < 0.5f)
                        {
                            jump();
                        }

                        else
                        {
                            spawnCube();
                        }
                    }

                    if (!getButton())
                    {
                        timer = 0.0f;
                    }

                    float vel = float.Parse(valueStrings[3]);

                    if (vel > 10.0f)
                    {
                        rb.velocity = new Vector3(-vel * 0.25f, rb.velocity.y, rb.velocity.z);
                    }

                    if (vel < -10.0f)
                    {
                        rb.velocity = new Vector3(-vel * 0.25f, rb.velocity.y, rb.velocity.z);
                    }
                    
                }

                else
                {
                    if (getButton())
                    {
                        resetPlayer();
                    }
                }

                prevButtonDown = buttonDown;
            }

            catch (System.Exception)
            {
            }
        }
    }

    void jump()
    {
        rb.AddForce(new Vector3(0, 250, 0), ForceMode.Force);
    }

    void spawnCube()
    {

        b = Instantiate(box, this.gameObject.transform.position + new Vector3(1.1f, 0, 0), this.gameObject.transform.rotation) as GameObject;
    }

    void resetPlayer()
    {
        dead = false;
        gameObject.transform.position = new Vector3(-21.3f, 1.6f, 0);
        text.text = "";
    }

    bool getButton()
    {
        if (buttonDown && prevButtonDown)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    bool getButtonDown()
    {
        if (buttonDown && !prevButtonDown)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    bool getButtonUp()
    {
        if (!buttonDown && prevButtonDown)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Death")
        {
            dead = true;
            text.text = ("You're dead!\n Press space to reset");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Win")
        {
            dead = true;
            text.text = ("You win!\n Press space to reset");
        }
    }
}
