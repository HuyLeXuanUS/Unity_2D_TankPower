using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameController
{
    // Singleton
    private static GameController instance;

    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameController();
            }
            return instance;
        }
    }

    private static int upgradeGuard = 0;
    private static int upgradeDamageBullet = 0;
    private static int upgradeDamageBeamFire = 0;
    private static int upgradeQuantityBullet = 0;
    private static int upgradeSpeedTank = 0;
    private static int upgradeSpeedBullet = 0;

    private static int savecountItem = 0;

    // Game State
    private static int curentLevel = 1;

    private static bool checkPause = false;

    public int getCurrentLevel()
    {
        return curentLevel;
    }

    public void increaseLevel()
    {
        curentLevel++;
    }

    public void setNewGame()
    {
        curentLevel = 1;
        savecountItem = 0;
    }

    // Pause Game and Resume Game
    public void pauseGame()
    {
        Time.timeScale = 0;
        checkPause = true;
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        checkPause = false;
    }

    public bool isPause()
    {
        return checkPause;
    }

    public int getPointUpgrade()
    {
        return curentLevel - 1;
    }

    // Save information upgrade
    public void setStartUpgrade()
    {
        upgradeGuard = 0;
        upgradeDamageBullet = 0;
        upgradeDamageBeamFire = 0;
        upgradeQuantityBullet = 0;
        upgradeSpeedTank = 0;
        upgradeSpeedBullet = 0;
    }

    public void setUpgradeGuard(int value)
    {
        upgradeGuard = value;
    }
    public void setUpgradeDamageBullet(int value)
    {
        upgradeDamageBullet = value;
    }
    public void setUpgradeDamageBeamFire(int value)
    {
        upgradeDamageBeamFire = value;
    }
    public void setUpgradeQuantityBullet(int value)
    {
        upgradeQuantityBullet = value;
    }
    public void setUpgradeSpeedTank(int value)
    {
        upgradeSpeedTank = value;
    }
    public void setUpgradeSpeedBullet(int value)
    {
        upgradeSpeedBullet = value;
    }

    // Get information upgrade
    public int getUpgradeGuard()
    {
        return upgradeGuard;
    }
    public int getUpgradeDamageBullet()
    {
        return upgradeDamageBullet;
    }
    public int getUpgradeDamageBeamFire()
    {
        return upgradeDamageBeamFire;
    }
    public int getUpgradeQuantityBullet()
    {
        return upgradeQuantityBullet;
    }
    public int getUpgradeSpeedTank()
    {
        return upgradeSpeedTank;
    }
    public int getUpgradeSpeedBullet()
    {
        return upgradeSpeedBullet;
    }

    // Set and Get Save Count Item
    public void setSaveCountItem(int value)
    {
        savecountItem = value;
    }

    public int getSaveCountItem()
    {
        return savecountItem;
    }

    // Get and Set Audio
    private static float volumeMusic = 0;
    private static float volumeSFX = 0;

    public void setVolumeMusic(float volume)
    {
        volumeMusic = volume;
    }

    public void setVolumeSFX(float volume)
    {
        volumeSFX = volume;
    }

    public float getVolumeMusic()
    {
        return volumeMusic;
    }

    public float getVolumeSFX()
    {
        return volumeSFX;
    }

    // Save and Load Game
    public void saveGame()
    {
        string path = Path.Combine(Application.persistentDataPath, "savegame.hd");
        FileStream file = File.Create(path);
        
        GameData data = new GameData(curentLevel, savecountItem, volumeMusic, volumeSFX);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public void loadGame()
    {
        string path = Path.Combine(Application.persistentDataPath, "savegame.hd");
        if (File.Exists(path))
        {
            FileStream file = File.Open(path, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();

            GameData data = (GameData)bf.Deserialize(file);
            file.Close();

            curentLevel = data.curentLevel;
            savecountItem = data.countItem;
            volumeMusic = data.Volume_Music;
            volumeSFX = data.Volume_SoundFX;
        }
    }
}
