namespace Generator.Models.Output.Difficulty
{
    public class Difficulty
    {
        public DifficultySettings easy { get; set; }
        public DifficultySettings normal { get; set; }
        public DifficultySettings hard { get; set; }
        public DifficultySettings impossible { get; set; }
    }

    public class DifficultySettings
    {
        public Lay Lay { get; set; }
        public Aiming Aiming { get; set; }
        public Look Look { get; set; }
        public Shoot Shoot { get; set; }
        public Move Move { get; set; }
        public Grenade Grenade { get; set; }
        public Change Change { get; set; }
        public Cover Cover { get; set; }
        public Patrol Patrol { get; set; }
        public Hearing Hearing { get; set; }
        public Mind Mind { get; set; }
        public Boss Boss { get; set; }
        public Core Core { get; set; }
        public Scattering Scattering { get; set; }
    }

    public class Lay
    {
        public Lay(bool cHECK_SHOOT_WHEN_LAYING, int dELTA_LAY_CHECK, double dELTA_GETUP, int dELTA_AFTER_GETUP, 
            int cLEAR_POINTS_OF_SCARE_SEC, int mAX_LAY_TIME, int dELTA_WANT_LAY_CHECL_SEC, int aTTACK_LAY_CHANCE, 
            double dIST_TO_COVER_TO_LAY, double dIST_TO_COVER_TO_LAY_SQRT, double dIST_GRASS_TERRAIN_SQRT, int dIST_ENEMY_NULL_DANGER_LAY, 
            int dIST_ENEMY_NULL_DANGER_LAY_SQRT, int dIST_ENEMY_GETUP_LAY, int dIST_ENEMY_GETUP_LAY_SQRT, int dIST_ENEMY_CAN_LAY,
            int dIST_ENEMY_CAN_LAY_SQRT, double lAY_AIM, int mIN_CAN_LAY_DIST_SQRT, int mIN_CAN_LAY_DIST,
            int mAX_CAN_LAY_DIST_SQRT, int mAX_CAN_LAY_DIST, int lAY_CHANCE_DANGER, int dAMAGE_TIME_TO_GETUP)
        {
            CHECK_SHOOT_WHEN_LAYING = cHECK_SHOOT_WHEN_LAYING;
            DELTA_LAY_CHECK = dELTA_LAY_CHECK;
            DELTA_GETUP = dELTA_GETUP;
            DELTA_AFTER_GETUP = dELTA_AFTER_GETUP;
            CLEAR_POINTS_OF_SCARE_SEC = cLEAR_POINTS_OF_SCARE_SEC;
            MAX_LAY_TIME = mAX_LAY_TIME;
            DELTA_WANT_LAY_CHECL_SEC = dELTA_WANT_LAY_CHECL_SEC;
            ATTACK_LAY_CHANCE = aTTACK_LAY_CHANCE;
            DIST_TO_COVER_TO_LAY = dIST_TO_COVER_TO_LAY;
            DIST_TO_COVER_TO_LAY_SQRT = dIST_TO_COVER_TO_LAY_SQRT;
            DIST_GRASS_TERRAIN_SQRT = dIST_GRASS_TERRAIN_SQRT;
            DIST_ENEMY_NULL_DANGER_LAY = dIST_ENEMY_NULL_DANGER_LAY;
            DIST_ENEMY_NULL_DANGER_LAY_SQRT = dIST_ENEMY_NULL_DANGER_LAY_SQRT;
            DIST_ENEMY_GETUP_LAY = dIST_ENEMY_GETUP_LAY;
            DIST_ENEMY_GETUP_LAY_SQRT = dIST_ENEMY_GETUP_LAY_SQRT;
            DIST_ENEMY_CAN_LAY = dIST_ENEMY_CAN_LAY;
            DIST_ENEMY_CAN_LAY_SQRT = dIST_ENEMY_CAN_LAY_SQRT;
            LAY_AIM = lAY_AIM;
            MIN_CAN_LAY_DIST_SQRT = mIN_CAN_LAY_DIST_SQRT;
            MIN_CAN_LAY_DIST = mIN_CAN_LAY_DIST;
            MAX_CAN_LAY_DIST_SQRT = mAX_CAN_LAY_DIST_SQRT;
            MAX_CAN_LAY_DIST = mAX_CAN_LAY_DIST;
            LAY_CHANCE_DANGER = lAY_CHANCE_DANGER;
            DAMAGE_TIME_TO_GETUP = dAMAGE_TIME_TO_GETUP;
        }

        public bool CHECK_SHOOT_WHEN_LAYING { get; set; }
        public int DELTA_LAY_CHECK { get; set; }
        public double DELTA_GETUP { get; set; }
        public int DELTA_AFTER_GETUP { get; set; }
        public int CLEAR_POINTS_OF_SCARE_SEC { get; set; }
        public int MAX_LAY_TIME { get; set; }
        public int DELTA_WANT_LAY_CHECL_SEC { get; set; }
        public int ATTACK_LAY_CHANCE { get; set; }
        public double DIST_TO_COVER_TO_LAY { get; set; }
        public double DIST_TO_COVER_TO_LAY_SQRT { get; set; }
        public double DIST_GRASS_TERRAIN_SQRT { get; set; }
        public int DIST_ENEMY_NULL_DANGER_LAY { get; set; }
        public int DIST_ENEMY_NULL_DANGER_LAY_SQRT { get; set; }
        public int DIST_ENEMY_GETUP_LAY { get; set; }
        public int DIST_ENEMY_GETUP_LAY_SQRT { get; set; }
        public int DIST_ENEMY_CAN_LAY { get; set; }
        public int DIST_ENEMY_CAN_LAY_SQRT { get; set; }
        public double LAY_AIM { get; set; }
        public int MIN_CAN_LAY_DIST_SQRT { get; set; }
        public int MIN_CAN_LAY_DIST { get; set; }
        public int MAX_CAN_LAY_DIST_SQRT { get; set; }
        public int MAX_CAN_LAY_DIST { get; set; }
        public int LAY_CHANCE_DANGER { get; set; }
        public int DAMAGE_TIME_TO_GETUP { get; set; }
    }

    public class Aiming
    {
        public Aiming(int mAX_AIM_PRECICING, double bETTER_PRECICING_COEF, double rECALC_DIST, double rECALC_SQR_DIST,
            double cOEF_FROM_COVER, double pANIC_COEF, double pANIC_ACCURATY_COEF, double hARD_AIM,
            int pANIC_TIME, int rECALC_MUST_TIME, int dAMAGE_PANIC_TIME, double dANGER_UP_POINT,
            double mAX_AIMING_UPGRADE_BY_TIME, int dAMAGE_TO_DISCARD_AIM_0_100, double mIN_TIME_DISCARD_AIM_SEC, double mAX_TIME_DISCARD_AIM_SEC,
            double xZ_COEF, int sHOOT_TO_CHANGE_PRIORITY, double bOTTOM_COEF, double fIRST_CONTACT_ADD_SEC,
            int fIRST_CONTACT_ADD_CHANCE_100, double bASE_HIT_AFFECTION_DELAY_SEC, int bASE_HIT_AFFECTION_MIN_ANG, int bASE_HIT_AFFECTION_MAX_ANG,
            int bASE_SHIEF, int sCATTERING_HAVE_DAMAGE_COEF, double sCATTERING_DIST_MODIF, double sCATTERING_DIST_MODIF_CLOSE,
            int aIMING_TYPE, int dIST_TO_SHOOT_TO_CENTER, int dIST_TO_SHOOT_NO_OFFSET, int sHPERE_FRIENDY_FIRE_SIZE,
            double cOEF_IF_MOVE, double tIME_COEF_IF_MOVE, double bOT_MOVE_IF_DELTA, int nEXT_SHOT_MISS_CHANCE_100,
            int nEXT_SHOT_MISS_Y_OFFSET, int aNYTIME_LIGHT_WHEN_AIM_100, int aNY_PART_SHOOT_TIME, double wEAPON_ROOT_OFFSET,
            int mIN_DAMAGE_TO_GET_HIT_AFFETS, double mAX_AIM_TIME, int oFFSET_RECAL_ANYWAY_TIME, double y_TOP_OFFSET_COEF,
            double y_BOTTOM_OFFSET_COEF, double bASE_SHIEF_STATIONARY_GRENADE, double xZ_COEF_STATIONARY_GRENADE, int dEAD_BODY_LOOK_PERIOD, bool cAN_HARD_AIM)
        {
            MAX_AIM_PRECICING = mAX_AIM_PRECICING;
            BETTER_PRECICING_COEF = bETTER_PRECICING_COEF;
            RECALC_DIST = rECALC_DIST;
            RECALC_SQR_DIST = rECALC_SQR_DIST;
            COEF_FROM_COVER = cOEF_FROM_COVER;
            PANIC_COEF = pANIC_COEF;
            PANIC_ACCURATY_COEF = pANIC_ACCURATY_COEF;
            HARD_AIM = hARD_AIM;
            PANIC_TIME = pANIC_TIME;
            RECALC_MUST_TIME = rECALC_MUST_TIME;
            DAMAGE_PANIC_TIME = dAMAGE_PANIC_TIME;
            DANGER_UP_POINT = dANGER_UP_POINT;
            MAX_AIMING_UPGRADE_BY_TIME = mAX_AIMING_UPGRADE_BY_TIME;
            DAMAGE_TO_DISCARD_AIM_0_100 = dAMAGE_TO_DISCARD_AIM_0_100;
            MIN_TIME_DISCARD_AIM_SEC = mIN_TIME_DISCARD_AIM_SEC;
            MAX_TIME_DISCARD_AIM_SEC = mAX_TIME_DISCARD_AIM_SEC;
            XZ_COEF = xZ_COEF;
            SHOOT_TO_CHANGE_PRIORITY = sHOOT_TO_CHANGE_PRIORITY;
            BOTTOM_COEF = bOTTOM_COEF;
            FIRST_CONTACT_ADD_SEC = fIRST_CONTACT_ADD_SEC;
            FIRST_CONTACT_ADD_CHANCE_100 = fIRST_CONTACT_ADD_CHANCE_100;
            BASE_HIT_AFFECTION_DELAY_SEC = bASE_HIT_AFFECTION_DELAY_SEC;
            BASE_HIT_AFFECTION_MIN_ANG = bASE_HIT_AFFECTION_MIN_ANG;
            BASE_HIT_AFFECTION_MAX_ANG = bASE_HIT_AFFECTION_MAX_ANG;
            BASE_SHIEF = bASE_SHIEF;
            SCATTERING_HAVE_DAMAGE_COEF = sCATTERING_HAVE_DAMAGE_COEF;
            SCATTERING_DIST_MODIF = sCATTERING_DIST_MODIF;
            SCATTERING_DIST_MODIF_CLOSE = sCATTERING_DIST_MODIF_CLOSE;
            AIMING_TYPE = aIMING_TYPE;
            DIST_TO_SHOOT_TO_CENTER = dIST_TO_SHOOT_TO_CENTER;
            DIST_TO_SHOOT_NO_OFFSET = dIST_TO_SHOOT_NO_OFFSET;
            SHPERE_FRIENDY_FIRE_SIZE = sHPERE_FRIENDY_FIRE_SIZE;
            COEF_IF_MOVE = cOEF_IF_MOVE;
            TIME_COEF_IF_MOVE = tIME_COEF_IF_MOVE;
            BOT_MOVE_IF_DELTA = bOT_MOVE_IF_DELTA;
            NEXT_SHOT_MISS_CHANCE_100 = nEXT_SHOT_MISS_CHANCE_100;
            NEXT_SHOT_MISS_Y_OFFSET = nEXT_SHOT_MISS_Y_OFFSET;
            ANYTIME_LIGHT_WHEN_AIM_100 = aNYTIME_LIGHT_WHEN_AIM_100;
            ANY_PART_SHOOT_TIME = aNY_PART_SHOOT_TIME;
            WEAPON_ROOT_OFFSET = wEAPON_ROOT_OFFSET;
            MIN_DAMAGE_TO_GET_HIT_AFFETS = mIN_DAMAGE_TO_GET_HIT_AFFETS;
            MAX_AIM_TIME = mAX_AIM_TIME;
            OFFSET_RECAL_ANYWAY_TIME = oFFSET_RECAL_ANYWAY_TIME;
            Y_TOP_OFFSET_COEF = y_TOP_OFFSET_COEF;
            Y_BOTTOM_OFFSET_COEF = y_BOTTOM_OFFSET_COEF;
            BASE_SHIEF_STATIONARY_GRENADE = bASE_SHIEF_STATIONARY_GRENADE;
            XZ_COEF_STATIONARY_GRENADE = xZ_COEF_STATIONARY_GRENADE;
            DEAD_BODY_LOOK_PERIOD = dEAD_BODY_LOOK_PERIOD;
            CAN_HARD_AIM = cAN_HARD_AIM;
        }

