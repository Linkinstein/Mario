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

    private void Start()
    {
        gmGO = GameObject.FindWithTag("GameManager");
        gm = gmGO.GetComponent<GameManager>();
        if (invisible) this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
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
                coinTimes--;
                if (coinTimes <= 0)
                {
                    GameObject coin = Instantiate(coinPrefab, newPOS, this.gameObject.transform.rotation);
                    activated = false;
                    gm.getScore(200);
                    StartCoroutine(destroyCoin(coin));
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
