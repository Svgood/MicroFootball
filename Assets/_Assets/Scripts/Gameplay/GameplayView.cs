using UnityEngine;

namespace _Assets.Scripts.Gameplay
{
    public sealed class GameplayView : MonoBehaviour, IGameplayPositionsProvider
    {
        [SerializeField] private GameObject _field;
        
        [SerializeField] private Transform _bot1StartingPosition;
        [SerializeField] private Transform _bot2StartingPosition;
        [SerializeField] private Transform _ballStartingPosition;
        
        public Vector3 Bot1StartingPosition => _bot1StartingPosition.position;
        public Vector3 Bot2StartingPosition => _bot2StartingPosition.position;
        public Vector3 BallStartingPosition => _ballStartingPosition.position;
        
        public GameObject Field => _field;

        private void Awake()
        {
            _bot1StartingPosition.gameObject.SetActive(false);
            _bot2StartingPosition.gameObject.SetActive(false); 
            _ballStartingPosition.gameObject.SetActive(false);
        }
    }

    public interface IGameplayPositionsProvider
    {
        Vector3 Bot1StartingPosition { get; }
        Vector3 Bot2StartingPosition { get; }
        Vector3 BallStartingPosition { get; }
    }
}