        public int MAX_AIM_PRECICING { get; set; }
        public double BETTER_PRECICING_COEF { get; set; }
        public double RECALC_DIST { get; set; }
        public double RECALC_SQR_DIST { get; set; }
        public double COEF_FROM_COVER { get; set; }
        public double PANIC_COEF { get; set; }
        public double PANIC_ACCURATY_COEF { get; set; }
        public double HARD_AIM { get; set; }
        public int PANIC_TIME { get; set; }
        public int RECALC_MUST_TIME { get; set; }
        public int DAMAGE_PANIC_TIME { get; set; }
        public double DANGER_UP_POINT { get; set; }
        public double MAX_AIMING_UPGRADE_BY_TIME { get; set; }
        public int DAMAGE_TO_DISCARD_AIM_0_100 { get; set; }
        public double MIN_TIME_DISCARD_AIM_SEC { get; set; }
        public double MAX_TIME_DISCARD_AIM_SEC { get; set; }
        public double XZ_COEF { get; set; }
        public int SHOOT_TO_CHANGE_PRIORITY { get; set; }
        public double BOTTOM_COEF { get; set; }
        public double FIRST_CONTACT_ADD_SEC { get; set; }
        public int FIRST_CONTACT_ADD_CHANCE_100 { get; set; }
        public double BASE_HIT_AFFECTION_DELAY_SEC { get; set; }
        public int BASE_HIT_AFFECTION_MIN_ANG { get; set; }
        public int BASE_HIT_AFFECTION_MAX_ANG { get; set; }
        public int BASE_SHIEF { get; set; }
        public int SCATTERING_HAVE_DAMAGE_COEF { get; set; }
        public double SCATTERING_DIST_MODIF { get; set; }
        public double SCATTERING_DIST_MODIF_CLOSE { get; set; }
        public int AIMING_TYPE { get; set; }
        public int DIST_TO_SHOOT_TO_CENTER { get; set; }
        public int DIST_TO_SHOOT_NO_OFFSET { get; set; }
        public int SHPERE_FRIENDY_FIRE_SIZE { get; set; }
        public double COEF_IF_MOVE { get; set; }
        public double TIME_COEF_IF_MOVE { get; set; }
        public double BOT_MOVE_IF_DELTA { get; set; }
        public int NEXT_SHOT_MISS_CHANCE_100 { get; set; }
        public int NEXT_SHOT_MISS_Y_OFFSET { get; set; }
        public int ANYTIME_LIGHT_WHEN_AIM_100 { get; set; }
        public int ANY_PART_SHOOT_TIME { get; set; }
        public double WEAPON_ROOT_OFFSET { get; set; }
        public int MIN_DAMAGE_TO_GET_HIT_AFFETS { get; set; }
        public double MAX_AIM_TIME { get; set; }
        public int OFFSET_RECAL_ANYWAY_TIME { get; set; }
        public double Y_TOP_OFFSET_COEF { get; set; }
        public double Y_BOTTOM_OFFSET_COEF { get; set; }
        public double BASE_SHIEF_STATIONARY_GRENADE { get; set; }
        public double XZ_COEF_STATIONARY_GRENADE { get; set; }
        public int DEAD_BODY_LOOK_PERIOD { get; set; }
        public bool CAN_HARD_AIM { get; set; }
    }

    public class Look
    {
        public Look(int oLD_TIME_POINT, double wAIT_NEW_SENSOR, double wAIT_NEW__LOOK_SENSOR, double lOOK_AROUND_DELTA,
            double mAX_VISION_GRASS_METERS, int mAX_VISION_GRASS_METERS_FLARE, double mAX_VISION_GRASS_METERS_OPT, 
            double mAX_VISION_GRASS_METERS_FLARE_OPT, int lightOnVisionDistance, int fAR_DISTANCE, int farDeltaTimeSec, 
            int mIDDLE_DIST, int middleDeltaTimeSec, double closeDeltaTimeSec, double pOSIBLE_VISION_SPACE, double gOAL_TO_FULL_DISSAPEAR, 
            double gOAL_TO_FULL_DISSAPEAR_SHOOT, double bODY_DELTA_TIME_SEARCH_SEC, double cOME_TO_BODY_DIST, double mARKSMAN_VISIBLE_DIST_COEF, 
            int vISIBLE_DISNACE_WITH_LIGHT, int eNEMY_LIGHT_ADD, int eNEMY_LIGHT_START_DIST, bool cAN_LOOK_TO_WALL, int dIST_NOT_TO_IGNORE_WALL,
            int dIST_CHECK_WALL, int lOOK_LAST_POSENEMY_IF_NO_DANGER_SEC, int mIN_LOOK_AROUD_TIME, bool oPTIMIZE_TO_ONLY_BODY)
        {
            OLD_TIME_POINT = oLD_TIME_POINT;
            WAIT_NEW_SENSOR = wAIT_NEW_SENSOR;
            WAIT_NEW__LOOK_SENSOR = wAIT_NEW__LOOK_SENSOR;
            LOOK_AROUND_DELTA = lOOK_AROUND_DELTA;
            MAX_VISION_GRASS_METERS = mAX_VISION_GRASS_METERS;
            MAX_VISION_GRASS_METERS_FLARE = mAX_VISION_GRASS_METERS_FLARE;
            MAX_VISION_GRASS_METERS_OPT = mAX_VISION_GRASS_METERS_OPT;
            MAX_VISION_GRASS_METERS_FLARE_OPT = mAX_VISION_GRASS_METERS_FLARE_OPT;
            LightOnVisionDistance = lightOnVisionDistance;
            FAR_DISTANCE = fAR_DISTANCE;
            FarDeltaTimeSec = farDeltaTimeSec;
            MIDDLE_DIST = mIDDLE_DIST;
            MiddleDeltaTimeSec = middleDeltaTimeSec;
            CloseDeltaTimeSec = closeDeltaTimeSec;
            POSIBLE_VISION_SPACE = pOSIBLE_VISION_SPACE;
            GOAL_TO_FULL_DISSAPEAR = gOAL_TO_FULL_DISSAPEAR;
            GOAL_TO_FULL_DISSAPEAR_SHOOT = gOAL_TO_FULL_DISSAPEAR_SHOOT;
            BODY_DELTA_TIME_SEARCH_SEC = bODY_DELTA_TIME_SEARCH_SEC;
            COME_TO_BODY_DIST = cOME_TO_BODY_DIST;
            MARKSMAN_VISIBLE_DIST_COEF = mARKSMAN_VISIBLE_DIST_COEF;
            VISIBLE_DISNACE_WITH_LIGHT = vISIBLE_DISNACE_WITH_LIGHT;
            ENEMY_LIGHT_ADD = eNEMY_LIGHT_ADD;
            ENEMY_LIGHT_START_DIST = eNEMY_LIGHT_START_DIST;
            CAN_LOOK_TO_WALL = cAN_LOOK_TO_WALL;
            DIST_NOT_TO_IGNORE_WALL = dIST_NOT_TO_IGNORE_WALL;
            DIST_CHECK_WALL = dIST_CHECK_WALL;
            LOOK_LAST_POSENEMY_IF_NO_DANGER_SEC = lOOK_LAST_POSENEMY_IF_NO_DANGER_SEC;
            MIN_LOOK_AROUD_TIME = mIN_LOOK_AROUD_TIME;
            OPTIMIZE_TO_ONLY_BODY = oPTIMIZE_TO_ONLY_BODY;
        }

        public int OLD_TIME_POINT { get; set; }
        public double WAIT_NEW_SENSOR { get; set; }
        public double WAIT_NEW__LOOK_SENSOR { get; set; }
        public double LOOK_AROUND_DELTA { get; set; }
        public double MAX_VISION_GRASS_METERS { get; set; }
        public int MAX_VISION_GRASS_METERS_FLARE { get; set; }
        public double MAX_VISION_GRASS_METERS_OPT { get; set; }
        public double MAX_VISION_GRASS_METERS_FLARE_OPT { get; set; }
        public int LightOnVisionDistance { get; set; }
        public int FAR_DISTANCE { get; set; }
        public int FarDeltaTimeSec { get; set; }
        public int MIDDLE_DIST { get; set; }
        public int MiddleDeltaTimeSec { get; set; }
        public double CloseDeltaTimeSec { get; set; }
        public double POSIBLE_VISION_SPACE { get; set; }
        public double GOAL_TO_FULL_DISSAPEAR { get; set; }
        public double GOAL_TO_FULL_DISSAPEAR_SHOOT { get; set; }
        public double BODY_DELTA_TIME_SEARCH_SEC { get; set; }
        public double COME_TO_BODY_DIST { get; set; }
        public double MARKSMAN_VISIBLE_DIST_COEF { get; set; }
        public int VISIBLE_DISNACE_WITH_LIGHT { get; set; }
        public int ENEMY_LIGHT_ADD { get; set; }
        public int ENEMY_LIGHT_START_DIST { get; set; }
        public bool CAN_LOOK_TO_WALL { get; set; }
        public int DIST_NOT_TO_IGNORE_WALL { get; set; }
        public int DIST_CHECK_WALL { get; set; }
        public int LOOK_LAST_POSENEMY_IF_NO_DANGER_SEC { get; set; }
        public int MIN_LOOK_AROUD_TIME { get; set; }
        public bool OPTIMIZE_TO_ONLY_BODY { get; set; }
    }

