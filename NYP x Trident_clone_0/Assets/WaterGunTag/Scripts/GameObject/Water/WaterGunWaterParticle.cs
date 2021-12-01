using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGunWaterParticle : MonoBehaviour
{
    //�^����_���[�W��
    [SerializeField] private int m_damage = 1;

    void Start()
    {
       
    }


    void Update()
    {

       
    }


    private void OnParticleCollision(GameObject other)
    {
        //�v���C���[�ւ̃_���[�W
        if(other.CompareTag("Player"))
        {
            if(other!= transform.root.gameObject)
            {
                other.GetComponent<PlayerActions>().HitWater(m_damage);
            }
          
        }

        //�|�C���g���Z
        if(other.CompareTag("Point"))
        {
            //�_���[�W��^����
            if ( !transform.root.gameObject.GetComponent<PlayerActions>().GetIsChase())
            {
                other.GetComponent<PointObject>().HitWater(m_damage);

                //�|�C���g�擾
                if (other.GetComponent<PointObject>().GetIsDestroy())
                {
                    transform.root.gameObject.GetComponent<PlayerActions>().m_point +=
                        other.GetComponent<PointObject>().GetPoint();
                }
            }
            
        }

    }
}





