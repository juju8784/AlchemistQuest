using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Runtime.ExceptionServices;
using UnityEngine;

public static class DecideAbility
{
    // Start is called before the first frame update
    public static GameObject CreateAbility(List<Abilities> Queue, Vector3 pos, Quaternion rotation)
    {
        GameObject Ability = GameObject.Instantiate(Resources.Load("AbilityPrefabs/BaseProjectile") as GameObject, pos, rotation);
        GameObject.Destroy(Ability);
        //Debug.Log("Hit This");
        if (Queue.Count == 0)
        {
            Ability = FindAbility(Abilities.NULL, pos, rotation);
        }
        else if (Queue.Count == 1)
        {
            Ability = FindAbility(Queue[0], pos, rotation);
        }
        else if (Queue.Count >= 2)
        {
            Ability = FindAbility(Queue[0], pos, rotation, Queue[1]);
        }
        return Ability;

    }

    static private Color GetColor(Abilities ability)
    {
        Color col = Color.white;
        if (ability == Abilities.red)
        {
            col = Color.red;
        }
        else if (ability == Abilities.blue)
        {
            col = Color.blue;
        }
        else if (ability == Abilities.green)
        {
            col = Color.green;
        }
        else if (ability == Abilities.yellow)
        {
            col = Color.yellow;
        }

        return col;
    }

