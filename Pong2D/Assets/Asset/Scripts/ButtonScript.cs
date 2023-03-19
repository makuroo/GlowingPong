using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour
{

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
}
