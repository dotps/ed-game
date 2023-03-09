using System;
using System.Collections;
using System.Linq;
using CodeBase.Data;
using CodeBase.Services.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Enemies
{
    public class LootInstance : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private GameObject _lootObject;
        [SerializeField] private GameObject _lootPickupFX;
        [SerializeField] private TMP_Text _lootText;
        [SerializeField] private GameObject _pickupPopup;
        [SerializeField] private float _secondsBeforeDestroy = 2f;

        private Loot _loot;
        private bool _isPicked;
        private WorldData _worldData;
        private string _id;

        public void Construct(WorldData worldData) => 
            _worldData = worldData;

        public void Init(Loot loot, string id = null)
        {
            _loot = loot;
            _id = id ?? GenerateId();
        }
        
        private string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }

        private void OnTriggerEnter(Collider other) => 
            PickupLoot();

        private void PickupLoot()
        {
            if (_isPicked)
                return;
            
            _isPicked = true;

            StartCoroutine(DestroyAfterTime(_secondsBeforeDestroy));
            UpdateWorldData();
            PlayPickupFX();
            ShowText();
            HideLootObject();
        }

        private void UpdateWorldData() => 
            _worldData.collectedLoot.Collect(_loot);

        private void HideLootObject() => 
            _lootObject.SetActive(false);

        private void PlayPickupFX() => 
            Instantiate(_lootPickupFX, transform.position, Quaternion.identity);

        private void ShowText()
        {
            _lootText.text = _loot.value.ToString();
            _pickupPopup.SetActive(true);
        }

        private IEnumerator DestroyAfterTime(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            Destroy(gameObject);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            SaveWorldLoot(progress);
            SaveDroppedLoot(progress);
        }

        private void SaveDroppedLoot(PlayerProgress progress)
        {
            var existDroppedLoot = progress.droppedLoot.dropped.SingleOrDefault(x => x.id == _id);

            if (_isPicked)
                progress.droppedLoot.dropped.Remove(existDroppedLoot);
            else if (existDroppedLoot == null)
            {
                var loot = new DroppedLootData(_id, _loot, SceneManager.GetActiveScene().name, transform.position.AsVector3Data());
                progress.droppedLoot.dropped.Add(loot);
            }
        }

        private void SaveWorldLoot(PlayerProgress progress)
        {
            progress.worldData.collectedLoot = _worldData.collectedLoot;
        }

        public void LoadProgress(PlayerProgress progress) {}
    }
}