    public class Shoot
    {
        public Shoot(int rECOIL_TIME_NORMALIZE, double rECOIL_PER_METER, double mAX_RECOIL_PER_METER, double hORIZONT_RECOIL_COEF,
            double wAIT_NEXT_SINGLE_SHOT, double wAIT_NEXT_SINGLE_SHOT_LONG_MAX, double wAIT_NEXT_SINGLE_SHOT_LONG_MIN, int mARKSMAN_DIST_SEK_COEF,
            double fINGER_HOLD_SINGLE_SHOT, double bASE_AUTOMATIC_TIME, double aUTOMATIC_FIRE_SCATTERING_COEF, int cHANCE_TO_CHANGE_TO_AUTOMATIC_FIRE_100,
            int fAR_DIST_ENEMY, int sHOOT_FROM_COVER, int fAR_DIST_ENEMY_SQR, double mAX_DIST_COEF, double rECOIL_DELTA_PRESS, int rUN_DIST_NO_AMMO,
            int rUN_DIST_NO_AMMO_SQRT, int cAN_SHOOTS_TIME_TO_AMBUSH, double nOT_TO_SEE_ENEMY_TO_WANT_RELOAD_PERCENT,
            int nOT_TO_SEE_ENEMY_TO_WANT_RELOAD_SEC, double rELOAD_PECNET_NO_ENEMY, int cHANCE_TO_CHANGE_WEAPON,
            int cHANCE_TO_CHANGE_WEAPON_WITH_HELMET, int lOW_DIST_TO_CHANGE_WEAPON, int fAR_DIST_TO_CHANGE_WEAPON, int sUPPRESS_BY_SHOOT_TIME,
            int sUPPRESS_TRIGGERS_DOWN, int wAIT_NEXT_STATIONARY_GRENADE, double fINGER_HOLD_STATIONARY_GRENADE)
        {
            RECOIL_TIME_NORMALIZE = rECOIL_TIME_NORMALIZE;
            RECOIL_PER_METER = rECOIL_PER_METER;
            MAX_RECOIL_PER_METER = mAX_RECOIL_PER_METER;
            HORIZONT_RECOIL_COEF = hORIZONT_RECOIL_COEF;
            WAIT_NEXT_SINGLE_SHOT = wAIT_NEXT_SINGLE_SHOT;
            WAIT_NEXT_SINGLE_SHOT_LONG_MAX = wAIT_NEXT_SINGLE_SHOT_LONG_MAX;
            WAIT_NEXT_SINGLE_SHOT_LONG_MIN = wAIT_NEXT_SINGLE_SHOT_LONG_MIN;
            MARKSMAN_DIST_SEK_COEF = mARKSMAN_DIST_SEK_COEF;
            FINGER_HOLD_SINGLE_SHOT = fINGER_HOLD_SINGLE_SHOT;
            BASE_AUTOMATIC_TIME = bASE_AUTOMATIC_TIME;
            AUTOMATIC_FIRE_SCATTERING_COEF = aUTOMATIC_FIRE_SCATTERING_COEF;
            CHANCE_TO_CHANGE_TO_AUTOMATIC_FIRE_100 = cHANCE_TO_CHANGE_TO_AUTOMATIC_FIRE_100;
            FAR_DIST_ENEMY = fAR_DIST_ENEMY;
            SHOOT_FROM_COVER = sHOOT_FROM_COVER;
            FAR_DIST_ENEMY_SQR = fAR_DIST_ENEMY_SQR;
            MAX_DIST_COEF = mAX_DIST_COEF;
            RECOIL_DELTA_PRESS = rECOIL_DELTA_PRESS;
            RUN_DIST_NO_AMMO = rUN_DIST_NO_AMMO;
            RUN_DIST_NO_AMMO_SQRT = rUN_DIST_NO_AMMO_SQRT;
            CAN_SHOOTS_TIME_TO_AMBUSH = cAN_SHOOTS_TIME_TO_AMBUSH;
            NOT_TO_SEE_ENEMY_TO_WANT_RELOAD_PERCENT = nOT_TO_SEE_ENEMY_TO_WANT_RELOAD_PERCENT;
            NOT_TO_SEE_ENEMY_TO_WANT_RELOAD_SEC = nOT_TO_SEE_ENEMY_TO_WANT_RELOAD_SEC;
            RELOAD_PECNET_NO_ENEMY = rELOAD_PECNET_NO_ENEMY;
            CHANCE_TO_CHANGE_WEAPON = cHANCE_TO_CHANGE_WEAPON;
            CHANCE_TO_CHANGE_WEAPON_WITH_HELMET = cHANCE_TO_CHANGE_WEAPON_WITH_HELMET;
            LOW_DIST_TO_CHANGE_WEAPON = lOW_DIST_TO_CHANGE_WEAPON;
            FAR_DIST_TO_CHANGE_WEAPON = fAR_DIST_TO_CHANGE_WEAPON;
            SUPPRESS_BY_SHOOT_TIME = sUPPRESS_BY_SHOOT_TIME;
            SUPPRESS_TRIGGERS_DOWN = sUPPRESS_TRIGGERS_DOWN;
            WAIT_NEXT_STATIONARY_GRENADE = wAIT_NEXT_STATIONARY_GRENADE;
            FINGER_HOLD_STATIONARY_GRENADE = fINGER_HOLD_STATIONARY_GRENADE;
        }

        public int RECOIL_TIME_NORMALIZE { get; set; }
        public double RECOIL_PER_METER { get; set; }
        public double MAX_RECOIL_PER_METER { get; set; }
        public double HORIZONT_RECOIL_COEF { get; set; }
        public double WAIT_NEXT_SINGLE_SHOT { get; set; }
        public double WAIT_NEXT_SINGLE_SHOT_LONG_MAX { get; set; }
        public double WAIT_NEXT_SINGLE_SHOT_LONG_MIN { get; set; }
        public int MARKSMAN_DIST_SEK_COEF { get; set; }
        public double FINGER_HOLD_SINGLE_SHOT { get; set; }
        public double BASE_AUTOMATIC_TIME { get; set; }
        public double AUTOMATIC_FIRE_SCATTERING_COEF { get; set; }
        public int CHANCE_TO_CHANGE_TO_AUTOMATIC_FIRE_100 { get; set; }
        public int FAR_DIST_ENEMY { get; set; }
        public int SHOOT_FROM_COVER { get; set; }
        public int FAR_DIST_ENEMY_SQR { get; set; }
        public double MAX_DIST_COEF { get; set; }
        public double RECOIL_DELTA_PRESS { get; set; }
        public int RUN_DIST_NO_AMMO { get; set; }
        public int RUN_DIST_NO_AMMO_SQRT { get; set; }
        public int CAN_SHOOTS_TIME_TO_AMBUSH { get; set; }
        public double NOT_TO_SEE_ENEMY_TO_WANT_RELOAD_PERCENT { get; set; }
        public int NOT_TO_SEE_ENEMY_TO_WANT_RELOAD_SEC { get; set; }
        public double RELOAD_PECNET_NO_ENEMY { get; set; }
        public int CHANCE_TO_CHANGE_WEAPON { get; set; }
        public int CHANCE_TO_CHANGE_WEAPON_WITH_HELMET { get; set; }
        public int LOW_DIST_TO_CHANGE_WEAPON { get; set; }
        public int FAR_DIST_TO_CHANGE_WEAPON { get; set; }
        public int SUPPRESS_BY_SHOOT_TIME { get; set; }
        public int SUPPRESS_TRIGGERS_DOWN { get; set; }
        public int WAIT_NEXT_STATIONARY_GRENADE { get; set; }
        public double FINGER_HOLD_STATIONARY_GRENADE { get; set; }
    }

    public class Move
    {
        public Move(int bASE_ROTATE_SPEED, double rEACH_DIST, double rEACH_DIST_RUN, double sTART_SLOW_DIST, double bASESTART_SLOW_DIST, int sLOW_COEF, int dIST_TO_CAN_CHANGE_WAY, int dIST_TO_START_RAYCAST, int bASE_START_SERACH, int uPDATE_TIME_RECAL_WAY, int fAR_DIST, int fAR_DIST_SQR, int dIST_TO_CAN_CHANGE_WAY_SQR, int dIST_TO_START_RAYCAST_SQR, int bASE_SQRT_START_SERACH, double y_APPROXIMATION, int dELTA_LAST_SEEN_ENEMY, int rEACH_DIST_COVER, int rUN_TO_COVER_MIN, int cHANCE_TO_RUN_IF_NO_AMMO_0_100, bool rUN_IF_CANT_SHOOT, int rUN_IF_GAOL_FAR_THEN, int sEC_TO_CHANGE_TO_RUN)
        {
            BASE_ROTATE_SPEED = bASE_ROTATE_SPEED;
            REACH_DIST = rEACH_DIST;
            REACH_DIST_RUN = rEACH_DIST_RUN;
            START_SLOW_DIST = sTART_SLOW_DIST;
            BASESTART_SLOW_DIST = bASESTART_SLOW_DIST;
            SLOW_COEF = sLOW_COEF;
            DIST_TO_CAN_CHANGE_WAY = dIST_TO_CAN_CHANGE_WAY;
            DIST_TO_START_RAYCAST = dIST_TO_START_RAYCAST;
            BASE_START_SERACH = bASE_START_SERACH;
            UPDATE_TIME_RECAL_WAY = uPDATE_TIME_RECAL_WAY;
            FAR_DIST = fAR_DIST;
            FAR_DIST_SQR = fAR_DIST_SQR;
            DIST_TO_CAN_CHANGE_WAY_SQR = dIST_TO_CAN_CHANGE_WAY_SQR;
            DIST_TO_START_RAYCAST_SQR = dIST_TO_START_RAYCAST_SQR;
            BASE_SQRT_START_SERACH = bASE_SQRT_START_SERACH;
            Y_APPROXIMATION = y_APPROXIMATION;
            DELTA_LAST_SEEN_ENEMY = dELTA_LAST_SEEN_ENEMY;
            REACH_DIST_COVER = rEACH_DIST_COVER;
            RUN_TO_COVER_MIN = rUN_TO_COVER_MIN;
            CHANCE_TO_RUN_IF_NO_AMMO_0_100 = cHANCE_TO_RUN_IF_NO_AMMO_0_100;
            RUN_IF_CANT_SHOOT = rUN_IF_CANT_SHOOT;
            RUN_IF_GAOL_FAR_THEN = rUN_IF_GAOL_FAR_THEN;
            SEC_TO_CHANGE_TO_RUN = sEC_TO_CHANGE_TO_RUN;
        }

        public int BASE_ROTATE_SPEED { get; set; }
        public double REACH_DIST { get; set; }
        public double REACH_DIST_RUN { get; set; }
        public double START_SLOW_DIST { get; set; }
        public double BASESTART_SLOW_DIST { get; set; }
        public int SLOW_COEF { get; set; }
        public int DIST_TO_CAN_CHANGE_WAY { get; set; }
        public int DIST_TO_START_RAYCAST { get; set; }
        public int BASE_START_SERACH { get; set; }
        public int UPDATE_TIME_RECAL_WAY { get; set; }
        public int FAR_DIST { get; set; }
        public int FAR_DIST_SQR { get; set; }
        public int DIST_TO_CAN_CHANGE_WAY_SQR { get; set; }
        public int DIST_TO_START_RAYCAST_SQR { get; set; }
        public int BASE_SQRT_START_SERACH { get; set; }
        public double Y_APPROXIMATION { get; set; }
        public int DELTA_LAST_SEEN_ENEMY { get; set; }
        public int REACH_DIST_COVER { get; set; }
        public int RUN_TO_COVER_MIN { get; set; }
        public int CHANCE_TO_RUN_IF_NO_AMMO_0_100 { get; set; }
        public bool RUN_IF_CANT_SHOOT { get; set; }
        public int RUN_IF_GAOL_FAR_THEN { get; set; }
        public int SEC_TO_CHANGE_TO_RUN { get; set; }
    }

