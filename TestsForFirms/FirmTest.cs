using Firms.Models;
using System.Text.Json;

namespace TestsForFirms
{
    [TestClass]
    public class FirmTest
    {
        #region TestCreateFirmAndContact
        //Проверка создания фирмы фабрикой, и добавления фирме контакта
        [TestMethod]
        public void TestCreateFirmAndContact()
        {
            FirmFactory fabricFirm = FirmFactory.Factory; 
            var firm = fabricFirm.Create("Первая тестовая фирма", "ПТФ", "Страна", 
                                        "Регион", "Город", "Улица", "000000", 
                                        "email@email", "web.web"); 

            var contact = new Contact("Описание", "Информация",
                                      new ContactType("Название", "Пояснение"),
                                      DateTime.Now, DateTime.MaxValue);

            firm.AddContact(contact);

            //Проверяем создание главного подразделения
            Assert.IsTrue(firm.SubFirms.Count > 0);
            //Проверяем, что GetMain возвращает не NULL
            Assert.IsNotNull(firm.GetMain());
            //Проверяем, что фирме добавился контакт
            Assert.IsTrue(firm.GetMain().Contacts.Count >0);
            //Проверяем наличие переданного контакта у фирмы
            Assert.IsTrue(firm.ExistContact(contact));
            //Проверяем, что у главного подразделения существует нужный конкакт
            Assert.IsTrue(firm.GetMain().ExistContact(contact));
        }
        #endregion

        #region TestGetListOfFirmsFilteredByTown
        //Проверка создания списка фирм Нижнего Новгорода
        [TestMethod]
        public void TestGetListOfFirmsFilteredByTown()
        {
            //Создание 30 фирм с помощью фабрики
            GenerateRandomsFirmsViaFirmFactory(30);

            //Получение из фабрики списка фирм, у которых город - Нижний Новгород
            var firms = FirmFactory.Firms.Where(x => x.Town == "Нижний Новгород").ToList();

            Assert.IsNotNull(firms);
            Assert.IsTrue(firms.All(x => x.Town == "Нижний Новгород"));
        }

        //Список городов 
        private string[] Towns = new[] { "Балахна", "Бор", "Нижний Новгород", "Владимир",  "Кстово" };
        
        //Функция создания заданного количество фирм с помощью фабрики
        public void GenerateRandomsFirmsViaFirmFactory(int count = 10)
        {
            var rnd = new Random();

            for (int i = 0; i < count; i++)
            {
                //Создание фирм 
                var firm = FirmFactory.Factory.Create($"Название {i}", $"Короткое название {i}", 
                    $"Страна {i}", $"Регион {i}",
                    Towns[rnd.Next(0, Towns.Length)], $"Улица {i}",
                    $"почтовый индекс {i}", $"email_{i}@email.com",
                    $"www.Web_{i}.com");

                //Добавление подразделений
                AddSubfirms(firm);
            }
        }

        //Коллекция типов подразделений 
        private SubFirmTypeCollection SubFirmTypes = new(new List<SubFirmType>()
        {
            new(false,"Отдел маркетинга"),
            new(false,"Отдел качества"),
            new(false,"Отдел администрации"),
            new(false,"Отдел кадров"),
            new(false,"Отдел охраны"),
            new(false,"Отдел разработки"),
            new(false,"Отдел снабжения")
        });

        //Функция добавления подразделений
        private void AddSubfirms(Firm firm)
        {
            var rnd = new Random();
            //Генерируем число подразделений
            var countOfSubfirmTypes = rnd.Next(SubFirmTypes.Count);
            //Перемешиваем список подразделений
            var shuffledSubFirmTypes = SubFirmTypes.Shuffle().ToList();

            for (int i = 0; i < countOfSubfirmTypes; i++)
            {
                firm.AddSubFirm(new SubFirm(
                    $"{firm.Name} подразделение {i}",
                    $"короткое имя {i}",
                    $"официальное имя {i}",
                    $"телефон {i}", 
                    $"{firm.Name}_{i}@email.com",
                    shuffledSubFirmTypes[i]));
            }
        }
        #endregion

        #region TestAddingContactToSpecialSubFirm
        //Проверка добавления контакта "Комерческое предложение" всем фирмам, содержащим отдел снабжения
        [TestMethod]
        public void TestAddingContactToSpecialSubFirm()
        { 
            SubFirmType supplyDepartment = new(false, "Отдел снабжения");

            var contact = new Contact("Описание", "Информация", new("Коммерческое предложение", "письмо"));

            GenerateRandomsFirmsViaFirmFactory(40);
            FirmFactory.Firms.ForEach(f => f.AddContactToSubFirm(contact, supplyDepartment, true));

            var firmsWithoutSupplyDepartmentAndWithOneSubfirm =
                FirmFactory.Firms.Where(firm => firm.SubFirms.Any(sf => sf.IsYourType(supplyDepartment) == false) && firm.SubFirms.Count == 0).ToList();

            firmsWithoutSupplyDepartmentAndWithOneSubfirm
                .ForEach(f => Assert.IsTrue(f.ExistContact(contact)));

            Console.WriteLine(JsonSerializer.Serialize(FirmFactory.Firms));
        }
        #endregion

        #region TestAddingContactToMultipleFirms
        //Проверка добавления группе фирм контакта "Письмо послали"
        [TestMethod]
        public void TestAddingContactToMultipleFirms()
        {
            var rnd = new Random();
            var countOfFirmsForContact = 10;
            var firmsForContact = new List<Firm>();
            var allFirms = new List<Firm>();

            var contact = new Contact("Тестовое письио", "Письмо для группы фирм", new("Письмо", "Письмо послали"));

            GenerateRandomsFirmsViaFirmFactory(40);
            allFirms = FirmFactory.Firms;

            while (countOfFirmsForContact-- >= 0)
            {
                var randFirm = allFirms[rnd.Next(allFirms.Count)];
                firmsForContact.Add(randFirm);
                allFirms.Remove(randFirm);
            }

            foreach (var firm in firmsForContact)
            {
                firm.AddContact(contact);
            }

            foreach (var firm in firmsForContact)
            {
                Assert.IsTrue(firm.ExistContact(contact));
            }

            foreach (var firm in allFirms)
            {
                Assert.IsFalse(firm.ExistContact(contact));
            }
        }
        #endregion
    }
}