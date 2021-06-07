using CAAS.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace CAAS.Tests.Helpers
{
    public class TestHelper
    {
        public static Byte[] GetRandomBytes()
        {
            Random rnd = new Random();
            Byte[] randomBytes = new Byte[10];
            rnd.NextBytes(randomBytes);
            return randomBytes;
        }
    }
}
