using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;


public class EndlessGameController : MonoBehaviour
{
    public enum GAMESTATE
    {
        MENU,
        READY,
        PLAYING,
        PAUSE,
        RESULT
    }
    [System.NonSerialized] public bool isFlying;
    [SerializeField] private GameObject landoltPref;
    private float distance = 100;
    public GAMESTATE gameState;
    private Vector3 startPos;
    [System.NonSerialized] public float eyesight = 1.0f;
    [System.NonSerialized] public bool isMissed;
    private float pastSpeed = 35;
    public int count;
    public PostProcessVolume volume;
    private DepthOfField dof;
    private Vignette vignette;
    private Bloom bloom;
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private HandMove hand;
    [SerializeField] private CanvasGroup resultPanel;
    [SerializeField] private CanvasGroup pausePanel;
    [SerializeField] private CanvasGroup pauseButton;




    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector3(0, 0, distance);
        volume.profile.TryGetSettings<DepthOfField>(out dof);
        volume.profile.TryGetSettings<Vignette>(out vignette);
        volume.profile.TryGetSettings<Bloom>(out bloom);
        
        gameState = GAMESTATE.MENU;
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GAMESTATE.PLAYING)
        {
            if (!isFlying)
            {
                GameObject landolt = Instantiate(landoltPref, startPos, Quaternion.identity);
                count++;
            }
            if (isMissed)
            {
                gameState = GAMESTATE.RESULT;
                count--;
                StartCoroutine(FinishGame());
            }
        }
    }

    ///////////////////ボタンからのゲームステート更新メソッド///////////////////
    public void PauseGame()
    {
        SetCanvasActive(pauseButton, false);
        SetCanvasActive(pausePanel, true);
        gameState = GAMESTATE.PAUSE;
    }

    public void ResumeGame()
    {
        SetCanvasActive(pausePanel, false);
        SetCanvasActive(pauseButton, true);
        gameState = GAMESTATE.PLAYING;
    }

    public void StartGame()
    {
        gameState = GAMESTATE.READY;
        eyesight = 1.2f;
        count = 0;
        isMissed = false;
        hand.direction = Player.DIRECTION.FORWARD;
        ChangeDepthOfField();
        SetCanvasActive(pauseButton, false);
        SetCanvasActive(resultPanel, false);
        SetCanvasActive(pausePanel, false);
        StartCoroutine(CountDown());
    }

    public void QuitGame()
    {
        gameState = GAMESTATE.MENU;
        SceneManager.LoadScene("MenuScene");
    }

    //キャンバスグループの疑似アクティブ処理
    void SetCanvasActive(CanvasGroup group, bool isActive)
    {
        if (isActive) group.alpha = 1;
        else group.alpha = 0;

        group.interactable = isActive;
        group.blocksRaycasts = isActive;
    }


    ///////////////////エフェクト関連メソッド・コルーチン///////////////////
    //解像度の計算と更新
    public void ChangeDepthOfField()
    {
        //eyesight1がのときに3、小さくなると同じ用に小さくなるが0より小さくならない
        //=3x(x^2-3x+3)
        dof.focusDistance.value = 3 * eyesight * (eyesight * eyesight - 3 * eyesight + 3);
    }

    //成功時のブルーム(画面全体を明るく)
    public IEnumerator ChangeBloom()
    {
        yield return new WaitForSeconds(0.2f);
        for (float i = 1.0f; i > 0.5f; i -= 0.05f)
        {
            bloom.threshold.value = i;
            yield return null;
        }
        for (float i = 0.5f; i < 1.0f; i += 0.05f)
        {
            bloom.threshold.value = i;
            yield return null;
        }
    }


    //ミス時のビネット(画面端を暗く)
    public IEnumerator ChangeVignette()
    {
        yield return new WaitForSeconds(0.2f);
        for (float i = 0f; i < 0.5f; i += 0.05f)
        {
            vignette.intensity.value = i;
            yield return null;
        }
        for (float i = 0.5f; i > 0.0f; i -= 0.05f)
        {
            vignette.intensity.value = i;
            yield return null;
        }
    }


    ///////////////////ゲームステートの繊維処理コルーチン///////////////////
    //スタート時のカウントダウン処理
    IEnumerator CountDown()
    {
        for (int i = 3; i > 0; i--)
        {
            countDownText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        countDownText.text = "START";
        yield return new WaitForSeconds(1);
        countDownText.text = "";
        gameState = GAMESTATE.PLAYING;
        SetCanvasActive(pauseButton, true);
    }

    //ゲーム終了
    IEnumerator FinishGame()
    {
        SetCanvasActive(pauseButton, false);
        SetCanvasActive(pausePanel, false);
        countDownText.text = "FINISH";
        yield return new WaitForSeconds(2);
        countDownText.text = "";
        resultText.text = "";

        SetCanvasActive(resultPanel, true);
        yield return new WaitForSeconds(1);
        resultText.text = "スコア：";
        yield return new WaitForSeconds(1);
        resultText.text += count.ToString();

    }


    
}
