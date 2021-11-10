using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	// Start is called before the first frame update
	private UnityEngine.AI.NavMeshAgent agent;
	public double HP = 100;
	public GameObject Player;
	public Player player;
	public GameObject[] Spawners;
	public UnityEngine.Object EnemyRef;
	public Text text;
	private void OnCollisionStay(Collision col)
	{
		
		if (col.collider.CompareTag("KillZone"))
		{
			HP -= 0.5;
			text.text = ("Enemy hp: "+HP.ToString());
		}
	}
	void Start()
	{
		
		text = GameObject.FindObjectOfType<Canvas>().transform.Find("EnemyHP").GetComponent<Text>();
		text.text = ("Enemy hp: " + HP.ToString());
		Player = GameObject.FindGameObjectWithTag("Player");
		Spawners = GameObject.FindGameObjectsWithTag("EnemySpawn");
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		
	}
	void Update()
	{
		agent.SetDestination(Player.transform.position);
		
		if (HP <= 0)
		{
			gameObject.SetActive(false);
			text.text = ("Enemy is dead :)");
			Invoke("Died",10f);
		}
	}

	void Died()
	{
		GameObject spawner = Spawners[Random.Range(0, Spawners.Length)];
		GameObject newEnemy = (GameObject)Instantiate(EnemyRef,new Vector3(spawner.transform.position.x, spawner.transform.position.y+2f, spawner.transform.position.z),Quaternion.identity);
		newEnemy.gameObject.tag = "Enemy";
		Destroy(gameObject);
	}
}
