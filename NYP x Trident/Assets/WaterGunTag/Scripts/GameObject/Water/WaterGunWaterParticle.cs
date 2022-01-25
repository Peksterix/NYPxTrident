using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WaterGunWaterParticle : MonoBehaviour
{
    //与えるダメージ量
    NetworkIdentity owner;
    [SerializeField] private int m_damage = 1;

    void Awake()
    {
        owner = GetComponentInParent<NetworkIdentity>();
    }

    private void OnParticleCollision(GameObject other)
    {
        // Returns if this is not a server
        if (!NetworkServer.active) return;

        //プレイヤーへのダメージ
        if(other.CompareTag("Player"))
        {
            if(other != transform.root.gameObject && 
                other.GetComponent<PlayerActions>().isChase != owner.GetComponent<PlayerActions>().isChase)
            {
                if (owner.GetComponent<PlayerActions>().isChase)
                {
                    bool isDead = other.GetComponent<PlayerActions>().HitWater(m_damage);
                    if (isDead)
                    {
                        other.GetComponent<PlayerActions>().ChangeChase();
                        owner.GetComponent<PlayerActions>().ChangeRunningAway();
                    }
                }
                else
                {
                    // In this case, it's a runner targetting a chaser
                }
            }
          
        }

        //ポイント加算
        if(other.CompareTag("Point"))
        {
            //ダメージを与える
            if (!transform.root.gameObject.GetComponent<PlayerActions>().isChase)
            {
                other.GetComponent<PointObject>().HitWater(m_damage);

                //ポイント取得
                if (other.GetComponent<PointObject>().GetIsDestroy())
                {
                    transform.root.gameObject.GetComponent<PlayerActions>().m_point +=
                        other.GetComponent<PointObject>().GetPoint();
                }
            }
            
        }

    }
}





