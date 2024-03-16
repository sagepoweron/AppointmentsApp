using AppointmentsApp.Data.Repositories;
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
            _client_repository_mock.Setup(x => x.ClientExists(test_guid))
                .Returns(true);

            // Act
            _client_repository_mock.Object.ClientExists(test_guid);

            // Assert
            _client_repository_mock.Verify(x => x.ClientExists(test_guid), Times.Once);
        }
    }
}