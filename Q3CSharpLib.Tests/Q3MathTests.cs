using Xunit;
using Q3CSharpLib;

namespace Q3CSharpLib.Tests
{
    public class Q3MathTests
    {
        [Fact]
        public void Q_rsqrt_ExpectedValue()
        {
            float input = 42f;
            float expected = 0.15403557f;
            float actual = QMath.Q_rsqrt(input);
            Assert.Equal(expected, actual, 5); // 5 decimals precision
        }
    }
}