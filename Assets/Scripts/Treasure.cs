using System.Collections;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    GameObject gmGO;
    GameManager gm;
    [SerializeField] bool activated = true;
    [SerializeField] bool shroom;
    [SerializeField] bool star;
    [SerializeField] bool coin;
    [SerializeField] bool itemless;
    [SerializeField] bool fake;
    [SerializeField] bool invisible;
    [SerializeField] int coinTimes = 0;

    [SerializeField] GameObject shroomPrefab;
    [SerializeField] GameObject starPrefab;
    [SerializeField] GameObject coinPrefab;

    //[SerializeField] Sprite blockSprite;

    private void Start()
    {
        gmGO = GameObject.FindWithTag("GameManager");
        gm = gmGO.GetComponent<GameManager>();
        if (invisible) this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        /**if (fake)
        {
        this.gameObject.GetComponent<Animator>.enabled = false;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = blockSprite;
        }**/
    }

    public void hit(float x, bool big)
    {

        //jiggle? no time baby
        if (activated)
        {
            Vector2 newPOS = this.gameObject.transform.position;
            newPOS.y = newPOS.y + 0.25f;

            if (coin)
            {
                newPOS.y = newPOS.y + 0.25f;
                gm.coined();
                coinTimes--;
                GameObject coin = Instantiate(coinPrefab, newPOS, this.gameObject.transform.rotation);
                StartCoroutine(destroyCoin(coin));
                if (coinTimes <= 0)
                {
                    activated = false;
                }
            }

            if (shroom)
            {
                activated = false;
                GameObject shroom = Instantiate(shroomPrefab, newPOS, this.gameObject.transform.rotation);
                shroom.GetComponent<Items>().x = x;
                if(big) shroom.GetComponent<Items>().Flower();
            }

            if (star)
            {
                activated = false;
                GameObject star = Instantiate(starPrefab, newPOS, this.gameObject.transform.rotation);
                star.GetComponent<Items>().x = x;
            }
        }
    }

    IEnumerator destroyCoin(GameObject coin)
    {
        yield return new WaitForSeconds(1f);
        Destroy(coin);
    }
}
