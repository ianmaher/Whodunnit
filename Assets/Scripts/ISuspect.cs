using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISuspect
{
	void Attack (GameObject killer);
	bool IsDead();
}


