using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ModiferDamage
{
    // Start is called before the first frame update
    static public float ReturnDamageModifier(Abilities Attacker, Abilities Target, bool IsVunerableToFire = false)
    {
        float modifer = 1;
        if (Target == Abilities.NULL)
            return modifer;
        switch(Attacker)
        {
            case Abilities.red:
                { 
                    switch(Target)
                    {
                        case Abilities.red:
                            {
                                break;
                            }
                        case Abilities.blue:
                            {
                                break;
                            }
                            
                        case Abilities.green:
                            {
                                break;
                            }
                        case Abilities.yellow:
                            {
                                break;
                            }
                    }

                    if (IsVunerableToFire == true)
                        modifer += .25f;
                    break;

                }
                


               // --------------------------------------------
            case Abilities.blue:
                {
                    switch (Target)
                    {
                        case Abilities.red:
                            {
                                break;
                            }
                        case Abilities.blue:
                            {
                                break;
                            }

                        case Abilities.green:
                            {
                                break;
                            }
                        case Abilities.yellow:
                            {
                                break;
                            }
                    }
                    break;
                }
               // ---------------------------------------------
            case Abilities.green:
                switch (Target)
                {
                    case Abilities.red:
                        {
                            break;
                        }
                    case Abilities.blue:
                        {
                            break;
                        }

                    case Abilities.green:
                        {
                            break;
                        }
                    case Abilities.yellow:
                        {
                            break;
                        }
                }
                break;
                //-----------------------------------------------
            case Abilities.yellow:
                switch (Target)
                {
                    case Abilities.red:
                        {
                            break;
                        }
                    case Abilities.blue:
                        {
                            break;
                        }

                    case Abilities.green:
                        {
                            break;
                        }
                    case Abilities.yellow:
                        {
                            break;
                        }
                }
                break;
        }
        
        return modifer;
        
    }
}
