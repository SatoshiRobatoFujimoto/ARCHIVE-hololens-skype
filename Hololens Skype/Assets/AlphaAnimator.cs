using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasRenderer))]
public class AlphaAnimator : MonoBehaviour {
	public float m_value = 1f;

	private CanvasRenderer m_renderer;

	void Start()
	{
		m_renderer = GetComponent<CanvasRenderer>();
		m_value = m_renderer.GetAlpha();
	}

	void Update ()
	{
		m_renderer.SetAlpha(m_value);
	}
}
