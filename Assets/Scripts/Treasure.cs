using UnityEngine;

public class Treasure : MonoBehaviour
{
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

    private void Start()
    {
        if (invisible) this.gameObject.GetComponent<Renderer>().enabled = false;
    }

    public void hit(float x, bool big)
    {
        //jiggle
        if (activated)
        {
            if (coin)
            {
                if (coinTimes <= 0)
                {
                    activated = false;
                }
                coinTimes--;
            }
            if (shroom)
            {
                activated = false;
                Vector2 newPOS = this.gameObject.transform.position;
                newPOS.y = newPOS.y + 0.25f;
                GameObject shroom = Instantiate(shroomPrefab, newPOS, this.gameObject.transform.rotation);
                shroom.GetComponent<Items>().x = x;
                if(big) shroom.GetComponent<Items>().Flower();
            }
            if (star)
            {
                activated = false;

            }
        }
    }
}
