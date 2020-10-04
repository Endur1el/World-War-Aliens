using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CharacterSaver
{
    string dataPath;

    void Start()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "Character.CHAR");
    }

    static void SaveCharacter (Pawn character, string path)
    {
        string jsonCharacter = JsonUtility.ToJson(character);

        using (StreamWriter streamWriter = File.CreateText(path))
        {
            streamWriter.Write(jsonCharacter);
        }
    }

    static Pawn LoadCharacter(string path)
    {
        using (StreamReader streamReader = File.OpenText(path))
        {
            string jsonCharacter = streamReader.ReadToEnd();
            return JsonUtility.FromJson<Pawn>(jsonCharacter);
        }
    }
}