using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    bool isGround;
    public Text text;
    public Text score;
    public GameObject enemy;
    public GameObject player;
    public GameObject Ground;
    public GameObject TimeZone;
    public GameObject TimeZone2;
    private Rigidbody _rb;
    public float JumpSpeed = 50f;
    public UnityEngine.Object CrystalRef;
    float moveHorizontal;
    float moveVertical;
    float Jump;
    public  double HP = 100;
    public int Score = 0;
    float mainSpeed = 10.0f;
    private void Start()
    {
        player = gameObject;
        CrystalRef = Resources.Load("Capsule");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        text = GameObject.FindObjectOfType<Canvas>().transform.Find("PlayerHP").GetComponent<Text>();
        text.text = ("Your hp: " + HP.ToString());
        score = GameObject.FindObjectOfType<Canvas>().transform.Find("Score").GetComponent<Text>();
        score.text = ("Score: " + Score.ToString());
        _rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
           //Debug.Log("floor en");
            isGround = true;
        }
        if (other.collider.CompareTag("KillZone"))
        {
            if (HP <= 100)
            {
                HP += 0.5;
                text.text = ("Your hp: " + HP.ToString());
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("TimeZoneDown"))
        {
            Time.timeScale = 0.5f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
        if (other.collider.CompareTag("TimeZoneUp"))
        {
            Time.timeScale = 1.5f;
        }
        if (other.collider.CompareTag("Crystal"))
        {
            other.gameObject.SetActive(false);
            Score++;
            score.text = ("Score: " + Score.ToString());
            CrystalSpawn(other.gameObject);
        }
        if (other.collider.CompareTag("Teleport"))
        {
            GameObject[] teleports = GameObject.FindGameObjectsWithTag("Teleport");

            if (teleports[0] == other.gameObject)
            {
                gameObject.transform.position = new Vector3(teleports[1].transform.position.x, teleports[1].transform.position.y);
            }
            if (teleports[1] == other.gameObject)
            {
                gameObject.transform.position = new Vector3(teleports[0].transform.position.x, teleports[0].transform.position.y);
            }
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
           // Debug.Log("floor ex");
            isGround = false;
        }
        if (other.collider.CompareTag("TimeZoneDown") || other.collider.CompareTag("TimeZoneUp"))
        {
            Time.timeScale = 1;
        }
    }
    void FixedUpdate()
    {
        if (enemy == null)
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy");
        }
            if (Vector3.Distance(player.transform.position, enemy.transform.position) <= 1)
            {
                HP -= 0.5;
                text.text = ("Your hp: " + HP.ToString());
            }
        if (HP <= 0)
        {
            gameObject.SetActive(false);
            text.text = ("Lol you died :)");
        }
            Move();
    }
    private void Move()
    {
        moveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime;
        moveVertical = Input.GetAxis("Vertical") * Time.deltaTime;
        if (isGround)
        {
            Jump = Input.GetAxis("Jump") * Time.deltaTime * JumpSpeed;
            Vector3 movement = new Vector3(moveHorizontal, Jump, moveVertical);
            GetComponent<Rigidbody>().AddForce(transform.up * Jump, ForceMode.Impulse);
        }
        transform.Translate(new Vector3(moveHorizontal * mainSpeed, 0.0f, moveVertical * mainSpeed));
    }

    private void CrystalSpawn(GameObject crystal)
    {
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("CrystalSpawn");
        GameObject spawn = spawns[Random.Range(0, spawns.Length)];
        GameObject newcrystal = (GameObject)Instantiate(CrystalRef, new Vector3(spawn.transform.position.x, spawn.transform.position.y + 1f, spawn.transform.position.z), Quaternion.identity);
        Destroy(crystal);
    }
}