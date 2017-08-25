using NETCore.RedisKit.Core.Internal;
using NETCore.RedisKit.Core;
using NETCore.RedisKit.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Logging;

namespace NETCore.RedisKit.Tests
{
    public class _RedisService_Transition_Tests
    {
        private readonly IRedisService _RedisService;
        public _RedisService_Transition_Tests()
        {
            IRedisProvider redisProvider = new RedisProvider(new RedisKitOptions()
            {
                EndPoints = "127.0.0.1:6379"
            }, true);


            IRedisKitLogger logger = new RedisKitLogger(new LoggerFactory(), redisProvider);

            _RedisService = new RedisService(redisProvider, logger);
        }

        [Fact(DisplayName = "Redis事务")]
        public async Task TransitionAsyncTest()
        {
            var test_key1 = "test_set";
            var test_key2 = "test_hash";

            await _RedisService.SetRemoveAllAsync(test_key1);
            await _RedisService.HashRemoveAllAsync(test_key2);

            var result = _RedisService.TransactionAsync((transition) =>
            {
                transition.SetAddAsync(test_key1, 1111);
                transition.HashSetAsync(test_key2, 1111, "11111111");
            }).Result;

            Assert.True(result);

            var setValues = _RedisService.SetGetAllAsync<int>(test_key1).Result;
            Assert.True(setValues.Count() == 1);

            var firstValue = setValues.First();
            Assert.True(firstValue == 1111);

            var hashValue = _RedisService.HashGetAsync<string>(test_key2, 1111).Result;
            Assert.NotEmpty(hashValue);
            Assert.NotNull(hashValue);
            Assert.Equal("11111111", hashValue);

            await _RedisService.SetRemoveAllAsync(test_key1);
            await _RedisService.HashRemoveAllAsync(test_key2);
        }
    }
}
