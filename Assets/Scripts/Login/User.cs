using System;

[System.Serializable]
public class User
{
    public int id;
    public string username;
    public int xp;
    public string photo;

    User(int id, string username, int xp, string photo)
    {
        this.id = id;
        this.username = username;
        this.xp = xp;
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