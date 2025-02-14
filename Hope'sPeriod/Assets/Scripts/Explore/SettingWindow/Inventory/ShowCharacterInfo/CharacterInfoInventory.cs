using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterInfoInventory: MonoBehaviour {

    [SerializeField] private List<EachChararacterInfoInventory> characterinfos;
    public static CharacterInfoInventory Instance { get; private set; } = null;
    public List<EachChararacterInfoInventory> Characterinfos => characterinfos;
    public EachChararacterInfoInventory GetCharacterInfo(int index) => characterinfos[index];

    private void Awake() {
        Instance = this;
    }

    public void UpdateState() {
        foreach (var character in characterinfos) {
            character.UpdateState();
        }
    }
}