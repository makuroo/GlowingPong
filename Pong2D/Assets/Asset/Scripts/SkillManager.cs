using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class SkillManager : MonoBehaviour
{
    public Skill skillType;
    public int save = 0;
    public float playerSpecialPercentage = 0;
    public GameObject playerGoal;
    private float chance;
    private int extraPoint = 2;
    [SerializeField] private TMP_Text txtPlayerSpecialSkillPercentage;
    public bool shieldOn = false;

    // Start is called before the first frame update
    void Start()
    {
        txtPlayerSpecialSkillPercentage.text = string.Format("{0:0.00}", playerSpecialPercentage)+"%";
    }

    // Update is called once per frame
    void Update()
    {
        if (save != 0)
        {
            SetExtraShieldPercentage(this);
        }

        if (playerSpecialPercentage >= 100)
        {
            playerSpecialPercentage = 100;
            txtPlayerSpecialSkillPercentage.text = string.Format("{0:0.00}", playerSpecialPercentage) + "%";
        }
        ExtraShield();
        ExtraPointVisual();
    }

    public void SetExtraPointPercentage(SkillManager player)
    {
        player.chance = Random.Range(20f, 30.1f);
        player.playerSpecialPercentage += player.chance;
        txtPlayerSpecialSkillPercentage.text = string.Format("{0:0.00}", player.playerSpecialPercentage) + "%";
    }

    public void ExtraPoint()
    {
        if (playerSpecialPercentage >= 100 && skillType == Skill.ExtraPoint)
        {
            if(GameManager.instance.playPowerup > 0)
            {
                GameManager.instance.playPowerup = 0;
            }
            GameManager.instance.point = extraPoint;
            playerSpecialPercentage = 0;
            txtPlayerSpecialSkillPercentage.text = string.Format("{0:0.00}", playerSpecialPercentage) + "%";
        }
        else
            GameManager.instance.point = 1;
    }

    public void ExtraPointVisual()
    {
        if(playerSpecialPercentage >= 100 && skillType == Skill.ExtraPoint)
        {
            ChangeColorSpecial(gameObject);
        }
        else
        {
            ChangeColorNormal(gameObject);
        }
    }

    private void ExtraShield()
    {
        if(playerSpecialPercentage >= 100 && skillType == Skill.ExtraShield)
        {
            
            playerSpecialPercentage = 0;
            txtPlayerSpecialSkillPercentage.text = string.Format("{0:0.00}", playerSpecialPercentage) + "%";
            shieldOn = true;
            AudioManager.audioManagerInstance.Play("Powerup");
            //disable isTrigger on goal collider creating a simple shield as OnTrigger is not called
            playerGoal.GetComponent<BoxCollider2D>().isTrigger = false;
            ChangeColorSpecial(playerGoal);
        }

        if (shieldOn == false)
        {
            //enable collider isTrigger so that enemy can score
            playerGoal.GetComponent<BoxCollider2D>().isTrigger = true;
            ChangeColorNormal(playerGoal);
        }
    }

    public void SetExtraShieldPercentage(SkillManager player)
    {
        var collision = player.GetComponent<Collider2D>();
        if (collision.gameObject.CompareTag("Player") && player.skillType == Skill.ExtraShield)
        {
            player.chance = Random.Range(20f, 30.1f);
            player.playerSpecialPercentage += player.chance;
            player.txtPlayerSpecialSkillPercentage.text = string.Format("{0:0.00}", player.playerSpecialPercentage) + "%";
        }

        if (collision.gameObject.CompareTag("Player2") && player.skillType == Skill.ExtraShield)
        {
            player.chance = Random.Range(20f, 30.1f);
            player.playerSpecialPercentage += player.chance;
            player.txtPlayerSpecialSkillPercentage.text = string.Format("{0:0.00}", player.playerSpecialPercentage) + "%";
        }

    }

    private void ChangeColorNormal(GameObject obj)
    {
        if (gameObject.CompareTag("Player"))
        {
            obj.GetComponent<SpriteRenderer>().color = new Color32(0, 136, 237, 255);
        }
        else
        {
            obj.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 71, 255);
        }
    }    
    private void ChangeColorSpecial(GameObject obj)
    {
        if (gameObject.CompareTag("Player"))
        {
            obj.GetComponent<SpriteRenderer>().color = new Color32(0, 221, 237, 255);
        }
        else
        {
            obj.GetComponent<SpriteRenderer>().color = new Color32(255, 132, 0, 255);
        }
    }


}