    public class Grenade
    {
        public Grenade(int dELTA_NEXT_ATTEMPT_FROM_COVER, int dELTA_NEXT_ATTEMPT, int mIN_DIST_NOT_TO_THROW, int nEAR_DELTA_THROW_TIME_SEC, int mIN_THROW_GRENADE_DIST, int mIN_THROW_GRENADE_DIST_SQRT, int mIN_DIST_NOT_TO_THROW_SQR, int rUN_AWAY, int rUN_AWAY_SQR, int aDD_GRENADE_AS_DANGER, int aDD_GRENADE_AS_DANGER_SQR, int cHANCE_TO_NOTIFY_ENEMY_GR_100, double grenadePerMeter, int rEQUEST_DIST_MUST_THROW_SQRT, int rEQUEST_DIST_MUST_THROW, int bEWARE_TYPE, int sHOOT_TO_SMOKE_CHANCE_100, int cHANCE_RUN_FLASHED_100, int mAX_FLASHED_DIST_TO_SHOOT, int mAX_FLASHED_DIST_TO_SHOOT_SQRT, double fLASH_GRENADE_TIME_COEF, int sIZE_SPOTTED_COEF, int bE_ATTENTION_COEF, int tIME_SHOOT_TO_FLASH, int cLOSE_TO_SMOKE_TO_SHOOT, int cLOSE_TO_SMOKE_TO_SHOOT_SQRT, int cLOSE_TO_SMOKE_TIME_DELTA, int sMOKE_CHECK_DELTA, double dELTA_GRENADE_START_TIME, int aMBUSH_IF_SMOKE_IN_ZONE_100, int aMBUSH_IF_SMOKE_RETURN_TO_ATTACK_SEC, bool nO_RUN_FROM_AI_GRENADES, double mAX_THROW_POWER, double grenadePrecision, bool sTOP_WHEN_THROW_GRENADE, double wAIT_TIME_TURN_AWAY, int sMOKE_SUPPRESS_DELTA, int dAMAGE_GRENADE_SUPPRESS_DELTA, int sTUN_SUPPRESS_DELTA, bool cHEAT_START_GRENADE_PLACE, bool cAN_THROW_STRAIGHT_CONTACT, int sTRAIGHT_CONTACT_DELTA_SEC, int aNG_TYPE)
        {
            DELTA_NEXT_ATTEMPT_FROM_COVER = dELTA_NEXT_ATTEMPT_FROM_COVER;
            DELTA_NEXT_ATTEMPT = dELTA_NEXT_ATTEMPT;
            MIN_DIST_NOT_TO_THROW = mIN_DIST_NOT_TO_THROW;
            NEAR_DELTA_THROW_TIME_SEC = nEAR_DELTA_THROW_TIME_SEC;
            MIN_THROW_GRENADE_DIST = mIN_THROW_GRENADE_DIST;
            MIN_THROW_GRENADE_DIST_SQRT = mIN_THROW_GRENADE_DIST_SQRT;
            MIN_DIST_NOT_TO_THROW_SQR = mIN_DIST_NOT_TO_THROW_SQR;
            RUN_AWAY = rUN_AWAY;
            RUN_AWAY_SQR = rUN_AWAY_SQR;
            ADD_GRENADE_AS_DANGER = aDD_GRENADE_AS_DANGER;
            ADD_GRENADE_AS_DANGER_SQR = aDD_GRENADE_AS_DANGER_SQR;
            CHANCE_TO_NOTIFY_ENEMY_GR_100 = cHANCE_TO_NOTIFY_ENEMY_GR_100;
            GrenadePerMeter = grenadePerMeter;
            REQUEST_DIST_MUST_THROW_SQRT = rEQUEST_DIST_MUST_THROW_SQRT;
            REQUEST_DIST_MUST_THROW = rEQUEST_DIST_MUST_THROW;
            BEWARE_TYPE = bEWARE_TYPE;
            SHOOT_TO_SMOKE_CHANCE_100 = sHOOT_TO_SMOKE_CHANCE_100;
            CHANCE_RUN_FLASHED_100 = cHANCE_RUN_FLASHED_100;
            MAX_FLASHED_DIST_TO_SHOOT = mAX_FLASHED_DIST_TO_SHOOT;
            MAX_FLASHED_DIST_TO_SHOOT_SQRT = mAX_FLASHED_DIST_TO_SHOOT_SQRT;
            FLASH_GRENADE_TIME_COEF = fLASH_GRENADE_TIME_COEF;
            SIZE_SPOTTED_COEF = sIZE_SPOTTED_COEF;
            BE_ATTENTION_COEF = bE_ATTENTION_COEF;
            TIME_SHOOT_TO_FLASH = tIME_SHOOT_TO_FLASH;
            CLOSE_TO_SMOKE_TO_SHOOT = cLOSE_TO_SMOKE_TO_SHOOT;
            CLOSE_TO_SMOKE_TO_SHOOT_SQRT = cLOSE_TO_SMOKE_TO_SHOOT_SQRT;
            CLOSE_TO_SMOKE_TIME_DELTA = cLOSE_TO_SMOKE_TIME_DELTA;
            SMOKE_CHECK_DELTA = sMOKE_CHECK_DELTA;
            DELTA_GRENADE_START_TIME = dELTA_GRENADE_START_TIME;
            AMBUSH_IF_SMOKE_IN_ZONE_100 = aMBUSH_IF_SMOKE_IN_ZONE_100;
            AMBUSH_IF_SMOKE_RETURN_TO_ATTACK_SEC = aMBUSH_IF_SMOKE_RETURN_TO_ATTACK_SEC;
            NO_RUN_FROM_AI_GRENADES = nO_RUN_FROM_AI_GRENADES;
            MAX_THROW_POWER = mAX_THROW_POWER;
            GrenadePrecision = grenadePrecision;
            STOP_WHEN_THROW_GRENADE = sTOP_WHEN_THROW_GRENADE;
            WAIT_TIME_TURN_AWAY = wAIT_TIME_TURN_AWAY;
            SMOKE_SUPPRESS_DELTA = sMOKE_SUPPRESS_DELTA;
            DAMAGE_GRENADE_SUPPRESS_DELTA = dAMAGE_GRENADE_SUPPRESS_DELTA;
            STUN_SUPPRESS_DELTA = sTUN_SUPPRESS_DELTA;
            CHEAT_START_GRENADE_PLACE = cHEAT_START_GRENADE_PLACE;
            CAN_THROW_STRAIGHT_CONTACT = cAN_THROW_STRAIGHT_CONTACT;
            STRAIGHT_CONTACT_DELTA_SEC = sTRAIGHT_CONTACT_DELTA_SEC;
            ANG_TYPE = aNG_TYPE;
        }

        public int DELTA_NEXT_ATTEMPT_FROM_COVER { get; set; }
        public int DELTA_NEXT_ATTEMPT { get; set; }
        public int MIN_DIST_NOT_TO_THROW { get; set; }
        public int NEAR_DELTA_THROW_TIME_SEC { get; set; }
        public int MIN_THROW_GRENADE_DIST { get; set; }
        public int MIN_THROW_GRENADE_DIST_SQRT { get; set; }
        public int MIN_DIST_NOT_TO_THROW_SQR { get; set; }
        public int RUN_AWAY { get; set; }
        public int RUN_AWAY_SQR { get; set; }
        public int ADD_GRENADE_AS_DANGER { get; set; }
        public int ADD_GRENADE_AS_DANGER_SQR { get; set; }
        public int CHANCE_TO_NOTIFY_ENEMY_GR_100 { get; set; }
        public double GrenadePerMeter { get; set; }
        public int REQUEST_DIST_MUST_THROW_SQRT { get; set; }
        public int REQUEST_DIST_MUST_THROW { get; set; }
        public int BEWARE_TYPE { get; set; }
        public int SHOOT_TO_SMOKE_CHANCE_100 { get; set; }
        public int CHANCE_RUN_FLASHED_100 { get; set; }
        public int MAX_FLASHED_DIST_TO_SHOOT { get; set; }
        public int MAX_FLASHED_DIST_TO_SHOOT_SQRT { get; set; }
        public double FLASH_GRENADE_TIME_COEF { get; set; }
        public int SIZE_SPOTTED_COEF { get; set; }
        public int BE_ATTENTION_COEF { get; set; }
        public int TIME_SHOOT_TO_FLASH { get; set; }
        public int CLOSE_TO_SMOKE_TO_SHOOT { get; set; }
        public int CLOSE_TO_SMOKE_TO_SHOOT_SQRT { get; set; }
        public int CLOSE_TO_SMOKE_TIME_DELTA { get; set; }
        public int SMOKE_CHECK_DELTA { get; set; }
        public double DELTA_GRENADE_START_TIME { get; set; }
        public int AMBUSH_IF_SMOKE_IN_ZONE_100 { get; set; }
        public int AMBUSH_IF_SMOKE_RETURN_TO_ATTACK_SEC { get; set; }
        public bool NO_RUN_FROM_AI_GRENADES { get; set; }
        public double MAX_THROW_POWER { get; set; }
        public double GrenadePrecision { get; set; }
        public bool STOP_WHEN_THROW_GRENADE { get; set; }
        public double WAIT_TIME_TURN_AWAY { get; set; }
        public int SMOKE_SUPPRESS_DELTA { get; set; }
        public int DAMAGE_GRENADE_SUPPRESS_DELTA { get; set; }
        public int STUN_SUPPRESS_DELTA { get; set; }
        public bool CHEAT_START_GRENADE_PLACE { get; set; }
        public bool CAN_THROW_STRAIGHT_CONTACT { get; set; }
        public int STRAIGHT_CONTACT_DELTA_SEC { get; set; }
        public int ANG_TYPE { get; set; }
        public double MIN_THROW_DIST_PERCENT_0_1 { get; set; }
    }

    public class Change
    {
        public Change(double sMOKE_VISION_DIST, double sMOKE_GAIN_SIGHT, double sMOKE_SCATTERING, double sMOKE_PRECICING, int sMOKE_HEARING, double sMOKE_ACCURATY, double sMOKE_LAY_CHANCE, double fLASH_VISION_DIST, double fLASH_GAIN_SIGHT, double fLASH_SCATTERING, double fLASH_PRECICING, int fLASH_HEARING, double fLASH_ACCURATY, int fLASH_LAY_CHANCE, double sTUN_HEARING)
        {
            SMOKE_VISION_DIST = sMOKE_VISION_DIST;
            SMOKE_GAIN_SIGHT = sMOKE_GAIN_SIGHT;
            SMOKE_SCATTERING = sMOKE_SCATTERING;
            SMOKE_PRECICING = sMOKE_PRECICING;
            SMOKE_HEARING = sMOKE_HEARING;
            SMOKE_ACCURATY = sMOKE_ACCURATY;
            SMOKE_LAY_CHANCE = sMOKE_LAY_CHANCE;
            FLASH_VISION_DIST = fLASH_VISION_DIST;
            FLASH_GAIN_SIGHT = fLASH_GAIN_SIGHT;
            FLASH_SCATTERING = fLASH_SCATTERING;
            FLASH_PRECICING = fLASH_PRECICING;
            FLASH_HEARING = fLASH_HEARING;
            FLASH_ACCURATY = fLASH_ACCURATY;
            FLASH_LAY_CHANCE = fLASH_LAY_CHANCE;
            STUN_HEARING = sTUN_HEARING;
        }

        public double SMOKE_VISION_DIST { get; set; }
        public double SMOKE_GAIN_SIGHT { get; set; }
        public double SMOKE_SCATTERING { get; set; }
        public double SMOKE_PRECICING { get; set; }
        public int SMOKE_HEARING { get; set; }
        public double SMOKE_ACCURATY { get; set; }
        public double SMOKE_LAY_CHANCE { get; set; }
        public double FLASH_VISION_DIST { get; set; }
        public double FLASH_GAIN_SIGHT { get; set; }
        public double FLASH_SCATTERING { get; set; }
        public double FLASH_PRECICING { get; set; }
        public int FLASH_HEARING { get; set; }
        public double FLASH_ACCURATY { get; set; }
        public int FLASH_LAY_CHANCE { get; set; }
        public double STUN_HEARING { get; set; }
    }

