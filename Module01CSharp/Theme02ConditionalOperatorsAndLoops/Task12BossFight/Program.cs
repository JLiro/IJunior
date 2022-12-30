using System;

namespace Task12BossFight
{
    internal class Program
    {
        static void Main()
        {
            const int RashamonCommand = 1;
            const int HuganzakuraCommand = 2;
            const int DimensionalCommand = 3;
            const int SongElementsCommand = 4;

            int healthBoss;
            int healthBossMinimumValue = 250;
            int healthBossMaximumValue = 500;
            int damageBoss;
            int damageBossMinimumValue = 25;
            int damageBossMaximumValue = 50;

            int healthMag;
            int healthMagMinimumValue = 100;
            int healthMagMaximumValue = 250;
            int manaMag;
            int manaMagMinimumValue = 100;
            int manaMagMaximumValue = 500;

            bool isActivateRamashonSpell = false;
            int rashamonManaMagValue = -35;
            int rashamonHealthBossValue = -50;

            int huganzakuraManaMagValue = -10;
            int huganzakuraHealthBossValue = -100;

            bool isCorrectHelthMagValue = false;
            int requiredValueHealthForSpell = 100;
            int dimensionalManaMagValue = -50;
            int dimensionalHealthMagValue = 250;

            bool isCanUseSongElements = false;
            int cooldownSongElementsMaxValue = 3;
            int cooldownSongElementsCount = 0;
            int songElementsManaMagValue = 250;
            int songElementsHealthMagValue = -75;
            int songElementsHealthBossValue = -25;

            Random random = new Random();
            healthBoss = random.Next(healthBossMinimumValue, healthBossMaximumValue);

            healthMag = random.Next(healthMagMinimumValue, healthMagMaximumValue);
            manaMag = random.Next(manaMagMinimumValue, manaMagMaximumValue);

            int usedSpells;
            string tempEvent = string.Empty;
            string winner;

            while (healthBoss > 0 && healthMag > 0)
            {
                Console.Write
                (
                    "=====0 Последнее событие:" +
                 $"\n     > {tempEvent}" +
                 $"\n" +
                 $"\n  БОСС [ЗДОРОВЬЕ: {healthBoss}]" +
                 $"\n" +
                 $"\n   МАГ [ЗДОРОВЬЕ: {healthMag}]" +
                 $"\n           [МАНА: {manaMag}]" +
                 $"\n" +
                 $"\n   Доступные заклинания:" +
                 $"\n   [1] Рашамон" +
                 $"\n       Отнимает у заклинателя {rashamonManaMagValue} маны | Наносит {rashamonHealthBossValue} урона боссу" +
                 $"\n" +
                 $"\n   [2] Хуганзакура" +
                 $"\n       Отнимает у заклинателя {huganzakuraManaMagValue} маны | Наносит {huganzakuraHealthBossValue} урона боссу" +
                 $"\n       Заклинание Хуганзакура доступно только после использования Рашамон" +
                 $"\n" +
                 $"\n   [3] Межпространственный разлом" +
                 $"\n       Отнимает у заклинателя {dimensionalManaMagValue} маны | Восстанавливает {dimensionalHealthMagValue} здоровья заклинателю" +
                 $"\n       Заклинание Межпространственный разлом доступно только если у вас менее {requiredValueHealthForSpell} здоровья" +
                 $"\n" +
                 $"\n   [4] Песнь стихий" +
                 $"\n       Отнимает {songElementsHealthBossValue} здоровья у босса и {songElementsHealthMagValue} здоровья у заклинателя | Восстанавливает {songElementsManaMagValue} маны заклинателю" +
                 $"\n       Можно использовать один раз в {cooldownSongElementsMaxValue} хода" +
                 $"\n" +
                 $"\n   Введите номер заклинания для атаки: "
                 );
                usedSpells = Convert.ToInt32(Console.ReadLine());

                switch (usedSpells)
                {
                    case RashamonCommand:
                        tempEvent = "Вы использовали заклинание Рашамон!";

                        manaMag += rashamonManaMagValue;
                        healthBoss += rashamonHealthBossValue;

                        isActivateRamashonSpell = true;
                        break;

                    case HuganzakuraCommand:
                        if (isActivateRamashonSpell)
                        {
                            tempEvent = "Вы использовали заклинание Хуганзакура!";

                            manaMag += huganzakuraManaMagValue;
                            healthBoss += huganzakuraHealthBossValue;

                            isActivateRamashonSpell = false;
                        }
                        else
                        {
                            tempEvent = "Заклинание Хуганзакура доступно только после использования Рашамон" +
                               "\n       Вы замешкались и пропустили удар!";
                        }
                        break;

                    case DimensionalCommand:
                        if (isCorrectHelthMagValue)
                        {
                            tempEvent = "Вы использовали заклинание Межпространственный разлом!";

                            manaMag += dimensionalManaMagValue;
                            healthMag += dimensionalHealthMagValue;
                        }
                        else
                        {
                            tempEvent = "Заклинание Межпространственный разлом доступно только если у вас менее 100 здоровья" +
                               "\n       Вы замешкались и пропустили удар!";
                        }
                        break;

                    case SongElementsCommand:
                        isCanUseSongElements = cooldownSongElementsCount == cooldownSongElementsMaxValue;

                        if (isCanUseSongElements)
                        {
                            tempEvent = "Вы использовали заклинание Песнь стихий!";

                            manaMag += songElementsManaMagValue;
                            healthMag += songElementsHealthMagValue;
                            healthBoss += songElementsHealthBossValue;

                            cooldownSongElementsCount -= cooldownSongElementsMaxValue;
                        }
                        else
                        {
                            tempEvent = "Заклинание Песнь стихий доступно раз в 3 хода.\n" +
                               "\n       Вы замешкались и пропустили удар!";
                        }
                        break;

                    default:
                        tempEvent = "Такое заклинание ещё не изучено. Пожалуйста, выбирите что-то, чем уже владеете" +
                           "\n       Вы замешкались и пропустили удар!";
                        break;
                }

                damageBoss = random.Next(damageBossMinimumValue, damageBossMaximumValue);
                healthMag -= damageBoss;
                
                isCanUseSongElements = cooldownSongElementsMaxValue == cooldownSongElementsCount;
                
                if (isCanUseSongElements == false)
                {
                    cooldownSongElementsCount++;
                }

                isCorrectHelthMagValue = healthMag < requiredValueHealthForSpell;

                Console.Clear();
            }


            if (healthBoss <= 0 && healthMag <= 0)
            {
                winner = "Ничья";
            }
            else if (healthBoss <= 0)
            {
                winner = "Маг";
            }
            else
            {
                winner = "Босс";
            }

            winner = healthBoss > healthMag ? "Босс" : "Маг";

            Console.Write
            (
                "=====0 Последнее событие:" +
             $"\n     > Победил {winner}!"
            );
            Console.ReadKey();
        }
    }
}