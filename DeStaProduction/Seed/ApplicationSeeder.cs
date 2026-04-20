using DeStaProduction.Infrastucture.Entities;

namespace DeStaProduction.Seed
{
    public class ApplicationSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (!context.EventTypes.Any())
            {
                context.EventTypes.AddRange(
                    new EventType { Name = "Комедия" },
                    new EventType { Name = "Концерт" },
                    new EventType { Name = "Драма" },
                    new EventType { Name = "Stand-up" }
                );

                try
                {
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException?.Message);
                    throw;
                }
            }

            if (!context.Locations.Any())
            {
                context.Locations.AddRange(
                    new Location { Name = "Театър ,,Сълза и смях'' София", Address = "София 1000, ул. Г. С. Раковски 127", Capacity=425, City="София" },
                    new Location { Name = "Дом на Културата ,,АРСЕНАЛ'' Казанлък", Address = "ул. ,,Старата река'' 2, 6100", Capacity = 500, City = "Казанлак" },
                    new Location { Name = "Драматичен театър ,,Никола Вапцаров'' Благоевград", Address = "пл. Георги Измирлиев 1", Capacity = 360, City = "Благоевград" }
                );

                try
                {
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException?.Message);
                    throw;
                }
            }

            if (!context.Events.Any())
            {
                var comedy = context.EventTypes.First(x => x.Name == "Комедия");

                context.Events.AddRange(
                    new Event
                    {
                        Description= "С най-голямата награда за авторски текст, на конкурса \"Иван Радоев\".\r\n\r\nПиеса на Оля Стоянова\r\n\r\nТри пълни обиколки около света Комедия. Или трагедия. Или революция. Но със сигурност - с много смях, ирония и усещане, че това вече го гледахме по новините. Самият диалог е до някъде абсурден, но адски забавен.\r\n\r\nС много хумор, сарказъм и нелека доза истина, пиесата хваща за гушата целия наш обществен климат - къде политически, къде екологичен, къде емоционален. Това не е пиеса за героите, а за гнева. За скуката, която ражда революции. За бунта, който започва с трошене на витрина, и свършва с броене до осем. Без да мръднеш от мястото си.",
                        Duration=70,
                        EventType = comedy.Id,
                        Title="Три пълни обиколки около света",
                        ImagePath = "/images/obikolki.jpg"
                    },
                    new Event
                    {
                        Description = "След \"Жената е странно животно\", Яна Огнянова ще влезе в ролята на учителка по английски, както и в кожата на 10 родители - амбициозни, безотговорни, луди, 6 ученици - сред тях зомбирания от видео игри Майкъл Томас, Павел с 43 фобии, фолк певица, опитваща да научи английски, един таксиметров шофьор, един съпруг, който говори само гръцки.\r\n\r\nВ какво ще се забърка добрата мис Стела, заради щедрото си сърце? Едва ли може да си го представите... Елате и вижте!\r\n\r\nАвтор и режисьор: Здрава Каменова\r\nУчаства: Яна Огнянова",
                        Duration = 85,
                        EventType = comedy.Id,
                        Title = "Преподавай трудно",
                        ImagePath = "/images/prepodavaj.jpg"
                    },
                    new Event
                    {
                        Description = "Една неуспяла, безработна актриса си търси работа, а между другото, и мъж. Всяка сутрин се буди с решението този ден нещата да се случат и всеки ден се вживява в ролята на различна жена…или по-скоро различно животно…\r\n\r\nЩе прекарате една абсурдно смешна седмица с нея и лудите й приятелки. Дали ще намери това, което търси някъде между дългите опашки за поредния кастинг, разходките в огромна картонена рекламна кутия, или на поредното предаване, в което е платена публика?\r\n\r\nЩе ви оставим да разберете сами. Но едно е сигурно, тя ще ви разсмее до сълзи и ще ви накара да я обикнете.\r\n\r\nОт автора на \"Преподавай трудно\", \"Извънредно положение\", \"Помощ имам две деца\", \"Извънредно любовно\" и много други - Здрава Каменова.",
                        Duration = 90,
                        EventType = comedy.Id,
                        Title = "Жената е странно животно",
                        ImagePath = "/images/jenata.jpg"
                    }
                );

                try
                {
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException?.Message);
                    throw;
                }

                if (!context.Performances.Any())
                {
                    var tri = context.Events.First(x => x.Title == "Три пълни обиколки около света");
                    var jena = context.Events.First(x => x.Title == "Жената е странно животно");
                    var prep = context.Events.First(x => x.Title == "Преподавай трудно");

                    var first = context.Locations.First(x => x.Name == "Театър ,,Сълза и смях'' София");
                    var second = context.Locations.First(x => x.Name == "Драматичен театър ,,Никола Вапцаров'' Благоевград");
                    var trird = context.Locations.First(x => x.Name == "Дом на Културата ,,АРСЕНАЛ'' Казанлък");

                    context.Performances.AddRange(
                        new Performance
                        {
                            Title = "Жената е странно животно",
                            Description = "Едно one-lady show с една китара и една безумно огнена актриса, която няма да забравите - Яна Огнянова! Гледайте този спектакъл от автора на \"Помощ, имам две деца\" и \"Извънредно положение\" - Здрава Каменова.",
                            EventId = jena.Id,
                            LocationId=first.Id,
                            Date= new DateTime(2026, 5, 15, 18, 0, 0)
                        },
                        new Performance
                        {
                            Title = "Три пълни обиколки окoло света",
                            Description = "Три пълни обиколки около света Комедия. Или трагедия. Или революция. Но със сигурност - с много смях, ирония и усещане, че това вече го гледахме по новините.",
                            EventId = tri.Id,
                            LocationId = second.Id,
                            Date = new DateTime(2026, 5, 21, 19, 0, 0)
                        },
                        new Performance
                        {
                            Title = "Преподавай трудно",
                            Description = "Гледайте този моноспектакъл на безспорната огнена фурия Яна Огнянова!",
                            EventId = prep.Id,
                            LocationId = trird.Id,
                            Date = new DateTime(2026, 5, 29, 19, 30, 0)
                        }
                    );

                    try
                    {
                        await context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.InnerException?.Message);
                        throw;
                    }
                }
            }
        }
    }
}