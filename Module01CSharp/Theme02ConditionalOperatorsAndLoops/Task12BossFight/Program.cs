using System;

namespace Task12BossFight
{
    internal class Program
    {
        static void Main()
        {
            const int rashamonCommand = 1;
            const int huganzakuraCommand = 2;
            const int dimensionalCommand = 3;
            const int songElementsCommand = 4;

            int healthBoss;
            int healthBossMinimumValue = 250;
            int healthBossMaximumValue = 500;
            int healthBossValueFromFinishGame = 0;
            int damageBoss;
            int damageBossMinimumValue = 25;
            int damageBossMaximumValue = 50;

            int healthMag;
            int healthMagMinimumValue = 100;
            int healthMagMaximumValue = 250;
            int healthMagValueFromFinishGame = 0;
            int manaMag;
            int manaMagMinimumValue = 100;
            int manaMagMaximumValue = 500;

            bool isActivateRamashonSpell = false;
            int rashamonManaMagChangeNumber = -35;
            int rashamonHealthBossChangeNumber = -50;

            int huganzakuraManaMagChangeNumber = -10;
            int huganzakuraHealthBossChangeNumber = -100;

            bool isCorrectHelthMagNumber = false;
            int requiredNumberHealthForSpell = 100;
            int dimensionalManaMagChangeNumber = -50;
            int dimensionalHealthMagChangeNumber = 250;

            bool isCanUseSongElements = false;
            int cooldownSongElementsNumberMoves = 3;
            int cooldownSongElementsNumberMovesCounter = 0;
            int songElementsManaMagChangeNumber = 250;
            int songElementsHealthMagChangeNumber = -75;
            int songElementsHealthBossChangeNumber = -25;

            int usedSpells;
            string tempEvent = string.Empty;
            string winner;

            Random random = new Random();
            healthBoss = random.Next(healthBossMinimumValue, healthBossMaximumValue);

            healthMag = random.Next(healthMagMinimumValue, healthMagMaximumValue);
            manaMag = random.Next(manaMagMinimumValue, manaMagMaximumValue);

            bool isGameOn = true;
            while (isGameOn)
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
                 $"\n       Отнимает у заклинателя {rashamonManaMagChangeNumber} маны | Наносит {rashamonHealthBossChangeNumber} урона боссу" +
                 $"\n" +
                 $"\n   [2] Хуганзакура" +
                 $"\n       Отнимает у заклинателя {huganzakuraManaMagChangeNumber} маны | Наносит {huganzakuraHealthBossChangeNumber} урона боссу" +
                 $"\n       Заклинание Хуганзакура доступно только после использования Рашамон" +
                 $"\n" +
                 $"\n   [3] Межпространственный разлом" +
                 $"\n       Отнимает у заклинателя {dimensionalManaMagChangeNumber} маны | Восстанавливает {dimensionalHealthMagChangeNumber} здоровья заклинателю" +
                 $"\n       Заклинание Межпространственный разлом доступно только если у вас менее {requiredNumberHealthForSpell} здоровья" +
                 $"\n" +
                 $"\n   [4] Песнь стихий" +
                 $"\n       Отнимает {songElementsHealthBossChangeNumber} здоровья у босса и {songElementsHealthMagChangeNumber} здоровья у заклинателя | Восстанавливает {songElementsManaMagChangeNumber} маны заклинателю" +
                 $"\n       Можно использовать один раз в {cooldownSongElementsNumberMoves} хода" +
                 $"\n" +
                 $"\n   Введите номер заклинания для атаки: "
                 );
                usedSpells = Convert.ToInt32(Console.ReadLine());

                Console.Clear();

                switch (usedSpells)
                {
                    case rashamonCommand:
                        tempEvent = "Вы использовали заклинание Рашамон!";

                        manaMag += rashamonManaMagChangeNumber;
                        healthBoss += rashamonHealthBossChangeNumber;

                        isActivateRamashonSpell = true;
                        break;

                    case huganzakuraCommand:
                        if (isActivateRamashonSpell)
                        {
                            tempEvent = "Вы использовали заклинание Хуганзакура!";

                            manaMag += huganzakuraManaMagChangeNumber;
                            healthBoss += huganzakuraHealthBossChangeNumber;

                            isActivateRamashonSpell = false;
                        }
                        else
                        {
                            tempEvent = "Заклинание Хуганзакура доступно только после использования Рашамон" +
                               "\n       Вы замешкались и пропустили удар!";
                        }
                        break;

                    case dimensionalCommand:
                        if (isCorrectHelthMagNumber)
                        {
                            tempEvent = "Вы использовали заклинание Межпространственный разлом!";

                            manaMag += dimensionalManaMagChangeNumber;
                            healthMag += dimensionalHealthMagChangeNumber;
                        }
                        else
                        {
                            tempEvent = "Заклинание Межпространственный разлом доступно только если у вас менее 100 здоровья" +
                               "\n       Вы замешкались и пропустили удар!";
                        }
                        break;

                    case songElementsCommand:
                        isCanUseSongElements = cooldownSongElementsNumberMovesCounter == cooldownSongElementsNumberMoves;
                        if (isCanUseSongElements)
                        {
                            tempEvent = "Вы использовали заклинание Песнь стихий!";

                            manaMag += songElementsManaMagChangeNumber;
                            healthMag += songElementsHealthMagChangeNumber;
                            healthBoss += songElementsHealthBossChangeNumber;

                            cooldownSongElementsNumberMovesCounter -= cooldownSongElementsNumberMoves;
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

                isCanUseSongElements = cooldownSongElementsNumberMoves == cooldownSongElementsNumberMovesCounter;
                
                if (!isCanUseSongElements)
                {
                    cooldownSongElementsNumberMovesCounter++;
                }

                isCorrectHelthMagNumber = healthMag < requiredNumberHealthForSpell;

                damageBoss = random.Next(damageBossMinimumValue, damageBossMaximumValue);
                healthMag -= damageBoss;

                isGameOn = healthBoss > healthBossValueFromFinishGame && healthMag > healthMagValueFromFinishGame;
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