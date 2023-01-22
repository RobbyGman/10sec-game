using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
public static Health Instance { get; private set; }

	public Image bar;

	float originalSize;

	// Use this for initialization
	void Awake ()
	{
		Instance = this;
	}

	void OnEnable()
	{
		originalSize = bar.rectTransform.rect.width;
	}

	public void SetValue(float value)
	{		
		bar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
	}}
