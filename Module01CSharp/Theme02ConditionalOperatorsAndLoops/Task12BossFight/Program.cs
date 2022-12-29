using System;

namespace Task12BossFight
{
    internal class Program
    {
        static void Main()
        {
            int healthBoss, damageBoss;
            int healthMag , manaMag;

            int useSpell;
            bool activateRamashonSpell = false;
            int cooldownSongElements = 3;

            string tempEvent = string.Empty;
            string winner;

            Random random = new Random();

            healthBoss = random.Next(250, 500);

            healthMag = random.Next(100, 250);
            manaMag   = random.Next(100, 500);

            while (healthBoss > 0 && healthMag > 0)
            {
                damageBoss = random.Next(25, 50);

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
                        case 1:
                            tempEvent = "Вы использовали заклинание Рашамон!";

                            manaMag -= 35;
                            healthBoss -= 50;

                            activateRamashonSpell = true;
                            break;

                        case 2:
                            if (activateRamashonSpell)
                            {
                                tempEvent = "Вы использовали заклинание Хуганзакура!";

                                manaMag -= 10;
                                healthBoss -= 100;

                                activateRamashonSpell = false;
                            }
                            else
                            {
                                tempEvent = "Заклинание Хуганзакура доступно только после использования Рашамон" +
                                   "\n       Вы замешкались и пропустили удар!";
                            }
                            break;

                        case 3:
                            if (healthMag < 100)
                            {
                                tempEvent = "Вы использовали заклинание Межпространственный разлом!";

                                manaMag -= 50;
                                healthMag += 250;
                            }
                            else
                            {
                                tempEvent = "Заклинание Межпространственный разлом доступно только если у вас менее 100 здоровья" +
                                   "\n       Вы замешкались и пропустили удар!";
                            }
                            break;

                        case 4:
                            if (cooldownSongElements > 3)
                            {
                                tempEvent = "Вы использовали заклинание Песнь стихий!";

                                manaMag += 250;
                                healthMag -= 75;
                                healthBoss -= 25;

                                cooldownSongElements = 0;
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

                    if (useSpell != 3) cooldownSongElements++;
                       
                    healthMag -= damageBoss;
                }
                catch 
                { 
                    Console.Clear();
                    tempEvent = "Такое заклинание ещё не изучено. Пожалуйста, выбирите что-то, чем уже владеете\n"; 
                }
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