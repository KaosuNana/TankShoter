using System;

public class CONSTANTS
{
	public const int FRAMERATE = 60;

	public const int FASTEST_QUALITY = 0;

	public const int GOOD_QUALITY = 3;

	public const int FANTASTIC_QUALITY = 5;

	public const int LAYER_IGNORE_RAYCAST = 5;

	public const int LAYER_GROUND = 8;

	public const int LAYER_ENEMY = 9;

	public const int LAYER_ENEMY_BOSS = 25;

	public const int LAYER_ENEMY_BULLET = 17;

	public const int LAYER_ENEMY_COLLIDER_ENABLER = 18;

	public const int LAYER_ENEMY_BULLET_DISABLER = 26;

	public const int LAYER_PLAYER = 10;

	public const int LAYER_ENEMY_DETECTOR = 15;

	public const int LAYER_MAGNET_RADAR = 11;

	public const int LAYER_MENU_OBJECT = 13;

	public const int LAYER_COIN = 14;

	public const int LAYER_POWER_UP = 19;

	public const int LAYER_SCREENSHOTS = 23;

	public const int LAYER_BULLETS = 20;

	public const int LAYER_BULLETS_SIDEKICK = 27;

	public const int LAYER_SHIELD_BULLETS = 24;

	public const int LAYER_ENEMY_TRIGGER_DISABLER = 12;

	public const int LEVEL2_XP = 100;

	public const int LEVEL3_XP = 200;

	public const int LEVEL4_XP = 325;

	public const int LEVEL5_XP = 500;

	public const int LEVEL6_XP = 650;

	public const int LEVEL7_XP = 800;

	public const int BENCHMARK_LEVEL2_XP = 100;

	public const int BENCHMARK_LEVEL3_XP = 300;

	public const int BENCHMARK_LEVEL4_XP = 625;

	public const int BENCHMARK_LEVEL5_XP = 1125;

	public const int BENCHMARK_LEVEL6_XP = 1775;

	public const int BENCHMARK_LEVEL7_XP = 2575;

	public const string PLAYER_XP = "PlayerXP";

	public const string PLAYER_LEVEL = "PlayerLevel";

	public const string ATTACK_LEVEL = "AttackLevel";

	public const string NUMBER_OF_CITY_SAVED = "NumberOfCitySaved";

	public const string ACHIEVED_LEVEL = "AchievedLevel";

	public const string CHECKPOINT_WAVES_SAVED = "CheckpointWavesSaved";

	public const string COIN_COLLECTED = "CoinCollected";

	public const string BOLT_COLLECTED = "GemCollected";

	public const string BATTERY_SAVER_MODE = "BatterySaver";

	public const string TUTORIAL_COMPLETED = "TutorialCompleted";

	public const string SHOULD_SEND_IDLE_LOCAL_NOTIFICATION = "ShouldSendIdleLocalNotification";

	public const string SHADOW_PREFERENCE = "ShadowPreference";

	public const string LATEST_VERSION_PLAYED = "LatestVersionPlayed";

	public const int PLAYER_Z_OFFSET_FOR_DISTANCE = 18;

	public const string NUMBER_OF_EGG_HATCHED = "NumberOfEggHatched";

	public const string NUMBER_OF_CAPSULE_OPENED = "NumberOfCapsuleOpened";

	public const string PLAY_TIME_IN_SECONDS = "PlayTimeInSeconds";

	public const string OFFLINE_INCOME_TIME_STAMP = "OfflineIncomeTimeStamp";

	public const string NUMBER_OF_PLAYER_PRESTIGE = "PlayerPrestigeCount";

	public const string QUIT_TIME_STAMP_FOR_IDLE_INCOME = "QuitTimeStamp";

	public const string GAMEOVER = "GAMEOVER";

	public const string CONTINUE = "CONTINUE";

	public const string EVENT_COINS_CHANGES = "EventCoinChanges";

	public const string EVENT_BOLT_CHANGES = "EventBoltChanges";

	public const string EVENT_BOSS_SLIME_DESTROYED = "EventBossSlimeDestroyed";

	public const string EVENT_BOLT_CHANGES_NO_SCALING = "EventBoltChangesNoScaling";

	public const string EVENT_ADS_AVAILABLE = "EventAdsAvailable";

	public const string EVENT_COINS_CHANGES_FROM_SHOP_PAGE = "EventCoinChanges";

	public const string EVENT_NEW_ANIMAL_UNLOCKED = "EventNewAnimalUnlocked";

	public const string EVENT_GAME_OVER_ANIMAL_ENABLE_GRAVITY = "EventGameOverAnimalEnableGravity";

	public const string EVENT_ANDROID_BACK_BUTTON = "EventAndroidBackButton";

