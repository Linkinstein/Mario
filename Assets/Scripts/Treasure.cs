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

    public void hit(float x)
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
            }
            if (star)
            {

            }
        }
    }
}
