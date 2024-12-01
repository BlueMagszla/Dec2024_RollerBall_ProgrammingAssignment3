using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class QuickPlacement : MonoBehaviour {

    [SerializeField] protected int width = 5;
    [SerializeField] protected float padding = .25f;

    [ExecuteInEditMode]
    void Update ()
    {
		for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            float x = i % width;
            float z = i / width;
            x += x * padding;
            z += z * padding;
            var pos = new Vector3(x, 0, z);
            child.transform.position = pos;
        }
	}

}
