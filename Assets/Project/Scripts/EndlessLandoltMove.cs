using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EndlessLandoltMove : MonoBehaviour
{
    public float speed = 35;
    [SerializeField] private float speedChangeRate = 2;
    public Player.DIRECTION direction;
    public float changePos;
    public float changeSize;
    private GameObject camera;
    private EndlessGameController egc;
    private HandMove hand;
    private float sizeChangeRate;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera");
        egc = GameObject.Find("EndlessGameController").GetComponent<EndlessGameController>();
        egc.isFlying = true;
        hand = GameObject.Find("Player").GetComponent<HandMove>();
        int num = Random.Range(0, 4);
        direction = EnumUtility.NoToType<Player.DIRECTION>(num);
        transform.eulerAngles = new Vector3(0, 0, 180 + 90 * num);
        transform.localScale = new Vector3(1, 1, 1);
        speed += egc.count * speedChangeRate;
    }

    // Update is called once per frame
    void Update()
    {        
        if (egc.gameState == EndlessGameController.GAMESTATE.PLAYING)
        {
            transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
            if (transform.position.z < -10)
            {
                egc.isFlying = false;
                Destroy(this.gameObject);
            }
            if (transform.position.z > changePos)
            {
                sizeChangeRate = (1 - changeSize) / (100 - changePos);
            }
            else
            {
                sizeChangeRate = (changeSize - 100) / changePos;
            }
            transform.localScale += new Vector3(sizeChangeRate, sizeChangeRate, sizeChangeRate) * Time.timeScale;
        }
        else if (egc.gameState != EndlessGameController.GAMESTATE.PAUSE)
        {
            //プレイ中・ポーズ中以外は削除
            Destroy(this.gameObject);
            egc.isFlying = false;
        }
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (direction == hand.direction)
            {
                egc.eyesight += 0.2f;
                egc.StartCoroutine(egc.ChangeBloom());

            }
            else
            {
                egc.isMissed = true;
                if (egc.eyesight > 0.0f) egc.eyesight -= 0.2f;
                egc.StartCoroutine(egc.ChangeVignette());
                if (egc.eyesight < 0.0f) egc.eyesight = 0.0f;
            }
            Debug.Log("eyesight: " + egc.eyesight.ToString());
            egc.ChangeDepthOfField();
            
        }
    }


    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            hand.direction = Player.DIRECTION.FORWARD;
        }
    }
}
