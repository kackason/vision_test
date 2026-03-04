using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LandoltMove : MonoBehaviour
{
    public float speed = 35;
    [SerializeField] private float speedChangeRate = 2;
    public Player.DIRECTION direction;
    public float changePos;
    public float changeSize;
    private GameObject camera;
    private GameController gc;
    private HandMove hand;
    private float sizeChangeRate;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera");
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        gc.isFlying = true;
        hand = GameObject.Find("Player").GetComponent<HandMove>();
        int num = Random.Range(0, 4);
        direction = EnumUtility.NoToType<Player.DIRECTION>(num);
        transform.eulerAngles = new Vector3(0, 0, 180 + 90 * num);
        transform.localScale = new Vector3(1, 1, 1);
        speed += gc.count * speedChangeRate;
    }

    // Update is called once per frame
    void Update()
    {

        if (gc.gameState == GameController.GAMESTATE.PLAYING)
        {
            transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
            if (transform.position.z < -10)
            {
                gc.isFlying = false;
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
        else if (gc.gameState != GameController.GAMESTATE.PAUSE)
        {
            //プレイ中・ポーズ中以外は削除
            Destroy(this.gameObject);
            gc.isFlying = false;
        }
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (direction == hand.direction)
            {
                gc.eyesight += 0.2f;
                gc.StartCoroutine(gc.ChangeBloom());

            }
            else
            {
                if (gc.eyesight > 0.0f) gc.eyesight -= 0.2f;
                gc.StartCoroutine(gc.ChangeVignette());
                if (gc.eyesight < 0.0f) gc.eyesight = 0.0f;
            }
            Debug.Log("eyesight: " + gc.eyesight.ToString());
            gc.ChangeDepthOfField();
            
        }
    }


    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            hand.direction = Player.DIRECTION.FORWARD;
            // gc.isFlying = false;
            // Destroy(this.gameObject);
        }
    }
}
