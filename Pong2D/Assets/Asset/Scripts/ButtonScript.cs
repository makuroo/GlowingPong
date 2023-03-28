using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject introPanel;
    [SerializeField] private List<GameObject> descPanel = new List<GameObject>();
    [SerializeField] private List<Button> introButtons = new List<Button>();

    public static int panelIndex = 0;
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

    public void SetActivePanelNext()
    {
        panelIndex++;

        descPanel[panelIndex].SetActive(true);

        if(panelIndex == descPanel.Count - 1)
        {
            introButtons[0].gameObject.SetActive(false);
        }
        else
        {
            introButtons[0].gameObject.SetActive(true);
            introButtons[1].gameObject.SetActive(true);
        }

        for(int i =0; i<descPanel.Count; i++)
        {
            if (i != panelIndex)
            {
                descPanel[i].SetActive(false);
            }
        }
    }

    public void SetActivePanelPrev()
    {
        panelIndex--;

        descPanel[panelIndex].SetActive(true);

        if (panelIndex == 0)
        {
            introButtons[1].gameObject.SetActive(false);
        }
        else
        {
            introButtons[0].gameObject.SetActive(true);
            introButtons[1].gameObject.SetActive(true);
        }

        for (int i = 0; i < descPanel.Count; i++)
        {
            if (i != panelIndex)
            {
                descPanel[i].SetActive(false);
            }
        }
    }
}
