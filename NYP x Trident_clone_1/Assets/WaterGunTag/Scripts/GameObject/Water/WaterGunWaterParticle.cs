using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGunWaterParticle : MonoBehaviour
{
    //与えるダメージ量
    [SerializeField] private int m_damage = 1;

    void Start()
    {
       
    }


    void Update()
    {

       
    }


    private void OnParticleCollision(GameObject other)
    {
        //プレイヤーへのダメージ
        if(other.CompareTag("Player"))
        {
            if(other!= transform.root.gameObject)
            {
                other.GetComponent<PlayerActions>().HitWater(m_damage);
            }
          
        }

        //ポイント加算
        if(other.CompareTag("Point"))
        {
            //ダメージを与える
            if ( !transform.root.gameObject.GetComponent<PlayerActions>().GetIsChase())
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





