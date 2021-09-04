using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void Save(PlayerData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerSave save = new PlayerSave(data);

        formatter.Serialize(stream, save);
        stream.Close();
    }
    public static PlayerSave LoadSave()
    {
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerSave save;
            if (stream.Length > 0)
                save = formatter.Deserialize(stream) as PlayerSave;
            else
                save = null;

            stream.Close();
            return save;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

}
