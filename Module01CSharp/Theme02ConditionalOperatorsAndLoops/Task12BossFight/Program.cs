using System;

namespace Task12BossFight
{
    internal class Program
    {
        static void Main()
        {
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

            const int rashamonCommand = 1;
                 bool isActivateRamashonSpell = false;
                  int rashamonManaMagChangeNumber = -35;
                  int rashamonHealthBossChangeNumber = -50;

            const int huganzakuraCommand = 2;
                  int huganzakuraManaMagChangeNumber = -10;
                  int huganzakuraHealthBossChangeNumber = -100;

            const int dimensionalCommand = 3;
                 bool isCorrectHelthMagNumber = false;
                  int requiredNumberHealthForSpell = 100;
                  int dimensionalManaMagChangeNumber = -50;
                  int dimensionalHealthMagChangeNumber = 250;

            const int songElementsCommand = 4;
                 bool isCanUseSongElements = false;
                  int cooldownSongElementsNumberMoves = 3;
                  int cooldownSongElementsNumberMovesCounter = 0;
                  int songElementsManaMagChangeNumber = 250;
                  int songElementsHealthMagChangeNumber = -75;
                  int songElementsHealthBossChangeNumber = -25;

            int useSpell;
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
                  "\n   Доступные заклинания:" +
                  "\n   [1] Рашамон" +
                  "\n       Отнимает у заклинателя 35 маны | Наносит 50 урона боссу" +
                  "\n" +
                  "\n   [2] Хуганзакура" +
                  "\n       Отнимает у заклинателя 10 маны | Наносит 100 урона боссу" +
                  "\n       Заклинание Хуганзакура доступно только после использования Рашамон" +
                  "\n" +
                  "\n   [3] Межпространственный разлом" +
                  "\n       Отнимает у заклинателя 50 маны | Восстанавливает 250 здоровья заклинателю" +
                  "\n       Заклинание Межпространственный разлом доступно только если у вас менее 100 здоровья" +
                  "\n" +
                  "\n   [4] Песнь стихий" +
                  "\n       Позволяет собрать ману из пространства рядом. + 250 маны" +
                  "\n       Отнимает 25 здоровья у босса и 75 здоровья у заклинателя | Восстанавливает 250 маны заклинателю" +
                  "\n" +
                  "\n   Введите номер заклинания для атаки: "
                 );
                try { 
                    useSpell = Convert.ToInt32(Console.ReadLine());
                    
                    Console.Clear();
                    
                    switch (useSpell)
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
                    if (!isCanUseSongElements) cooldownSongElementsNumberMovesCounter++;
                    
                    isCorrectHelthMagNumber = healthMag < requiredNumberHealthForSpell;

                    damageBoss = random.Next(damageBossMinimumValue, damageBossMaximumValue);
                    healthMag -= damageBoss;
                }
                catch 
                { 
                    Console.Clear();
                    tempEvent = "Такое заклинание ещё не изучено. Пожалуйста, выбирите что-то, чем уже владеете\n"; 
                }
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