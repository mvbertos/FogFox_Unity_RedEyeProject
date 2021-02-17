using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Dash : Skill
{
    public IEnumerator OnDash(CharacterEntity character)
    {
        Enum_CharacterState defaultState = character.characterState;
        float dashExecutionTime = Time.time + ActionTime;

        if (character.characterState != Enum_CharacterState.Dashing && character.VerifyAtribute(Cost, 2))//Start Dash
        {
            print("Working");
            character.ReduceAtribute(Cost, 2);
            do
            {
                foreach (Struct_Effect effect in Effects)
                {
                    if (effect.EffectType == Enum_EffectType.Travel)
                    {
                        character.characterState = Enum_CharacterState.Dashing;
                        print(character.characterState);
                        character.OnMove(character.moveDirection * effect.EffectValue);
                    }
                }
                yield return new WaitForEndOfFrame();

            } while ((Time.time <= dashExecutionTime) == true);
        }
        else //End Dash
        {
            dashExecutionTime = 0;

            yield return new WaitForSeconds(Cooldown);

            character.characterState = defaultState;
        }
    }
}