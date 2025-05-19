using Moq;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;
using UrlShortener.Api.Services;

using Xunit;

namespace UrlShortener.Tests
{
    public class UrlShortenerServiceTests
    {
        private readonly Mock<IConnectionMultiplexer> _redisMock;
        private readonly Mock<IDatabase> _dbMock;
        private readonly UrlShortenerService _service;

        public UrlShortenerServiceTests()
        {
            _redisMock = new Mock<IConnectionMultiplexer>();
            _dbMock = new Mock<IDatabase>();
            _redisMock.Setup(r => r.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(_dbMock.Object);
            _service = new UrlShortenerService(_redisMock.Object);
        }

        [Fact]
        public async Task ShortenUrlAsync_Should_Return_ShortUrl()
        {
            // Arrange
            string longUrl = "https://efarda.ir/test-url";
            _dbMock.Setup(x => x.StringGetAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>())).ReturnsAsync((RedisValue)"{}");
            _dbMock.Setup(x => x.StringSetAsync(It.IsAny<RedisKey>(),It.IsAny<RedisValue>(),It.IsAny<TimeSpan?>(),
        false,                       // keepTtl
        When.Always,                 // when
        CommandFlags.None            // flags
    ))
    .ReturnsAsync(true);

            // Act
            var result = await _service.ShortenUrlAsync(longUrl, 10);

            // Assert
            Assert.StartsWith("https://efarda.ir/", result);
        }

        [Fact]
        public async Task GetOriginalUrlAsync_Should_Return_Url_When_Exists()
        {
            // Arrange
            string code = "abc123";
            string expectedUrl = "https://efarda.ir/test-url";
            _dbMock.Setup(x => x.StringGetAsync(code, It.IsAny<CommandFlags>())).ReturnsAsync(expectedUrl);

            // Act
            var result = await _service.GetOriginalUrlAsync(code);

            // Assert
            Assert.Equal(expectedUrl, result);
        }

        [Fact]
        public async Task GetOriginalUrlAsync_Should_Return_Null_When_NotFound()
        {
            // Arrange
            string code = "notexist";
            _dbMock.Setup(x => x.StringGetAsync(code, It.IsAny<CommandFlags>())).ReturnsAsync(RedisValue.Null);

            // Act
            var result = await _service.GetOriginalUrlAsync(code);

            // Assert
            Assert.Null(result);
        }
    }
}