    /* Fancy Idea
     Base Spell Enum 0-3;
     0 Red Spells 4-7;
     1 Blue Spells 8-11;
     2 green Spells 12-15;
     3 Yellow Spells 16-19
     */
    public static GameObject FindAbility(Abilities FirstSlot, Vector3 pos, Quaternion rotation, Abilities SecondSlot = Abilities.NULL)
    {
        int AbilityID = (int)FirstSlot + ((int)SecondSlot + 1) * 4;
        if (SecondSlot == Abilities.NULL)
            AbilityID -= ((int)Abilities.NULL + 1) * 4;
        GameObject Ability;
        //AudioSource sound = Resources.Load("SFX/Basic_Shot_Sound") as AudioSource;

        switch (AbilityID)
        {
            case 0:
                {
                    // red base spell
                    Ability = GameObject.Instantiate(Resources.Load("AbilityPrefabs/Base_Fire") as GameObject, pos, rotation);
                    //sound = Resources.Load("SFX/Base_Fire_Sound") as AudioSource;
                    break;
                }
            case 1:
                {
                    Ability = GameObject.Instantiate(Resources.Load("AbilityPrefabs/Base_Ice") as GameObject, pos, rotation);
                    Ability.transform.position -= Ability.transform.forward * 2f;
                    Ability.transform.position -= new Vector3(0, 1, 0);
                    break;
                }
            case 2:
                {
                    Ability = GameObject.Instantiate(Resources.Load("AbilityPrefabs/Pool_Basic_Green_3") as GameObject, pos, rotation);
                    Ability.transform.position += Ability.transform.forward * 3f;
                    Ability.transform.position -= new Vector3(0, 1.5f, 0);
                    //sound = Resources.Load("SFX/Acid_Burn_Sound") as AudioSource;
                    break;
                }
            case 3:
                {
                    Ability = GameObject.Instantiate(Resources.Load("AbilityPrefabs/GasProjectile") as GameObject, pos, rotation);
                    break;
                }
            case 4:
                {
                    // this is double red
                    Ability = GameObject.Instantiate(Resources.Load("AbilityPreFabs/4_Azidoazie_Azide_RR/AzidProjectile") as GameObject, pos, rotation);
                    break;
                }
            case 5:
                {
                    // Blue Red
                    Ability = GameObject.Instantiate(Resources.Load("AbilityPreFabs/5_Steam_Flash_BR/Steam_Flash_Base") as GameObject, pos, rotation);
                    break;
                }
            case 6:
                {
                    // Green Red
                    Ability = GameObject.Instantiate(Resources.Load("AbilityPreFabs/6_FluorineFire_GR/FluorineProjectile") as GameObject, pos, rotation);
                    break;
                }
                
            case 7:
                {// yellow red
                    Ability = GameObject.Instantiate(Resources.Load("AbilityPreFabs/7_Implosion_YR/ImplosionProjectile") as GameObject, pos, rotation);
                    break;
                }
            case 8:
                {
                    // this red blue
                    GameManager.instance.ResetHitByLightning();
                    Ability = GameObject.Instantiate(Resources.Load("AbilityPreFabs/8_Lightning_RB/LightingProjectile") as GameObject, pos, rotation);
                    break;
                }
            case 9:
                {
                    // this blue blue
                    Ability = GameObject.Instantiate(Resources.Load("AbilityPreFabs/9_Avalanche_BB/Avalanche") as GameObject, pos, rotation);
                    Ability.transform.position -= Ability.transform.forward * 2f;
                    Ability.transform.position -= new Vector3(0, 1, 0);
                    break;
                }
            case 10:
                {
                    /// Green Blue
                    Ability = GameObject.Instantiate(Resources.Load("AbilityPreFabs/10_Formic_Acid_GB/Formic_Acid_Projectile") as GameObject, pos, rotation);

                    break;
                }
            case 11:
                {
                    // Yellow Blue
                    Ability = GameObject.Instantiate(Resources.Load("AbilityPreFabs/11_Flash_Freeze_YB/Flash_Freeze_Base") as GameObject, pos, rotation);
                    Ability.transform.position -= Ability.transform.forward * 2f;
                    break;
                }
            case 12:
                {
                    // Rad Green
                    Ability = GameObject.Instantiate(Resources.Load("AbilityPreFabs/12_LavaPool_RG/LavaPool") as GameObject, pos, rotation);
                    Ability.transform.position += Ability.transform.forward * 3f;
                    break;
                }
            case 13:
                {
                    //  Blue Green
                    Ability = GameObject.Instantiate(Resources.Load("AbilityPreFabs/13_Magnetic_BG/Push") as GameObject, pos, rotation);

                    break;
                }
            case 14:
                {
                    // Green Green
                    Ability = GameObject.Instantiate(Resources.Load("AbilityPreFabs/14_FluorAcid_GG/FluorEmiterProjectile") as GameObject, pos, rotation);
                    break;
                }
            case 15:
                {
                    // Green Green
                    Ability = GameObject.Instantiate(Resources.Load("AbilityPreFabs/15_Dimethylcadmium_YG/DimethylCloud") as GameObject, pos, rotation);
                    Ability.transform.position += Ability.transform.forward * 5f;
                    break;
                }
            case 16:
                {
                    //Red Yellow
                    Ability = GameObject.Instantiate(Resources.Load("AbilityPreFabs/16_Explosion_RY/Explosion_Projectile") as GameObject, pos, rotation);

                    break;
                }
            case 17:
                {
                    // Blue Yellow
                    Ability = GameObject.Instantiate(Resources.Load("AbilityPreFabs/17_Gas_Mine_BY/Gas_Mine") as GameObject, pos, rotation);

                    break;
                }
            case 18:
                {
                    // Green Yellow
                    Ability = GameObject.Instantiate(Resources.Load("AbilityPreFabs/18_Viral_GY/Viral_Projectile") as GameObject, pos, rotation);

                    break;
                }
            case 19:
                {
                    // Yellow Yellow
                    Ability = GameObject.Instantiate(Resources.Load("AbilityPreFabs/19_Thioacetone_YY/Thia_Projectile") as GameObject, pos, rotation);

                    break;
                }
            default:
                {
                    Ability = GameObject.Instantiate(Resources.Load("AbilityPrefabs/BaseProjectile") as GameObject, pos, rotation);
                    //sound = Resources.Load("SFX/Basic_Shot_Sound") as AudioSource;
                    break;
                }


        }
        //sound.Play();
        return Ability;
    }

    public static GameObject ReturnCopy (LinkedList<GameObject> Chain)
    {
        GameObject ob = GameObject.Instantiate((GameObject) Chain.First.Value);
        Chain.RemoveFirst();
        return ob;
    }

    // Update is called once per frame





    /*Abilities FirstSlot = Queue[0];
            
            Color FirstSlotColor = GetColor(FirstSlot);
            
           
            PlayerBullet bullet = Ability.GetComponent<PlayerBullet>();
            ParticleSystem.MainModule bulletParticle = Ability.GetComponent<ProjectileSpell>().hitEffect.GetComponent<ParticleSystem>().main;
            TrailRenderer trail = Ability.GetComponent<TrailRenderer>();
            Light light = Ability.GetComponent<Light>();

            Abilities SecondSlot = Abilities.red;
            Color SecondSlotColor = Color.white;

            if (Queue.Count >= 2)
            {
                SecondSlot = Queue[1];
                SecondSlotColor = GetColor(SecondSlot);
                
            }
            Color NewColor = FirstSlotColor + SecondSlotColor;
            bulletParticle.startColor = NewColor;
            trail.startColor = NewColor;
            trail.endColor = NewColor;
            light.color = NewColor;*/ // OldCode but good reference
}