    public class Cover
    {
        public Cover(int rETURN_TO_ATTACK_AFTER_AMBUSH_MIN, int rETURN_TO_ATTACK_AFTER_AMBUSH_MAX, int sOUND_TO_GET_SPOTTED, int tIME_TO_MOVE_TO_COVER, int mAX_DIST_OF_COVER, int cHANGE_RUN_TO_COVER_SEC, double cHANGE_RUN_TO_COVER_SEC_GREANDE, int mIN_DIST_TO_ENEMY, int dIST_CANT_CHANGE_WAY, int dIST_CHECK_SFETY, int tIME_CHECK_SAFE, double hIDE_TO_COVER_TIME, int mAX_DIST_OF_COVER_SQR, int dIST_CANT_CHANGE_WAY_SQR, int sPOTTED_COVERS_RADIUS, double lOOK_LAST_ENEMY_POS_MOVING, int lOOK_TO_HIT_POINT_IF_LAST_ENEMY, int lOOK_LAST_ENEMY_POS_LOOKAROUND, int oFFSET_LOOK_ALONG_WALL_ANG, int sPOTTED_GRENADE_RADIUS, int mAX_SPOTTED_TIME_SEC, int wAIT_INT_COVER_FINDING_ENEMY, int cLOSE_DIST_POINT_SQRT, int dELTA_SEEN_FROM_COVE_LAST_POS, bool mOVE_TO_COVER_WHEN_TARGET, bool rUN_COVER_IF_CAN_AND_NO_ENEMIES, int sPOTTED_GRENADE_TIME, bool dEPENDS_Y_DIST_TO_BOT, int rUN_IF_FAR, int rUN_IF_FAR_SQRT, int sTAY_IF_FAR, int sTAY_IF_FAR_SQRT, bool cHECK_COVER_ENEMY_LOOK, int sHOOT_NEAR_TO_LEAVE, double sHOOT_NEAR_SEC_PERIOD, int hITS_TO_LEAVE_COVER, int hITS_TO_LEAVE_COVER_UNKNOWN, int dOG_FIGHT_AFTER_LEAVE, bool nOT_LOOK_AT_WALL_IS_DANGER, int mIN_DEFENCE_LEVEL, double gOOD_DIST_TO_POINT_COEF, int eNEMY_DIST_TO_GO_OUT, int sTATIONARY_WEAPON_NO_ENEMY_GETUP, int sTATIONARY_WEAPON_MAX_DIST_TO_USE)
        {
            RETURN_TO_ATTACK_AFTER_AMBUSH_MIN = rETURN_TO_ATTACK_AFTER_AMBUSH_MIN;
            RETURN_TO_ATTACK_AFTER_AMBUSH_MAX = rETURN_TO_ATTACK_AFTER_AMBUSH_MAX;
            SOUND_TO_GET_SPOTTED = sOUND_TO_GET_SPOTTED;
            TIME_TO_MOVE_TO_COVER = tIME_TO_MOVE_TO_COVER;
            MAX_DIST_OF_COVER = mAX_DIST_OF_COVER;
            CHANGE_RUN_TO_COVER_SEC = cHANGE_RUN_TO_COVER_SEC;
            CHANGE_RUN_TO_COVER_SEC_GREANDE = cHANGE_RUN_TO_COVER_SEC_GREANDE;
            MIN_DIST_TO_ENEMY = mIN_DIST_TO_ENEMY;
            DIST_CANT_CHANGE_WAY = dIST_CANT_CHANGE_WAY;
            DIST_CHECK_SFETY = dIST_CHECK_SFETY;
            TIME_CHECK_SAFE = tIME_CHECK_SAFE;
            HIDE_TO_COVER_TIME = hIDE_TO_COVER_TIME;
            MAX_DIST_OF_COVER_SQR = mAX_DIST_OF_COVER_SQR;
            DIST_CANT_CHANGE_WAY_SQR = dIST_CANT_CHANGE_WAY_SQR;
            SPOTTED_COVERS_RADIUS = sPOTTED_COVERS_RADIUS;
            LOOK_LAST_ENEMY_POS_MOVING = lOOK_LAST_ENEMY_POS_MOVING;
            LOOK_TO_HIT_POINT_IF_LAST_ENEMY = lOOK_TO_HIT_POINT_IF_LAST_ENEMY;
            LOOK_LAST_ENEMY_POS_LOOKAROUND = lOOK_LAST_ENEMY_POS_LOOKAROUND;
            OFFSET_LOOK_ALONG_WALL_ANG = oFFSET_LOOK_ALONG_WALL_ANG;
            SPOTTED_GRENADE_RADIUS = sPOTTED_GRENADE_RADIUS;
            MAX_SPOTTED_TIME_SEC = mAX_SPOTTED_TIME_SEC;
            WAIT_INT_COVER_FINDING_ENEMY = wAIT_INT_COVER_FINDING_ENEMY;
            CLOSE_DIST_POINT_SQRT = cLOSE_DIST_POINT_SQRT;
            DELTA_SEEN_FROM_COVE_LAST_POS = dELTA_SEEN_FROM_COVE_LAST_POS;
            MOVE_TO_COVER_WHEN_TARGET = mOVE_TO_COVER_WHEN_TARGET;
            RUN_COVER_IF_CAN_AND_NO_ENEMIES = rUN_COVER_IF_CAN_AND_NO_ENEMIES;
            SPOTTED_GRENADE_TIME = sPOTTED_GRENADE_TIME;
            DEPENDS_Y_DIST_TO_BOT = dEPENDS_Y_DIST_TO_BOT;
            RUN_IF_FAR = rUN_IF_FAR;
            RUN_IF_FAR_SQRT = rUN_IF_FAR_SQRT;
            STAY_IF_FAR = sTAY_IF_FAR;
            STAY_IF_FAR_SQRT = sTAY_IF_FAR_SQRT;
            CHECK_COVER_ENEMY_LOOK = cHECK_COVER_ENEMY_LOOK;
            SHOOT_NEAR_TO_LEAVE = sHOOT_NEAR_TO_LEAVE;
            SHOOT_NEAR_SEC_PERIOD = sHOOT_NEAR_SEC_PERIOD;
            HITS_TO_LEAVE_COVER = hITS_TO_LEAVE_COVER;
            HITS_TO_LEAVE_COVER_UNKNOWN = hITS_TO_LEAVE_COVER_UNKNOWN;
            DOG_FIGHT_AFTER_LEAVE = dOG_FIGHT_AFTER_LEAVE;
            NOT_LOOK_AT_WALL_IS_DANGER = nOT_LOOK_AT_WALL_IS_DANGER;
            MIN_DEFENCE_LEVEL = mIN_DEFENCE_LEVEL;
            GOOD_DIST_TO_POINT_COEF = gOOD_DIST_TO_POINT_COEF;
            ENEMY_DIST_TO_GO_OUT = eNEMY_DIST_TO_GO_OUT;
            STATIONARY_WEAPON_NO_ENEMY_GETUP = sTATIONARY_WEAPON_NO_ENEMY_GETUP;
            STATIONARY_WEAPON_MAX_DIST_TO_USE = sTATIONARY_WEAPON_MAX_DIST_TO_USE;
        }

        public int RETURN_TO_ATTACK_AFTER_AMBUSH_MIN { get; set; }
        public int RETURN_TO_ATTACK_AFTER_AMBUSH_MAX { get; set; }
        public int SOUND_TO_GET_SPOTTED { get; set; }
        public int TIME_TO_MOVE_TO_COVER { get; set; }
        public int MAX_DIST_OF_COVER { get; set; }
        public int CHANGE_RUN_TO_COVER_SEC { get; set; }
        public double CHANGE_RUN_TO_COVER_SEC_GREANDE { get; set; }
        public int MIN_DIST_TO_ENEMY { get; set; }
        public int DIST_CANT_CHANGE_WAY { get; set; }
        public int DIST_CHECK_SFETY { get; set; }
        public int TIME_CHECK_SAFE { get; set; }
        public double HIDE_TO_COVER_TIME { get; set; }
        public int MAX_DIST_OF_COVER_SQR { get; set; }
        public int DIST_CANT_CHANGE_WAY_SQR { get; set; }
        public int SPOTTED_COVERS_RADIUS { get; set; }
        public double LOOK_LAST_ENEMY_POS_MOVING { get; set; }
        public int LOOK_TO_HIT_POINT_IF_LAST_ENEMY { get; set; }
        public int LOOK_LAST_ENEMY_POS_LOOKAROUND { get; set; }
        public int OFFSET_LOOK_ALONG_WALL_ANG { get; set; }
        public int SPOTTED_GRENADE_RADIUS { get; set; }
        public int MAX_SPOTTED_TIME_SEC { get; set; }
        public int WAIT_INT_COVER_FINDING_ENEMY { get; set; }
        public int CLOSE_DIST_POINT_SQRT { get; set; }
        public int DELTA_SEEN_FROM_COVE_LAST_POS { get; set; }
        public bool MOVE_TO_COVER_WHEN_TARGET { get; set; }
        public bool RUN_COVER_IF_CAN_AND_NO_ENEMIES { get; set; }
        public int SPOTTED_GRENADE_TIME { get; set; }
        public bool DEPENDS_Y_DIST_TO_BOT { get; set; }
        public int RUN_IF_FAR { get; set; }
        public int RUN_IF_FAR_SQRT { get; set; }
        public int STAY_IF_FAR { get; set; }
        public int STAY_IF_FAR_SQRT { get; set; }
        public bool CHECK_COVER_ENEMY_LOOK { get; set; }
        public int SHOOT_NEAR_TO_LEAVE { get; set; }
        public double SHOOT_NEAR_SEC_PERIOD { get; set; }
        public int HITS_TO_LEAVE_COVER { get; set; }
        public int HITS_TO_LEAVE_COVER_UNKNOWN { get; set; }
        public int DOG_FIGHT_AFTER_LEAVE { get; set; }
        public bool NOT_LOOK_AT_WALL_IS_DANGER { get; set; }
        public int MIN_DEFENCE_LEVEL { get; set; }
        public double GOOD_DIST_TO_POINT_COEF { get; set; }
        public int ENEMY_DIST_TO_GO_OUT { get; set; }
        public int STATIONARY_WEAPON_NO_ENEMY_GETUP { get; set; }
        public int STATIONARY_WEAPON_MAX_DIST_TO_USE { get; set; }
    }

    public class Patrol
    {
        public Patrol(int dEAD_BODY_LOOK_PERIOD, int lOOK_TIME_BASE, bool cAN_CHOOSE_RESERV, bool tRY_CHOOSE_RESERV_WAY_ON_START, bool cAN_LOOK_TO_DEADBODIES, bool cAN_FRIENDLY_TILT, bool cAN_HARD_AIM, int rESERVE_TIME_STAY, int fRIEND_SEARCH_SEC, double tALK_DELAY, int mIN_TALK_DELAY, double tALK_DELAY_BIG, double cHANGE_WAY_TIME, int mIN_DIST_TO_CLOSE_TALK, double vISION_DIST_COEF_PEACE, int mIN_DIST_TO_CLOSE_TALK_SQR, int cHANCE_TO_CUT_WAY_0_100, double cUT_WAY_MIN_0_1, double cUT_WAY_MAX_0_1, int cHANCE_TO_CHANGE_WAY_0_100, int cHANCE_TO_SHOOT_DEADBODY, int sUSPETION_PLACE_LIFETIME, int rESERVE_OUT_TIME, int cLOSE_TO_SELECT_RESERV_WAY, int mAX_YDIST_TO_START_WARN_REQUEST_TO_REQUESTER)
        {
            DEAD_BODY_LOOK_PERIOD = dEAD_BODY_LOOK_PERIOD;
            LOOK_TIME_BASE = lOOK_TIME_BASE;
            CAN_CHOOSE_RESERV = cAN_CHOOSE_RESERV;
            TRY_CHOOSE_RESERV_WAY_ON_START = tRY_CHOOSE_RESERV_WAY_ON_START;
            CAN_LOOK_TO_DEADBODIES = cAN_LOOK_TO_DEADBODIES;
            CAN_FRIENDLY_TILT = cAN_FRIENDLY_TILT;
            CAN_HARD_AIM = cAN_HARD_AIM;
            RESERVE_TIME_STAY = rESERVE_TIME_STAY;
            FRIEND_SEARCH_SEC = fRIEND_SEARCH_SEC;
            TALK_DELAY = tALK_DELAY;
            MIN_TALK_DELAY = mIN_TALK_DELAY;
            TALK_DELAY_BIG = tALK_DELAY_BIG;
            CHANGE_WAY_TIME = cHANGE_WAY_TIME;
            MIN_DIST_TO_CLOSE_TALK = mIN_DIST_TO_CLOSE_TALK;
            VISION_DIST_COEF_PEACE = vISION_DIST_COEF_PEACE;
            MIN_DIST_TO_CLOSE_TALK_SQR = mIN_DIST_TO_CLOSE_TALK_SQR;
            CHANCE_TO_CUT_WAY_0_100 = cHANCE_TO_CUT_WAY_0_100;
            CUT_WAY_MIN_0_1 = cUT_WAY_MIN_0_1;
            CUT_WAY_MAX_0_1 = cUT_WAY_MAX_0_1;
            CHANCE_TO_CHANGE_WAY_0_100 = cHANCE_TO_CHANGE_WAY_0_100;
            CHANCE_TO_SHOOT_DEADBODY = cHANCE_TO_SHOOT_DEADBODY;
            SUSPETION_PLACE_LIFETIME = sUSPETION_PLACE_LIFETIME;
            RESERVE_OUT_TIME = rESERVE_OUT_TIME;
            CLOSE_TO_SELECT_RESERV_WAY = cLOSE_TO_SELECT_RESERV_WAY;
            MAX_YDIST_TO_START_WARN_REQUEST_TO_REQUESTER = mAX_YDIST_TO_START_WARN_REQUEST_TO_REQUESTER;
        }

