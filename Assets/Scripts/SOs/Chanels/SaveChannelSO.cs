using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Save Channel", menuName = "Game/Save Channel")]
public class SaveChannelSO : ScriptableObject
{
	[SerializeField] SaveData _defaultValues = new SaveData() {
		Level = 1,
		Coins = 0
	};

	[Header("Saved Data"), Space]
	[SerializeField] IntSO _level;
	[SerializeField] IntSO _coin;

	public void Save()
	{
		try
		{
			SaveData save = new SaveData()
			{
				Level = _level.Value,
				Coins = _coin.Value
			};

			string txtSave = JsonUtility.ToJson(save);
			PlayerPrefs.SetString("save_game", txtSave);
			PlayerPrefs.Save();
		}
		catch (Exception ex)
		{
			Debug.LogError($"ERROR WHILE SAVING GAME, MESSAGE: {ex.Message}");
		}
	}

	public void Load()
	{
		try
		{
			SaveData save = _defaultValues;

            if (PlayerPrefs.HasKey("save_game"))
			{
                string txtSave = PlayerPrefs.GetString("save_game");
                save = JsonUtility.FromJson<SaveData>(txtSave);
            }

			_level.Value = save.Level;
			_coin.Value = save.Coins;
		}
		catch (Exception ex)
		{
			Debug.LogError($"ERROR WHILE LOADING SAVE GAME, MESSAGE: {ex.Message}");
		}
    }

	[System.Serializable]
	public struct SaveData
	{
		public int Level;
		public int Coins;
	}
}
