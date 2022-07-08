using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 6;
    private Rigidbody rigidbody;
    public TextMeshProUGUI time;
    public float timeElapsed = 0f;
    public bool gameStarted = false;
    public bool gameFinished = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        time.text = "";
        
    }

    // Update is called once per frame
    void Update()
    {
        var velo = rigidbody.velocity;
        float hor = Input.GetAxis ("Horizontal");  
        float ver = Input.GetAxis ("Vertical");
        float y_position = rigidbody.position.y;
        Vector3 moveVer = transform.forward * ver;
        Vector3 moveHor = transform.right * hor;
        Vector3 moveUp = new Vector3(0,velo.y,0);
        RaycastHit groundDistance;
        Physics.Raycast(rigidbody.position, -Vector3.up, out groundDistance);
        Debug.Log(groundDistance.distance);
        
         if (Input.GetKeyDown(KeyCode.Space) && groundDistance.distance <=1){
            moveUp = 5 * transform.up;
        }
        velo = (moveVer + moveHor) * -speed + moveUp;
    
        rigidbody.velocity = velo;
        float mouseX = Input.GetAxis ("Mouse X");  
        transform.Rotate(0, mouseX * 8, 0);
    
        if(rigidbody.position.z < 24.63 && rigidbody.position.x > -25.58 && !gameFinished){
            gameStarted = true;
            timeElapsed += Time.deltaTime;
            time.text = "Gecen Zaman: "+ timeElapsed.ToString("0");    
        }
        else if(rigidbody.position.z >= 24.63){
            gameStarted = false;
        }
        else if(gameStarted){
            time.text = "Oyun bitti. Bitirme suresi: "+ timeElapsed.ToString("0") +" saniye";  
            gameFinished = true;
        }
    }
}
