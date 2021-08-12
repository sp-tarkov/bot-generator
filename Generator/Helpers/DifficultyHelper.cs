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
                Lay = DifficultHelper.GenerateLay(),
                Aiming = DifficultHelper.GenerateAiming(),
                Look = DifficultHelper.GenerateLook(),
                Shoot = DifficultHelper.GenerateShoot(),
                Move = DifficultHelper.GenerateMove(),
                Grenade = DifficultHelper.GenerateGrenade(),
                Change = DifficultHelper.GenerateChange(),
                Cover = DifficultHelper.GenerateCover(),
                Patrol = DifficultHelper.GeneratePatrol(),
                Hearing = DifficultHelper.GenerateHearing(),
                Mind = DifficultHelper.GenerateMind(),
                Boss = DifficultHelper.GenerateBoss(),
                Core = DifficultHelper.GenerateCore(),
                Scattering = DifficultHelper.GenerateScattering()
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
