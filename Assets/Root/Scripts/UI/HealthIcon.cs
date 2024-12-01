using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIcon : MonoBehaviour
{
    [SerializeField] protected Image image;
    [SerializeField] protected Outline outline;

    public Image Image => image;
    public Outline Outline => outline;
}
