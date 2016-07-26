using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class NotificationController : MonoBehaviour {
	public GameObject m_notificationPrefab;
	public GameObject m_notificationGrid;
	public ScrollRect m_scrollArea;
	public Animator m_arrowAnimator;
	public Sprite[] m_avatars;
	public string[] m_notificationText;

	/** Can be NULL on deselecting all of them */
	public event Action<InfoCardController> OnNotificationSelected;

	private InfoCardController m_activeCard;
	private float m_endOfGrid;

	public void RemoveNotification(InfoCardController controller)
	{
		if (m_activeCard == controller)
			m_activeCard = null;

		RectTransform controllerRect = (RectTransform)controller.transform;
		// Hack, no easy way to check it's size since it's collapsed
		float removedWidth = 250f + m_notificationGrid.GetComponent<HorizontalLayoutGroup>().spacing;

		controller.OnRemoveAnimationFinished += (_) =>
		{
			RectTransform gridRect = (RectTransform)m_notificationGrid.transform;
			Vector2 newSize = new Vector2(gridRect.sizeDelta.x - removedWidth, gridRect.sizeDelta.y);
			Vector3 newPos = new Vector3(Mathf.Min(Mathf.Max(gridRect.localPosition.x, 0), gridRect.localPosition.x + removedWidth), gridRect.localPosition.y, gridRect.localPosition.z);
			gridRect.sizeDelta = newSize;
			gridRect.localPosition = newPos;
			Destroy(controller.gameObject);
		};

		controller.StartRemove();
	}

	void Start () {
		if (m_avatars.Length != m_notificationText.Length)
			Debug.LogError("Amount of avatars not consistent with notifications texts");
		else
		{
			float spacing = m_notificationGrid.GetComponent<HorizontalLayoutGroup>().spacing;
			float totalWidth = (m_avatars.Length - 1) * spacing;
			GameObject[] cards = new GameObject[m_avatars.Length];

			for (int i = 0; i < m_avatars.Length; i++)
			{
				cards[i] = Instantiate(m_notificationPrefab);
				cards[i].transform.SetParent(m_notificationGrid.transform, false);
			}

			Canvas.ForceUpdateCanvases();

			for (int i = 0; i < m_avatars.Length; i++)
			{
				GameObject newNotification = cards[i];
				RectTransform newTransform = (RectTransform)newNotification.transform;
				totalWidth += newTransform.sizeDelta.x;
				InfoCardController controller = newNotification.GetComponent<InfoCardController>();
				controller.id = i;
				controller.m_avatarImage.sprite = m_avatars[i];
				controller.m_notificationText.text = m_notificationText[i];
				controller.OnInfoCardClicked += handleInfoCardPress;
			}
			RectTransform gridRect = (RectTransform)m_notificationGrid.transform;
			Vector2 newSize = new Vector2(totalWidth, gridRect.sizeDelta.y);
			gridRect.sizeDelta = newSize;
		}
	}

	void Update()
	{
		m_arrowAnimator.ForceStateNormalizedTime(Mathf.Min(0.999f, m_scrollArea.horizontalNormalizedPosition));
		//m_arrowAnimator.SetFloat("Percent", m_scrollArea.horizontalNormalizedPosition);
	}

	private void handleInfoCardPress(InfoCardController pressed)
	{
		if (m_activeCard == pressed)
		{
			pressed.SetState(false);
			m_activeCard = null;
		}
		else
		{
			if (m_activeCard != null)
			{
				RemoveNotification(m_activeCard);
				m_activeCard.SetState(false);
			}
			pressed.SetState(true);
			m_activeCard = pressed;
		}

		if (OnNotificationSelected != null)
			OnNotificationSelected(m_activeCard);
	}
}
