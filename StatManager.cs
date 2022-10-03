using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

[System.Serializable]

public class Monster //몬스터 객체 생성
{
    public Monster(string _ID, string _Type, string _Name, string _HP, string _ATK, string _Type_A, string _Type_D)
    { ID = _ID; Type = _Type; Name = _Name; HP = _HP; ATK = _ATK; Type_A = _Type_A; Type_D = _Type_D; }

    public string ID, Type, Name, HP, ATK, Type_A, Type_D;
}

[System.Serializable]
public class Equipment //장비 객체 생성
{
    public Equipment(string _ID, string _Type, string _Name, string _ATK, string _DEF, string _Type_A, string _Type_D, string _Type_P)
    { ID = _ID; Type = _Type; Name = _Name; ATK = _ATK; DEF = _DEF; Type_A = _Type_A; Type_D = _Type_D; Type_P = _Type_P; }

    public string ID, Type, Name, ATK, DEF, Type_A, Type_D, Type_P;
}

public class Player
{
    public Player(string _level, string _levelUpPoint, string _meleeDamage, string _rangeDamage, string _HP, string _exp)
    {
        Level = _level; LevelUpPoint = _levelUpPoint; MeleeDamage = _meleeDamage; RangeDamage = _rangeDamage; HP = _HP; EXP = _exp;
    }

    public string Level, LevelUpPoint, MeleeDamage, RangeDamage, HP, EXP;
}

public class StatManager : MonoBehaviour
{
    public List<Monster> Monsters; //txt to json에 사용할 리스트
    public List<Equipment> Equipments; //txt to json에 사용할 리스트

    [HideInInspector]
    public string monsterdata; //몬스터 데이터를 담을 문자열
    [HideInInspector]
    public string equipdata; //장비 데이터를 담을 문자열
    public static List<Monster> Monsters_d; //json to object에 사용할 리스트
    public static List<Equipment> Equipments_d; //json to object에 사용할 리스트

    void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create); //json파일 생성
        byte[] data = Encoding.UTF8.GetBytes(jsonData); //문자열을 utf8 방식으로 인코딩함 (텍스트파일로 저장하기 위해서)
        fileStream.Write(data, 0, data.Length); //data를 json파일에 저장
        fileStream.Close();
    }

    T LoadJsonFile<T>(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open); //읽을 json파일을 불러옴
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data); //읽은 데이터를 utf8 방식으로 인코딩
        return JsonUtility.FromJson<T>(jsonData); //데이터를 반환
    }

    void Save()
    {
        string monsterdata = JsonConvert.SerializeObject(Monsters); //Monsters(몬스터데이터가 담긴 리스트)를 직렬화 시켜서 문자열에 저장함(json으로 저장하기 위해선 꼭 직렬화가 필요함)
        File.WriteAllText(Application.dataPath + "/Resources/MonsterStat.json", monsterdata);

        string equipdata = JsonConvert.SerializeObject(Equipments); //Equipments(장비데이터가 담긴 리스트)를 직렬화 시켜서 문자열에 저장함(json으로 저장하기 위해선 꼭 직렬화가 필요함)
        File.WriteAllText(Application.dataPath + "/Resources/EquipmentStat.json", equipdata);
    }

    void Load()
    {
        string monsterdata = File.ReadAllText(Application.dataPath + "/Resources/MonsterStat.json");
        Monsters_d = JsonConvert.DeserializeObject<List<Monster>>(monsterdata); //json파일에서 긁어온 데이터를 Monsters_d 리스트에 저장

        string equipdata = File.ReadAllText(Application.dataPath + "/Resources/EquipmentStat.json");
        Equipments_d = JsonConvert.DeserializeObject<List<Equipment>>(equipdata); //json파일에서 긁어온 데이터를 Equipments_d 리스트에 저장
    }

    void Awake()
    {
        TextAsset MonsterDatabase = Resources.Load<TextAsset>("MonsterData"); //Resources폴더에 있는 MonsterData.txt를 TextAsset화 시켜서 불러옴
        string[] monsterline = MonsterDatabase.text.Substring(0, MonsterDatabase.text.Length - 1).Split('\n'); //Excel 파일을 붙여넣기 하는 과정에서 맨 아랫줄에 저절로 빈 줄이 생기므로 빈 줄을 제거하고, 띄어쓰기를 제거함
        for (int i = 0; i < monsterline.Length; i++)
        {
            string[] row = monsterline[i].Split('\t'); //text 간에 공백을 제거하여 파싱함
            Monsters.Add(new Monster(row[0], row[1], row[2], row[3], row[4], row[5], row[6].TrimEnd())); //파싱한 문자열 배열을 바탕으로 몬스터 객체를 생성하여 몬스터 리스트에 넣음(맨 끝에 생기는 공백은 제거)
        }

        TextAsset EquipDatabase = Resources.Load<TextAsset>("EquipData"); //Resources폴더에 있는 EquipData.txt를 TextAsset화 시켜서 불러옴
        string[] equipline = EquipDatabase.text.Substring(0, EquipDatabase.text.Length - 1).Split('\n'); //Excel 파일을 붙여넣기 하는 과정에서 맨 아랫줄에 저절로 빈 줄이 생기므로 빈 줄을 제거하고, 띄어쓰기를 제거함
        for (int i = 0; i < equipline.Length; i++)
        {
            string[] row = equipline[i].Split('\t'); //text 간에 공백을 제거하여 파싱함
            Equipments.Add(new Equipment(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7].TrimEnd())); //파싱한 문자열 배열을 바탕으로 몬스터 객체를 생성하여 몬스터 리스트에 넣음(맨 끝에 생기는 공백은 제거)
        }

        Save();

        Load();
    }
}
