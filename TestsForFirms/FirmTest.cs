using Firms.Models;
using System.Text.Json;

namespace TestsForFirms
{
    [TestClass]
    public class FirmTest
    {
        #region TestCreateFirmAndContact
        //�������� �������� ����� ��������, � ���������� ����� ��������
        [TestMethod]
        public void TestCreateFirmAndContact()
        {
            FirmFactory fabricFirm = FirmFactory.Factory; 
            var firm = fabricFirm.Create("������ �������� �����", "���", "������", 
                                        "������", "�����", "�����", "000000", 
                                        "email@email", "web.web"); 

            var contact = new Contact("��������", "����������",
                                      new ContactType("��������", "���������"),
                                      DateTime.Now, DateTime.MaxValue);

            firm.AddContact(contact);

            //��������� �������� �������� �������������
            Assert.IsTrue(firm.SubFirms.Count > 0);
            //���������, ��� GetMain ���������� �� NULL
            Assert.IsNotNull(firm.GetMain());
            //���������, ��� ����� ��������� �������
            Assert.IsTrue(firm.GetMain().Contacts.Count >0);
            //��������� ������� ����������� �������� � �����
            Assert.IsTrue(firm.ExistContact(contact));
            //���������, ��� � �������� ������������� ���������� ������ �������
            Assert.IsTrue(firm.GetMain().ExistContact(contact));
        }
        #endregion

        #region TestGetListOfFirmsFilteredByTown
        //�������� �������� ������ ���� ������� ���������
        [TestMethod]
        public void TestGetListOfFirmsFilteredByTown()
        {
            //�������� 30 ���� � ������� �������
            GenerateRandomsFirmsViaFirmFactory(30);

            //��������� �� ������� ������ ����, � ������� ����� - ������ ��������
            var firms = FirmFactory.Firms.Where(x => x.Town == "������ ��������").ToList();

            Assert.IsNotNull(firms);
            Assert.IsTrue(firms.All(x => x.Town == "������ ��������"));
        }

        //������ ������� 
        private string[] Towns = new[] { "�������", "���", "������ ��������", "��������",  "������" };
        
        //������� �������� ��������� ���������� ���� � ������� �������
        public void GenerateRandomsFirmsViaFirmFactory(int count = 10)
        {
            var rnd = new Random();

            for (int i = 0; i < count; i++)
            {
                //�������� ���� 
                var firm = FirmFactory.Factory.Create($"�������� {i}", $"�������� �������� {i}", 
                    $"������ {i}", $"������ {i}",
                    Towns[rnd.Next(0, Towns.Length)], $"����� {i}",
                    $"�������� ������ {i}", $"email_{i}@email.com",
                    $"www.Web_{i}.com");

                //���������� �������������
                AddSubfirms(firm);
            }
        }

        //��������� ����� ������������� 
        private SubFirmTypeCollection SubFirmTypes = new(new List<SubFirmType>()
        {
            new(false,"����� ����������"),
            new(false,"����� ��������"),
            new(false,"����� �������������"),
            new(false,"����� ������"),
            new(false,"����� ������"),
            new(false,"����� ����������"),
            new(false,"����� ���������")
        });

        //������� ���������� �������������
        private void AddSubfirms(Firm firm)
        {
            var rnd = new Random();
            //���������� ����� �������������
            var countOfSubfirmTypes = rnd.Next(SubFirmTypes.Count);
            //������������ ������ �������������
            var shuffledSubFirmTypes = SubFirmTypes.Shuffle().ToList();

            for (int i = 0; i < countOfSubfirmTypes; i++)
            {
                firm.AddSubFirm(new SubFirm(
                    $"{firm.Name} ������������� {i}",
                    $"�������� ��� {i}",
                    $"����������� ��� {i}",
                    $"������� {i}", 
                    $"{firm.Name}_{i}@email.com",
                    shuffledSubFirmTypes[i]));
            }
        }
        #endregion

        #region TestAddingContactToSpecialSubFirm
        //�������� ���������� �������� "����������� �����������" ���� ������, ���������� ����� ���������
        [TestMethod]
        public void TestAddingContactToSpecialSubFirm()
        { 
            SubFirmType supplyDepartment = new(false, "����� ���������");

            var contact = new Contact("��������", "����������", new("������������ �����������", "������"));

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
        //�������� ���������� ������ ���� �������� "������ �������"
        [TestMethod]
        public void TestAddingContactToMultipleFirms()
        {
            var rnd = new Random();
            var countOfFirmsForContact = 10;
            var firmsForContact = new List<Firm>();
            var allFirms = new List<Firm>();

            var contact = new Contact("�������� ������", "������ ��� ������ ����", new("������", "������ �������"));

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