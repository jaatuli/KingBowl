using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionMaster {

	public enum Action {Tidy, Reset, EndTurn, EndGame}

    private int[] bowls = new int[21];
    private int bowl = 1;

    public static Action NextAction (List<int> pinFalls) {
        ActionMaster am = new ActionMaster();
        Action currentAction = new Action();

        foreach(int pinFall in pinFalls) {
            currentAction = am.Bowl(pinFall);
        }

        return currentAction;
    }

    public Action Bowl (int pins) {   // TODO make private       

        // Valid pins
        if (pins < 0 || pins > 10) {throw new UnityException("Invalid pins");}

        bowls[bowl - 1] = pins;

        // If 10 frame with 3 bowl, end game
        if (bowl == 21) {
            return Action.EndGame;
        }

        // Strike at 19 will reset
        if (bowl == 19 && pins == 10) {
            bowl += 1;
            return Action.Reset;
        }

        //  20 frame special cases
        if (bowl == 20) {

            if (Score19And20() < 10) {
                return Action.EndGame;
            }

            if (Score19And20() == 10 || Score19And20() == 20) {
                bowl += 1;
                return Action.Reset;
            }

            if (Score19And20() > 10) {
                bowl += 1;
                return Action.Tidy;
            }
        }    
        
        // End turn at Strike at first throw
        if (pins == 10 && bowl % 2 != 0) {
            bowl += 2;
            return Action.EndTurn;
        }


        if (bowl % 2 != 0) { // Mid-frame or final frame
            bowl += 1;
            return Action.Tidy;
        } else if(bowl % 2 == 0) { // end of frame
            bowl += 1;
            return Action.EndTurn;
        }

        throw new UnityException("Not sure what action to return!");
        
    }

    private int Score19And20() {
        return bowls[19 - 1] + bowls[20 - 1];
    }

}
