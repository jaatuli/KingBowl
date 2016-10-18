using UnityEngine;
using UnityEditor;
using NUnit.Framework;

[TestFixture]
public class ScoreMasterTest {

    private static ActionMaster.Action ENDTURN = ActionMaster.Action.EndTurn;
    private static ActionMaster.Action TIDY = ActionMaster.Action.Tidy;
    private static ActionMaster.Action RESET = ActionMaster.Action.Reset;
    private static ActionMaster.Action ENDGAME = ActionMaster.Action.EndGame;
    private ActionMaster actionMaster;

    [SetUp]
    public void Setup() {
        actionMaster = new ActionMaster();
    }

    [Test]
    public void T00PassingTest() {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01OneStrikeReturnsEndTurn() {
        Assert.AreEqual(ENDTURN, actionMaster.Bowl(10));
    }

    [Test]
    public void T02Bowl8ReturnsTidy() {
        Assert.AreEqual(TIDY, actionMaster.Bowl(8));
    }

    [Test]
    public void T03Bowl28ReturnsEndTurn() {
        actionMaster.Bowl(2);
        Assert.AreEqual(ENDTURN, actionMaster.Bowl(8));
    }

    [Test]
    public void T04BowlStrikeFinalFrameReturnsReset() {
        int[] rolls = { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 };
        foreach(int roll in rolls) {
            actionMaster.Bowl(roll);
        }
                
        Assert.AreEqual(RESET, actionMaster.Bowl(10));
    }

    [Test]
    public void T05Bowl19FinalFrameReturnsReset() {
        int[] rolls = { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }

        actionMaster.Bowl(1);
        Assert.AreEqual(RESET, actionMaster.Bowl(9));
    }

    [Test]
    public void T06BowlIs21() {
        int[] rolls = { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 8, 2 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }

        Assert.AreEqual(ENDGAME, actionMaster.Bowl(9));
    }

    [Test]
    public void T07BowlIs20WithNo21() {
        int[] rolls = { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 5 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }

        Assert.AreEqual(ENDGAME, actionMaster.Bowl(1));
    }

    [Test]
    public void T08BowlIs20WithStrike() {
        int[] rolls = { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2,};
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }

        actionMaster.Bowl(10);
        Assert.AreEqual(TIDY, actionMaster.Bowl(5));
    }

    [Test]
    public void T09Bowl0And10And5And1() {
        Assert.AreEqual(TIDY, actionMaster.Bowl(0));
        Assert.AreEqual(ENDTURN, actionMaster.Bowl(10));
        Assert.AreEqual(TIDY, actionMaster.Bowl(5));
        Assert.AreEqual(ENDTURN, actionMaster.Bowl(1));
    }

    [Test]
    public void T10RandomTestScore18Bowls() {
        
        for (int i = 1; i <= 18; i++) {
            if (i % 2 != 0) {
                Assert.AreEqual(TIDY, actionMaster.Bowl(Random.Range(1,10)));
            } else {
                Assert.AreEqual(ENDTURN, actionMaster.Bowl(Random.Range(1, 10)));
            }
        }
    }

    [Test]
    public void T11Bowl10thFrameTurkey() {
        for (int i = 1; i <= 18; i++) {
            actionMaster.Bowl(Random.Range(1, 10));            
        }
        
        Assert.AreEqual(RESET, actionMaster.Bowl(10));
        Assert.AreEqual(RESET, actionMaster.Bowl(10));
        Assert.AreEqual(ENDGAME, actionMaster.Bowl(10));
    }

}
