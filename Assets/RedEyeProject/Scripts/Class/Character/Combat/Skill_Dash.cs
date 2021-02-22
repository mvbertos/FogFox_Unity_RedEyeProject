using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Dash : Skill
{
    public IEnumerator OnDash(CharacterEntity character)
    {
        if (executing)
        {
            print("Ill Do Nothing");
        }
        else
        {
            Enum_CharacterState defaultState = character.CharacterState;
            float dashExecutionTime = Time.time + ActionTime;

            if (character.CharacterState != Enum_CharacterState.Dashing && character.VerifyAtribute(Cost, 2))//Start Dash
            {

                character.ReduceAtribute(Cost, 2);
                do
                {
                    foreach (Struct_Effect effect in Effects)
                    {
                        if (effect.EffectType == Enum_EffectType.Travel)
                        {
                            character.UpdateCharacterState(Enum_CharacterState.Dashing);
                            character.OnMove(character.MoveDirection * effect.EffectValue);
                            executing = true;
                        }
                    }
                    yield return new WaitForEndOfFrame();

                } while ((Time.time <= dashExecutionTime) == true);
            }
            dashExecutionTime = 0;
            yield return new WaitForSeconds(Cooldown);

            character.UpdateCharacterState(defaultState);
            executing = false;
        }
    }
}