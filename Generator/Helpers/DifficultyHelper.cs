using Generator.Models.Output;

namespace Generator.Helpers
{
    public static class DifficultyHelper
    {
        public static void AddAssaultDifficulties(Bot bot)
        {
            bot.difficulty.easy = new Models.Output.Difficulty.DifficultySettings
            {
                Lay = EasyHelper.GenerateLay(),
                Aiming = EasyHelper.GenerateAiming(),
                Look = EasyHelper.GenerateLook(),
                Shoot = EasyHelper.GenerateShoot(),
                Move = EasyHelper.GenerateMove(),
                Grenade = EasyHelper.GenerateGrenade(),
                Change = EasyHelper.GenerateChange(),
                Cover = EasyHelper.GenerateCover(),
                Patrol = EasyHelper.GeneratePatrol(),
                Hearing = EasyHelper.GenerateHearing(),
                Mind = EasyHelper.GenerateMind(),
                Boss = EasyHelper.GenerateBoss(),
                Core = EasyHelper.GenerateCore(),
                Scattering = EasyHelper.GenerateScattering()
            };

            bot.difficulty.normal = new Models.Output.Difficulty.DifficultySettings(){
                Lay = NormalHelper.GenerateLay(),
                Aiming = NormalHelper.GenerateAiming(),
                Look = NormalHelper.GenerateLook(),
                Shoot = NormalHelper.GenerateShoot(),
                Move = NormalHelper.GenerateMove(),
                Grenade = NormalHelper.GenerateGrenade(),
                Change = NormalHelper.GenerateChange(),
                Cover = NormalHelper.GenerateCover(),
                Patrol = NormalHelper.GeneratePatrol(),
                Hearing = NormalHelper.GenerateHearing(),
                Mind = NormalHelper.GenerateMind(),
                Boss = NormalHelper.GenerateBoss(),
                Core = NormalHelper.GenerateCore(),
                Scattering = NormalHelper.GenerateScattering()
            };

            bot.difficulty.hard = new Models.Output.Difficulty.DifficultySettings()
            {
                Lay = HardHelper.GenerateLay(),
                Aiming = HardHelper.GenerateAiming(),
                Look = HardHelper.GenerateLook(),
                Shoot = HardHelper.GenerateShoot(),
                Move = HardHelper.GenerateMove(),
                Grenade = HardHelper.GenerateGrenade(),
                Change = HardHelper.GenerateChange(),
                Cover = HardHelper.GenerateCover(),
                Patrol = HardHelper.GeneratePatrol(),
                Hearing = HardHelper.GenerateHearing(),
                Mind = HardHelper.GenerateMind(),
                Boss = HardHelper.GenerateBoss(),
                Core = HardHelper.GenerateCore(),
                Scattering = HardHelper.GenerateScattering()
            };
            bot.difficulty.impossible = new Models.Output.Difficulty.DifficultySettings()
            {
                Lay = ImpossibleHelper.GenerateLay(),
                Aiming = ImpossibleHelper.GenerateAiming(),
                Look = ImpossibleHelper.GenerateLook(),
                Shoot = ImpossibleHelper.GenerateShoot(),
                Move = ImpossibleHelper.GenerateMove(),
                Grenade = ImpossibleHelper.GenerateGrenade(),
                Change = ImpossibleHelper.GenerateChange(),
                Cover = ImpossibleHelper.GenerateCover(),
                Patrol = ImpossibleHelper.GeneratePatrol(),
                Hearing = ImpossibleHelper.GenerateHearing(),
                Mind = ImpossibleHelper.GenerateMind(),
                Boss = ImpossibleHelper.GenerateBoss(),
                Core = ImpossibleHelper.GenerateCore(),
                Scattering = ImpossibleHelper.GenerateScattering()
            };
        }

    }
}
