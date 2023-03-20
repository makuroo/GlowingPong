using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using Cinemachine;
public enum Skill
{
    ExtraPoint,
    ExtraShield
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int player1Score { get; private set;}
    public int player2Score { get; private set; }
    public int targetScore  { get; private set; }
    public CinemachineVirtualCamera vCam;

    [Space(3)]
    [Header("UI")]
    [SerializeField] private TMP_Text txtPlayer1Score;
    [SerializeField] private TMP_Text txtPlayer2Score;
    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private Canvas skillSelectionCanvas;
    [SerializeField] private GameObject player1SkillSelectPanel;
    private bool inSkillSelection = true;
    private int index;
    
    [Space(3)]
    [Header("Skill")]
    public List<SkillManager> player = new List<SkillManager>();
    public int point;
    public int playPowerup = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        player1Score = 0;
        player2Score = 0;
        targetScore = 10;
        txtPlayer1Score.text =  player1Score.ToString();
        txtPlayer2Score.text =  player2Score.ToString();
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && inSkillSelection == false)
        {
            if(pauseCanvas.gameObject.activeSelf == false)
            {
                pauseCanvas.gameObject.SetActive(true);
                Time.timeScale = 0;
            }

            else
            {
                pauseCanvas.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
                
        }
    }

    public void Score(string goalTag)
    {
        StartCoroutine(CameraShake(0.5f));
        if(goalTag == "Player1 Goal")
        {
            AudioManager.audioManagerInstance.PlayOneShot("Goal");
            Player1TakeEXtraPointSkill();
            //add score based on point value
            player2Score += point;
            txtPlayer2Score.text =  player2Score.ToString();
        }
        else if(goalTag == "Player2 Goal")
        {
            AudioManager.audioManagerInstance.PlayOneShot("Goal");
            Player2TakeExtraPointSkill();


            //add score based on point value
            player1Score +=point;
            txtPlayer1Score.text =  player1Score.ToString();
        }


    }


    public void ActivateParticleEfffects( ParticleSystem particleSystem)
    {
        if (particleSystem != null)
            particleSystem.Play();
    }

    private IEnumerator CameraShake(float shakeDuration)
    {
        vCam.GetComponent<CinemachineVirtualCamera>().enabled = true;
        yield return new WaitForSeconds(shakeDuration);
        vCam.GetComponent<CinemachineVirtualCamera>().enabled = false;
    }

    public void PlayerTakeExtraPointSkill()
    {
        player[index].skillType = Skill.ExtraPoint;
        index++;
        AudioManager.audioManagerInstance.PlayOneShot("MouseClick");
        player1SkillSelectPanel.gameObject.SetActive(false);
    }

    public void PlayerTakeExtraShieldSkill()
    {
        player[index].skillType = Skill.ExtraShield;
        index++;
        AudioManager.audioManagerInstance.PlayOneShot("MouseClick");
        player1SkillSelectPanel.gameObject.SetActive(false);
    }

    public void ResetTimeScale()
    {
        Time.timeScale = 1f;
        skillSelectionCanvas.gameObject.SetActive(false);
        inSkillSelection = false;
    }


    public IEnumerator GameEnded()
    {
        Time.timeScale = 0.01f;
        Time.fixedDeltaTime *= Time.timeScale; 
        yield return new WaitForSecondsRealtime(2f);
        Time.fixedDeltaTime = 0.02f;
        Time.timeScale = 0;
        gameOverCanvas.gameObject.SetActive(true);
    }


    private void Player1TakeEXtraPointSkill()
    {
        if (player[0].skillType == Skill.ExtraPoint)
        {
            player[0].SetExtraPointPercentage(player[0]);
            if (player[0].playerSpecialPercentage >= 100 && playPowerup == 0)
            {
                playPowerup++;
                AudioManager.audioManagerInstance.PlayOneShot("Powerup");
            }
        }
        //Player 2 score with extra point
        player[1].ExtraPoint();
    }

    private void Player2TakeExtraPointSkill()
    {
        if (player[1].skillType == Skill.ExtraPoint)
        {
            player[1].SetExtraPointPercentage(player[1]);
            if (player[1].playerSpecialPercentage >= 100)
            {
                if (playPowerup == 0)
                {
                    playPowerup++;
                    AudioManager.audioManagerInstance.PlayOneShot("Powerup");
                }
            }
        }

        //Player 1 score with extra point
        player[0].ExtraPoint();
    }

    public void CheckShieldStatus(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player1 Goal") && instance.player[0].shieldOn == true)
        {
            instance.player[0].shieldOn = false;
        }

        if (collision.gameObject.CompareTag("Player2 Goal") && instance.player[1].shieldOn == true)
        {
            instance.player[1].shieldOn = false;
        }
    }
}