        public int DEAD_BODY_LOOK_PERIOD { get; set; }
        public int LOOK_TIME_BASE { get; set; }
        public bool CAN_CHOOSE_RESERV { get; set; }
        public bool TRY_CHOOSE_RESERV_WAY_ON_START { get; set; }
        public bool CAN_LOOK_TO_DEADBODIES { get; set; }
        public bool CAN_FRIENDLY_TILT { get; set; }
        public bool CAN_HARD_AIM { get; set; }
        public int RESERVE_TIME_STAY { get; set; }
        public int FRIEND_SEARCH_SEC { get; set; }
        public double TALK_DELAY { get; set; }
        public int MIN_TALK_DELAY { get; set; }
        public double TALK_DELAY_BIG { get; set; }
        public double CHANGE_WAY_TIME { get; set; }
        public int MIN_DIST_TO_CLOSE_TALK { get; set; }
        public double VISION_DIST_COEF_PEACE { get; set; }
        public int MIN_DIST_TO_CLOSE_TALK_SQR { get; set; }
        public int CHANCE_TO_CUT_WAY_0_100 { get; set; }
        public double CUT_WAY_MIN_0_1 { get; set; }
        public double CUT_WAY_MAX_0_1 { get; set; }
        public int CHANCE_TO_CHANGE_WAY_0_100 { get; set; }
        public int CHANCE_TO_SHOOT_DEADBODY { get; set; }
        public int SUSPETION_PLACE_LIFETIME { get; set; }
        public int RESERVE_OUT_TIME { get; set; }
        public int CLOSE_TO_SELECT_RESERV_WAY { get; set; }
        public int MAX_YDIST_TO_START_WARN_REQUEST_TO_REQUESTER { get; set; }
    }

    public class Hearing
    {
        public Hearing(int bOT_CLOSE_PANIC_DIST, double cHANCE_TO_HEAR_SIMPLE_SOUND_0_1, double dISPERSION_COEF, int cLOSE_DIST, int fAR_DIST, int sOUND_DIR_DEEFREE, int dIST_PLACE_TO_FIND_POINT, int dEAD_BODY_SOUND_RAD, bool lOOK_ONLY_DANGER, int rESET_TIMER_DIST, double hEAR_DELAY_WHEN_PEACE, double hEAR_DELAY_WHEN_HAVE_SMT, int lOOK_ONLY_DANGER_DELTA)
        {
            BOT_CLOSE_PANIC_DIST = bOT_CLOSE_PANIC_DIST;
            CHANCE_TO_HEAR_SIMPLE_SOUND_0_1 = cHANCE_TO_HEAR_SIMPLE_SOUND_0_1;
            DISPERSION_COEF = dISPERSION_COEF;
            CLOSE_DIST = cLOSE_DIST;
            FAR_DIST = fAR_DIST;
            SOUND_DIR_DEEFREE = sOUND_DIR_DEEFREE;
            DIST_PLACE_TO_FIND_POINT = dIST_PLACE_TO_FIND_POINT;
            DEAD_BODY_SOUND_RAD = dEAD_BODY_SOUND_RAD;
            LOOK_ONLY_DANGER = lOOK_ONLY_DANGER;
            RESET_TIMER_DIST = rESET_TIMER_DIST;
            HEAR_DELAY_WHEN_PEACE = hEAR_DELAY_WHEN_PEACE;
            HEAR_DELAY_WHEN_HAVE_SMT = hEAR_DELAY_WHEN_HAVE_SMT;
            LOOK_ONLY_DANGER_DELTA = lOOK_ONLY_DANGER_DELTA;
        }

        public int BOT_CLOSE_PANIC_DIST { get; set; }
        public double CHANCE_TO_HEAR_SIMPLE_SOUND_0_1 { get; set; }
        public double DISPERSION_COEF { get; set; }
        public int CLOSE_DIST { get; set; }
        public int FAR_DIST { get; set; }
        public int SOUND_DIR_DEEFREE { get; set; }
        public int DIST_PLACE_TO_FIND_POINT { get; set; }
        public int DEAD_BODY_SOUND_RAD { get; set; }
        public bool LOOK_ONLY_DANGER { get; set; }
        public int RESET_TIMER_DIST { get; set; }
        public double HEAR_DELAY_WHEN_PEACE { get; set; }
        public double HEAR_DELAY_WHEN_HAVE_SMT { get; set; }
        public int LOOK_ONLY_DANGER_DELTA { get; set; }
    }

    public class Mind
    {
        public Mind(int hOW_WORK_OVER_DEAD_BODY, int mIN_SHOOTS_TIME, int mAX_SHOOTS_TIME, int tIME_LEAVE_MAP, int tIME_TO_RUN_TO_COVER_CAUSE_SHOOT_SEC, int dAMAGE_REDUCTION_TIME_SEC, int mIN_DAMAGE_SCARE, int cHANCE_TO_RUN_CAUSE_DAMAGE_0_100, int tIME_TO_FORGOR_ABOUT_ENEMY_SEC, int tIME_TO_FIND_ENEMY, int mAX_AGGRO_BOT_DIST, int hIT_POINT_DETECTION, int dANGER_POINT_CHOOSE_COEF, double sIMPLE_POINT_CHOOSE_COEF, double lASTSEEN_POINT_CHOOSE_COEF, double cOVER_DIST_COEF, int dIST_TO_FOUND_SQRT, int mAX_AGGRO_BOT_DIST_SQR, int dIST_TO_STOP_RUN_ENEMY, int eNEMY_LOOK_AT_ME_ANG, int mIN_START_AGGRESION_COEF, int mAX_START_AGGRESION_COEF, int bULLET_FEEL_DIST, int bULLET_FEEL_CLOSE_SDIST, int aTTACK_IMMEDIATLY_CHANCE_0_100, int cHANCE_FUCK_YOU_ON_CONTACT_100, double fRIEND_DEAD_AGR_LOW, double fRIEND_AGR_KILL, int lAST_ENEMY_LOOK_TO, bool cAN_RECIVE_PLAYER_REQUESTS, bool cAN_TAKE_ITEMS, bool cAN_USE_MEDS, int sUSPETION_POINT_CHANCE_ADD100, bool aMBUSH_WHEN_UNDER_FIRE, int aMBUSH_WHEN_UNDER_FIRE_TIME_RESIST, double aTTACK_ENEMY_IF_PROTECT_DELTA_LAST_TIME_SEEN, double hOLD_IF_PROTECT_DELTA_LAST_TIME_SEEN, int fIND_COVER_TO_GET_POSITION_WITH_SHOOT, bool pROTECT_TIME_REAL, int cHANCE_SHOOT_WHEN_WARN_PLAYER_100, bool cAN_PANIC_IS_PROTECT, bool nO_RUN_AWAY_FOR_SAFE, double pART_PERCENT_TO_HEAL, int pROTECT_DELTA_HEAL_SEC, bool cAN_STAND_BY, bool cAN_THROW_REQUESTS, int gROUP_ANY_PHRASE_DELAY, int gROUP_EXACTLY_PHRASE_DELAY, int dIST_TO_ENEMY_YO_CAN_HEAL, int cHANCE_TO_STAY_WHEN_WARN_PLAYER_100, int dOG_FIGHT_OUT, int dOG_FIGHT_IN, int sHOOT_INSTEAD_DOG_FIGHT, int pISTOL_SHOTGUN_AMBUSH_DIST, int sTANDART_AMBUSH_DIST, int aI_POWER_COEF, int cOVER_SECONDS_AFTER_LOSE_VISION, bool cOVER_SELF_ALWAYS_IF_DAMAGED, int sEC_TO_MORE_DIST_TO_RUN, int hEAL_DELAY_SEC, int hIT_DELAY_WHEN_HAVE_SMT, int hIT_DELAY_WHEN_PEACE, bool tALK_WITH_QUERY, bool wILL_PERSUE_AXEMAN)
        {
            HOW_WORK_OVER_DEAD_BODY = hOW_WORK_OVER_DEAD_BODY;
            MIN_SHOOTS_TIME = mIN_SHOOTS_TIME;
            MAX_SHOOTS_TIME = mAX_SHOOTS_TIME;
            TIME_LEAVE_MAP = tIME_LEAVE_MAP;
            TIME_TO_RUN_TO_COVER_CAUSE_SHOOT_SEC = tIME_TO_RUN_TO_COVER_CAUSE_SHOOT_SEC;
            DAMAGE_REDUCTION_TIME_SEC = dAMAGE_REDUCTION_TIME_SEC;
            MIN_DAMAGE_SCARE = mIN_DAMAGE_SCARE;
            CHANCE_TO_RUN_CAUSE_DAMAGE_0_100 = cHANCE_TO_RUN_CAUSE_DAMAGE_0_100;
            TIME_TO_FORGOR_ABOUT_ENEMY_SEC = tIME_TO_FORGOR_ABOUT_ENEMY_SEC;
            TIME_TO_FIND_ENEMY = tIME_TO_FIND_ENEMY;
            MAX_AGGRO_BOT_DIST = mAX_AGGRO_BOT_DIST;
            HIT_POINT_DETECTION = hIT_POINT_DETECTION;
            DANGER_POINT_CHOOSE_COEF = dANGER_POINT_CHOOSE_COEF;
            SIMPLE_POINT_CHOOSE_COEF = sIMPLE_POINT_CHOOSE_COEF;
            LASTSEEN_POINT_CHOOSE_COEF = lASTSEEN_POINT_CHOOSE_COEF;
            COVER_DIST_COEF = cOVER_DIST_COEF;
            DIST_TO_FOUND_SQRT = dIST_TO_FOUND_SQRT;
            MAX_AGGRO_BOT_DIST_SQR = mAX_AGGRO_BOT_DIST_SQR;
            DIST_TO_STOP_RUN_ENEMY = dIST_TO_STOP_RUN_ENEMY;
            ENEMY_LOOK_AT_ME_ANG = eNEMY_LOOK_AT_ME_ANG;
            MIN_START_AGGRESION_COEF = mIN_START_AGGRESION_COEF;
            MAX_START_AGGRESION_COEF = mAX_START_AGGRESION_COEF;
            BULLET_FEEL_DIST = bULLET_FEEL_DIST;
            BULLET_FEEL_CLOSE_SDIST = bULLET_FEEL_CLOSE_SDIST;
            ATTACK_IMMEDIATLY_CHANCE_0_100 = aTTACK_IMMEDIATLY_CHANCE_0_100;
            CHANCE_FUCK_YOU_ON_CONTACT_100 = cHANCE_FUCK_YOU_ON_CONTACT_100;
            FRIEND_DEAD_AGR_LOW = fRIEND_DEAD_AGR_LOW;
            FRIEND_AGR_KILL = fRIEND_AGR_KILL;
            LAST_ENEMY_LOOK_TO = lAST_ENEMY_LOOK_TO;
            CAN_RECIVE_PLAYER_REQUESTS = cAN_RECIVE_PLAYER_REQUESTS;
            CAN_TAKE_ITEMS = cAN_TAKE_ITEMS;
            CAN_USE_MEDS = cAN_USE_MEDS;
            SUSPETION_POINT_CHANCE_ADD100 = sUSPETION_POINT_CHANCE_ADD100;
            AMBUSH_WHEN_UNDER_FIRE = aMBUSH_WHEN_UNDER_FIRE;
            AMBUSH_WHEN_UNDER_FIRE_TIME_RESIST = aMBUSH_WHEN_UNDER_FIRE_TIME_RESIST;
            ATTACK_ENEMY_IF_PROTECT_DELTA_LAST_TIME_SEEN = aTTACK_ENEMY_IF_PROTECT_DELTA_LAST_TIME_SEEN;
            HOLD_IF_PROTECT_DELTA_LAST_TIME_SEEN = hOLD_IF_PROTECT_DELTA_LAST_TIME_SEEN;
            FIND_COVER_TO_GET_POSITION_WITH_SHOOT = fIND_COVER_TO_GET_POSITION_WITH_SHOOT;
            PROTECT_TIME_REAL = pROTECT_TIME_REAL;
            CHANCE_SHOOT_WHEN_WARN_PLAYER_100 = cHANCE_SHOOT_WHEN_WARN_PLAYER_100;
            CAN_PANIC_IS_PROTECT = cAN_PANIC_IS_PROTECT;
            NO_RUN_AWAY_FOR_SAFE = nO_RUN_AWAY_FOR_SAFE;
            PART_PERCENT_TO_HEAL = pART_PERCENT_TO_HEAL;
            PROTECT_DELTA_HEAL_SEC = pROTECT_DELTA_HEAL_SEC;
            CAN_STAND_BY = cAN_STAND_BY;
            CAN_THROW_REQUESTS = cAN_THROW_REQUESTS;
            GROUP_ANY_PHRASE_DELAY = gROUP_ANY_PHRASE_DELAY;
            GROUP_EXACTLY_PHRASE_DELAY = gROUP_EXACTLY_PHRASE_DELAY;
            DIST_TO_ENEMY_YO_CAN_HEAL = dIST_TO_ENEMY_YO_CAN_HEAL;
            CHANCE_TO_STAY_WHEN_WARN_PLAYER_100 = cHANCE_TO_STAY_WHEN_WARN_PLAYER_100;
            DOG_FIGHT_OUT = dOG_FIGHT_OUT;
            DOG_FIGHT_IN = dOG_FIGHT_IN;
            SHOOT_INSTEAD_DOG_FIGHT = sHOOT_INSTEAD_DOG_FIGHT;
            PISTOL_SHOTGUN_AMBUSH_DIST = pISTOL_SHOTGUN_AMBUSH_DIST;
            STANDART_AMBUSH_DIST = sTANDART_AMBUSH_DIST;
            AI_POWER_COEF = aI_POWER_COEF;
            COVER_SECONDS_AFTER_LOSE_VISION = cOVER_SECONDS_AFTER_LOSE_VISION;
            COVER_SELF_ALWAYS_IF_DAMAGED = cOVER_SELF_ALWAYS_IF_DAMAGED;
            SEC_TO_MORE_DIST_TO_RUN = sEC_TO_MORE_DIST_TO_RUN;
            HEAL_DELAY_SEC = hEAL_DELAY_SEC;
            HIT_DELAY_WHEN_HAVE_SMT = hIT_DELAY_WHEN_HAVE_SMT;
            HIT_DELAY_WHEN_PEACE = hIT_DELAY_WHEN_PEACE;
            TALK_WITH_QUERY = tALK_WITH_QUERY;
            WILL_PERSUE_AXEMAN = wILL_PERSUE_AXEMAN;
        }

