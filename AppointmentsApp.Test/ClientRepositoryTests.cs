using AppointmentsApp.Data.Repositories;
using AppointmentsApp.Data.Validators;
using Moq;

namespace AppointmentsApp.Test
{
    [TestClass]
    public class ClientRepositoryTests
    {
        private readonly Mock<IClientRepository> _client_repository_mock;

        public ClientRepositoryTests()
        {
            _client_repository_mock = new Mock<IClientRepository>();
        }

        [TestMethod]
        public void ClientExists_CallsRepository()
        {
            Guid test_guid = Guid.NewGuid();

            // Arrange
            _client_repository_mock.Setup(x => x.Exists(test_guid))
                .Returns(true);

            // Act
            _client_repository_mock.Object.Exists(test_guid);

            // Assert
            _client_repository_mock.Verify(x => x.Exists(test_guid), Times.Once);
        }

        [TestMethod]
        public void ValidateName_IsValid()
        {
            List<string> valid_names = ["Bill", "Bill Smith", "Bill.Smith"];

            for (int i = 0; i < valid_names.Count; i++)
            {
                Assert.IsTrue(Helpers.ValidateName(valid_names[i]));
            }
        }

        [TestMethod]
        public void ValidateName_IsNotValid()
        {
            List<string> invalid_names = ["12345", "Name1", " ", "Name#"];

            for (int i = 0; i < invalid_names.Count; i++)
            {
                Assert.IsFalse(Helpers.ValidateName(invalid_names[i]));
            }
        }
    }
}