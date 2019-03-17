
public enum EBulletLauncherType
{
    Single = 0, //单发模式
    Shotgun = 1, //霰弹模式
    Laser = 2,   //激光模式
    Missile = 3, //导弹模式
}

public enum EBulletStorageStage
{
    Normal = 0,
    Second = 1,
    Third = 2,
    Fourth = 3,
}

public enum EBulletDamageType
{
    CalculateDamageOnce = 0,        //瞬发
    CalculateDamageContinue = 1,    //持续
}