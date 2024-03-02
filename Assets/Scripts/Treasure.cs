using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField] bool activated = true;
    [SerializeField] bool shroom;
    [SerializeField] bool star;
    [SerializeField] bool coin;
    [SerializeField] int coinTimes = 0;

    public void hit()
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

            }
            if (star)
            {

            }
        }
    }
}