	public const string EVENT_SETTING_LISTEN_TO_ANDROID_BACK_BUTTON = "EventSettingListenToAndroidBackButton";

	public const string EVENT_RESULT_LISTEN_TO_ANDROID_BACK_BUTTON = "EventResultListenToAndroidBackButton";

	public const string EVENT_INCREASE_PLAYER_SPEED = "EventIncreasePlayerSpeed";

	public const string EVENT_NEW_AREA_UNLOCKED = "EventNewAreaUnlocked";

	public const int UILAYER = 5;

	public const int CARLAYER = 10;

	public const int CHARACTERPAGELAYER = 13;

	public const int UNLOCKINGPAGE = 15;

	public const int HERO_TROPHY_BRONZE = 250;

	public const int HERO_TROPHY_SILVER = 500;

	public const int HERO_TROPHY_GOLD = 1000;

	public const int CHESTCOST = 200;

	public const int COINS_EARN_FROM_ADS = 30;

	public const int CONTINUE_COINS_STARTING = 30;

	public const int BUYCOIN1 = 250;

	public const int BUYCOIN2 = 500;

	public const int BUYCOIN3 = 1000;

	public const int BUYCOIN4 = 2500;

	public const int BUYCOIN5 = 5000;

	public const string LEADERBOARD_BEST_STAGE = "CgkIgomCmYcKEAIQAQ";

	public const string NUMBER_OF_SILVER_CHEST_COLLECTED = "SilverChestCollected";

	public const string NUMBER_OF_GOLDEN_CHEST_COLLECTED = "GoldenChestCollected";

	public const string STORE_COIN_TIER_1 = "com.kisekigames.tankbuddies.coins.tier1";

	public const string STORE_COIN_TIER_2 = "com.kisekigames.tankbuddies.coins.tier2";

	public const string STORE_COIN_TIER_3 = "com.kisekigames.tankbuddies.coins.tier3";

	public const string STORE_BOLT_TIER_1 = "com.kisekigames.tankbuddies.bolts.tier1";

	public const string STORE_BOLT_TIER_2 = "com.kisekigames.tankbuddies.bolts.tier2";

	public const string STORE_BOLT_TIER_3 = "com.kisekigames.tankbuddies.bolts.tier3";

	public const string STORE_BOLT_TIER_4 = "com.kisekigames.tankbuddies.bolts.tier4a";

	public const string STORE_FREE_CONTINUE = "com.kisekigames.tankbuddies.freecontinue";

	public const string HAS_PURCHASED_GOLDEN_GIFT_BOX = "HasPurchasedGoldenGiftBox";

	public const string HAS_PURCHASED_FREE_CONTINUE = "HasPurchasedFreeContinue";

	public const string HAS_PURCHASED_SOMETHING = "HasPurchasedSomething";

	public const int NUMBER_OF_SESSION_BEFORE_FORCING_ADS = 3;

	public const int EXTRA_COIN_TIER_1 = 1000;

	public const int EXTRA_COIN_TIER_2 = 4800;

	public const int EXTRA_COIN_TIER_3 = 12000;

	public const int EXTRA_BOLT_TIER_1 = 100;

	public const int EXTRA_BOLT_TIER_2 = 480;

	public const int EXTRA_BOLT_TIER_3 = 900;

	public const int EXTRA_BOLT_TIER_4 = 2000;

	public const int EXTRA_COIN_FOR_PURCHASING_FREE_CONTINUE = 500;

	public const string COMPLETED_TUTORIAL = "HasCompletedTutorial";

	public const string TUTORIAL_MISSION_PROGRESS = "TutorialMissionProgress";

	public const string TUTORIAL_CHEST_KEY_REQUIREMENT = "TutorialChestKeyRequirement";

	public const string MAX_LEVEL = "MaxLevel";

	public const string PLAYER_PREFS_MUSIC = "MusicOnPref";

	public const string PLAYER_PREFS_SFX = "SfxOnPref";

	public const string PLAYER_PREFS_JOYSTICK_CONTROL = "JoystickControlPrefs";

	public const string PLAYER_PREFS_JOYSTICK_SENSITIVITY = "JoystickSensitivity";

	public const float PLAYER_PREFS_DEFAULT_JOYSTICK_SENSITIVITY = 0.8f;

	public const string PLAYER_PREFS_TEST_MODE = "TestMode";

	public const string HERO_PREFERENCE = "HeroPrefs";

	public const string HERO_ID = "PilotID";

	public const string ANIMAL_ID = "AnimalID";

	public const string THEMES_PREFERENCE = "ThemesPrefs";

	public const string THEMES_ID = "ThemesID";

	public const string PILOT_ID_XP = "PilotIDXP";

