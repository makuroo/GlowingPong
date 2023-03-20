using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject introPanel;
    [SerializeField] private List<GameObject> text = new List<GameObject>();
    [SerializeField] private List<Button> introButtons = new List<Button>();
    // Start is called before the first frame update
    void Start()
    {
        //Play click sfx on click
        GetComponent<Button>().onClick.AddListener(()=>AudioManager.audioManagerInstance.Play("MouseClick"));

        //Add event trigger onPointerEnter
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { OnPointerEnterDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnPointerEnterDelegate(PointerEventData data)
    {
        AudioManager.audioManagerInstance.Play("MouseHover");
    }
    public void IntoductionPanel()
    {
        introPanel.SetActive(true);
    }

    public void CloseIntroductionPanel()
    {
        introPanel.SetActive(false);
    }

    public void Next()
    {
        text[0].SetActive(false);
        text[1].SetActive(true);
        introButtons[1].gameObject.SetActive(true);
        introButtons[0].gameObject.SetActive(false);
    }

    public void Prev()
    {
        text[1].SetActive(false);
        text[0].SetActive(true);
        introButtons[0].gameObject.SetActive(true);
        introButtons[1].gameObject.SetActive(false);
    }
}
