using UnityEngine;

public class AwinchanAI : MonoBehaviour
{
    private enum State{
        Roaming,
        Cheasing,
        Attack,
    }
    private State state;
    private void Awake(){
        state = State.Roaming;
    }
    private void Update(){
        switch (state){
            case State.Roaming:
                break;
            case State.Cheasing:
                break;
            case State.Attack:
                break;
        }
    }

}
