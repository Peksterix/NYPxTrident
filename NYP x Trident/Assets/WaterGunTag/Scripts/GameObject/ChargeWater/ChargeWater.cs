using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeWater : MonoBehaviour
{
    //…‚Ì•â‹‹—Ê
    [SerializeField] int m_waterChargeNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //“–‚½‚è”»’è‚Ìˆ—
    private void OnTriggerStay(Collider other)
    {
        //ƒvƒŒƒCƒ„[‚É“–‚½‚Á‚½‚ç…‚Ì‰ñ•œ‚ğ‚Å‚«‚é‚æ‚¤‚É‚·‚é
        if (other.transform.CompareTag("Player"))
        {
            if (!other.gameObject.GetComponent<WGTPlayerController>().m_isInoperable)
            {
                if (Input.GetMouseButton(1))
                {
                    if (!other.gameObject.GetComponentInChildren<WaterGun>().GetIsShotWaterGun())
                    {
                        other.gameObject.GetComponentInChildren<WaterGun>().ChargeWaterGauge(m_waterChargeNum);
                    }
                }
            }

        }
    }

}
