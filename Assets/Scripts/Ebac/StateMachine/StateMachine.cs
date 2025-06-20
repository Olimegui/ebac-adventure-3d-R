using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

namespace Ebac.StateMachine
{

    public class StateMachine<T> where T : System.Enum
    {

        public Dictionary<T, StateBase> dictionaryState;

        private StateBase _currentState;
        public float timeToStartGame = 1f;

        public StateBase CurrentState
        {
            get
            {
                return _currentState;
            }
        }

        public StateMachine(T state)
        {
            Init();
            SwitchState(state);
        }

        public void Init()
        {
            dictionaryState = new Dictionary<T, StateBase>();
        }

        public void RegisterStates(T typeEnum, StateBase state)
        {
            if(!dictionaryState.ContainsKey(typeEnum))
            {
                dictionaryState.Add(typeEnum, state);
            }
            else
            {
                Debug.LogWarning("Estado não registrado na máguina de estados!");
            }
        }

        public void SwitchState(T state, params object[] objs)
        {
            if (_currentState != null) _currentState.OnStateExit();

            if (dictionaryState.TryGetValue(state, out StateBase newState))
            {
                _currentState = dictionaryState[state];
                //_currentState = newState;
                _currentState.OnStateEnter(objs);
            }
        }

        public void Update()
        {
            if (_currentState != null) _currentState.OnStateStay();
        }
    }
}