	public const string LAST_MISSION_TIME = "ChestTime";

	public const string CHEST_KEY_REQUIREMENT = "ChestKeyRequirement";

	public const string LAST_FREE_GIFT = "LastFreeChest";

	public const string NUMBER_OF_DUPLICATED_EGG_HATCHED = "DupeEggHatch";

	public const string NUMBER_OF_DUPLICATED_SEED_HATCHED = "DupeSeedHatch";

	public const string NUMBER_OF_DUPLICATED_PILOT_HATCHED = "DupePilotHatch";

	public const float ADDED_PROBABILITY_FACTOR_FOR_LOCKED_ANIMALS = 0.2f;

	public const float ADDED_PROBABILITY_FACTOR_FOR_LOCKED_PLANTS = 0.2f;

	public const float WEATHER_RAIN_PROBABILITY = 0.2f;

	public const int ENEMY_FARMER_PILOT_XP_REQUIREMENT = 5;

	public const int ENEMY_AGENT_PILOT_XP_REQUIREMENT = 5;

	public const int ENEMY_BOMBANI_PILOT_XP_REQUIREMENT = 3;

	public const int BOLT_COST_TO_CONTINUE = 10;

	public const string M_GATHER_NUMBER_OF = "GatherNumberOf";

	public const string M_GATHER_NUMBER_OF_IN_MAGNET_POWER_UP = "GatherNumberOfInMagnetPowerUp";

	public const string M_GATHER_NUMBER_OF_IN_GIANT_POWER_UP = "GatherNumberOfInGiantPowerUp";

	public const string M_GATHER_NUMBER_OF_IN_ROCKET_POWER_UP = "GatherNumberOfInRocketPowerUp";

	public const string M_PLAY_NUMBER_OF_GAMES = "PlayNumberOfGames";

	public const string M_COMPLETE_NUMBER_OF_LEVELS = "CompleteNumberOfLevels";

	public const string M_REACH_LEVEL = "ReachLevel";

	public const string M_RUN_TOTAL = "RunTotal";

	public const string M_SAVE_RUN_PROGRESS = "SaveRunProgress";

	public const string M_SHOOT_ENEMY_BOMB_TOTAL = "EnemyBomb";

	public const string M_SHOOT_TOTAL = "ShootTotal";

	public const string M_SHOOT_BOSS = "ShootBoss";

	public const string M_SHOOT_MINI_BOSS = "ShootMiniBoss";

	public const string M_TIMER_END = "HpTimed";

	public const string M_CONTINUE_TOTAL = "ContinueTotal";

	public const string M_COLLECT_BOLTS = "CollectBolts";

	public const string M_COLLECT_POWER_UP = "CollectPowerUp";

	public const string M_SAVE_RUN_GIANT_MODE_PROGRESS = "SaveRunGiantModeProgress";

	public const string M_SAVE_RUN_ROCKET_MODE_PROGRESS = "SaveRunRocketModeProgress";

	public const string M_COLLECT_COIN_MAGNET_POWER_UP = "CollectCoinMagnetPowerUp";

	public const string M_WAVE_CLEARED = "WaveCleared";

	public const string NUMBER_OF_MISSION_COMPLETED = "NumberOfMissionCompleted";

	public const string IOS_URL = "http://tiny.cc/TBiOS";

	public const string ANDROID_URL = "http://tiny.cc/TBDroid";

	public const string KISEKI_PLAY_STORE_URL = "https://play.google.com/store/apps/details?id=com.kisekigames.bouncyhero";

	public const string MORE_GAMES_APP_STORE_URL = "http://tiny.cc/BHeroiOS";

	public const string RESA_TWITTER_URL = "https://twitter.com/resaliputra";

	public const string JOHAN_TWITTER_URL = "https://twitter.com/kewlj";

	public const string KISEKI_TWITTER_URL = "https://twitter.com/KisekiGames";

	public const string FACEBOOK_URL = "fb://profile/1156099021105113";

	public const int NUMBER_OF_GAMES_BEFORE_OFFER_VIDEO_ADS = 1;

	public const int CROWN_SCORE_WOOD = 250;

	public const int CROWN_SCORE_BRONZE = 500;

	public const int CROWN_SCORE_SILVER = 1000;

	public const int CROWN_SCORE_GOLD = 2000;

	public const int MULT_SCORE_NONE = 3;

	public const int MULT_SCORE_WOOD = 4;

	public const int MULT_SCORE_BRONZE = 5;

	public const int MULT_SCORE_SILVER = 6;

	public const int MULT_SCORE_GOLD = 7;

	public const int CAR_SCORE_SILVER = 500;

	public const int CAR_SCORE_GOLD = 1000;
}
