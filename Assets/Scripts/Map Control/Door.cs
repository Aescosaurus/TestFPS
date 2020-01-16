using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Door
	:
	MonoBehaviour
{
	void Start()
	{
		Assert.IsTrue( !openable || levelName != "" );
	}

	[SerializeField] public bool openable = false;
	[SerializeField] public string levelName = "";
}
