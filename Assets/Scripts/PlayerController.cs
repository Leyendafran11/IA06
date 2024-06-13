using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    int vida;

	private void Start()
	{
		vida = 3;

		this.GetComponent<Renderer>().material.color = Color.blue;

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("zarpa"))
		{
			vida--;

			if (vida == 2)
			{
				this.GetComponent<Renderer>().material.color = Color.yellow;
			}
			if (vida == 1)
			{
				this.GetComponent<Renderer>().material.color = Color.red;
			}


			if (vida == 0)
			{
				Destroy(this.gameObject);
			}
		}
	}
}


