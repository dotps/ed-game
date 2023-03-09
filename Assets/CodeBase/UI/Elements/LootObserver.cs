using CodeBase.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class LootObserver : MonoBehaviour
    {
        [SerializeField] private TMP_Text _lootText;
        private WorldData _worldData;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
            _worldData.collectedLoot.Changed += UpdateLootText;
        }

        private void Start()
        {
            UpdateLootText();
        }

        private void UpdateLootText()
        {
            _lootText.text = _worldData.collectedLoot.collected.ToString();
        }
        
        // public void LoadProgress(PlayerProgress progress)
        // {
        //     Debug.Log(progress.worldData.collectedLoot.collected);
        //     Debug.Log("LoadProgress UpdateProgress 2");
        // }
        //
        // public void UpdateProgress(PlayerProgress progress)
        // {
        //     Debug.Log("LootData UpdateProgress 2");
        //     // progress.worldData.collectedLoot.collected = 100;
        // }
        
    }
}