        public int HOW_WORK_OVER_DEAD_BODY { get; set; }
        public int MIN_SHOOTS_TIME { get; set; }
        public int MAX_SHOOTS_TIME { get; set; }
        public int TIME_LEAVE_MAP { get; set; }
        public int TIME_TO_RUN_TO_COVER_CAUSE_SHOOT_SEC { get; set; }
        public int DAMAGE_REDUCTION_TIME_SEC { get; set; }
        public int MIN_DAMAGE_SCARE { get; set; }
        public int CHANCE_TO_RUN_CAUSE_DAMAGE_0_100 { get; set; }
        public int TIME_TO_FORGOR_ABOUT_ENEMY_SEC { get; set; }
        public int TIME_TO_FIND_ENEMY { get; set; }
        public int MAX_AGGRO_BOT_DIST { get; set; }
        public int HIT_POINT_DETECTION { get; set; }
        public int DANGER_POINT_CHOOSE_COEF { get; set; }
        public double SIMPLE_POINT_CHOOSE_COEF { get; set; }
        public double LASTSEEN_POINT_CHOOSE_COEF { get; set; }
        public double COVER_DIST_COEF { get; set; }
        public int DIST_TO_FOUND_SQRT { get; set; }
        public int MAX_AGGRO_BOT_DIST_SQR { get; set; }
        public int DIST_TO_STOP_RUN_ENEMY { get; set; }
        public int ENEMY_LOOK_AT_ME_ANG { get; set; }
        public int MIN_START_AGGRESION_COEF { get; set; }
        public int MAX_START_AGGRESION_COEF { get; set; }
        public int BULLET_FEEL_DIST { get; set; }
        public int BULLET_FEEL_CLOSE_SDIST { get; set; }
        public int ATTACK_IMMEDIATLY_CHANCE_0_100 { get; set; }
        public int CHANCE_FUCK_YOU_ON_CONTACT_100 { get; set; }
        public double FRIEND_DEAD_AGR_LOW { get; set; }
        public double FRIEND_AGR_KILL { get; set; }
        public int LAST_ENEMY_LOOK_TO { get; set; }
        public bool CAN_RECIVE_PLAYER_REQUESTS { get; set; }
        public bool CAN_TAKE_ITEMS { get; set; }
        public bool CAN_USE_MEDS { get; set; }
        public int SUSPETION_POINT_CHANCE_ADD100 { get; set; }
        public bool AMBUSH_WHEN_UNDER_FIRE { get; set; }
        public int AMBUSH_WHEN_UNDER_FIRE_TIME_RESIST { get; set; }
        public double ATTACK_ENEMY_IF_PROTECT_DELTA_LAST_TIME_SEEN { get; set; }
        public double HOLD_IF_PROTECT_DELTA_LAST_TIME_SEEN { get; set; }
        public int FIND_COVER_TO_GET_POSITION_WITH_SHOOT { get; set; }
        public bool PROTECT_TIME_REAL { get; set; }
        public int CHANCE_SHOOT_WHEN_WARN_PLAYER_100 { get; set; }
        public bool CAN_PANIC_IS_PROTECT { get; set; }
        public bool NO_RUN_AWAY_FOR_SAFE { get; set; }
        public double PART_PERCENT_TO_HEAL { get; set; }
        public int PROTECT_DELTA_HEAL_SEC { get; set; }
        public bool CAN_STAND_BY { get; set; }
        public bool CAN_THROW_REQUESTS { get; set; }
        public int GROUP_ANY_PHRASE_DELAY { get; set; }
        public int GROUP_EXACTLY_PHRASE_DELAY { get; set; }
        public int DIST_TO_ENEMY_YO_CAN_HEAL { get; set; }
        public int CHANCE_TO_STAY_WHEN_WARN_PLAYER_100 { get; set; }
        public int DOG_FIGHT_OUT { get; set; }
        public int DOG_FIGHT_IN { get; set; }
        public int SHOOT_INSTEAD_DOG_FIGHT { get; set; }
        public int PISTOL_SHOTGUN_AMBUSH_DIST { get; set; }
        public int STANDART_AMBUSH_DIST { get; set; }
        public int AI_POWER_COEF { get; set; }
        public int COVER_SECONDS_AFTER_LOSE_VISION { get; set; }
        public bool COVER_SELF_ALWAYS_IF_DAMAGED { get; set; }
        public int SEC_TO_MORE_DIST_TO_RUN { get; set; }
        public int HEAL_DELAY_SEC { get; set; }
        public int HIT_DELAY_WHEN_HAVE_SMT { get; set; }
        public int HIT_DELAY_WHEN_PEACE { get; set; }
        public bool TALK_WITH_QUERY { get; set; }
        public bool WILL_PERSUE_AXEMAN { get; set; }
    }

    public class Boss
    {
        public Boss(int bOSS_DIST_TO_WARNING, int bOSS_DIST_TO_WARNING_SQRT, int bOSS_DIST_TO_WARNING_OUT, int bOSS_DIST_TO_WARNING_OUT_SQRT,
            int bOSS_DIST_TO_SHOOT, int bOSS_DIST_TO_SHOOT_SQRT, int cHANCE_TO_SEND_GRENADE_100, int mAX_DIST_COVER_BOSS,
            int mAX_DIST_COVER_BOSS_SQRT, int mAX_DIST_DECIDER_TO_SEND, int mAX_DIST_DECIDER_TO_SEND_SQRT, int tIME_AFTER_LOSE,
            int tIME_AFTER_LOSE_DELTA, int pERSONS_SEND, int dELTA_SEARCH_TIME, bool cOVER_TO_SEND, int wAIT_NO_ATTACK_SAVAGE,
            int cHANCE_USE_RESERVE_PATROL_100, int kILLA_Y_DELTA_TO_BE_ENEMY_BOSS, int kILLA_DITANCE_TO_BE_ENEMY_BOSS, int kILLA_START_SEARCH_SEC,
            int kILLA_CONTUTION_TIME, int kILLA_CLOSE_ATTACK_DIST, int kILLA_MIDDLE_ATTACK_DIST, int kILLA_LARGE_ATTACK_DIST, int kILLA_SEARCH_METERS,
            int kILLA_DEF_DIST_SQRT, int kILLA_SEARCH_SEC_STOP_AFTER_COMING, int kILLA_DIST_TO_GO_TO_SUPPRESS, int kILLA_AFTER_GRENADE_SUPPRESS_DELAY,
            int kILLA_CLOSEATTACK_TIMES, int kILLA_CLOSEATTACK_DELAY, int kILLA_HOLD_DELAY, int kILLA_BULLET_TO_RELOAD, bool sHALL_WARN,
            int kILLA_ENEMIES_TO_ATTACK, int kILLA_ONE_IS_CLOSE, int kILLA_TRIGGER_DOWN_DELAY, int kILLA_WAIT_IN_COVER_COEF, int kOJANIY_DIST_WHEN_READY,
            int kOJANIY_DIST_TO_BE_ENEMY, int kOJANIY_MIN_DIST_TO_LOOT, int kOJANIY_MIN_DIST_TO_LOOT_SQRT, int kOJANIY_DIST_ENEMY_TOO_CLOSE, double kOJANIY_MANY_ENEMIES_COEF)
        {
            BOSS_DIST_TO_WARNING = bOSS_DIST_TO_WARNING;
            BOSS_DIST_TO_WARNING_SQRT = bOSS_DIST_TO_WARNING_SQRT;
            BOSS_DIST_TO_WARNING_OUT = bOSS_DIST_TO_WARNING_OUT;
            BOSS_DIST_TO_WARNING_OUT_SQRT = bOSS_DIST_TO_WARNING_OUT_SQRT;
            BOSS_DIST_TO_SHOOT = bOSS_DIST_TO_SHOOT;
            BOSS_DIST_TO_SHOOT_SQRT = bOSS_DIST_TO_SHOOT_SQRT;
            CHANCE_TO_SEND_GRENADE_100 = cHANCE_TO_SEND_GRENADE_100;
            MAX_DIST_COVER_BOSS = mAX_DIST_COVER_BOSS;
            MAX_DIST_COVER_BOSS_SQRT = mAX_DIST_COVER_BOSS_SQRT;
            MAX_DIST_DECIDER_TO_SEND = mAX_DIST_DECIDER_TO_SEND;
            MAX_DIST_DECIDER_TO_SEND_SQRT = mAX_DIST_DECIDER_TO_SEND_SQRT;
            TIME_AFTER_LOSE = tIME_AFTER_LOSE;
            TIME_AFTER_LOSE_DELTA = tIME_AFTER_LOSE_DELTA;
            PERSONS_SEND = pERSONS_SEND;
            DELTA_SEARCH_TIME = dELTA_SEARCH_TIME;
            COVER_TO_SEND = cOVER_TO_SEND;
            WAIT_NO_ATTACK_SAVAGE = wAIT_NO_ATTACK_SAVAGE;
            CHANCE_USE_RESERVE_PATROL_100 = cHANCE_USE_RESERVE_PATROL_100;
            KILLA_Y_DELTA_TO_BE_ENEMY_BOSS = kILLA_Y_DELTA_TO_BE_ENEMY_BOSS;
            KILLA_DITANCE_TO_BE_ENEMY_BOSS = kILLA_DITANCE_TO_BE_ENEMY_BOSS;
            KILLA_START_SEARCH_SEC = kILLA_START_SEARCH_SEC;
            KILLA_CONTUTION_TIME = kILLA_CONTUTION_TIME;
            KILLA_CLOSE_ATTACK_DIST = kILLA_CLOSE_ATTACK_DIST;
            KILLA_MIDDLE_ATTACK_DIST = kILLA_MIDDLE_ATTACK_DIST;
            KILLA_LARGE_ATTACK_DIST = kILLA_LARGE_ATTACK_DIST;
            KILLA_SEARCH_METERS = kILLA_SEARCH_METERS;
            KILLA_DEF_DIST_SQRT = kILLA_DEF_DIST_SQRT;
            KILLA_SEARCH_SEC_STOP_AFTER_COMING = kILLA_SEARCH_SEC_STOP_AFTER_COMING;
            KILLA_DIST_TO_GO_TO_SUPPRESS = kILLA_DIST_TO_GO_TO_SUPPRESS;
            KILLA_AFTER_GRENADE_SUPPRESS_DELAY = kILLA_AFTER_GRENADE_SUPPRESS_DELAY;
            KILLA_CLOSEATTACK_TIMES = kILLA_CLOSEATTACK_TIMES;
            KILLA_CLOSEATTACK_DELAY = kILLA_CLOSEATTACK_DELAY;
            KILLA_HOLD_DELAY = kILLA_HOLD_DELAY;
            KILLA_BULLET_TO_RELOAD = kILLA_BULLET_TO_RELOAD;
            SHALL_WARN = sHALL_WARN;
            KILLA_ENEMIES_TO_ATTACK = kILLA_ENEMIES_TO_ATTACK;
            KILLA_ONE_IS_CLOSE = kILLA_ONE_IS_CLOSE;
            KILLA_TRIGGER_DOWN_DELAY = kILLA_TRIGGER_DOWN_DELAY;
            KILLA_WAIT_IN_COVER_COEF = kILLA_WAIT_IN_COVER_COEF;
            KOJANIY_DIST_WHEN_READY = kOJANIY_DIST_WHEN_READY;
            KOJANIY_DIST_TO_BE_ENEMY = kOJANIY_DIST_TO_BE_ENEMY;
            KOJANIY_MIN_DIST_TO_LOOT = kOJANIY_MIN_DIST_TO_LOOT;
            KOJANIY_MIN_DIST_TO_LOOT_SQRT = kOJANIY_MIN_DIST_TO_LOOT_SQRT;
            KOJANIY_DIST_ENEMY_TOO_CLOSE = kOJANIY_DIST_ENEMY_TOO_CLOSE;
            KOJANIY_MANY_ENEMIES_COEF = kOJANIY_MANY_ENEMIES_COEF;
        }

