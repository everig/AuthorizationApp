namespace AuthorizationApp.tests
{
    [TestFixture]
    public class RxLoyaltyServiceTests
    {
        private RxLoyaltyService _service;

        [SetUp]
        public void SetUp()
        {
            _service = new RxLoyaltyService();
        }

        [Test]
        public async Task Authorization_ShouldReturnValue() 
        {
            var expected = "ППП1 Чернявский";
            var account = new Account()
            {
                Login = "+79052454450",
                Password = "123456",
            };

            await _service.Authorize(account);
            var result = _service.accountInfo.Client.FullName;

            ClassicAssert.AreEqual(expected, result);
        }
        [Test]
        public async Task Authorization_ShouldThrowException()
        {
            var account = new Account()
            {
                Login = "",
                Password = "",
            };

            Assert.ThrowsAsync<Exception>(async () => await _service.Authorize(account));
        }
    }
}
