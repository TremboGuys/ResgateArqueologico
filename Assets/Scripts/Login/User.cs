using System;

[System.Serializable]
public class User
{
    public int id;
    public string username;
    public int score;
    public string photo;

    User(int id, string username, int score, string photo)
    {
        this.id = id;
        this.username = username;
        this.score = score;
        this.photo = photo;
    }
}

[System.Serializable]
public class UserRegister
{
    public string username;
    public string photo_path;

    public UserRegister(string username, string photoPath)
    {
        this.username = username;
        this.photo_path = photoPath;
    }
}