        public int BOSS_DIST_TO_WARNING { get; set; }
        public int BOSS_DIST_TO_WARNING_SQRT { get; set; }
        public int BOSS_DIST_TO_WARNING_OUT { get; set; }
        public int BOSS_DIST_TO_WARNING_OUT_SQRT { get; set; }
        public int BOSS_DIST_TO_SHOOT { get; set; }
        public int BOSS_DIST_TO_SHOOT_SQRT { get; set; }
        public int CHANCE_TO_SEND_GRENADE_100 { get; set; }
        public int MAX_DIST_COVER_BOSS { get; set; }
        public int MAX_DIST_COVER_BOSS_SQRT { get; set; }
        public int MAX_DIST_DECIDER_TO_SEND { get; set; }
        public int MAX_DIST_DECIDER_TO_SEND_SQRT { get; set; }
        public int TIME_AFTER_LOSE { get; set; }
        public int TIME_AFTER_LOSE_DELTA { get; set; }
        public int PERSONS_SEND { get; set; }
        public int DELTA_SEARCH_TIME { get; set; }
        public bool COVER_TO_SEND { get; set; }
        public int WAIT_NO_ATTACK_SAVAGE { get; set; }
        public int CHANCE_USE_RESERVE_PATROL_100 { get; set; }
        public int KILLA_Y_DELTA_TO_BE_ENEMY_BOSS { get; set; }
        public int KILLA_DITANCE_TO_BE_ENEMY_BOSS { get; set; }
        public int KILLA_START_SEARCH_SEC { get; set; }
        public int KILLA_CONTUTION_TIME { get; set; }
        public int KILLA_CLOSE_ATTACK_DIST { get; set; }
        public int KILLA_MIDDLE_ATTACK_DIST { get; set; }
        public int KILLA_LARGE_ATTACK_DIST { get; set; }
        public int KILLA_SEARCH_METERS { get; set; }
        public int KILLA_DEF_DIST_SQRT { get; set; }
        public int KILLA_SEARCH_SEC_STOP_AFTER_COMING { get; set; }
        public int KILLA_DIST_TO_GO_TO_SUPPRESS { get; set; }
        public int KILLA_AFTER_GRENADE_SUPPRESS_DELAY { get; set; }
        public int KILLA_CLOSEATTACK_TIMES { get; set; }
        public int KILLA_CLOSEATTACK_DELAY { get; set; }
        public int KILLA_HOLD_DELAY { get; set; }
        public int KILLA_BULLET_TO_RELOAD { get; set; }
        public bool SHALL_WARN { get; set; }
        public int KILLA_ENEMIES_TO_ATTACK { get; set; }
        public int KILLA_ONE_IS_CLOSE { get; set; }
        public int KILLA_TRIGGER_DOWN_DELAY { get; set; }
        public int KILLA_WAIT_IN_COVER_COEF { get; set; }
        public int KOJANIY_DIST_WHEN_READY { get; set; }
        public int KOJANIY_DIST_TO_BE_ENEMY { get; set; }
        public int KOJANIY_MIN_DIST_TO_LOOT { get; set; }
        public int KOJANIY_MIN_DIST_TO_LOOT_SQRT { get; set; }
        public int KOJANIY_DIST_ENEMY_TOO_CLOSE { get; set; }
        public double KOJANIY_MANY_ENEMIES_COEF { get; set; }
    }

    public class Core
    {
        public Core(int visibleAngle, int visibleDistance, double gainSightCoef, double scatteringPerMeter, double scatteringClosePerMeter, int damageCoeff, double hearingSense, bool canRun, bool canGrenade, string aimingType, int pistolFireDistancePref, int shotgunFireDistancePref, int rifleFireDistancePref, double accuratySpeed, double waitInCoverBetweenShotsSec)
        {
            VisibleAngle = visibleAngle;
            VisibleDistance = visibleDistance;
            GainSightCoef = gainSightCoef;
            ScatteringPerMeter = scatteringPerMeter;
            ScatteringClosePerMeter = scatteringClosePerMeter;
            DamageCoeff = damageCoeff;
            HearingSense = hearingSense;
            CanRun = canRun;
            CanGrenade = canGrenade;
            AimingType = aimingType;
            PistolFireDistancePref = pistolFireDistancePref;
            ShotgunFireDistancePref = shotgunFireDistancePref;
            RifleFireDistancePref = rifleFireDistancePref;
            AccuratySpeed = accuratySpeed;
            WaitInCoverBetweenShotsSec = waitInCoverBetweenShotsSec;
        }

        public int VisibleAngle { get; set; }
        public int VisibleDistance { get; set; }
        public double GainSightCoef { get; set; }
        public double ScatteringPerMeter { get; set; }
        public double ScatteringClosePerMeter { get; set; }
        public int DamageCoeff { get; set; }
        public double HearingSense { get; set; }
        public bool CanRun { get; set; }
        public bool CanGrenade { get; set; }
        public string AimingType { get; set; }
        public int PistolFireDistancePref { get; set; }
        public int ShotgunFireDistancePref { get; set; }
        public int RifleFireDistancePref { get; set; }
        public double AccuratySpeed { get; set; }
        public double WaitInCoverBetweenShotsSec { get; set; }
    }

    public class Scattering
    {
        public Scattering(double minScatter, double workingScatter, double maxScatter, double speedUp, double speedUpAim, double speedDown, double toSlowBotSpeed, double toLowBotSpeed, double toUpBotSpeed, double movingSlowCoef, int toLowBotAngularSpeed, int toStopBotAngularSpeed, double fromShot, double tracerCoef, double handDamageScatteringMinMax, double handDamageAccuracySpeed, double bloodFall, double caution, double toCaution, double recoilControlCoefShootDone, double recoilControlCoefShootDoneAuto, double aMPLITUDE_FACTOR, double aMPLITUDE_SPEED, int dIST_FROM_OLD_POINT_TO_NOT_AIM, int dIST_FROM_OLD_POINT_TO_NOT_AIM_SQRT, double dIST_NOT_TO_SHOOT, double poseChnageCoef, double layFactor, double recoilYCoef, double recoilYCoefSppedDown, int recoilYMax)
        {
            MinScatter = minScatter;
            WorkingScatter = workingScatter;
            MaxScatter = maxScatter;
            SpeedUp = speedUp;
            SpeedUpAim = speedUpAim;
            SpeedDown = speedDown;
            ToSlowBotSpeed = toSlowBotSpeed;
            ToLowBotSpeed = toLowBotSpeed;
            ToUpBotSpeed = toUpBotSpeed;
            MovingSlowCoef = movingSlowCoef;
            ToLowBotAngularSpeed = toLowBotAngularSpeed;
            ToStopBotAngularSpeed = toStopBotAngularSpeed;
            FromShot = fromShot;
            TracerCoef = tracerCoef;
            HandDamageScatteringMinMax = handDamageScatteringMinMax;
            HandDamageAccuracySpeed = handDamageAccuracySpeed;
            BloodFall = bloodFall;
            Caution = caution;
            ToCaution = toCaution;
            RecoilControlCoefShootDone = recoilControlCoefShootDone;
            RecoilControlCoefShootDoneAuto = recoilControlCoefShootDoneAuto;
            AMPLITUDE_FACTOR = aMPLITUDE_FACTOR;
            AMPLITUDE_SPEED = aMPLITUDE_SPEED;
            DIST_FROM_OLD_POINT_TO_NOT_AIM = dIST_FROM_OLD_POINT_TO_NOT_AIM;
            DIST_FROM_OLD_POINT_TO_NOT_AIM_SQRT = dIST_FROM_OLD_POINT_TO_NOT_AIM_SQRT;
            DIST_NOT_TO_SHOOT = dIST_NOT_TO_SHOOT;
            PoseChnageCoef = poseChnageCoef;
            LayFactor = layFactor;
            RecoilYCoef = recoilYCoef;
            RecoilYCoefSppedDown = recoilYCoefSppedDown;
            RecoilYMax = recoilYMax;
        }

        public double MinScatter { get; set; }
        public double WorkingScatter { get; set; }
        public double MaxScatter { get; set; }
        public double SpeedUp { get; set; }
        public double SpeedUpAim { get; set; }
        public double SpeedDown { get; set; }
        public double ToSlowBotSpeed { get; set; }
        public double ToLowBotSpeed { get; set; }
        public double ToUpBotSpeed { get; set; }
        public double MovingSlowCoef { get; set; }
        public int ToLowBotAngularSpeed { get; set; }
        public int ToStopBotAngularSpeed { get; set; }
        public double FromShot { get; set; }
        public double TracerCoef { get; set; }
        public double HandDamageScatteringMinMax { get; set; }
        public double HandDamageAccuracySpeed { get; set; }
        public double BloodFall { get; set; }
        public double Caution { get; set; }
        public double ToCaution { get; set; }
        public double RecoilControlCoefShootDone { get; set; }
        public double RecoilControlCoefShootDoneAuto { get; set; }
        public double AMPLITUDE_FACTOR { get; set; }
        public double AMPLITUDE_SPEED { get; set; }
        public int DIST_FROM_OLD_POINT_TO_NOT_AIM { get; set; }
        public int DIST_FROM_OLD_POINT_TO_NOT_AIM_SQRT { get; set; }
        public double DIST_NOT_TO_SHOOT { get; set; }
        public double PoseChnageCoef { get; set; }
        public double LayFactor { get; set; }
        public double RecoilYCoef { get; set; }
        public double RecoilYCoefSppedDown { get; set; }
        public int RecoilYMax { get; set; }
    }

}
